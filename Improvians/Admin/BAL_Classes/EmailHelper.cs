using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Admin.BAL_Classes
{
    public class EmailHelper
    {
        public string getEmailBody(string fileName, Dictionary<string, string> maildictionary)
        {
            string emailBody;

            string filePath;

            filePath = fileName;

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                emailBody = streamReader.ReadToEnd();
            }

            foreach (KeyValuePair<string, string> pair in maildictionary)
            {
                emailBody = emailBody.Replace(pair.Key, pair.Value);
            }

            return emailBody;
        }
    }
}