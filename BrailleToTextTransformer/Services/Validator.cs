namespace BrailleToTextTransformer.Services
{
    public sealed class Validator
    {
        private bool IsReverseTranslation { get; }
        public Validator(bool isReverseTranslation)
        {
            IsReverseTranslation = isReverseTranslation;
        }
        
        /// <summary>
        /// Allow to skip invalid numeric string when Braille text translated into Numeric. If substring in text doesn't contains NumericMarkerSymbol, it isn't a numeric, it is a language chars string.
        /// </summary>
        /// <returns></returns>
        public string ValidateIsNumericSubstring(string textSubString)
        {
            if (!IsReverseTranslation || string.IsNullOrEmpty(textSubString)) return textSubString;

            return textSubString.Contains(NumericTranslator.NumericMarkerSymbol) ? textSubString : "";
        }
    }
}