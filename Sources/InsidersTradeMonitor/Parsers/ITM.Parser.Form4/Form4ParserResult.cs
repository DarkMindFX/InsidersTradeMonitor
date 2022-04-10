using ITM.Interfaces;

namespace ITM.Parser.Form4
{
    public class Form4ParserResult : ResultBase, IFilingParserResult
    {
        public IStatement Statement
        {
            get; set;
        }
    }
}
