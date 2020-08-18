using System;

using BrailleToTextTransformer.Models;
using BrailleToTextTransformer.Services;

namespace BrailleExampleApp
{
    class Program
    {
        private static string EnglishTextExample = 
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore " +
            "et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut " +
            "aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum " +
            "dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui " +
            "officia deserunt mollit anim id est laborum.";
        
        static void Main(string[] args) 
        {
            UseDefaultTranslatorExample();
        }

        private static void UseDefaultTranslatorExample()
        {
            var brailleResult = FromLanguageToBrailleExample(EnglishTextExample);
            var textReturn = FromBrailleToTextExample(EnglishTextExample);
            Console.Write($"Example english text: {EnglishTextExample}\n Braille result: {brailleResult}\n Return text back: {textReturn}\n");
        }

        private static string FromLanguageToBrailleExample(string input)
        {
            TextTranslator translator = new TextTranslator(Language.English);
            return translator.Translate(input);
        }

        private static string FromBrailleToTextExample(string inputBraille)
        {
            TextTranslator fromBrailleTranslator = new TextTranslator(Language.English, isReverseTranslation: true);
            return fromBrailleTranslator.Translate(inputBraille);
        }
        
    }
}