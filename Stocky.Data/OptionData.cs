using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Stocky.Data
{
    public class OptionData
    {
        public static async Task DownloadAsync()
        {
            var symbols = DbHelper.GetUsaStockSymbols("select distinct Symbol from usasecurity");
            //("(SELECT symbol FROM usastock WHERE iswatchlisted = 1) except (select distinct symbol from usaoption )");

            var skipSymbols = new List<string>(); //DbHelper.GetUsaStockSymbols("select distinct symbol  from usaoption");
            var apiToken = "5f2f3a4a7c8d63.97311962"; //free = "OeAFFmMliFG5orCUuwAKQ8l4WWFQ67YX";
            foreach(var symbol in symbols.Except(skipSymbols))
            {
                try
                {
                    var route = $"api/options/{symbol}.US?api_token={apiToken}";
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://eodhistoricaldata.com/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //GET Method  
                        HttpResponseMessage response = await client.GetAsync(route);
                        if (response.IsSuccessStatusCode)
                        {
                            var option = await response.Content.ReadAsAsync<OptionModel>();

                            if (option != null)
                            {
                                List<OptionModelRecord> dbRecords = new List<OptionModelRecord>();
                                OptionModelRecord dbRecord = null;
                                foreach (var record in option.data.Where(d => d.options.CALL != null).SelectMany(d => d.options.CALL).ToList())
                                {
                                    dbRecord = new OptionModelRecord();
                                    SetOptionRecord(option, dbRecord, record);
                                    dbRecords.Add(dbRecord);
                                }

                                dbRecord = null;
                                foreach (var record in option.data.Where(d => d.options.PUT != null).SelectMany(d => d.options.PUT).ToList())
                                {
                                    dbRecord = new OptionModelRecord();
                                    SetOptionRecord(option, dbRecord, record);
                                    dbRecords.Add(dbRecord);
                                }

                                //save into database
                                if(dbRecords.Count > 0)
                                    DbHelper.SaveToDatabase(dbRecords);
                            }
                        }
                    }
                }
                catch (Exception ex) { }

            }

            Console.WriteLine("Task Completed.");

        }

        private static void SetOptionRecord(OptionModel option, OptionModelRecord dbRecord, dynamic record)
        {
            dbRecord.symbol = option.code;
            dbRecord.exchange = option.exchange;
            dbRecord.lastTradeDate = option.lastTradeDate;
            dbRecord.lastTradePrice = option.lastTradePrice;
            dbRecord.contractName = record.contractName;
            dbRecord.contractSize = record.contractSize;
            dbRecord.currency = record.currency;
            dbRecord.type = record.type;
            dbRecord.inTheMoney = record.inTheMoney;
            dbRecord.lastTradeDateTime = record.lastTradeDateTime;
            dbRecord.expirationDate = record.expirationDate;
            dbRecord.strike = record.strike;
            dbRecord.lastPrice = record.lastPrice;
            dbRecord.bid = record.bid;
            dbRecord.ask = record.ask;
            dbRecord.change = record.change;
            dbRecord.changePercent = record.changePercent;
            dbRecord.volume = record.volume;
            dbRecord.openInterest = record.openInterest;
            dbRecord.impliedVolatility = record.impliedVolatility;
            dbRecord.delta = record.delta;
            dbRecord.gamma = record.gamma;
            dbRecord.theta = record.theta;
            dbRecord.vega = record.vega;
            dbRecord.rho = record.rho;
            dbRecord.theoretical = record.theoretical;
            dbRecord.intrinsicValue = record.intrinsicValue;
            dbRecord.timeValue = record.timeValue;
            dbRecord.updatedAt = record.updatedAt;
            dbRecord.daysBeforeExpiration = record.daysBeforeExpiration;
        }
    }
}
