using System.Collections.Generic;
using System.Linq;
using BrailleToTextTransformer.Base;

namespace BrailleToTextTransformer.Services
{
    public sealed class NumericTranslator : TranslatorBase
    {
        public const string NumericMarkerSymbol = "⠼";
        public NumericTranslator(bool isReverseTranslation = false) : base(isReverseTranslation)
        {
            TranslatorDictionary = CreateTranslationDictionary(isReverseTranslation);
        }

        public override string Translate(string input) 
            => IsReverseTranslation 
                ? GetTranslatedString(input).Replace(NumericMarkerSymbol, "") 
                : GetTranslatedString(input);
        public override string TranslateChar(char input) => GetTranslatedNumeric(input).ToString();
        
        private string GetTranslatedString(string input)
            => GetNumberMarker(input) + string.Join("", input.Select(GetTranslatedNumeric).Where(item => item != default));
        private char GetTranslatedNumeric(char input) 
            => TranslatorDictionary[input.ToString()].FirstOrDefault();

        private static string GetNumberMarker(string input) => string.IsNullOrEmpty(input) ? "" : NumericMarkerSymbol;
        
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