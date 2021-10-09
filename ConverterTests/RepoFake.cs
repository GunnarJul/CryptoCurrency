using CryptoCurrency;
using CryptoCurrency.Model;

using System.Linq;
using System.Collections.Generic;

namespace ConverterTests
{
    public class RepoFake : ICurrencyRepository
    {
        private List<CurrencyRate> _currentcies = new();

        public void AddCurrencyRate(CurrencyRate currencyRate)
        {
            _currentcies.Add(currencyRate);
        }

        public CurrencyRate GetCurrencyRate(string currencyName)
        {
            return _currentcies.Where(w => w.CurrencyName == currencyName).FirstOrDefault();
        }

        public int NumberOfCurrencies()
        {
            return _currentcies.Count();
        }

        public void SetCurrencyRate(CurrencyRate currencyRate)
        {

            GetCurrencyRate(currencyRate.CurrencyName ).CurrencyValue =currencyRate.CurrencyValue ;
        }
    }
}
