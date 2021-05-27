using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ShowMeTheMoney.Helpers
{
    /// <summary>
    ///     Class for retrieving a company logo
    /// </summary>
    internal static class GetCompanyLogo
    {
        internal static string Get(string keyword)
        {
            var remoteUri = "https://www.bing.com/images/search?q=" + keyword +
                            " logotyp&form=HDRSC3&first=1&tsc=ImageBasicHover";

            var request = (HttpWebRequest) WebRequest.Create(remoteUri);
            var response = (HttpWebResponse) request.GetResponse();

            string data;
            using (var dataStream = response.GetResponseStream())
            {
                using var sr = new StreamReader(dataStream);
                data = sr.ReadToEnd();
            }

            var urls = new List<string>();
            var ndx = data.IndexOf("class=\"img_cont hoff\"", StringComparison.Ordinal);

            if (ndx <= 0) return null;
            ndx = data.IndexOf("<img", ndx, StringComparison.Ordinal);

            while (ndx >= 0)
            {
                ndx = data.IndexOf("src=\"", ndx, StringComparison.Ordinal);
                ndx += 5;
                var ndx2 = data.IndexOf("\"", ndx, StringComparison.Ordinal);
                remoteUri = data[ndx..ndx2];
                urls.Add(remoteUri);
                break;
            }

            return urls[0];
        }
    }
}