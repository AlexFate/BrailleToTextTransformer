using System;
using System.Linq;
using BrailleToTextTransformer.Base;
using BrailleToTextTransformer.Base.Interfaces;
using BrailleToTextTransformer.Models;

namespace BrailleToTextTransformer.Services
{
    public sealed class TextTranslator : ITranslator
    {
        private TranslatorBase LanguageTranslator { get; }
        private TranslatorBase NumericTranslator { get; }
        private TranslatorBase SpecialSymbolTranslator { get; }

        public TextTranslator(TranslatorBase languageTranslator, TranslatorBase numericTranslator, 
            TranslatorBase specialSymbolTranslator)
        {
            LanguageTranslator = languageTranslator;
            NumericTranslator = numericTranslator;
            SpecialSymbolTranslator = specialSymbolTranslator;
        }
        
        public TextTranslator(Language language, bool isReverseTranslation = false)
        {
            LanguageTranslator = new MultilingualTranslator(language, isReverseTranslation);
            NumericTranslator = new NumericTranslator(isReverseTranslation);
            SpecialSymbolTranslator = new SpecialTranslator(isReverseTranslation);
        }

        public string Translate(string input) => TranslateText(input);

        public string TranslateChar(char input) => TranslateText(input.ToString());
        private string TranslateText(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";

            var result = "";
            var countOfTranslatedSymbol = 0;
            while (!IsAllTextWasTranslated(input, countOfTranslatedSymbol))
            {
                var forLangTranslation = GetPartOfString(input, LanguageTranslator.CanTranslate, ref countOfTranslatedSymbol);

                var forSpecialTranslation = GetPartOfString(input, SpecialSymbolTranslator.CanTranslate, ref countOfTranslatedSymbol);

                var forNumericTranslation = GetPartOfString(
                    input,
                    NumericTranslator.CanTranslate,
                    ref countOfTranslatedSymbol,
                    NumericTranslator.CanTranslate);

                var untranslatedSymbols =
                    GetPartOfString(input, inputChar => !CanTranslate(inputChar), ref countOfTranslatedSymbol);

                result += $"{LanguageTranslator.Translate(forLangTranslation)}" +
                          $"{SpecialSymbolTranslator.Translate(forSpecialTranslation)}" +
                          $"{NumericTranslator.Translate(forNumericTranslation)}" +
                          $"{untranslatedSymbols}";
            }
            return result;
        }
        public bool CanTranslate(string input)
        {
            return LanguageTranslator.CanTranslate(input) || 
                   SpecialSymbolTranslator.CanTranslate(input) ||
                   NumericTranslator.CanTranslate(input);
        }
        public bool CanTranslate(char input)
        {
            return LanguageTranslator.CanTranslate(input) || 
                   SpecialSymbolTranslator.CanTranslate(input) ||
                   NumericTranslator.CanTranslate(input);        
        }
        private static bool IsAllTextWasTranslated(string input, int internalSkip) => internalSkip >= input.Length;
        private static string GetPartOfString(string input, Func<char, bool> takeWhile, ref int valueForSkip, Func<string, bool> validateResult = null)
        {
            var forTranslation = string.Join("", input.Skip(valueForSkip).TakeWhile(takeWhile));

            var mustBeTranslated = validateResult?.Invoke(forTranslation) ?? true;

            forTranslation = mustBeTranslated ? forTranslation : "";

            valueForSkip += forTranslation.Length;

            return forTranslation;
        }
    }
}