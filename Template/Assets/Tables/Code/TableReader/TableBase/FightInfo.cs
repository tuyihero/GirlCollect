using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

using UnityEngine;

namespace Tables
{
    public partial class FightInfoRecord  : TableRecordBase
    {
        public DataRecord ValueStr;

        public override string Id { get; set; }        public string Name { get; set; }
        public string Desc { get; set; }
        public int VitalityDec { get; set; }
        public int MoodDec { get; set; }
        public List<MultiTable> ObjPlan { get; set; }
        public FightInfoRecord(DataRecord dataRecord)
        {
            if (dataRecord != null)
            {
                ValueStr = dataRecord;
                Id = ValueStr[0];

            }
            ObjPlan = new List<MultiTable>();
        }
        public override string[] GetRecordStr()
        {
            List<string> recordStrList = new List<string>();
            recordStrList.Add(TableWriteBase.GetWriteStr(Id));
            recordStrList.Add(TableWriteBase.GetWriteStr(Name));
            recordStrList.Add(TableWriteBase.GetWriteStr(Desc));
            recordStrList.Add(TableWriteBase.GetWriteStr(VitalityDec));
            recordStrList.Add(TableWriteBase.GetWriteStr(MoodDec));
            foreach (var testTableItem in ObjPlan)
            {
                recordStrList.Add(TableWriteBase.GetWriteStr(testTableItem));
            }

            return recordStrList.ToArray();
        }
    }

    public partial class FightInfo : TableFileBase
    {
        public Dictionary<string, FightInfoRecord> Records { get; internal set; }

        public bool ContainsKey(string key)
        {
             return Records.ContainsKey(key);
        }

        public FightInfoRecord GetRecord(string id)
        {
            try
            {
                return Records[id];
            }
            catch (Exception ex)
            {
                throw new Exception("FightInfo" + ": " + id, ex);
            }
        }

        public FightInfo(string pathOrContent,bool isPath = true)
        {
            Records = new Dictionary<string, FightInfoRecord>();
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

                    FightInfoRecord record = new FightInfoRecord(data);
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
                pair.Value.VitalityDec = TableReadBase.ParseInt(pair.Value.ValueStr[3]);
                pair.Value.MoodDec = TableReadBase.ParseInt(pair.Value.ValueStr[4]);
                if(!string.IsNullOrEmpty(pair.Value.ValueStr[5]))
                {
                   pair.Value.ObjPlan.Add( TableReadBase.ParseMultiTable(pair.Value.ValueStr[5]));
                }
                if(!string.IsNullOrEmpty(pair.Value.ValueStr[6]))
                {
                   pair.Value.ObjPlan.Add( TableReadBase.ParseMultiTable(pair.Value.ValueStr[6]));
                }
                if(!string.IsNullOrEmpty(pair.Value.ValueStr[7]))
                {
                   pair.Value.ObjPlan.Add( TableReadBase.ParseMultiTable(pair.Value.ValueStr[7]));
                }
                if(!string.IsNullOrEmpty(pair.Value.ValueStr[8]))
                {
                   pair.Value.ObjPlan.Add( TableReadBase.ParseMultiTable(pair.Value.ValueStr[8]));
                }
                if(!string.IsNullOrEmpty(pair.Value.ValueStr[9]))
                {
                   pair.Value.ObjPlan.Add( TableReadBase.ParseMultiTable(pair.Value.ValueStr[9]));
                }
                if(!string.IsNullOrEmpty(pair.Value.ValueStr[10]))
                {
                   pair.Value.ObjPlan.Add( TableReadBase.ParseMultiTable(pair.Value.ValueStr[10]));
                }
                if(!string.IsNullOrEmpty(pair.Value.ValueStr[11]))
                {
                   pair.Value.ObjPlan.Add( TableReadBase.ParseMultiTable(pair.Value.ValueStr[11]));
                }
                if(!string.IsNullOrEmpty(pair.Value.ValueStr[12]))
                {
                   pair.Value.ObjPlan.Add( TableReadBase.ParseMultiTable(pair.Value.ValueStr[12]));
                }
                if(!string.IsNullOrEmpty(pair.Value.ValueStr[13]))
                {
                   pair.Value.ObjPlan.Add( TableReadBase.ParseMultiTable(pair.Value.ValueStr[13]));
                }
                if(!string.IsNullOrEmpty(pair.Value.ValueStr[14]))
                {
                   pair.Value.ObjPlan.Add( TableReadBase.ParseMultiTable(pair.Value.ValueStr[14]));
                }
            }
        }
    }

}