using CryptoCurrency.Model;

namespace CryptoCurrency
{
    public interface ICurrencyRepository
    {
        void AddCurrencyRate(CurrencyRate currencyRate);
        void SetCurrencyRate(CurrencyRate currencyRate);
        CurrencyRate GetCurrencyRate(string currencyName);
        int NumberOfCurrencies();
    
    }
}