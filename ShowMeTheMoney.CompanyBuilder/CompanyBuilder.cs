using ShowMeTheMoney.StockAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ShowMeTheMoney.CompanyBuilder
{
    /// <summary>
    /// Class used for getting companies on the swedish stock
    /// exchange to later use the for parsing sotck candles.
    /// </summary>
    public static class CompanyBuilder
    {
        /// <summary>
        /// Get copmanies form nasdaqomnordic
        /// </summary>
        public static void GetCompanies()
        {
            Logger.WriteLine("Fetching companies from Nasdaq Omx Nordig");
            try
            {
                string html;
                var request =
                    (HttpWebRequest)WebRequest.Create("http://www.nasdaqomxnordic.com/aktier/listed-companies/stockholm");
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using var response = (HttpWebResponse)request.GetResponse();
                using var stream = response.GetResponseStream();
                using var reader = new StreamReader(stream);
                html = reader.ReadToEnd();

                int From = html.IndexOf("<table");
                int To = html.LastIndexOf("</table");
                var result = html[From..To];
                result = result[result.IndexOf("<thead>")..result.Length];
                var rowResult = new List<string>();
                foreach (var row in (string[])result.Split("</tr>"))
                {
                    int rowFrom = row.IndexOf("<tr>");
                    if (rowFrom == -1) continue;
                    rowResult.Add(row[rowFrom..row.Length]);
                }
                Logger.WriteLine($"Found {rowResult.Count} companies");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Logger.WriteLine($"Not able to parse company information {ex}");
            }
            

           
        }
    }
}

/*
 *             <th style="text-align:left;" class="headerLeft header" title="Name">Name</th>
            <th style="text-align:left;" class="headerLeft header" title="Symbol">Symbol</th>
            <th style="text-align:left;" class="headerLeft header" title="Currency">Currency</th>
            <th style="text-align:left;" class="headerLeft header" title="ISIN">ISIN</th>
            <th style="text-align:left;" class="headerLeft header" title="Sector">Sector</th>
            <th style="text-align:left;" class="headerLeft header" title="Sector Code">ICB Code</th>
            <th style="text-align:left;" class="headerLeft header" title="Fact Sheet">Fact Sheet</th>
*/