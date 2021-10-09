using CryptoCurrency.Model;
using System;
using System.Collections.Generic;

namespace CryptoCurrency
{
    public class Converter
    {
        private readonly ICurrencyRepository _repository;
        public Converter(ICurrencyRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// Angiver prisen for en enhed af en kryptovaluta. Prisen angives i dollars.
        /// Hvis der tidligere er angivet en værdi for samme kryptovaluta, 
        /// bliver den gamle værdi overskrevet af den nye værdi
        /// </summary>
        /// <param name="currencyName">Navnet på den kryptovaluta der angives</param>
        /// <param name="price">Prisen på en enhed af valutaen målt i dollars. Prisen kan ikke være negativ</param>
        public void SetPricePerUnit(String currencyName, double price)
        {
            if (price < 0)
                return;
            if (string.IsNullOrWhiteSpace(currencyName))
                return;

            var currency = GetCurrency(currencyName);
            if (currency != null)
            {
                currency.CurrencyValue = price;
                _repository.SetCurrencyRate(currency);
                return;
            }
            _repository.AddCurrencyRate(new CurrencyRate { CurrencyName = currencyName, CurrencyValue = price });
        }

        /// <summary>
        /// Konverterer fra en kryptovaluta til en anden. 
        /// Hvis en af de angivne valutaer ikke findes, kaster funktionen en ArgumentException
        /// 
        /// </summary>
        /// <param name="fromCurrencyName">Navnet på den valuta, der konverterers fra</param>
        /// <param name="toCurrencyName">Navnet på den valuta, der konverteres til</param>
        /// <param name="amount">Beløbet angivet i valutaen angivet i fromCurrencyName</param>
        /// <returns>Værdien af beløbet i toCurrencyName</returns>
        public double Convert(String fromCurrencyName, String toCurrencyName, double amount)
        {

            if (!CurrencyExists(fromCurrencyName))
            {
                throw new ArgumentException($"Currency <{fromCurrencyName}> not defined");
            }
            if (!CurrencyExists(toCurrencyName))
            {
                throw new ArgumentException($"Currency <{toCurrencyName}> not defined");
            }

            var fromRate = _repository.GetCurrencyRate(fromCurrencyName);
            var toRate = _repository.GetCurrencyRate(toCurrencyName);

            return fromRate.Convert(toRate, amount);

        }
        public int NumberOfCurrency()
        {
            return _repository.NumberOfCurrencies();
        }
        private bool CurrencyExists(string fromCurrencyName)
        {
            return _repository.GetCurrencyRate(fromCurrencyName) != null;
        }
        private CurrencyRate GetCurrency(string currencyName)
        {
            return _repository.GetCurrencyRate(currencyName);
        }


    }


}
