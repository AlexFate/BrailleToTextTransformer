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
        
        [Theory]
        [InlineData("", "")]
        [InlineData("Привет мир. Но 123 123", "⠠⠏⠗⠊⠺⠑⠞ ⠍⠊⠗⠲ ⠠⠝⠕ ⠼⠁⠃⠉ ⠼⠁⠃⠉")]
        [InlineData("Привет some text on another language мир. Но 123 123", "⠠⠏⠗⠊⠺⠑⠞ some text on another language ⠍⠊⠗⠲ ⠠⠝⠕ ⠼⠁⠃⠉ ⠼⠁⠃⠉")]
        public void RussianTextTranslatorTest(string input, string expected)
        {
            var russianTextTranslator = new TextTranslator(Language.Russian);

            Assert.Equal(expected, russianTextTranslator.Translate(input));
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("Hello my dear dude. 123 apple123", "⠠⠓⠑⠇⠇⠕ ⠍⠽ ⠙⠑⠁⠗ ⠙⠥⠙⠑⠲ ⠼⠁⠃⠉ ⠁⠏⠏⠇⠑⠼⠁⠃⠉")]
        public void EnglishTextTranslatorTest(string input, string expected)
        {
            var englishTextTranslator = new TextTranslator(Language.English);

            Assert.Equal(expected, englishTextTranslator.Translate(input));
        }
    }
}