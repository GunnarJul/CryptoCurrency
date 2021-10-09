using CryptoCurrency;
using CurrencyInfrastructure;
using System;

namespace Crypto
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Crypto beregning");

            var repo = new CurrencyRepository();

            var convert = new Converter(repo);
            convert.SetPricePerUnit("bitcon", 100);
            convert.SetPricePerUnit("bitcon2", 120);

            var val = convert.Convert("bitcon", "bitcon2", 1);
            var val2 = convert.Convert("bitcon2", "bitcon", 1);
            var val3 = convert.Convert("bitcon", "bitcon", 1);
            Console.ReadLine();
        }
    }
}
