using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrency.Model
{
    public class CurrencyRate
    {
        private string _currencyName;
        private double _currencyValue;
        public string CurrencyName { get { return _currencyName; } set { _currencyName = value.Trim(); } }
        public double CurrencyValue { get { return _currencyValue; } set { _currencyValue = (value < 0) ? 0 : value; } }

        public bool HasValue {get { return _currencyValue > 0; } }

        public double Convert(CurrencyRate otherRate, double otherRateAmount)
        {
            if (otherRate == null)
                return 0;
            if (CurrencyValue <= 0 ||
              otherRate.CurrencyValue <= 0)
                return 0;
           return  otherRateAmount * (CurrencyValue / otherRate.CurrencyValue);
        }
    }
}
