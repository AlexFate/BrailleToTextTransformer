using System.Collections.Generic;
using System.Linq;
using BrailleToTextTransformer.Interfaces;

namespace BrailleToTextTransformer.Services
{
    public class SpecialTranslator : ITranslator
    {
        public Dictionary<string, string> TranslatorDictionary { get; }
        public bool IsReverseTranslation { get; }

        public SpecialTranslator(bool isReverseTranslation = false)
        {
            IsReverseTranslation = isReverseTranslation;
            TranslatorDictionary = CreateTranslationDictionary(isReverseTranslation);
        }

        public string TranslateChar(char input) => GetTranslatedChar(input).ToString();

        private char GetTranslatedChar(char input) => TranslatorDictionary[input.ToString()].First();
        private static Dictionary<string, string> CreateTranslationDictionary(bool isReverseTranslation)
        {
            var result = new Dictionary<string, string>
            {
                { ".", "⠲" },
                { ",", "⠂" },
                { "!", "⠖" },
                { "?", "⠢" },
                { "-", "⠤" },
                { " ", " " },
            };

            return isReverseTranslation
                ? result.ToDictionary(originalDict => originalDict.Value, originalDict => originalDict.Key)
                : result;
        }
    }
}