using QuantConnect.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocky.WinForm.Helper
{
    class HistoricalDataHelper
    {
        public static void SaveHistoricalData()
        {
            var algo = new QCAlgorithm();
            IEnumerable<KeyValuePair<string, string>> header = null;
            var content = algo.Download("https://www.quantconnect.com/", header);

            //algo.SetApi(new Api.Api());
            //var content = string.Empty;
            //Assert.DoesNotThrow(() => content = algo.Download("https://www.quantconnect.com/"));
            //Assert.IsNotEmpty(content);
        }
    }
}
