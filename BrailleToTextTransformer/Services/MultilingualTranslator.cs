using System.Collections.Generic;
using System.Linq;
using BrailleToTextTransformer.Base;
using BrailleToTextTransformer.Models;

namespace BrailleToTextTransformer.Services
{
    public class MultilingualTranslator : TranslatorBase
    {
        private const char UpperSpecial = '⠠';

        public MultilingualTranslator(bool isReverseTranslation = false) : base(isReverseTranslation)
        {
        }

        //TODO: Remove virtual member call from ctor (CreateTranslationDictionary)
        public MultilingualTranslator(Language language, bool isReverseTranslation = false) : base(isReverseTranslation) 
            => TranslatorDictionary = CreateTranslationDictionary(language, isReverseTranslation);

        public override string Translate(string input) => IsReverseTranslation ? ReturnUpperCase(base.Translate(input)) : base.Translate(input);

        public override string TranslateChar(char input) => GetTranslatedChar(input);

        private string GetTranslatedChar(char input)
            => IsUpperCaseNeeded(input) 
                ? $"{UpperSpecial}{TranslatorDictionary[input.ToString().ToLower()]}"
                : TranslatorDictionary[input.ToString().ToLower()];

        private static bool IsUpperCaseNeeded(char item) => (item >= 'А' && item <= 'Я') || (item >= 'A' && item <= 'Z') || item == '⠠';

        private static string ReturnUpperCase(string output)
        {
            var charList = output.ToCharArray().ToList();
            var countOfUpperCaseSymbol = output.Count(item => item == UpperSpecial);
            for (var i = 0; i < countOfUpperCaseSymbol; i++)
            {
                var indexOfUpperSpecial = charList.IndexOf(UpperSpecial);
                var indexOfUpperSymbol = indexOfUpperSpecial + 1;
                charList[indexOfUpperSymbol] = charList[indexOfUpperSymbol].ToString().ToUpper().First();
                charList[indexOfUpperSpecial] = default;
            }

            return string.Join("", charList.ToArray().Where(item => item != default));
        }

        private protected virtual Dictionary<string, string> CreateTranslationDictionary(Language language, bool isReverseTranslation)
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