using ShowMeTheMoney.CompanyBuilder.Models;
using ShowMeTheMoney.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using YahooFinanceApi;

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
            Database.Database.ClearCollection("Companies");
            try
            {
                GetCompaniesFromUrl(out List<string> rowResult);

                for (int i = 1; i < rowResult.Count; i++)
                {
                    var splitRows = rowResult[i].Split("<td style='text-align:");
                    var _company = new Company
                    {
                        Name = Regex.Match(splitRows[1], "<a (.*?)>(.*?)<").Groups[2].Value,
                        Symbol = Regex.Match(splitRows[2], ">(.*?)</").Groups[1].Value,
                        Currency = Regex.Match(splitRows[3], ">(.*?)</").Groups[1].Value,
                        ISIN = Regex.Match(splitRows[4], ">(.*?)</").Groups[1].Value,
                        Sector = Regex.Match(splitRows[5], "title='(.*?)'").Groups[1].Value,
                        ICBCode = Regex.Match(splitRows[6], ">(.*?)</").Groups[1].Value,
                        FactSheet = Regex.Match(splitRows[7], "<a href='(.*?)'").Groups[1].Value,
                    };
                    Logger.WriteLine($"Getting company information for company {_company.Name}");
                    var priceData = Yahoo.Symbols(_company.Symbol.Trim().Replace(" ", "-") + ".ST").Fields(
                        Field.Ask,
                        Field.FiftyTwoWeekHigh,
                        Field.FiftyTwoWeekLow,
                        Field.FiftyDayAverage).QueryAsync();

                    PriceData priceResult;
                    try
                    {
                        priceResult = priceData.Result.Values.Select(x => new PriceData
                        {
                            Ask = x.Ask,
                            FiftyTwoWeekHigh = x.FiftyTwoWeekHigh,
                            FiftyTwoWeekLow = x.FiftyTwoWeekLow,
                            FiftyDayAverage = x.FiftyDayAverage,
                            TrailingPe = 0
                        }).First();
                    }
                    catch (Exception ex)
                    {
                        priceResult = new PriceData();
                    }

                    _company.PriceData = priceResult;
                    Logger.WriteLine($"Getting price data for company {_company.Name}");
                    Database.Database.Create("Companies", _company);
                }

                Logger.WriteLine("Companies Saved to database");
            }
            catch (Exception ex)
            {
                Logger.WriteLine($"Not able to parse company information {ex}");
            }
        }
        /// <summary>
        /// Get Company data from url nasdaqomxnordic
        /// </summary>
        /// <param name="rowResult"></param>
        internal static void GetCompaniesFromUrl(out List<string> rowResult)
        {
            HttpWebResponse response;
            Stream stream;
            StreamReader reader;
            string html;
            var request =
                (HttpWebRequest)WebRequest.Create("http://www.nasdaqomxnordic.com/aktier/listed-companies/stockholm");
            request.AutomaticDecompression = DecompressionMethods.GZip;
            response = (HttpWebResponse)request.GetResponse();
            stream = response.GetResponseStream();
            reader = new StreamReader(stream);
            html = reader.ReadToEnd();

            int From = html.IndexOf("<table");
            int To = html.LastIndexOf("</table");
            var result = html[From..To];
            result = result[result.IndexOf("<thead>")..result.Length];
            rowResult = new List<string>();
            foreach (var row in (string[])result.Split("</tr>"))
            {
                int rowFrom = row.IndexOf("<tr>");
                if (rowFrom == -1) continue;
                rowResult.Add(row[rowFrom..row.Length]);
            }
            Logger.WriteLine($"Found {rowResult.Count - 1} companies");
        }
    }
}