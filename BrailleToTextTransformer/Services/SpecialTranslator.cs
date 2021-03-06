using System.Collections.Generic;
using System.Linq;
using BrailleToTextTransformer.Base;

namespace BrailleToTextTransformer.Services
{
    public sealed class SpecialTranslator : TranslatorBase
    {
        public SpecialTranslator(bool isReverseTranslation) : base(isReverseTranslation)
        {
            TranslatorDictionary = CreateTranslationDictionary(isReverseTranslation);
        }
        
        public override string TranslateChar(char input) => GetTranslatedChar(input).ToString();

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
                { " ", " " }
            };

            return isReverseTranslation
                ? result.ToDictionary(originalDict => originalDict.Value, originalDict => originalDict.Key)
                : result;
        }
    }
}