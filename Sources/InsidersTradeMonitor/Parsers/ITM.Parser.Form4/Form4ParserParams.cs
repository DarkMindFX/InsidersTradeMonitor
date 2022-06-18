using ITM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM.Parser.Form4
{
    public class Form4ParserParams : IFilingParserParams
    {
        public Stream FileContent { get; set; }
        public string ReportID { get; set; }
    }
}
