using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YahooFinanceApi;

namespace Stocky.Data
{
    class DbHelper
    {  
        public static List<string> GetUsaStockSymbols()
        {
            List<string> symbols = new List<string>();
            string queryString = "select symbol from UsaStock";
            using (SqlConnection connection = new SqlConnection(Constants.DatabaseConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = queryString;
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string symbol = reader["symbol"].ToString();
                        if(!string.IsNullOrWhiteSpace(symbol))
                        {
                            symbols.Add(symbol.Trim());
                        }
                    }
                }
            }
            return symbols;
        }

        public static DateTime? GetLatestEodDate()
        {
            using (IDbConnection db = new SqlConnection(Constants.DatabaseConnectionString))
            {
                DateTime? maxDate = null; 
                string selectQuery = "SELECT MAX(datetime) FROM usastockeod";
                try
                {
                    maxDate = Convert.ToDateTime(db.ExecuteScalar(selectQuery));
                }
                catch (Exception ex) { }

                return maxDate;
            }
        }

        public static void SaveToDatabase(IReadOnlyDictionary<string, Security> securities)
        {
            using (IDbConnection db = new SqlConnection(Constants.DatabaseConnectionString))
            {
                //string insertQuery = @"INSERT INTO [dbo].[usasecurity] ([Symbol],[LongName],[Ask],[AskSize],[AverageDailyVolume10Day],[AverageDailyVolume3Month],[Bid],[BidSize],[bigintName],[BookValue],[Currency],[DividendDate],[EarningsTimestamp],[EarningsTimestampEnd],[EarningsTimestampStart],[EpsForward],[EpsTrailingTwelveMonths],[EsgPopulated],[Exchange],[ExchangeDataDelayedBy],[ExchangeTimezoneName],[ExchangeTimezoneShortName],[FiftyDayAverage],[FiftyDayAverageChange],[FiftyDayAverageChangePercent],[FiftyTwoWeekHigh],[FiftyTwoWeekHighChange],[FiftyTwoWeekHighChangePercent],[FiftyTwoWeekLow],[FiftyTwoWeekLowChange],[FiftyTwoWeekLowChangePercent],[FiftyTwoWeekRange],[FinancialCurrency],[FirstTradeDateMilliseconds],[ForwardPE],[FullExchangeName],[GmtOffSetMilliseconds],[Language],[Market],[MarketCap],[MarketState],[MessageBoardId],[PostMarketChange],[PostMarketChangePercent],[PostMarketPrice],[PostMarketTime],[PriceHint],[PriceToBook],[QuoteSourceName],[QuoteType],[Region],[RegularMarketDayRange],[RegularMarketChange],[RegularMarketChangePercent],[RegularMarketDayHigh],[RegularMarketDayLow],[RegularMarketOpen],[RegularMarketPreviousClose],[RegularMarketPrice],[RegularMarketTime],[RegularMarketVolume],[SharesOutstanding],[ShortName],[SourceInterval],[Triggerable],[Tradeable],[TrailingAnnualDividendRate],[TrailingAnnualDividendYield],[TrailingPE],[TwoHundredDayAverage],[TwoHundredDayAverageChange],[TwoHundredDayAverageChangePercent]) 
                //                       VALUES (@Symbol,@LongName,@Ask,@AskSize,@AverageDailyVolume10Day,@AverageDailyVolume3Month,@Bid,@BidSize,@bigintName,@BookValue,@Currency,@DividendDate,@EarningsTimestamp,@EarningsTimestampEnd,@EarningsTimestampStart,@EpsForward,@EpsTrailingTwelveMonths,@EsgPopulated,@Exchange,@ExchangeDataDelayedBy,@ExchangeTimezoneName,@ExchangeTimezoneShortName,@FiftyDayAverage,@FiftyDayAverageChange,@FiftyDayAverageChangePercent,@FiftyTwoWeekHigh,@FiftyTwoWeekHighChange,@FiftyTwoWeekHighChangePercent,@FiftyTwoWeekLow,@FiftyTwoWeekLowChange,@FiftyTwoWeekLowChangePercent,@FiftyTwoWeekRange,@FinancialCurrency,@FirstTradeDateMilliseconds,@ForwardPE,@FullExchangeName,@GmtOffSetMilliseconds,@Language,@Market,@MarketCap,@MarketState,@MessageBoardId,@PostMarketChange,@PostMarketChangePercent,@PostMarketPrice,@PostMarketTime,@PriceHint,@PriceToBook,@QuoteSourceName,@QuoteType,@Region,@RegularMarketDayRange,@RegularMarketChange,@RegularMarketChangePercent,@RegularMarketDayHigh,@RegularMarketDayLow,@RegularMarketOpen,@RegularMarketPreviousClose,@RegularMarketPrice,@RegularMarketTime,@RegularMarketVolume,@SharesOutstanding,@ShortName,@SourceInterval,@Triggerable,@Tradeable,@TrailingAnnualDividendRate,@TrailingAnnualDividendYield,@TrailingPE,@TwoHundredDayAverage,@TwoHundredDayAverageChange,@TwoHundredDayAverageChangePercent)";

                foreach (var security in securities)
                {
                    var filteredKeys = security.Value.Fields.Keys.
                        Where(x => (x != "Region" && x != "Triggerable" && x != "RegularMarketDayRange" && x != "FiftyTwoWeekRange"
                        && x != "EsgPopulated" && x != "FirstTradeDateMilliseconds"));
                    string columnNames = string.Join(",", filteredKeys);
                    string columnParams = string.Join(",@", filteredKeys);
                    //string columnValues = string.Join(",", security.Value.Fields.Values);
                    string insertQuery = $"INSERT INTO [dbo].[usasecurity] ({columnNames}) VALUES (@{columnParams})";

                    try
                    {
                        db.Execute(insertQuery, security.Value);
                    }
                    catch (Exception ex) { }
                }
            }
        }

        public static void SaveToDatabase(IReadOnlyList<Candle> history, string symbol)
        {
            using (IDbConnection db = new SqlConnection(Constants.DatabaseConnectionString))
            {
                //var columnNames = "Symbol,[DateTime],Open,High,Low,Close,Volume,AdjustedClose";
                //var columnParams = $"'{symbol}',@DateTime,@Open,@High,@Low,@Close,@Volume,@AdjustedClose";
               

                foreach (var candle in history)
                {
                    string insertQuery = $"INSERT INTO [dbo].[usastockeod] " +
                   $"(Symbol,[DateTime],[Open],[High],[Low],[Close],Volume,AdjustedClose) " +
                   $"VALUES ('{symbol}','{candle.DateTime}',{candle.Open},{candle.High},{candle.Low}," +
                   $"{candle.Close},{candle.Volume},{candle.AdjustedClose})";

                    try
                    {
                        db.Execute(insertQuery);
                    }
                    catch (Exception ex) { }
                }
            }
        }

    }
}
