using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter
{
    public class ValuteInfo {
        public string ID { get; set; }
        public string NumCode { get; set; }
        public string CharCode { get; set; }
        public int Nominal { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public double Previous { get; set; }
        public ValuteInfo() {}
        public ValuteInfo(string name, int nominal, double value) { CharCode = name; Nominal = nominal; Value = value; }
    }


    public class Valute
    {
        public ValuteInfo AUD { get; set; }
        public ValuteInfo AZN { get; set; }
        public ValuteInfo GBP { get; set; }
        public ValuteInfo AMD { get; set; }
        public ValuteInfo BYN { get; set; }
        public ValuteInfo BGN { get; set; }
        public ValuteInfo BRL { get; set; }
        public ValuteInfo HUF { get; set; }
        public ValuteInfo HKD { get; set; }
        public ValuteInfo DKK { get; set; }
        public ValuteInfo USD { get; set; }
        public ValuteInfo EUR { get; set; }
        public ValuteInfo INR { get; set; }
        public ValuteInfo KZT { get; set; }
        public ValuteInfo CAD { get; set; }
        public ValuteInfo KGS { get; set; }
        public ValuteInfo CNY { get; set; }
        public ValuteInfo MDL { get; set; }
        public ValuteInfo NOK { get; set; }
        public ValuteInfo PLN { get; set; }
        public ValuteInfo RON { get; set; }
        public ValuteInfo XDR { get; set; }
        public ValuteInfo SGD { get; set; }
        public ValuteInfo TJS { get; set; }
        public ValuteInfo TRY { get; set; }
        public ValuteInfo TMT { get; set; }
        public ValuteInfo UZS { get; set; }
        public ValuteInfo UAH { get; set; }
        public ValuteInfo CZK { get; set; }
        public ValuteInfo SEK { get; set; }
        public ValuteInfo CHF { get; set; }
        public ValuteInfo ZAR { get; set; }
        public ValuteInfo KRW { get; set; }
        public ValuteInfo JPY { get; set; }
        public ValuteInfo RUB { get; set; } = new ValuteInfo("RUB", 1, 1);

    }

    public class Root
    {
        public DateTime Date { get; set; }
        public DateTime PreviousDate { get; set; }
        public string PreviousURL { get; set; }
        public DateTime Timestamp { get; set; }
        public Valute Valute { get; set; }
    }
}
