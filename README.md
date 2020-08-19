# BrailleToTextTransformer
This project allow your to transform text from language (English, Russian were supported now) to Braille text.
And, of course, allows your to make reverse conversion from Braille to lang.
## Example
* [HowTo](https://github.com/AlexFate/BrailleToTextTransformer/wiki) - Project wiki on github
## Getting Started - Installation
Your can use it as NuGetPackage, or download, compile and add reference on it in your project.
## Built With
* [xUnit] (https://xunit.net/) - Used for testing
* [.Net Standart](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) - Project can be restored with any version
* Default set to standard2.0 for compatibility with .Net Framework.
## Authors
* **Alexandr Ponizov** - *Initial work* - [AlexFate](https://github.com/AlexFate)
* **Maxim Pimkin** - *Creation of an idea and preparation of material for a subject area* - [wowmaks](https://gitlab.com/wowmaks)
## Q&A
### Why your create it project?
Hackathone. And of course we don't find simple braille to text translator in NuGet Gallery. (Not mathematics text)          

### How use BrailleToTextTransformer?
Install NuGet package or add link on BrailleToTextTransformer. Then look on our [BrailleExampleApp](https://github.com/AlexFate/BrailleToTextTransformer/tree/master/BrailleExampleApp).

### What is TextTranslator? How it works? How it use my custom translator?
TextTranslator is ITranslator inheritor. It provide default realisation for interface. Of course, it is honoring the contract.
In backend it use three translators for input string. It use theirs CanTranslate method for take substring from input
For more info look its code on GitHub.
And you can pass custom translator to its .ctor
### The TextTranslator does not suit me, what should I do?
Create yours.           

### TextTranslator return result, but some char || substrings wasn't translated?
It is not bug, it is feature. It ignore symbols that can't be translated.
Some example:
#### [First]
```c#
Your select translationLang to Language.English. But input string contains russian symbol.
For example: Hi, Джим.
TextTranslator translate 'Hi, .' to  braille. But ignore Джим.
```
####[Second]
```c#
Input string contains special symbol, that can't be translated with SpecialTranslator.
For example: #it_hash_teg
SpecialTranslators TranslateDictionary doesn't contains symbol '#' and '_', of course it can't translate it.
```
### I want MORE braille translation, but TextTranslator can translate only casual text?
Yes, but your can create MathTranslator and use it with TextTranslator. For example:
Input: casual text with some math
First iteration (TextTranslator) -> Translate text, and it ignore math.
Second iteration (put previous result to you MathTranslator) -> it translating maths parts.
### How can I add feature in project?
Create issue, clone project, add feature, run test (m.b. write test for feature), pull request.
### How can I add another translators || Language in your project?
If your are developer, use previous answer. If not, create issue and put us info that allow us to implementing feature.
