using CryptoCurrency;
using CryptoCurrency.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CurrencyInfrastructure
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private Dictionary<string, double> _CrytocurrencyRate = new(StringComparer.CurrentCultureIgnoreCase);
        private readonly string _fileName = $"{AppDomain.CurrentDomain.BaseDirectory}/CurrencyData.json";
        public object Properties { get; private set; }

        public CurrencyRepository()
        {

            ReadRates();

        }



        public void SetCurrencyRate(CurrencyRate currencyRate)
        {
            if (_CrytocurrencyRate.ContainsKey(currencyRate.CurrencyName))
            {
                _CrytocurrencyRate[currencyRate.CurrencyName] = currencyRate.CurrencyValue;
            }
            else
            {
                _CrytocurrencyRate.Add(currencyRate.CurrencyName, currencyRate.CurrencyValue);
            }
            SaveRates();
        }


        private void SaveRates()
        {
            var crytocurrencyRate = new List<CurrencyRate>();
            foreach (var key in _CrytocurrencyRate.Keys)
            {
                crytocurrencyRate.Add(new CurrencyRate { CurrencyName = key, CurrencyValue = _CrytocurrencyRate[key] });
            }

            var json = JsonConvert.SerializeObject(crytocurrencyRate.ToArray());
            File.WriteAllText(_fileName, json);
        }

        private void ReadRates()
        {
            if (!File.Exists(_fileName))
            {
                var file = File.Create(_fileName);
                file.Close();
                return;
            }
            string json = File.ReadAllText(_fileName);

            if (string.IsNullOrWhiteSpace(json))
                return;

            var items = JsonConvert.DeserializeObject<List<CurrencyRate>>(json);
            foreach (var item in items)
            {
                SetCurrencyRate(item);
            }
        }


        public CurrencyRate GetCurrencyRate(string currencyName)
        {

            return _CrytocurrencyRate.ContainsKey(currencyName)
            ? new CurrencyRate
            {
                CurrencyName = currencyName,
                CurrencyValue = _CrytocurrencyRate[currencyName]
            }
            : null;
        }

        public int NumberOfCurrencies()
        {
            return _CrytocurrencyRate.Count;
        }

        public void AddCurrencyRate(CurrencyRate currencyRate)
        {
            SetCurrencyRate(currencyRate);
        }
    }
}

