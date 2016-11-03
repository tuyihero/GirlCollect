using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

using UnityEngine;

namespace Tables
{
    public partial class ShopItemRecord  : TableRecordBase
    {
        public DataRecord ValueStr;

        public override string Id { get; set; }        public string Name { get; set; }
        public string Desc { get; set; }
        public ItemInfoRecord ItemInfo { get; set; }
        public int ItemNum { get; set; }
        public CURRENCY_TYPE CostType { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public ShopItemRecord(DataRecord dataRecord)
        {
            if (dataRecord != null)
            {
                ValueStr = dataRecord;
                Id = ValueStr[0];

            }
        }
        public override string[] GetRecordStr()
        {
            List<string> recordStrList = new List<string>();
            recordStrList.Add(TableWriteBase.GetWriteStr(Id));
            recordStrList.Add(TableWriteBase.GetWriteStr(Name));
            recordStrList.Add(TableWriteBase.GetWriteStr(Desc));
            if (ItemInfo != null)
            {
                recordStrList.Add(ItemInfo.Id);
            }
            else
            {
                recordStrList.Add("");
            }
            recordStrList.Add(TableWriteBase.GetWriteStr(ItemNum));
            recordStrList.Add(((int)CostType).ToString());
            recordStrList.Add(TableWriteBase.GetWriteStr(Price));
            recordStrList.Add(TableWriteBase.GetWriteStr(Discount));

            return recordStrList.ToArray();
        }
    }

    public partial class ShopItem : TableFileBase
    {
        public Dictionary<string, ShopItemRecord> Records { get; internal set; }

        public bool ContainsKey(string key)
        {
             return Records.ContainsKey(key);
        }

        public ShopItemRecord GetRecord(string id)
        {
            try
            {
                return Records[id];
            }
            catch (Exception ex)
            {
                throw new Exception("ShopItem" + ": " + id, ex);
            }
        }

        public ShopItem(string pathOrContent,bool isPath = true)
        {
            Records = new Dictionary<string, ShopItemRecord>();
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

                    ShopItemRecord record = new ShopItemRecord(data);
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
                    pair.Value.ItemInfo =  TableReader.ItemInfo.GetRecord(pair.Value.ValueStr[3]);
                }
                else
                {
                    pair.Value.ItemInfo = null;
                }
                pair.Value.ItemNum = TableReadBase.ParseInt(pair.Value.ValueStr[4]);
                pair.Value.CostType =  (CURRENCY_TYPE)TableReadBase.ParseInt(pair.Value.ValueStr[5]);
                pair.Value.Price = TableReadBase.ParseInt(pair.Value.ValueStr[6]);
                pair.Value.Discount = TableReadBase.ParseInt(pair.Value.ValueStr[7]);
            }
        }
    }

}