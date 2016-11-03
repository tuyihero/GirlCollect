using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

using UnityEngine;

namespace Tables
{
    public partial class DropGroupRecord  : TableRecordBase
    {
        public DataRecord ValueStr;

        public override string Id { get; set; }        public string Name { get; set; }
        public string Desc { get; set; }
        public DROP_GROUP_TYPE DropGroupType { get; set; }
        public List<DropItemRecord> DropItem { get; set; }
        public List<float> DropRate { get; set; }
        public DropGroupRecord(DataRecord dataRecord)
        {
            if (dataRecord != null)
            {
                ValueStr = dataRecord;
                Id = ValueStr[0];

            }
            DropItem = new List<DropItemRecord>();
            DropRate = new List<float>();
        }
        public override string[] GetRecordStr()
        {
            List<string> recordStrList = new List<string>();
            recordStrList.Add(TableWriteBase.GetWriteStr(Id));
            recordStrList.Add(TableWriteBase.GetWriteStr(Name));
            recordStrList.Add(TableWriteBase.GetWriteStr(Desc));
            recordStrList.Add(((int)DropGroupType).ToString());
            foreach (var testTableItem in DropItem)
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
            foreach (var testTableItem in DropRate)
            {
                recordStrList.Add(TableWriteBase.GetWriteStr(testTableItem));
            }

            return recordStrList.ToArray();
        }
    }

    public partial class DropGroup : TableFileBase
    {
        public Dictionary<string, DropGroupRecord> Records { get; internal set; }

        public bool ContainsKey(string key)
        {
             return Records.ContainsKey(key);
        }

        public DropGroupRecord GetRecord(string id)
        {
            try
            {
                return Records[id];
            }
            catch (Exception ex)
            {
                throw new Exception("DropGroup" + ": " + id, ex);
            }
        }

        public DropGroup(string pathOrContent,bool isPath = true)
        {
            Records = new Dictionary<string, DropGroupRecord>();
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

                    DropGroupRecord record = new DropGroupRecord(data);
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
                pair.Value.DropGroupType =  (DROP_GROUP_TYPE)TableReadBase.ParseInt(pair.Value.ValueStr[3]);
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[4]))
                {
                    pair.Value.DropItem.Add( TableReader.DropItem.GetRecord(pair.Value.ValueStr[4]));
                }
                else
                {
                    pair.Value.DropItem.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[5]))
                {
                    pair.Value.DropItem.Add( TableReader.DropItem.GetRecord(pair.Value.ValueStr[5]));
                }
                else
                {
                    pair.Value.DropItem.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[6]))
                {
                    pair.Value.DropItem.Add( TableReader.DropItem.GetRecord(pair.Value.ValueStr[6]));
                }
                else
                {
                    pair.Value.DropItem.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[7]))
                {
                    pair.Value.DropItem.Add( TableReader.DropItem.GetRecord(pair.Value.ValueStr[7]));
                }
                else
                {
                    pair.Value.DropItem.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[8]))
                {
                    pair.Value.DropItem.Add( TableReader.DropItem.GetRecord(pair.Value.ValueStr[8]));
                }
                else
                {
                    pair.Value.DropItem.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[9]))
                {
                    pair.Value.DropItem.Add( TableReader.DropItem.GetRecord(pair.Value.ValueStr[9]));
                }
                else
                {
                    pair.Value.DropItem.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[10]))
                {
                    pair.Value.DropItem.Add( TableReader.DropItem.GetRecord(pair.Value.ValueStr[10]));
                }
                else
                {
                    pair.Value.DropItem.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[11]))
                {
                    pair.Value.DropItem.Add( TableReader.DropItem.GetRecord(pair.Value.ValueStr[11]));
                }
                else
                {
                    pair.Value.DropItem.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[12]))
                {
                    pair.Value.DropItem.Add( TableReader.DropItem.GetRecord(pair.Value.ValueStr[12]));
                }
                else
                {
                    pair.Value.DropItem.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[13]))
                {
                    pair.Value.DropItem.Add( TableReader.DropItem.GetRecord(pair.Value.ValueStr[13]));
                }
                else
                {
                    pair.Value.DropItem.Add(null);
                }
                pair.Value.DropRate.Add(TableReadBase.ParseFloat(pair.Value.ValueStr[14]));
                pair.Value.DropRate.Add(TableReadBase.ParseFloat(pair.Value.ValueStr[15]));
                pair.Value.DropRate.Add(TableReadBase.ParseFloat(pair.Value.ValueStr[16]));
                pair.Value.DropRate.Add(TableReadBase.ParseFloat(pair.Value.ValueStr[17]));
                pair.Value.DropRate.Add(TableReadBase.ParseFloat(pair.Value.ValueStr[18]));
                pair.Value.DropRate.Add(TableReadBase.ParseFloat(pair.Value.ValueStr[19]));
                pair.Value.DropRate.Add(TableReadBase.ParseFloat(pair.Value.ValueStr[20]));
                pair.Value.DropRate.Add(TableReadBase.ParseFloat(pair.Value.ValueStr[21]));
                pair.Value.DropRate.Add(TableReadBase.ParseFloat(pair.Value.ValueStr[22]));
                pair.Value.DropRate.Add(TableReadBase.ParseFloat(pair.Value.ValueStr[23]));
            }
        }
    }

}