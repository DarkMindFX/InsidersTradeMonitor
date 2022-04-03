
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace ITM.SEC.Api
{
    public class SECApi
    {
        private static string BaseURL = "https://www.sec.gov";

        #region Json responses

        [DataContract]
        class ArchivesEdgarDirectoryItem
        {
            [DataMember(Name = "name")]
            public string name
            {
                get;
                set;
            }

            [DataMember(Name = "last-modified")]
            public string last_modified
            {
                get;
                set;
            }

            [DataMember(Name = "type")]
            public string type
            {
                get;
                set;
            }

            [DataMember(Name = "size")]
            public string size
            {
                get;
                set;
            }
        }

        [DataContract]
        class ArchivesEdgarDirectory
        {
            [DataMember(Name = "item")]
            public List<ArchivesEdgarDirectoryItem> item
            {
                get;
                set;
            }

            [DataMember(Name = "name")]
            public string name
            {
                get;
                set;
            }

            [DataMember(Name = "parent-dir")]
            public string parent_dir
            {
                get;
                set;
            }
        }

        [DataContract]
        class ArchivesEdgarDataCIKResponse
        {
            [DataMember(Name = "directory")]
            public ArchivesEdgarDirectory directory
            {
                get; set;
            }
        }

        [DataContract]
        class ArchivesEdgarDataCIKSubmissionResponse
        {
            [DataMember(Name = "directory")]
            public ArchivesEdgarDirectory directory
            {
                get; set;
            }
        }
        #endregion

        private static DateTime _LastCall = DateTime.UtcNow;
        private static object _lock = new object();

        // Call: /Archives/edgar/data/<CIK>
        public async Task<Submissions> ArchivesEdgarDataCIK(string cik)
        {
            AvoidBlocking();

            string Command = "{0}/Archives/edgar/data/{1}/index.json";

            Submissions submissions = null;

            using (var client = CreateClient())
            {
                string request = string.Format(Command, BaseURL, cik);

                using (var response = await client.GetAsync(request))
                {
                    ArchivesEdgarDataCIKResponse model = await this.ConvertResponse<ArchivesEdgarDataCIKResponse>(response);

                    submissions = Convert(model);
                    if (submissions != null)
                    {
                        submissions.CIK = cik;
                        submissions.TimeStamp = DateTime.UtcNow;
                    }
                }
            }

            return submissions;
        }

        // Call: /Archives/edgar/data/<CIK>/<Submission Access Number>
        public async Task<Submission> ArchivesEdgarDataCIKSubmission(string cik, string accessNumber)
        {
            AvoidBlocking();

            string Command = "{0}/Archives/edgar/data/{1}/{2}/index.json";

            Submission submission = null;

            using (var client = CreateClient())
            {
                string request = string.Format(Command, BaseURL, cik, accessNumber);

                using (var response = await client.GetAsync(request))
                {
                    ArchivesEdgarDataCIKSubmissionResponse model = await this.ConvertResponse<ArchivesEdgarDataCIKSubmissionResponse>(response);

                    submission = Convert(model);
                    if (submission != null)
                    {
                        submission.Name = accessNumber;
                    }
                }
            }

            return submission;
        }

        // Call: /Archives/edgar/data/<CIK>/<Submission Access Number>/<File Name>
        public async Task<SubmissionFile> ArchivesEdgarDataCIKSubmissionFile(string cik, string accessNumber, string fileName)
        {
            AvoidBlocking();

            string Command = "{0}/Archives/edgar/data/{1}/{2}/{3}";

            SubmissionFile submission = null;

            using (var client = CreateClient())
            {
                string request = string.Format(Command, BaseURL, cik, accessNumber, fileName);

                using (var response = await client.GetAsync(request))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        byte[] fileContent = Encoding.ASCII.GetBytes(response.Content.ReadAsStringAsync().Result);

                        submission = Convert(fileName, fileContent);
                    }
                    else
                    {
                        throw new FileNotFoundException($"Submission file {fileName} not found");
                    }
                }

            }

            return submission;
        }

        #region Support methods

        /// <summary>
        /// There is a limitation in SEC APi: there can be only 10 requests per second from single client. 
        /// This function records the time of last call and if delta is less then 0.1 sec - performs the delay to avoid blocking
        /// </summary>
        private static void AvoidBlocking()
        {
            lock (_lock)
            {
                Thread.Sleep(150);
            }
        }

        private Submissions Convert(ArchivesEdgarDataCIKResponse model)
        {
            Submissions submissions = new Submissions();

            foreach (var item in model.directory.item)
            {
                SubmissionFolderInfo folder = new SubmissionFolderInfo(item.name, !string.IsNullOrEmpty(item.last_modified) ? DateTime.Parse(item.last_modified) : DateTime.MinValue);
                submissions.Folders.Add(folder);
            }

            return submissions;
        }

        private Submission Convert(ArchivesEdgarDataCIKSubmissionResponse model)
        {
            Submission submission = new Submission(model.directory.name, DateTime.MinValue);

            foreach (var item in model.directory.item)
            {
                SubmissionFileInfo folder = new SubmissionFileInfo(item.name, !string.IsNullOrEmpty(item.last_modified) ? DateTime.Parse(item.last_modified) : DateTime.MinValue, !string.IsNullOrEmpty(item.size) ? UInt32.Parse(item.size) : 0);
                submission.Files.Add(folder);
            }

            return submission;
        }

        private SubmissionFile Convert(string fileName, byte[] fileContent)
        {
            SubmissionFile file = new SubmissionFile(fileName);

            file.Content.AddRange(fileContent);

            return file;
        }

        private async Task<T> ConvertResponse<T>(HttpResponseMessage response)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();

            T obj = JsonConvert.DeserializeObject<T>(apiResponse);

            return obj;

        }

        private HttpClient CreateClient()
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders
              .Accept
              .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("User-Agent", "InsidersTraderMinotor.v.1.0");

            return client;
        }
        #endregion

    }
}
