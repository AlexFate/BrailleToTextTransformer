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
        [InlineData("⠁⠃⠉", "")]
        [InlineData("⠙⠑⠁⠗", "")]
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
        [InlineData("⠙⠑⠁⠗", "dear")]
        public void ReverseEnglishTranslatorTest(string input, string expected)
        {
            var englishTranslator = new MultilingualTranslator(Language.English, true);

            Assert.Equal(expected, englishTranslator.Translate(input));
        }
        
        [Theory]
        [InlineData("", "")]
        [InlineData("⠠⠏⠗⠊⠺⠑⠞ ⠍⠊⠗⠲ ⠠⠝⠕ ⠼⠁⠃⠉ ⠼⠁⠃⠉", "Привет мир. Но 123 123")]
        [InlineData("⠠⠏⠗⠊⠺⠑⠞ some text on another language ⠍⠊⠗⠲ ⠠⠝⠕ ⠼⠁⠃⠉ ⠼⠁⠃⠉", "Привет some text on another language мир. Но 123 123")]
        public void ReverseRussianTextTranslatorTest(string input, string expected)
        {
            var russianTextTranslator = new TextTranslator(Language.Russian, isReverseTranslation: true);

            Assert.Equal(expected, russianTextTranslator.Translate(input));
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("⠠⠓⠑⠇⠇⠕ ⠍⠽ ⠙⠑⠁⠗ ⠙⠥⠙⠑⠲ ⠼⠁⠃⠉ ⠁⠏⠏⠇⠑⠼⠁⠃⠉", "Hello my dear dude. 123 apple123")]
        public void ReverseEnglishTextTranslatorTest(string input, string expected)
        {
            var englishTextTranslator = new TextTranslator(Language.English, isReverseTranslation: true);

            Assert.Equal(expected, englishTextTranslator.Translate(input));
        }
    }
}