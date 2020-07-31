using System.Collections.Generic;
using System.Linq;

namespace BrailleToTextTransformer.Interfaces
{
    public interface ITranslator
    {
        public Dictionary<string, string> TranslatorDictionary { get; }
        public bool IsReverseTranslation { get; }
        
        public virtual string Translate(string input) => string.Join("", input.Select(TranslateChar));
        public abstract string TranslateChar(char input);
        
        public virtual bool CanTranslate(string input) => input.All(CanTranslate);
        public virtual bool CanTranslate(char input) => TranslatorDictionary.ContainsKey(input.ToString().ToLower());
    }
}