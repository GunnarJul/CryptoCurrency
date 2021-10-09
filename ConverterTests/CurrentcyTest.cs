using CryptoCurrency;
using System;
using Xunit;

namespace ConverterTests
{
    public class CurrentcyTest
    {

        private static Converter Setup()
        {
            var repository = new RepoFake();
            var converter = new Converter(repository);
            return converter;
        }
        [Fact]
        public void Samme_cryto_gemmes_kun_en_gang  ()
        {
            //arrange
            var curName = "bitcon";
            var converter = Setup();
            
            //act
            converter.SetPricePerUnit(curName, 100);
            converter.SetPricePerUnit(curName, 100);

            // assert 
            Assert.Equal(1,converter.NumberOfCurrency());
        }

        [Fact]
        public void Samme_cryto_tildeles_sidst_angivne_vaerdi()
        {
            var curName = "bitcon";
            var curName2 = "bitcon2";
            //arrange
            var converter = Setup();

            //act
            converter.SetPricePerUnit(curName, 100);
            converter.SetPricePerUnit(curName, 10);
            converter.SetPricePerUnit(curName2, 10);
            var result = converter.Convert(curName, curName2, 10);
            // Assert 
            Assert.Equal(10, result);
        }

        [Theory]
        [InlineData("", "bitcon", 10) ]
        [InlineData(null, "bitcon", 10)]
        [InlineData(" ",  "bitcon", 10)]
        [InlineData("bitcon","" , 10)]
        [InlineData("bitcon", null, 10)]
        [InlineData( "bitcon", " ", 10)]
        [InlineData("",  "", 10)]
        [InlineData("",  null, 10)]
        public void Invalide_crytonavne_ignoreres(string currencyFrom, 
                                                  string currencyTo, 
                                                  double price)
        {
            //arrange
            var converter = Setup();

            //act
            converter.SetPricePerUnit(currencyFrom, price);
            converter.SetPricePerUnit(currencyTo, price);

            // Assert
            Assert.Throws<ArgumentException>(() => converter.Convert(currencyFrom, currencyTo, 10)); ;

        }

        [Theory]
        [InlineData("bitcon2", "bitcon", 10, 0)]
        [InlineData("bitcon2", "bitcon", 0, 10)]
        [InlineData("bitcon2", "bitcon", 0, 0)]
        
        public void nul_vaerid_cryto_pris_ignoreres(string currencyFrom, 
                                                  string currencyTo, 
                                                  double priceFrom,
                                                  double priceTo )
        {
            //arrange
            var converter = Setup();

            //act
            converter.SetPricePerUnit(currencyFrom, priceFrom);
            converter.SetPricePerUnit(currencyTo, priceTo);
            var result = converter.Convert(currencyFrom, currencyTo, 10);

            // Assert
            Assert.Equal(0, result);
        }
        [Theory]
        [InlineData("bitcon2", "bitcon", 1, -10)]
        [InlineData("bitcon2", "bitcon", -1, 0)]
        public void Negativ_crypto_kaster_undtagelse(string currencyFrom,
                                                  string currencyTo,
                                                  double priceFrom,
                                                  double priceTo)
        {
            var converter = Setup();

            //act
            converter.SetPricePerUnit(currencyFrom, priceFrom);
            converter.SetPricePerUnit(currencyTo, priceTo);

            // Aassert
            Assert.Throws<ArgumentException>(() => converter.Convert(currencyFrom, currencyTo, 10));

        }

        [Theory]
        [InlineData("bitcon", "bitcon2", 1, 2, 5)]
        [InlineData("bitcon", "bitcon2", 2, 1, 20)]
        [InlineData("bitcon", "bitcon2", 10, 100,1)]
        [InlineData("bitcon", "bitcon2", 100, 1000, 1)]
        [InlineData("bitcon", "bitcon2", 100, 10, 100)]
        [InlineData("bitcon", "bitcon2", 1000, 100, 100)]
        public void Valide_cryto_beregnes_korrekt(string currencyFrom,
                                                  string currencyTo,
                                                  double priceFrom,
                                                  double priceTo,
                                                  double expected)
        {
            //arrange
            var converter = Setup();

            //act
            converter.SetPricePerUnit(currencyFrom, priceFrom);
            converter.SetPricePerUnit(currencyTo, priceTo);
            var result = converter.Convert(currencyFrom, currencyTo, 10);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Ikke_eksistende_crypto_kaster_undtagelse()
        {
            var converter = Setup();

            //act
            converter.SetPricePerUnit("bitcon", 1);
            converter.SetPricePerUnit("bitcon2", 2);
 
            // act & assert
            Assert.Throws<ArgumentException>(() => converter.Convert("bitcon4", "bitcon5", 10));

        }

    }
}
