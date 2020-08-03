using BrailleToTextTransformer.Models;
using BrailleToTextTransformer.Services;
using Xunit;

namespace BrailleTests
{
    public class ReverseTranslatorsTest
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("⠲⠂⠖⠢⠤ ", ".,!?- ")]
        [InlineData("⠲", ".")]
        public void ReverseSpecialSymbolTranslatorTest(string brailleInput, string expected)
        {
            var specialTranslator = new SpecialTranslator(true);
            
            Assert.Equal(expected, specialTranslator.Translate(brailleInput));
        }
        
        [Theory]
        [InlineData("", "")]
        [InlineData("⠼⠁⠃⠉", "123")]
        [InlineData("⠁⠃⠉", "123")]
        public void ReverseNumericTranslatorTest(string input, string expected)
        {
            var numericTranslator = new NumericTranslator(true);
            
            Assert.Equal(expected, numericTranslator.Translate(input));
        }
        
        [Theory]
        [InlineData("", "")]
        [InlineData("⠠⠏", "П" )]
        [InlineData("⠠⠏⠗⠊⠺⠑⠞", "Привет")]
        public void ReverseRussianTranslatorTest(string input, string expected)
        {
            var russianTranslator = new MultilingualTranslator(Language.Russian, true);

            Assert.Equal(expected, russianTranslator.Translate(input));
        }
        
        [Theory]
        [InlineData("", "")]
        [InlineData("⠠⠓", "H")]
        [InlineData("⠠⠓⠑⠇⠇⠕", "Hello")]
        public void ReverseEnglishTranslatorTest(string input, string expected)
        {
            var englishTranslator = new MultilingualTranslator(Language.English, true);

            Assert.Equal(expected, englishTranslator.Translate(input));
        }
    }
}