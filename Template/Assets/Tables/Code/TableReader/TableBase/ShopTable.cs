﻿using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

using UnityEngine;

namespace Tables
{
    public partial class ShopTableRecord  : TableRecordBase
    {
        public DataRecord ValueStr;

        public override string Id { get; set; }        public string Name { get; set; }
        public string Desc { get; set; }
        public List<ShopItemRecord> ShopItems { get; set; }
        public ShopTableRecord(DataRecord dataRecord)
        {
            if (dataRecord != null)
            {
                ValueStr = dataRecord;
                Id = ValueStr[0];

            }
            ShopItems = new List<ShopItemRecord>();
        }
        public override string[] GetRecordStr()
        {
            List<string> recordStrList = new List<string>();
            recordStrList.Add(TableWriteBase.GetWriteStr(Id));
            recordStrList.Add(TableWriteBase.GetWriteStr(Name));
            recordStrList.Add(TableWriteBase.GetWriteStr(Desc));
            foreach (var testTableItem in ShopItems)
            {
                if (testTableItem != null)
                {
                    recordStrList.Add(testTableItem.Id);
                }
                else
                {
                    recordStrList.Add("");
                }
            }

            return recordStrList.ToArray();
        }
    }

    public partial class ShopTable : TableFileBase
    {
        public Dictionary<string, ShopTableRecord> Records { get; internal set; }

        public bool ContainsKey(string key)
        {
             return Records.ContainsKey(key);
        }

        public ShopTableRecord GetRecord(string id)
        {
            try
            {
                return Records[id];
            }
            catch (Exception ex)
            {
                throw new Exception("ShopTable" + ": " + id, ex);
            }
        }

        public ShopTable(string pathOrContent,bool isPath = true)
        {
            Records = new Dictionary<string, ShopTableRecord>();
            if(isPath)
            {
                string[] lines = File.ReadAllLines(pathOrContent);
                lines[0] = lines[0].Replace("\r\n", "\n");
                ParserTableStr(string.Join("\n", lines));
            }
            else
            {
                ParserTableStr(pathOrContent.Replace("\r\n", "\n"));
            }
        }

        private void ParserTableStr(string content)
        {
            StringReader rdr = new StringReader(content);
            using (var reader = new CsvReader(rdr))
            {
                HeaderRecord header = reader.ReadHeaderRecord();
                while (reader.HasMoreRecords)
                {
                    DataRecord data = reader.ReadDataRecord();
                    if (data[0].StartsWith("#"))
                        continue;

                    ShopTableRecord record = new ShopTableRecord(data);
                    Records.Add(record.Id, record);
                }
            }
        }

        public void CoverTableContent()
        {
            foreach (var pair in Records)
            {
                pair.Value.Name = TableReadBase.ParseString(pair.Value.ValueStr[1]);
                pair.Value.Desc = TableReadBase.ParseString(pair.Value.ValueStr[2]);
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[3]))
                {
                    pair.Value.ShopItems.Add( TableReader.ShopItem.GetRecord(pair.Value.ValueStr[3]));
                }
                else
                {
                    pair.Value.ShopItems.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[4]))
                {
                    pair.Value.ShopItems.Add( TableReader.ShopItem.GetRecord(pair.Value.ValueStr[4]));
                }
                else
                {
                    pair.Value.ShopItems.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[5]))
                {
                    pair.Value.ShopItems.Add( TableReader.ShopItem.GetRecord(pair.Value.ValueStr[5]));
                }
                else
                {
                    pair.Value.ShopItems.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[6]))
                {
                    pair.Value.ShopItems.Add( TableReader.ShopItem.GetRecord(pair.Value.ValueStr[6]));
                }
                else
                {
                    pair.Value.ShopItems.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[7]))
                {
                    pair.Value.ShopItems.Add( TableReader.ShopItem.GetRecord(pair.Value.ValueStr[7]));
                }
                else
                {
                    pair.Value.ShopItems.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[8]))
                {
                    pair.Value.ShopItems.Add( TableReader.ShopItem.GetRecord(pair.Value.ValueStr[8]));
                }
                else
                {
                    pair.Value.ShopItems.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[9]))
                {
                    pair.Value.ShopItems.Add( TableReader.ShopItem.GetRecord(pair.Value.ValueStr[9]));
                }
                else
                {
                    pair.Value.ShopItems.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[10]))
                {
                    pair.Value.ShopItems.Add( TableReader.ShopItem.GetRecord(pair.Value.ValueStr[10]));
                }
                else
                {
                    pair.Value.ShopItems.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[11]))
                {
                    pair.Value.ShopItems.Add( TableReader.ShopItem.GetRecord(pair.Value.ValueStr[11]));
                }
                else
                {
                    pair.Value.ShopItems.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[12]))
                {
                    pair.Value.ShopItems.Add( TableReader.ShopItem.GetRecord(pair.Value.ValueStr[12]));
                }
                else
                {
                    pair.Value.ShopItems.Add(null);
                }
            }
        }
    }

}