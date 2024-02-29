using CsvHelper;
using CsvHelper.Configuration;
using MortgagePlan.Business.Interfaces;
using MortgagePlan.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MortgagePlan.Business
{
    public class ProspectFileReader : IProspectFileReader
    {
        private readonly string _filePath;
        public ProspectFileReader()
        {
            string appDataFolderPath = HttpContext.Current.Server.MapPath("~/App_Data");
            _filePath = Path.Combine(appDataFolderPath, "prospects.txt");
        }
        public IEnumerable<Prospect> ReadFile()
        {

            using (var reader = new StreamReader(_filePath))
            {
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    csv.Context.RegisterClassMap<Prospect>();

                    return csv.GetRecords<Prospect>().ToList();
                }
            }
        }
        public void WriteFile(Prospect prospects)
        {
            using (var writer = new StreamWriter(_filePath, append: true))
            {
                using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    csv.Context.RegisterClassMap<Prospect>();
                    csv.NextRecord();
                    csv.WriteRecord(prospects);
                }
            }
        }
    }
}
