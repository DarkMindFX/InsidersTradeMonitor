namespace ITM.Interfaces
{
    public interface IFilingParserParams
    {
        IDictionary<string, Stream> FileContent
        {
            get;
            set;
        }

        bool ExtractNumbers
        {
            get; set;
        }

        bool ExtractDates
        {
            get; set;
        }

        bool ExtractStrings
        {
            get; set;
        }
    }

    public interface IStatementRecord
    {
        public DateTime PeriodStart
        {
            get;
            set;
        }

        DateTime PeriodEnd
        {
            get;
            set;
        }

        public DateTime Instant
        {
            get;
            set;
        }

        string SourceFactId
        {
            get;
            set;
        }

        string FactId
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
        string RegulatorCode
        {
            get;
            set;
        }

        Dictionary<string, string> CompanyData
        {
            get;
            set;
        }

        Dictionary<string, string> FilingData
        {
            get;
            set;
        }

        string Type
        {
            get;
        }

        DateTime PeriodStart
        {
            get;
        }

        DateTime PeriodEnd
        {
            get;
        }

        IStatement Statement
        {
            get;
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