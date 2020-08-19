using System;
using System.Collections.Generic;
using System.Linq;
using BrailleToTextTransformer.Base;
using BrailleToTextTransformer.Models;

namespace BrailleToTextTransformer.Services
{
    public sealed class MultilingualTranslator : TranslatorBase
    {
        private const char UpperCaseMarker = '⠠';

        public MultilingualTranslator(Language language, bool isReverseTranslation) : base(isReverseTranslation) 
            => TranslatorDictionary = CreateTranslationDictionary(language, isReverseTranslation);

        public override string Translate(string input) => IsReverseTranslation ? ReturnUpperCase(base.Translate(input)) : base.Translate(input);

        public override string TranslateChar(char input) => GetTranslatedChar(input);

        private string GetTranslatedChar(char input)
            => IsUpperCaseNeeded(input) 
                ? $"{UpperCaseMarker}{TranslatorDictionary[input.ToString().ToLower()]}"
                : TranslatorDictionary[input.ToString().ToLower()];

        private static bool IsUpperCaseNeeded(char item) => (item >= 'А' && item <= 'Я') || (item >= 'A' && item <= 'Z') || item == '⠠';

        private static string ReturnUpperCase(string output)
        {
            var charArray = output.ToCharArray();
            var countOfUpperCaseMarker = output.Count(item => item == UpperCaseMarker);
            for (var i = 0; i < countOfUpperCaseMarker; i++)
            {
                var indexOfUpperMarker = Array.IndexOf(charArray, UpperCaseMarker);
                var indexOfUpperSymbol = indexOfUpperMarker + 1;
                charArray[indexOfUpperSymbol] = char.ToUpper(charArray[indexOfUpperSymbol]);
                charArray[indexOfUpperMarker] = default;
            }
            
            return string.Join("", charArray.Where(item => item != default));
        }

        private static Dictionary<string, string> CreateTranslationDictionary(Language language, bool isReverseTranslation)
        {
            Dictionary<string, string> result;
            switch (language)
            {
                case Language.English:
                    result = new Dictionary<string, string>
                    {
                        { "", "⠠" },
                        { "a", "⠁" },
                        { "b", "⠃" },
                        { "c", "⠉" },
                        { "d", "⠙" },
                        { "e", "⠑" },
                        { "f", "⠋" },
                        { "g", "⠛" },
                        { "h", "⠓" },
                        { "i", "⠊" },
                        { "j", "⠚" },
                        { "k", "⠅" },
                        { "l", "⠇" },
                        { "m", "⠍" },
                        { "n", "⠝" },
                        { "o", "⠕" },
                        { "p", "⠏" },
                        { "q", "⠟" },
                        { "r", "⠗" },
                        { "s", "⠎" },
                        { "t", "⠞" },
                        { "u", "⠥" },
                        { "v", "⠧" },
                        { "w", "⠺" },
                        { "x", "⠭" },
                        { "y", "⠽" },
                        { "z", "⠵" }
                    };
                    break;
                case Language.Russian:
                    result = new Dictionary<string, string>
                    {
                        { "", "⠠" },
                        { "а", "⠁" },
                        { "б", "⠃" },
                        { "в", "⠺" },
                        { "г", "⠛" },
                        { "д", "⠙" },
                        { "е", "⠑" },
                        { "ё", "⠡" },
                        { "ж", "⠚" },
                        { "з", "⠵" },
                        { "и", "⠊" },
                        { "й", "⠯" },
                        { "к", "⠅" },
                        { "л", "⠇" },
                        { "м", "⠍" },
                        { "н", "⠝" },
                        { "о", "⠕" },
                        { "п", "⠏" },
                        { "р", "⠗" },
                        { "с", "⠎" },
                        { "т", "⠞" },
                        { "у", "⠥" },
                        { "ф", "⠋" },
                        { "х", "⠓" },
                        { "ц", "⠉" },
                        { "ч", "⠟" },
                        { "ш", "⠱" },
                        { "щ", "⠭" },
                        { "ь", "⠷" },
                        { "ы", "⠮" },
                        { "ъ", "⠾" },
                        { "э", "⠪" },
                        { "ю", "⠳" },
                        { "я", "⠫" }
                    };
                    break;
                default:
                    return new Dictionary<string, string>();
            }

            return isReverseTranslation
                        ? result.ToDictionary(originalDict => originalDict.Value, originalDict => originalDict.Key)
                        : result;
        }

    }
}