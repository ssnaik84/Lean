using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YahooFinanceApi;

namespace Stocky.Data
{
    public class EodData
    {
        public static async Task DownloadAsync()
        {
            var symbols = DbHelper.GetUsaStockSymbols();

            // You could query multiple symbols with multiple fields through the following steps:
            //var securities = await Yahoo.Symbols("AAPL", "GOOG").Fields(Field.Symbol, Field.RegularMarketPrice, Field.FiftyTwoWeekHigh).QueryAsync();
            // Sometimes, yahoo returns broken rows for historical calls, you could decide if these invalid rows is ignored or not by the following statement
            Yahoo.IgnoreEmptyRows = true;
            var batchSize = 100;
            var totalSymbolCount = symbols.Count();
            //totalSymbolCount = 5;

            for (var index = 0; index < totalSymbolCount; index+=batchSize)
            {
                var currentSymbols = symbols.Skip(index).Take(batchSize);
                var securities = await Yahoo.Symbols(currentSymbols.ToArray()).QueryAsync();

                DbHelper.SaveToDatabase(securities);

                //sleep for some time
                System.Threading.Thread.Sleep(2000);
            }

            
            //var securities = await Yahoo.Symbols("AAPL", "GOOG").QueryAsync();
            //var aapl = securities["AAPL"];
            //var price = aapl[Field.RegularMarketPrice]; // or, you could use aapl.RegularMarketPrice directly for typed-value
        }

        public static async Task DownloadEodAsync()
        {
            var symbols = DbHelper.GetUsaStockSymbols();
            Yahoo.IgnoreEmptyRows = true;
            var batchSize = 100;
            var totalSymbolCount = symbols.Count();
            //totalSymbolCount = 5;
            var startDate = DateTime.Now.AddDays(-15); //will download upto 15 days old data
            var latestDbDate = DbHelper.GetLatestEodDate() ?? DateTime.Now.AddDays(-100);
            startDate = (latestDbDate > startDate) ? latestDbDate.AddDays(1) : startDate;

            foreach(var symbol in symbols)
            {
                IReadOnlyList<Candle> history = null;
                // You should be able to query data from various markets including US, HK, TW
                // The startTime & endTime here defaults to EST timezone
                try
                {
                    history = await Yahoo.GetHistoricalAsync(symbol, startDate, DateTime.Now, Period.Daily); 
                }
                catch(Exception ex)
                {

                }

                if (history != null)
                {
                    DbHelper.SaveToDatabase(history, symbol);
                    //foreach (var candle in history)
                    //{
                    //    Console.WriteLine($"DateTime: {candle.DateTime}, Open: {candle.Open}, High: {candle.High}, Low: {candle.Low}, Close: {candle.Close}, Volume: {candle.Volume}, AdjustedClose: {candle.AdjustedClose}");
                    //}
                }
            }
            
          }


        public static string GetUserTimeLine()
        {
            var auth = new SingleUserAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore
                {
                    ConsumerKey = "yBSYepGAiXDak6538MUL41yRf",
                    ConsumerSecret = "DSIRCC8kZGKnM0a40bZRlmkhmjVnNl5Eqz99b9iW4TwfNVQQnr",
                    AccessToken = "79739922-UQlghLqgywh3MBsqCrfsPJG0HG2l31TzB867umIAy",
                    AccessTokenSecret = "Pam53cJMbDIW1ePMRTWhPRBkj5oItrUjOwnk7Oupl2mej"
                }
            };

            TwitterContext twitterCtx = new TwitterContext(auth);
            StringBuilder result = new StringBuilder();

            string searchTerm = "aytu bioscience";
            Search searchResponse =
                (from search in twitterCtx.Search
                 where search.Type == SearchType.Search &&
                       search.Query == searchTerm &&
                       search.IncludeEntities == false &&
                       search.TweetMode == TweetMode.Compat
                 select search).FirstOrDefault();
                        
            if (searchResponse?.Statuses != null)
                searchResponse.Statuses.ForEach(tweet =>
                    result.AppendLine("*******" + (tweet.Text ?? tweet.FullText) ?? "")   
                );

            //var test = 0;

            //if (searchResponse?.Statuses != null)
            //    searchResponse.Statuses.ForEach(tweet =>
            //        Console.WriteLine(
            //            "\n  User: {0} ({1})\n  Tweet: {2}",
            //            tweet.User.ScreenNameResponse,
            //            tweet.User.UserIDResponse,
            //            tweet.Text ?? tweet.FullText));
            //else
            //    Console.WriteLine("No entries found.");


            //var statusTweets =
            //   from tweet in twitterCtx.Status
            //   where tweet.Type == StatusType.User &&
            //         tweet.Count == 3200 &&
            //         tweet.ScreenName == "abc"
            //   select tweet;

            //var count = (new[] { statusTweets }.AsQueryable()).Count();

            ////PrintTweetsResults(statusTweets);
            //foreach (var tweet in statusTweets)
            //{


            //    result.AppendLine("(" + tweet.StatusID + ")" +
            //        "[" + tweet.User.UserID + "]" +
            //        tweet.User.Name + ", " +
            //        tweet.Text + ", " +
            //        tweet.CreatedAt);
            //}

            //// DEFINE FILE PATH NAME
            //string dwnloadFilePath = @"C:\temp\Tweet.log";

            //// CREATE AN EMPTY TEXT FILE
            //FileStream fs1 = null;
            //if (!File.Exists(dwnloadFilePath))
            //{
            //    using (fs1 = File.Create(dwnloadFilePath)) ;
            //}

            //// WRITE DATA INTO TEXT FILE
            //if (File.Exists(dwnloadFilePath))
            //{
            //    using (StreamWriter sw = new StreamWriter(dwnloadFilePath))
            //    {

            //        statusTweets.ToList().ForEach(
            //    tweet => sw.Write(
            //    "{3}, Tweet ID: {2}, Tweet: {1}\n",
            //    tweet.User.Name, tweet.Text, tweet.StatusID, tweet.CreatedAt));

            //    }
            //}


            return result.ToString();
            //Console.ReadLine();
        }
    }
}
