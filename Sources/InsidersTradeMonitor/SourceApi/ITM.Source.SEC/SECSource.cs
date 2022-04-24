using ITM.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HtmlAgilityPack;
using ITM.SEC.Api;
using System.Collections;

namespace ITM.Source.SEC
{

    [Export("SEC", typeof(ISource))]
    public class SECSource : ISource
    {
        private SECApi _secApi = null;
        private ILogger _logger = null;

        [ImportingConstructor]
        public SECSource()
        {
            _secApi = new SECApi();
        }

        public void Init(ISourceInitParams initParams)
        {
            _logger = initParams.Logger;
        }

        public async Task<ISourceExtractResult> ExtractReports(ISourceExtractParams extractParams)
        {
            SECSourceExtractResult result = new SECSourceExtractResult();

            SECSourceExtractParams extractSECParams = extractParams as SECSourceExtractParams;
            if (extractSECParams != null)
            {
                string cik = extractSECParams.CIK; // TODO: lookup in dictionary
                if (!string.IsNullOrEmpty(cik))
                {
                    foreach (var filing in extractSECParams.Items)
                    {
                        // getting list of files for each filing
                        Submission submission = await _secApi.ArchivesEdgarDataCIKSubmission(cik, filing.Name);
                        if (submission != null)
                        {
                            foreach (var fileInfo in submission.Files)
                            {
                                // to speed up we need to extract only xml files and index headers file
                                if (Path.GetExtension(fileInfo.Name) == ".xml" || fileInfo.Name.Contains(".txt"))
                                {
                                    SubmissionFile file = await _secApi.ArchivesEdgarDataCIKSubmissionFile(cik, filing.Name, fileInfo.Name);
                                    if (file != null)
                                    {
                                        SECSourceItem sourceItem = new SECSourceItem();
                                        sourceItem.Name = fileInfo.Name;
                                        sourceItem.FilingName = filing.Name;
                                        sourceItem.CIK = extractSECParams.CIK;
                                        sourceItem.Content = file.Content;

                                        result.Items.Add(sourceItem);
 
                                    }
                                }
                            }

                        }
                        else
                        {
                            result.AddError(EErrorCodes.ImporterError, EErrorType.Warning, string.Format("Failed to import filing {0}", filing.Name));
                        }
                    }
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.AddError(EErrorCodes.InvalidParserParams, EErrorType.Error, string.Format("Cannot find the SEC CIK for company with code {0}", extractSECParams.CIK));
                }
            }
            else
            {
                result.Success = false;
                result.AddError(EErrorCodes.InvalidParserParams, EErrorType.Error, "Parameters of invalid type were provided");
            }

            return result;
        }

        public async Task<ISourceFilingsListResult> GetFilingsList(ISourceValidateParams vldParams)
        {
            SECSourceFilingsListResult result = new SECSourceFilingsListResult();

            SECSourceValidateParams vldSECParams = vldParams as SECSourceValidateParams;
            if (vldSECParams != null)
            {
                string cik = vldSECParams.CIK;
                if (!string.IsNullOrEmpty(cik))
                {
                     
                    Submissions submissions = await _secApi.ArchivesEdgarDataCIK(cik);
                    
                    foreach (var filing in submissions.Folders.OrderBy( x => x.LastModified ).Where(x => x.LastModified >= vldSECParams.UpdateFromDate && x.LastModified <= vldSECParams.UpdateToDate))
                    {
                        SECSourceItemInfo secSourceItemInfo = new SECSourceItemInfo();
                        secSourceItemInfo.Name = filing.Name;
                        secSourceItemInfo.LastModified = filing.LastModified;
                        result.Filings.Add(secSourceItemInfo);
                    }

                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.AddError(EErrorCodes.InvalidParserParams, EErrorType.Error, string.Format("Cannot find the SEC CIK for company with code {0}", vldParams.CIK));
                }

            }

            return result;
        }

        public async Task<ISourceSubmissionsInfoResult> GetSubmissionsInfo(ISourceSubmissionsInfoParams infoParams)
        {
            ISourceSubmissionsInfoResult result = new SECSourceSubmissionsInfoResult();

            SECSourceSubmissionsInfoParams secInfoParams = infoParams as SECSourceSubmissionsInfoParams;
            if (secInfoParams != null)
            {
                string cik = infoParams.CIK; // TODO: lookup in dictionary
                // for each submission - extracting content and checking type
                int count = 0;
                foreach (var item in secInfoParams.Items)
                {
                    Submission submission = GetSubmissionFromApi(cik, item.Name);

                    if (submission != null)
                    {
                        try
                        {
                            // extracting txt index file
                            SubmissionFileInfo subFileInfo = submission.Files.FirstOrDefault(s => s.Name.Contains("-index.html"));
                            SubmissionFile indexFile = null;

                            if (subFileInfo != null)
                            {
                                indexFile = await _secApi.ArchivesEdgarDataCIKSubmissionFile(cik, item.Name, subFileInfo.Name) ;
                            }

                            if (indexFile != null)
                            {
                                SECSourceSubmissionInfo submissionInfo = ExtractReportDetailsIndexHTML(indexFile);
                                if (submissionInfo != null && !string.IsNullOrEmpty(submissionInfo.Type))
                                {                                                             

                                    submissionInfo.Name = item.Name;
                                    result.Submissions.Add(submissionInfo);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            result.AddError(new Error() { Code = EErrorCodes.ImporterError, Message = string.Format("Report'{0}', Error: {1}", item.Name, ex.Message) });
                        }
                    }
                    else
                    {
                        result.Errors.Add(new Error() { Code = EErrorCodes.SubmissionNotFound, Type = EErrorType.Warning, Message = string.Format("Submission '{0}' was not found", item.Name) });
                    }

                    ++count;
                }

                result.Success = true;
            }
            else
            {
                result.Success = false;
                result.Errors.Add(new Error() { Code = EErrorCodes.InvalidSourceParams, Type = EErrorType.Error, Message = "Invalid parameter provided" });
            }

            return result;
        }


        public async Task<ISourceExtractResult> ExtractFilingItems(ISourceExtractFilingItemsParams extractItemsParams)
        {
          
            SECSourceExtractResult result = new SECSourceExtractResult();
            SECSourceExtractFilingItemsParams extractSECItemsParams = extractItemsParams as SECSourceExtractFilingItemsParams;
            if (extractSECItemsParams != null)
            {
                
                string cik = extractSECItemsParams.CIK; // TODO: lookup in dictionary
                if (!string.IsNullOrEmpty(cik))
                {
                    foreach (var item in extractSECItemsParams.Items)
                    {
                        SubmissionFile file = await _secApi.ArchivesEdgarDataCIKSubmissionFile(cik, extractSECItemsParams.Filing.Name, item.Name);
                        
                        if (file != null)
                        {
                            SECSourceItem sourceItem = ToSourceItem(extractItemsParams.CIK, extractSECItemsParams.Filing.Name, file);
                            
                            result.Items.Add(sourceItem);
                        }
                    }                  
                }
            }

            return result;
        }

        #region Create params methods

        public ISourceInitParams CreateInitParams()
        {
            return new SECSourceInitParams();
        }

        public ISourceExtractFilingItemsParams CreateSourceExtractFilingItemsParams()
        {
            return new SECSourceExtractFilingItemsParams();
        }

        public ISourceValidateParams CreateValidateParams()
        {
            return new SECSourceValidateParams();
        }

        public ISourceExtractParams CreateExtractParams()
        {
            return new SECSourceExtractParams();
        }

        public ISourceItemInfo CreateSourceItemInfo()
        {
            return new SECSourceItemInfo();
        }

        public ISourceSubmissionsInfoParams CreateSourceSubmissionsInfoParams()
        {
            return new SECSourceSubmissionsInfoParams();
        }

        #endregion

        #region Support  methods

        private Submission GetSubmissionFromApi(string cik, string name)
        {
            Submission result = _secApi.ArchivesEdgarDataCIKSubmission(cik, name).Result;

            return result;
        }

        private Submission GetSubmission<SECApi>(string cik, string name)
        {
            Submission result = null;

            return result;
        }

        private SECSourceItem ToSourceItem(string cik, string filingName, SubmissionFile file)
        {
            SECSourceItem result = new SECSourceItem();
            result.CIK = cik;
            result.FilingName = filingName;
            result.Name = file.Name;
            result.Content = file.Content;

            return result;
        }

        #region IndexHTML

        private SECSourceSubmissionInfo ExtractReportDetailsIndexHTML(SubmissionFile submissionIndexFile)
        {
            SECSourceSubmissionInfo subInfo = new SECSourceSubmissionInfo();


            string txtContent = System.Text.Encoding.Default.GetString(submissionIndexFile.Content.ToArray());

            var doc = new HtmlDocument();
            doc.LoadHtml(txtContent);


            HtmlNode nodeType = doc.DocumentNode.SelectSingleNode("//div[@id='formDiv']/div[@id='formHeader']/div[@id='formName']/strong"); // 
            if (nodeType != null)
            {
                if (nodeType.InnerText.Equals("10-Q"))
                {
                    subInfo.Type = "10-Q";
                }
                
                else if (nodeType.InnerText.Equals("10-K"))
                {
                    subInfo.Type = "10-K";
                }

                else if(nodeType.InnerText.Equals("Form 424B2"))
                {
                    subInfo.Type = "424B2";
                }

                else if (nodeType.InnerText.Equals("Form 4"))
                {
                    subInfo.Type = "4";
                }

                else if(nodeType.InnerText.Equals("13F-HR"))
                {
                    subInfo.Type = "13F-HR";
                }

                if (!string.IsNullOrEmpty(subInfo.Type))
                {
                    // extracting dates
                    var nodesMetadata = doc.DocumentNode.SelectNodes("//div[@id='formDiv']/div/div/div[@class='infoHead']");
                    if (nodesMetadata != null)
                    {
                        foreach (HtmlNode node in nodesMetadata)
                        {
                            HtmlNode nodeDate = node.SelectSingleNode("../div[@class='info']");
                            if (nodeDate != null)
                            {
                                if (node.InnerText == "Accepted")
                                {
                                    subInfo.Submitted = DateTime.Parse(nodeDate.InnerText);
                                }
                                else if (node.InnerText == "Period of Report")
                                {
                                    subInfo.PeriodEnd = DateTime.Parse(nodeDate.InnerText);
                                }
                            }
                        }
                    }
                    // extracting report file name
                    HtmlNode nodeFilingData = doc.DocumentNode.SelectSingleNode("//div[@id='formDiv']/div/table/tr/td[text()='EX-101.INS']/..");
                    if (nodeFilingData == null)
                    {
                        switch (subInfo.Type)
                        {
                            case "10-Q":
                            case "10-K":
                                nodeFilingData = doc.DocumentNode.SelectSingleNode("//div[@id='formDiv']/div/table/tr/td[text()='XML']/..");
                                break;
                            case "4":
                                {
                                    var fileNodes = doc.DocumentNode.SelectNodes("//div[@id='formDiv']/div/table/tr/td[text()='4']/..");
                                    nodeFilingData = fileNodes.FirstOrDefault(x => x.SelectSingleNode("td/a") != null && x.SelectSingleNode("td/a").InnerText.IndexOf(".xml") >= 0);
                                }
                                break;
                            case "13F-HR":
                                subInfo.Report.Add("primary_doc.xml");
                                subInfo.Report.Add("form13fInfoTable.xml");
                                break;

                        }
                    }
                    if (nodeFilingData != null)
                    {
                        HtmlNode nodeFileName = nodeFilingData.SelectSingleNode("td/a");
                        subInfo.Report.Add(nodeFileName.InnerText.Trim());
                    }
                }
            }

            return subInfo;
        }

        private string FixIndexXmlContent(string content)
        {
            string result = content;
            // index.html file usually lacks closing tags - fixing it
            if (!result.Contains("</body>"))
            {
                result += "</body>";
            }
            if (!result.Contains("</html>"))
            {
                result += "</html>";
            }

            return result;
        }

        #endregion

        #endregion
    }
}
