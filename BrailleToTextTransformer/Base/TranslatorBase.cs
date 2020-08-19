using System.Collections.Generic;
using System.Linq;
using BrailleToTextTransformer.Base.Interfaces;

namespace BrailleToTextTransformer.Base
{
    public abstract class TranslatorBase : ITranslator
    {
        protected Dictionary<string, string> TranslatorDictionary { get; set; }
        protected bool IsReverseTranslation { get; }

        protected TranslatorBase(bool isReverseTranslation) => IsReverseTranslation = isReverseTranslation;

        public virtual string Translate(string input)
            => CanTranslate(input) ? string.Join("", input.Select(TranslateChar)) : "";
        public abstract string TranslateChar(char input);
        
        public virtual bool CanTranslate(string input) => input.All(CanTranslate);
        public virtual bool CanTranslate(char input) => TranslatorDictionary.ContainsKey(input.ToString().ToLower());

    }
}