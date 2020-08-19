using BrailleToTextTransformer.Models;
using BrailleToTextTransformer.Services;

namespace BrailleExampleApp
{
    public sealed class UsageOfCustomTranslatorsExample
    {
        public void UseCustomWithTextTranslator()
        {
            // Define translators:
            // Is reverse translation?
            var isReverseTranslations = false;
            // Create our CustomNumericTranslator
            var customNumericTranslator = new CustomNumericTranslator(isReverseTranslations);
            // Create defaults
            var defaultLangTranslator = new MultilingualTranslator(Language.English, isReverseTranslations);
            var defaultSpecialsTranslator = new SpecialTranslator(isReverseTranslations);
            
            // Create TextTranslator, and pass our preconfigured translators as .ctor args.
            var defaultTextTranslator = new TextTranslator(defaultLangTranslator, customNumericTranslator, defaultSpecialsTranslator);
        }
    }
}