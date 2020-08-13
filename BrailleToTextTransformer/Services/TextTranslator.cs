using System;
using System.Linq;
using BrailleToTextTransformer.Base.Interfaces;
using BrailleToTextTransformer.Models;

namespace BrailleToTextTransformer.Services
{
    public sealed class TextTranslator : ITranslator
    {
        private MultilingualTranslator LanguageTranslator { get; }
        private NumericTranslator NumericTranslator { get; }
        private SpecialTranslator SpecialSymbolTranslator { get; }
        private Validator Validator { get; }

        public TextTranslator(Language language, bool isReverseTranslation = false)
        {
            LanguageTranslator = new MultilingualTranslator(language, isReverseTranslation);
            NumericTranslator = new NumericTranslator(isReverseTranslation);
            SpecialSymbolTranslator = new SpecialTranslator(isReverseTranslation);
            Validator = new Validator(isReverseTranslation);
        }

        public string Translate(string input) => TranslateText(input);

        public string TranslateChar(char input) => TranslateText(input.ToString());
        private string TranslateText(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";

            var result = "";
            var internalSkip = 0;
            while (!IsAllTextWasTranslated(input, internalSkip))
            {
                var forLangTranslation = GetPartOfString(input, LanguageTranslator.CanTranslate, ref internalSkip);

                var forSpecialTranslation = GetPartOfString(input, SpecialSymbolTranslator.CanTranslate, ref internalSkip);

                var forNumericTranslation = GetPartOfString(
                    input,
                    NumericTranslator.CanTranslate,
                    ref internalSkip,
                    Validator.ValidateIsNumericSubstring);

                var untranslatedSymbols =
                    GetPartOfString(input, inputChar => !CanTranslate(inputChar), ref internalSkip);

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
        private static string GetPartOfString(string input, Func<char, bool> takeWhile, ref int valueForSkip, Func<string, string> validateResult = null)
        {
            var forTranslation = string.Join("", input.Skip(valueForSkip).TakeWhile(takeWhile));

            forTranslation = validateResult == null ? forTranslation : validateResult(forTranslation);

            valueForSkip += forTranslation.Length;

            return forTranslation;
        }
    }
}