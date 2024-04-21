using System;
using System.Configuration;
using System.Text;
using System.Text.Json;

namespace ITM.Misc.EntitySECSeedDataGenerator
{
    internal class Program
    {
        static void DumpToOutput(string outputPath, Dictionary<string, SECCompanyTicker> tickers)
        {
            using (FileStream fs = new FileStream(outputPath, FileMode.Create))
            {
                StreamWriter sw = new StreamWriter(fs, encoding: System.Text.Encoding.UTF8, bufferSize: 2000000);
                sw.AutoFlush = true;
                string header = "ID,EntityTypeID,CIK,Name,TradingSymbol,IsMonitored";

                sw.WriteLine(header);

                StringBuilder content = new StringBuilder();

                int id = 1;
                foreach (var k in tickers.Keys)
                {
                    var v = tickers[k];
                    content.AppendLine($"{id},1,{v.CIK},{v.Name.Replace(",", " ")},{v.TradingSymbol},0");
                    ++id;                  
                }
                sw.Write(content.ToString());

                
            }

        }

        static void Main(string[] args)
        {
            string path = ConfigurationManager.AppSettings["SECjson"];
            string outPath = ConfigurationManager.AppSettings["OutputPath"];

            string content = File.ReadAllText(path);

            var tickers = JsonSerializer.Deserialize<Dictionary<string, SECCompanyTicker>>(content);

            DumpToOutput(outPath, tickers);
        }
    }
}
