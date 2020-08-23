using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocky.Data
{
    public class OptionModel
    {
        public string code { get; set; }
        public string exchange { get; set; }
        public string lastTradeDate { get; set; }
        public double? lastTradePrice { get; set; }
        public IList<Datum> data { get; set; }
    }

    public class CALL
    {
        public string contractName { get; set; }
        public string contractSize { get; set; }
        public string currency { get; set; }
        public string type { get; set; }
        public string inTheMoney { get; set; }
        public string lastTradeDateTime { get; set; }
        public string expirationDate { get; set; }
        public double? strike { get; set; }
        public double? lastPrice { get; set; }
        public double? bid { get; set; }
        public double? ask { get; set; }
        public double? change { get; set; }
        public double? changePercent { get; set; }
        public double? volume { get; set; }
        public double? openInterest { get; set; }
        public double? impliedVolatility { get; set; }
        public double? delta { get; set; }
        public double? gamma { get; set; }
        public double? theta { get; set; }
        public double? vega { get; set; }
        public double? rho { get; set; }
        public double? theoretical { get; set; }
        public double? intrinsicValue { get; set; }
        public double? timeValue { get; set; }
        public string updatedAt { get; set; }
        public int? daysBeforeExpiration { get; set; }
    }

    public class PUT
    {
        public string contractName { get; set; }
        public string contractSize { get; set; }
        public string currency { get; set; }
        public string type { get; set; }
        public string inTheMoney { get; set; }
        public string lastTradeDateTime { get; set; }
        public string expirationDate { get; set; }
        public double? strike { get; set; }
        public double? lastPrice { get; set; }
        public double? bid { get; set; }
        public double? ask { get; set; }
        public double? change { get; set; }
        public double? changePercent { get; set; }
        public double? volume { get; set; }
        public double? openInterest { get; set; }
        public double? impliedVolatility { get; set; }
        public double? delta { get; set; }
        public double? gamma { get; set; }
        public double? theta { get; set; }
        public double? vega { get; set; }
        public double? rho { get; set; }
        public double? theoretical { get; set; }
        public double? intrinsicValue { get; set; }
        public double? timeValue { get; set; }
        public string updatedAt { get; set; }
        public int? daysBeforeExpiration { get; set; }
    }

    public class Options
    {
        public IList<CALL> CALL { get; set; }
        public IList<PUT> PUT { get; set; }
    }

    public class Datum
    {
        public string expirationDate { get; set; }
        public Options options { get; set; }
    }

}
