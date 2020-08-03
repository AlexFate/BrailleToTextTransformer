using BrailleToTextTransformer.Base.Interfaces;
using BrailleToTextTransformer.Models;
using BrailleToTextTransformer.Services;
using Xunit;

namespace BrailleTests
{
    public class TranslatorsTest
    {
        [Theory]
        [InlineData("", "")]
        [InlineData(".,!?- ", "⠲⠂⠖⠢⠤ ")]
        [InlineData(".", "⠲")]
        public void SpecialSymbolTranslatorTest(string input, string expected)
        {
            ITranslator specialTranslator = new SpecialTranslator();
            
            Assert.Equal(expected, specialTranslator.Translate(input));
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("123", "⠼⠁⠃⠉")]
        public void NumericTranslatorTest(string input, string expected)
        {
            ITranslator numericTranslator = new NumericTranslator();
            
            Assert.Equal(expected, numericTranslator.Translate(input));
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("П", "⠠⠏")]
        [InlineData("Привет", "⠠⠏⠗⠊⠺⠑⠞")]
        public void RussianTranslatorTest(string input, string expected)
        {
            var russianTranslator = new MultilingualTranslator(Language.Russian);

            Assert.Equal(expected, russianTranslator.Translate(input));
        }
        
        [Theory]
        [InlineData("", "")]
        [InlineData("H", "⠠⠓")]
        [InlineData("Hello", "⠠⠓⠑⠇⠇⠕")]
        public void EnglishTranslatorTest(string input, string expected)
        {
            var englishTranslator = new MultilingualTranslator(Language.English);

            Assert.Equal(expected, englishTranslator.Translate(input));
        }
    }
}