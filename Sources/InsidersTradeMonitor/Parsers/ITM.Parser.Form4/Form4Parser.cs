using ITM.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ITM.Parser.Form4
{
    [Export( typeof(IFilingParser))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class Form4Parser : IFilingParser
    {
        private const string ReportTypeName = "4";
        public string ReportType => ReportTypeName;

        public IFilingParserParams CreateFilingParserParams()
        {
            return new Form4ParserParams();
        }

        public IFilingParserResult Parse(IFilingParserParams parserParams)
        {
            IFilingParserResult result = null;
            var form4Params = parserParams as Form4ParserParams;
            if (form4Params != null)
            {
                var xmlDoc = GetXmlFromStream(form4Params.FileContent);
                result = new Form4ParserResult();
                var statement = new Form4Report();

                ExtractReportInfo(xmlDoc, statement);

                result.Statement = statement;
            }
            else
            {
                throw new ArgumentException($"Expected type Form4ParserParams but got {parserParams.GetType().ToString()}");
            }

            return result;
        }

        #region Support methods

        private void ExtractReportInfo(XmlDocument xmlDoc, Form4Report statement)
        {
            string[] tags =
            {
                "issuerName",
                "issuerTradingSymbol", 
                "issuerCik"
            };

            Dictionary<string, string> values = new Dictionary<string, string>();

            ExtractXmlData(xmlDoc, tags, values);

            statement.IssuerName = values[tags[0]];
            statement.IssuerSymbol = values[tags[1]];
            statement.IssuerCIK = values[tags[2]];
        }

        private XmlDocument GetXmlFromStream(Stream s)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(s);

            return doc;
        }

        protected void ExtractXmlData(XmlDocument doc, string[] tags, Dictionary<string, string> values)
        {
            foreach (var t in tags)
            {
                XmlNodeList nodes = doc.GetElementsByTagName(t);
                if (nodes != null && nodes.Count > 0)
                {
                    values.Add(t, nodes[0].InnerText);
                }
            }
        }
        #endregion
    }
}
