using System;

namespace BrailleToTextTransformer.Models
{
    [Flags]
    public enum Language
    {
        English = 1,
        Russian = 1 << 1
    }
}