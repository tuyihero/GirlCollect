using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

using UnityEngine;

namespace Tables
{
    public partial class DropItem_CurrencyRecord  : TableRecordBase
    {
        public DataRecord ValueStr;

        public override string Id { get; set; }        public string Name { get; set; }
        public string Desc { get; set; }
        public CURRENCY_TYPE CurrencyType { get; set; }
        public int CurrencyValueMin { get; set; }
        public int CurrencyValueMax { get; set; }
        public DropItem_CurrencyRecord(DataRecord dataRecord)
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
            recordStrList.Add(((int)CurrencyType).ToString());
            recordStrList.Add(TableWriteBase.GetWriteStr(CurrencyValueMin));
            recordStrList.Add(TableWriteBase.GetWriteStr(CurrencyValueMax));

            return recordStrList.ToArray();
        }
    }

    public partial class DropItem_Currency : TableFileBase
    {
        public Dictionary<string, DropItem_CurrencyRecord> Records { get; internal set; }

        public bool ContainsKey(string key)
        {
             return Records.ContainsKey(key);
        }

        public DropItem_CurrencyRecord GetRecord(string id)
        {
            try
            {
                return Records[id];
            }
            catch (Exception ex)
            {
                throw new Exception("DropItem_Currency" + ": " + id, ex);
            }
        }

        public DropItem_Currency(string pathOrContent,bool isPath = true)
        {
            Records = new Dictionary<string, DropItem_CurrencyRecord>();
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

                    DropItem_CurrencyRecord record = new DropItem_CurrencyRecord(data);
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
                pair.Value.CurrencyType =  (CURRENCY_TYPE)TableReadBase.ParseInt(pair.Value.ValueStr[3]);
                pair.Value.CurrencyValueMin = TableReadBase.ParseInt(pair.Value.ValueStr[4]);
                pair.Value.CurrencyValueMax = TableReadBase.ParseInt(pair.Value.ValueStr[5]);
            }
        }
    }

}