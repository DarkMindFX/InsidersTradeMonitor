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
    [Export(typeof(IFilingParser))]
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
            IFilingParserResult result = new Form4ParserResult();
            try
            {
                
                var form4Params = parserParams as Form4ParserParams;
                if (form4Params != null)
                {
                    var xmlDoc = GetXmlFromStream(form4Params.FileContent);
                    result = new Form4ParserResult();
                    var statement = new Form4Report();

                    ExtractReportInfo(xmlDoc, statement);
                    ExtractReportingOwnerData(xmlDoc, statement);
                    ExtractNonDerivaties(xmlDoc, statement);
                    ExtractDerivaties(xmlDoc, statement);

                    result.Statement = statement;
                }
                else
                {
                    throw new ArgumentException($"Expected type Form4ParserParams but got {parserParams.GetType().ToString()}");
                }
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Errors.Add(new Error()
                {
                    Code = EErrorCodes.ParserError,
                    Message = ex.Message,
                    Type = EErrorType.Error
                });
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
                "issuerCik",
                "periodOfReport"
            };

            Dictionary<string, string> values = new Dictionary<string, string>();

            ExtractXmlData(xmlDoc, tags, values);

            statement.IssuerName = values[tags[0]];
            statement.IssuerSymbol = values[tags[1]];
            statement.IssuerCIK = values[tags[2]];
            statement.PeriodOfReport = DateTime.Parse(values[tags[3]]);
        }

        private void ExtractReportingOwnerData(XmlDocument xmlDoc, Form4Report statement)
        {
            string[] tags =
            {
                "rptOwnerName", 
                "rptOwnerCik", 
                "rptOwnerStreet1", 
                "rptOwnerStreet2", 
                "rptOwnerCity", 
                "rptOwnerState",
                "rptOwnerZipCode", 
                "rptOwnerStateDescription", 

                "isDirector",
                "isOfficer",
                "isTenPercentOwner",
                "isOther",
                "officerTitle",
                "otherText"
            };

            Dictionary<string, string> values = new Dictionary<string, string>();
            ExtractXmlData(xmlDoc, tags, values);

            statement.OwnerName = values["rptOwnerName"];
            statement.OwnerCIK = values["rptOwnerCik"];
            statement.OwnerStreet1 = values["rptOwnerStreet1"];
            statement.OwnerStreet2 = values["rptOwnerStreet2"];
            statement.OwnerCity = values["rptOwnerCity"];
            statement.OwnerState = values["rptOwnerState"];
            statement.OwnerZipCode = values["rptOwnerZipCode"];
            statement.OwnerStateDescription = values["rptOwnerStateDescription"];

            statement.IsDirector = "1".Equals(values["isDirector"]);
            statement.IsOfficer = "1".Equals(values["isOfficer"]);
            statement.IsTenPercentHolder = "1".Equals(values["isTenPercentOwner"]);
            statement.IsOther = "1".Equals(values["isOther"]);
            statement.OfficerTitle = values["officerTitle"];
            statement.OwnerOtherText = values["otherText"];
        }

        private void ExtractDerivaties(XmlDocument xmlDoc, Form4Report statement)
        {
            string xpath = "//derivativeTable//derivativeTransaction";

            XmlNodeList nsNonDerivs = xmlDoc.SelectNodes(xpath);
            if (nsNonDerivs != null && nsNonDerivs.Count > 0)
            {
                statement.DerivativeTransactions = new List<DerivativeTransaction>();
                foreach (XmlNode n in nsNonDerivs)
                {

                    XmlNode? nSecurityTitle = n.SelectSingleNode("securityTitle//value");
                    XmlNode? nConvExPrice = n.SelectSingleNode("conversionOrExercisePrice//value");

                    XmlNode? nTransactionDate = n.SelectSingleNode("transactionDate//value");
                    XmlNode? nTransactionDeemedExecDate = n.SelectSingleNode("deemedExecutionDate//value");

                    XmlNode? nTransactionFormType = n.SelectSingleNode("transactionCoding//transactionFormType");
                    XmlNode? nTransactionFormTransCode = n.SelectSingleNode("transactionCoding//transactionCode");
                    XmlNode? nTransactionFormEquitySwapInvolved = n.SelectSingleNode("transactionCoding//equitySwapInvolved");

                    XmlNode? nTransactionAmountShares = n.SelectSingleNode("transactionAmounts//transactionShares//value");
                    XmlNode? nTransactionAmountPrice = n.SelectSingleNode("transactionAmounts//transactionPricePerShare//value");
                    XmlNode? nTransactionAmountADCode = n.SelectSingleNode("transactionAmounts//transactionAcquiredDisposedCode//value");

                    XmlNode? nExDate = n.SelectSingleNode("exerciseDate//value");
                    XmlNode? nExpDate = n.SelectSingleNode("expirationDate//value");

                    XmlNode? nUnderSecurityTitle = n.SelectSingleNode("underlyingSecurity//underlyingSecurityTitle//value");
                    XmlNode? nUnderSecuritySharesAmount = n.SelectSingleNode("underlyingSecurity//underlyingSecurityShares//value");

                    XmlNode? nPostShares = n.SelectSingleNode("postTransactionAmounts//sharesOwnedFollowingTransaction//value");

                    XmlNode? nPostOwnership = n.SelectSingleNode("ownershipNature//directOrIndirectOwnership//value");

                    XmlNode? nOwnershipNature = n.SelectSingleNode("ownershipNature//natureOfOwnership//value");


                    DerivativeTransaction t = new DerivativeTransaction();
                    statement.DerivativeTransactions.Add(t);

                    if(nSecurityTitle != null) 
                        t.TitleOfDerivative = nSecurityTitle.InnerText;

                    if(nConvExPrice != null) 
                        t.ConversionExcercisePrice = !string.IsNullOrEmpty(nConvExPrice.InnerText) ? decimal.Parse(nConvExPrice.InnerText) : 0;

                    if(nTransactionDate != null) 
                        t.TransactionDate = !string.IsNullOrEmpty(nTransactionDate.InnerText) ? DateTime.Parse(nTransactionDate.InnerText) : DateTime.MinValue;
                    
                    if (nTransactionDeemedExecDate != null) 
                        t.DeemedExecDate = !string.IsNullOrEmpty(nTransactionDeemedExecDate.InnerText) ? DateTime.Parse(nTransactionDeemedExecDate.InnerText) : null;
                    
                    if (nTransactionFormTransCode != null) 
                        t.TransactionCode = nTransactionFormTransCode.InnerText;
                    
                    if (nTransactionFormEquitySwapInvolved != null) 
                        t.EquitySwapsInvolved = "1".Equals(nTransactionFormEquitySwapInvolved.InnerText);
                    
                    if (nExDate != null) 
                        t.DateExercisable = !string.IsNullOrEmpty(nExDate.InnerText) ? DateTime.Parse(nExDate.InnerText) : null;
                    
                    if (nExpDate != null) 
                        t.ExpirationDate = !string.IsNullOrEmpty(nExpDate.InnerText) ? DateTime.Parse(nExDate.InnerText) : null;
                    
                    if (nTransactionAmountShares != null) 
                        t.SharesAmount = long.Parse(nTransactionAmountShares.InnerText);
                    
                    if (nTransactionAmountPrice != null) 
                        t.DerivativeSecurityPrice = decimal.Parse(nTransactionAmountPrice.InnerText);

                    if (nTransactionAmountADCode != null)
                        t.TransactionADType = nTransactionAmountADCode.InnerText;

                    if (nUnderSecurityTitle != null)
                        t.UnderlyingTitle = nUnderSecurityTitle.InnerText;

                    if (nUnderSecuritySharesAmount != null)
                        t.UnderlyingSharesAmount = !string.IsNullOrEmpty(nUnderSecuritySharesAmount.InnerText) ? (long)decimal.Parse(nUnderSecuritySharesAmount.InnerText) : 0;

                    if (nPostShares != null) 
                        t.AmountFollowingReport = long.Parse(nPostShares.InnerText);
                    
                    if (nPostOwnership != null) 
                        t.OwnershipType = nPostOwnership.InnerText;
                    
                    if (nOwnershipNature != null) 
                        t.NatureOfIndirectOwnership = nOwnershipNature.InnerText;
                }
            }
        }

        private void ExtractNonDerivaties(XmlDocument xmlDoc, Form4Report statement)
        {
            string[] xpaths = { "//nonDerivativeTable//nonDerivativeTransaction", "//nonDerivativeTable//nonDerivativeHolding" };

            foreach (var xpath in xpaths)
            {
                XmlNodeList nsNonDerivs = xmlDoc.SelectNodes(xpath);
                if (nsNonDerivs != null && nsNonDerivs.Count > 0)
                {
                    statement.NonDerivativeTransactions = new List<NonDerivativeTransaction>();
                    foreach (XmlNode n in nsNonDerivs)
                    {
                        XmlNode? nSecurityTitle = n.SelectSingleNode("securityTitle//value");

                        XmlNode? nTransactionDate = n.SelectSingleNode("transactionDate//value");
                        XmlNode? nTransactionDeemedExecDate = n.SelectSingleNode("deemedExecutionDate//value");

                        XmlNode? nTransactionFormType = n.SelectSingleNode("transactionCoding//transactionFormType");
                        XmlNode? nTransactionFormTransCode = n.SelectSingleNode("transactionCoding//transactionCode");
                        XmlNode? nTransactionFormEquitySwapInvolved = n.SelectSingleNode("transactionCoding//equitySwapInvolved");

                        XmlNode? nTransactionAmountShares = n.SelectSingleNode("transactionAmounts//transactionShares//value");
                        XmlNode? nTransactionAmountPrice = n.SelectSingleNode("transactionAmounts//transactionPricePerShare//value");
                        XmlNode? nTransactionAmountADCode = n.SelectSingleNode("transactionAmounts//transactionAcquiredDisposedCode//value");

                        XmlNode? nPostShares = n.SelectSingleNode("postTransactionAmounts//sharesOwnedFollowingTransaction//value");

                        XmlNode? nPostOwnership = n.SelectSingleNode("ownershipNature//directOrIndirectOwnership//value");

                        XmlNode? nOwnershipNature = n.SelectSingleNode("ownershipNature//natureOfOwnership//value");

                        NonDerivativeTransaction t = new NonDerivativeTransaction();
                        statement.NonDerivativeTransactions.Add(t);

                        if(nSecurityTitle != null) 
                            t.TitleOfSecurity = nSecurityTitle.InnerText;
                        
                        if (nTransactionDeemedExecDate != null) 
                            t.DeemedExecDate = !string.IsNullOrEmpty(nTransactionDeemedExecDate.InnerText) ? DateTime.Parse(nTransactionDeemedExecDate.InnerText) : null;
                        
                        if (nTransactionDate != null) 
                            t.TransactionDate = DateTime.Parse(nTransactionDate.InnerText);
                        
                        if (nTransactionAmountADCode != null) 
                            t.TransactionADType = nTransactionAmountADCode.InnerText;
                        
                        if (nTransactionFormTransCode != null) 
                            t.TransactionCode = nTransactionFormTransCode.InnerText;
                        
                        if (nTransactionFormEquitySwapInvolved != null) 
                            t.EquitySwapsInvolved = "1".Equals(nTransactionFormEquitySwapInvolved.InnerText);
                        
                        if (nTransactionAmountShares != null) 
                            t.SharesAmount = long.Parse(nTransactionAmountShares.InnerText);
                        
                        if (nTransactionAmountPrice != null) 
                            t.Price = decimal.Parse(nTransactionAmountPrice.InnerText);
                        
                        if (nPostShares != null) 
                            t.AmountFollowingReport = long.Parse(nPostShares.InnerText);
                        
                        if (nPostOwnership != null) 
                            t.OwnershipType = nPostOwnership.InnerText;
                        
                        if (nOwnershipNature != null) 
                            t.NatureOfIndirectOwnership = nOwnershipNature.InnerText;
                        
                    }
                }
            }

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
