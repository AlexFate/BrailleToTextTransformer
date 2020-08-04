namespace BrailleToTextTransformer.Base.Interfaces
{
    public interface ITranslator
    {
        string Translate(string input);
        string TranslateChar(char input);

        bool CanTranslate(string input);
        bool CanTranslate(char input);
    }
}