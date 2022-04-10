namespace ITM.Interfaces
{
    public interface IFilingParserParams
    {
        Stream FileContent
        {
            get;
            set;
        }
    }

    public interface IStatement
    {
        string ReportType
        {
            get;
        }
    }

    public interface IFilingParserResult : IResult
    {
        IStatement Statement
        {
            get;
            set;
        }
    }

    public interface IFilingParser
    {
        /// <summary>
        /// Main method responsible for parsing reports
        /// </summary>
        /// <param name="parserParams"></param>
        /// <returns></returns>
        IFilingParserResult Parse(IFilingParserParams parserParams);

        /// <summary>
        /// Type of report this parser can parse
        /// </summary>
        string ReportType
        {
            get;
        }

        IFilingParserParams CreateFilingParserParams();
    }
}