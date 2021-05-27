using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ShowMeTheMoney.Database
{
    public static class Database
    {
        /// <summary>
        ///     Create a database file and populate it with data
        ///     To split the handling of data I json serialize if before insert
        ///     This method does only allow one record at a time.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static void Create<T>(string collection, T data)
        {
            var dataList = new List<T>();
            string jsonData;
            var id = 0;
            if (File.Exists(collection))
            {
                jsonData = File.ReadAllText(collection);
                dataList.AddRange(JsonConvert.DeserializeObject<List<T>>(jsonData) ??
                                  throw new InvalidOperationException());
                try
                {
                    id = (int) dataList.Select(row =>
                        row.GetType().GetProperty("Id")?.GetValue(row, null)).LastOrDefault() + 1;
                }
                catch (NullReferenceException)
                {
                    id = 0;
                }
            }

            JsonConvert.PopulateObject("{'Id': " + id + "}", data);
            dataList.Add(data);
            jsonData = JsonConvert.SerializeObject(dataList);
            File.WriteAllText(collection, jsonData);
        }

        ///// <summary>
        ///// Get data from the database
        ///// </summary>
        ///// <param name="collection"></param>
        ///// <param name="data"></param>
        public static List<T> Get<T>(string collection)
        {
            var dataList = new List<T>();
            if (!File.Exists(collection)) return new List<T>();
            var jsonData = File.ReadAllText(collection);
            dataList.AddRange(JsonConvert.DeserializeObject<List<T>>(jsonData) ??
                              throw new InvalidOperationException());
            return dataList;
        }

        ///// <summary>
        ///// Get Item based on symbol
        ///// </summary>
        ///// <param name="collection"></param>
        ///// <param name="itemID"></param>
        public static T GetItem<T>(string collection, string symbol)
        {
            var dataList = new List<T>();
            if (!File.Exists(collection)) return default;
            var jsonData = File.ReadAllText(collection);
            dataList.AddRange(JsonConvert.DeserializeObject<List<T>>(jsonData) ??
                              throw new InvalidOperationException());
            return dataList.FirstOrDefault(row =>
                (string) row.GetType().GetProperty("Symbol")?.GetValue(row, null) == symbol);
        }

        public static void ClearCollection(string collection)
        {
            if (File.Exists(collection))
                File.Delete(collection);
        }

        ///// <summary>
        ///// Update data in database
        ///// </summary>
        ///// <param name="collection"></param>
        ///// <param name="data"></param>
        //public Task Update(string collection, object data)
        //{
        //    return Task.CompletedTask;
        //}
    }
}