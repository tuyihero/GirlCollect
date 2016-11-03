using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

using UnityEngine;

namespace Tables
{
    public partial class LotteryRecord  : TableRecordBase
    {
        public DataRecord ValueStr;

        public override string Id { get; set; }        public string Name { get; set; }
        public string Desc { get; set; }
        public CURRENCY_TYPE CostType { get; set; }
        public List<int> CostValue { get; set; }
        public List<DropGroupRecord> DropGroup { get; set; }
        public LotteryRecord(DataRecord dataRecord)
        {
            if (dataRecord != null)
            {
                ValueStr = dataRecord;
                Id = ValueStr[0];

            }
            CostValue = new List<int>();
            DropGroup = new List<DropGroupRecord>();
        }
        public override string[] GetRecordStr()
        {
            List<string> recordStrList = new List<string>();
            recordStrList.Add(TableWriteBase.GetWriteStr(Id));
            recordStrList.Add(TableWriteBase.GetWriteStr(Name));
            recordStrList.Add(TableWriteBase.GetWriteStr(Desc));
            recordStrList.Add(((int)CostType).ToString());
            foreach (var testTableItem in CostValue)
            {
                recordStrList.Add(TableWriteBase.GetWriteStr(testTableItem));
            }
            foreach (var testTableItem in DropGroup)
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

    public partial class Lottery : TableFileBase
    {
        public Dictionary<string, LotteryRecord> Records { get; internal set; }

        public bool ContainsKey(string key)
        {
             return Records.ContainsKey(key);
        }

        public LotteryRecord GetRecord(string id)
        {
            try
            {
                return Records[id];
            }
            catch (Exception ex)
            {
                throw new Exception("Lottery" + ": " + id, ex);
            }
        }

        public Lottery(string pathOrContent,bool isPath = true)
        {
            Records = new Dictionary<string, LotteryRecord>();
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

                    LotteryRecord record = new LotteryRecord(data);
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
                pair.Value.CostType =  (CURRENCY_TYPE)TableReadBase.ParseInt(pair.Value.ValueStr[3]);
                pair.Value.CostValue.Add(TableReadBase.ParseInt(pair.Value.ValueStr[4]));
                pair.Value.CostValue.Add(TableReadBase.ParseInt(pair.Value.ValueStr[5]));
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[6]))
                {
                    pair.Value.DropGroup.Add( TableReader.DropGroup.GetRecord(pair.Value.ValueStr[6]));
                }
                else
                {
                    pair.Value.DropGroup.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[7]))
                {
                    pair.Value.DropGroup.Add( TableReader.DropGroup.GetRecord(pair.Value.ValueStr[7]));
                }
                else
                {
                    pair.Value.DropGroup.Add(null);
                }
            }
        }
    }

}