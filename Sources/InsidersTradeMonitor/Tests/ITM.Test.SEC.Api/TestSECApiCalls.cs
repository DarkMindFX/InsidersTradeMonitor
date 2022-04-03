using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITM.SEC.Api;
using System.Configuration;
using System.IO;

namespace ITM.Test.SEC
{
    [TestFixture]
    public class TestSECApiCalls
    {
        [Test]
        public void SubmissionsByCIK_Success()
        {
            SECApi api = new SECApi();

            string cik = ConfigurationManager.AppSettings["SEC_CIK_AAPL"];

            Submissions submissions = api.ArchivesEdgarDataCIK(cik).Result;
            Assert.AreNotEqual(submissions, null, "Submissions are NULL");
            Assert.AreEqual(submissions.CIK, cik, "Invalid CIK returned");
            Assert.IsTrue(submissions.Folders.Count > 0, "List of folders is EMPTY");
        }

        [Test]
        public void SubmissionByAccessNumber_Success()
        {
            SECApi api = new SECApi();

            string cik = ConfigurationManager.AppSettings["SEC_CIK_AAPL"];
            string accessNum = ConfigurationManager.AppSettings["SEC_CIK_AAPL_SUBMISSION_FOLDER_20170701"];

            Submission submission = api.ArchivesEdgarDataCIKSubmission(cik, accessNum).Result;
            Assert.AreNotEqual(submission, null, "Submission is NULL");
            Assert.AreEqual(submission.Name, accessNum, "Invalid submission name");
            Assert.IsTrue(submission.Files.Count > 0, "List of files is EMPTY");
        }

        [Test]
        public void SubmissionByAccessNumberFileXml_Success()
        {
            SECApi api = new SECApi();

            string cik = ConfigurationManager.AppSettings["SEC_CIK_AAPL"];
            string accessNum = ConfigurationManager.AppSettings["SEC_CIK_AAPL_SUBMISSION_FOLDER_20170701"];
            string fileName = ConfigurationManager.AppSettings["SEC_CIK_AAPL_10Q_FILE_XML"];

            SubmissionFile file = api.ArchivesEdgarDataCIKSubmissionFile(cik, accessNum, fileName).Result;
            Assert.AreNotEqual(file, null, "Submission is NULL");
            Assert.AreEqual(file.Name, fileName, "Invalid file name");
            Assert.IsTrue(file.Content.Count > 0, "File content is empty");
        }

        [Test]
        public void SubmissionByAccessNumberFileZip_Success()
        {
            SECApi api = new SECApi();

            string cik = ConfigurationManager.AppSettings["SEC_CIK_AAPL"];
            string accessNum = ConfigurationManager.AppSettings["SEC_CIK_AAPL_SUBMISSION_FOLDER_20170701"];
            string fileName = ConfigurationManager.AppSettings["SEC_CIK_AAPL_10Q_FILE_ZIP"];

            SubmissionFile file = api.ArchivesEdgarDataCIKSubmissionFile(cik, accessNum, fileName).Result;
            Assert.AreNotEqual(file, null, "Submission is NULL");
            Assert.AreEqual(file.Name, fileName, "Invalid file name");
            Assert.IsTrue(file.Content.Count > 0, "File content is empty");
        }

        [Test]
        public void SubmissionByAccessNumberFile_InvalidName()
        {
            SECApi api = new SECApi();

            string cik = ConfigurationManager.AppSettings["SEC_CIK_AAPL"];
            string accessNum = ConfigurationManager.AppSettings["SEC_CIK_AAPL_SUBMISSION_FOLDER_20170701"];
            string fileName = "2FA82DB1-8BFF-4363-B947-5A3BC70AA89D.xml";

            try
            {
                SubmissionFile file = api.ArchivesEdgarDataCIKSubmissionFile(cik, accessNum, fileName).Result;
            }
            catch (System.AggregateException ex)
            {
                Assert.True(ex.InnerException.GetType().Equals(typeof(FileNotFoundException))); // exception thrown as expected
            }          
        }
    }
}
