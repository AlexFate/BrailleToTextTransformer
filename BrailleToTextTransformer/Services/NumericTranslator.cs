using BrailleToTextTransformer.Base;

using System.Collections.Generic;
using System.Linq;

namespace BrailleToTextTransformer.Services
{
    public sealed class NumericTranslator : TranslatorBase
    {
        public const string NumericMarkerSymbol = "⠼";
        public NumericTranslator(bool isReverseTranslation) : base(isReverseTranslation)
        {
            TranslatorDictionary = CreateTranslationDictionary(isReverseTranslation);
        }

        public override string Translate(string input)
        {
            var result = base.Translate(input);
            if (string.IsNullOrEmpty(result)) return result;
            
            return IsReverseTranslation ? result : NumericMarkerSymbol + result;
        }

        public override string TranslateChar(char input) => TranslatorDictionary[input.ToString()];

        public override bool CanTranslate(string input) => base.CanTranslate(input) && ContainsNumericMarker(input);

        private bool ContainsNumericMarker(string input) => !IsReverseTranslation || string.IsNullOrEmpty(input) || input.Contains(NumericMarkerSymbol);
        
        private static Dictionary<string, string> CreateTranslationDictionary(bool isReverseTranslation)
        {
            var result = new Dictionary<string, string>
            {
                { "", NumericMarkerSymbol },
                { "0", "⠚" },
                { "1", "⠁" },
                { "2", "⠃" },
                { "3", "⠉" },
                { "4", "⠙" },
                { "5", "⠑" },
                { "6", "⠋" },
                { "7", "⠛" },
                { "8", "⠓" },
                { "9", "⠊" }
            };

            return isReverseTranslation
                ? result.ToDictionary(originalDict => originalDict.Value, originalDict => originalDict.Key)
                : result;
        }
    }
}