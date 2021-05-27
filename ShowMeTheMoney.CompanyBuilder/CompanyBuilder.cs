using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using ShowMeTheMoney.CompanyBuilder.Models;
using ShowMeTheMoney.Core;
using YahooFinanceApi;

namespace ShowMeTheMoney.CompanyBuilder
{
    /// <summary>
    ///     Class used for getting companies on the swedish stock
    ///     exchange to later use the for parsing stock candles.
    /// </summary>
    public static class CompanyBuilder
    {
        /// <summary>
        ///     Get companies form nasdaqomnordic
        /// </summary>
        public static void GetCompanies()
        {
            Logger.WriteLine("Fetching companies from Nasdaq Omx Nordic");
            Database.Database.ClearCollection("Companies");
            try
            {
                GetCompaniesFromUrl(out var rowResult);

                for (var i = 1; i < rowResult.Count; i++)
                {
                    var splitRows = rowResult[i].Split("<td style='text-align:");
                    var company = new Company
                    {
                        Name = Regex.Match(splitRows[1], "<a (.*?)>(.*?)<").Groups[2].Value,
                        Symbol = Regex.Match(splitRows[2], ">(.*?)</").Groups[1].Value,
                        Currency = Regex.Match(splitRows[3], ">(.*?)</").Groups[1].Value,
                        Isin = Regex.Match(splitRows[4], ">(.*?)</").Groups[1].Value,
                        Sector = Regex.Match(splitRows[5], "title='(.*?)'").Groups[1].Value,
                        IcbCode = Regex.Match(splitRows[6], ">(.*?)</").Groups[1].Value,
                        FactSheet = Regex.Match(splitRows[7], "<a href='(.*?)'").Groups[1].Value
                    };
                    Logger.WriteLine($"Getting company information for company {company.Name}");
                    var priceData = Yahoo.Symbols(company.Symbol.Trim().Replace(" ", "-") + ".ST").Fields(
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
                    catch (Exception)
                    {
                        priceResult = new PriceData();
                    }

                    company.PriceData = priceResult;
                    Logger.WriteLine($"Getting price data for company {company.Name}");
                    Database.Database.Create("Companies", company);
                }

                Logger.WriteLine("Companies Saved to database");
            }
            catch (Exception ex)
            {
                Logger.WriteLine($"Not able to parse company information {ex}");
            }
        }

        /// <summary>
        ///     Get Company data from url nasdaqomxnordic
        /// </summary>
        /// <param name="rowResult"></param>
        internal static void GetCompaniesFromUrl(out List<string> rowResult)
        {
            var request =
                (HttpWebRequest) WebRequest.Create("http://www.nasdaqomxnordic.com/aktier/listed-companies/stockholm");
            request.AutomaticDecompression = DecompressionMethods.GZip;
            var response = (HttpWebResponse) request.GetResponse();
            var stream = response.GetResponseStream();
            var reader = new StreamReader(stream);
            var html = reader.ReadToEnd();

            var from = html.IndexOf("<table", StringComparison.Ordinal);
            var to = html.LastIndexOf("</table", StringComparison.Ordinal);
            var result = html[from..to];
            result = result[result.IndexOf("<thead>", StringComparison.Ordinal)..result.Length];
            rowResult = (from row in (string[]) result.Split("</tr>")
                let rowFrom = row.IndexOf("<tr>", StringComparison.Ordinal)
                where rowFrom != -1
                select row[rowFrom..row.Length]).ToList();
            Logger.WriteLine($"Found {rowResult.Count - 1} companies");
        }
    }
}