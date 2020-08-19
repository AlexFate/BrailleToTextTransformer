using System.Collections.Generic;
using BrailleToTextTransformer.Base;

namespace BrailleExampleApp
{
    // Your can use TranslatorBase or ITranslator.
    // TranslatorBase provide basic translator logic, but your should implement abstract Translate(char input) method.
    // ITranslator is interface that provide contract for your class.
    /// <summary>
    /// Custom translator can be use as part of TextTranslator and with combination of default translators.
    /// TextTranslator require three member:
    /// LanguageTranslator, SpecialTranslator, NumericTranslator. (Special use to translate specials symbol ( ,./%$ ...))
    /// </summary>
    public sealed class CustomNumericTranslator : TranslatorBase
    {
        // This field exists in TranslatorBase
        //protected Dictionary<string, string> TranslatorDictionary { get; set; }
        //protected bool IsReverseTranslation { get; }

        public CustomNumericTranslator(bool isReverseTranslations = false) : base(isReverseTranslations)
        {
            // TranslatorBase has Dictionary<string, string> that is a dictionary of translations
            // You should create it for custom type
            TranslatorDictionary = new Dictionary<string, string>();
        }
        // This method can be overridden optionally.
        // Base it use Translate(char input) to every char in input string  
        public override string Translate(string input)
        {
            return base.Translate(input);
        }

        // This method must be always implemented
        public override string TranslateChar(char input)
        {
            //Your code here...
            throw new System.NotImplementedException();
        }
        
        // This method can be overridden optionally.
        // Base it check that CanTranslate(char input) return true for all strings chars
        public override bool CanTranslate(string input)
        {
            return base.CanTranslate(input);
        }
        // This method can be overridden optionally.
        // Default it check that input char exist in TranslatorDictionary
        public override bool CanTranslate(char input)
        {
            return base.CanTranslate(input);
        }
    }
}