using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

using UnityEngine;

namespace Tables
{
    public partial class MissionInfoRecord  : TableRecordBase
    {
        public DataRecord ValueStr;

        public override string Id { get; set; }        public string Name { get; set; }
        public string Desc { get; set; }
        public MISSION_TYPE MissionType { get; set; }
        public string Series { get; set; }
        public int MissionRewardGold { get; set; }
        public int MissionRewardDiamond { get; set; }
        public List<MultiTable> FinishCheck { get; set; }
        public int FailCheck { get; set; }
        public string GoToUI { get; set; }
        public string IconName { get; set; }
        public MissionInfoRecord(DataRecord dataRecord)
        {
            if (dataRecord != null)
            {
                ValueStr = dataRecord;
                Id = ValueStr[0];

            }
            FinishCheck = new List<MultiTable>();
        }
        public override string[] GetRecordStr()
        {
            List<string> recordStrList = new List<string>();
            recordStrList.Add(TableWriteBase.GetWriteStr(Id));
            recordStrList.Add(TableWriteBase.GetWriteStr(Name));
            recordStrList.Add(TableWriteBase.GetWriteStr(Desc));
            recordStrList.Add(((int)MissionType).ToString());
            recordStrList.Add(TableWriteBase.GetWriteStr(Series));
            recordStrList.Add(TableWriteBase.GetWriteStr(MissionRewardGold));
            recordStrList.Add(TableWriteBase.GetWriteStr(MissionRewardDiamond));
            foreach (var testTableItem in FinishCheck)
            {
                recordStrList.Add(TableWriteBase.GetWriteStr(testTableItem));
            }
            recordStrList.Add(TableWriteBase.GetWriteStr(FailCheck));
            recordStrList.Add(TableWriteBase.GetWriteStr(GoToUI));
            recordStrList.Add(TableWriteBase.GetWriteStr(IconName));

            return recordStrList.ToArray();
        }
    }

    public partial class MissionInfo : TableFileBase
    {
        public Dictionary<string, MissionInfoRecord> Records { get; internal set; }

        public bool ContainsKey(string key)
        {
             return Records.ContainsKey(key);
        }

        public MissionInfoRecord GetRecord(string id)
        {
            try
            {
                return Records[id];
            }
            catch (Exception ex)
            {
                throw new Exception("MissionInfo" + ": " + id, ex);
            }
        }

        public MissionInfo(string pathOrContent,bool isPath = true)
        {
            Records = new Dictionary<string, MissionInfoRecord>();
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

                    MissionInfoRecord record = new MissionInfoRecord(data);
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
                pair.Value.MissionType =  (MISSION_TYPE)TableReadBase.ParseInt(pair.Value.ValueStr[3]);
                pair.Value.Series = TableReadBase.ParseString(pair.Value.ValueStr[4]);
                pair.Value.MissionRewardGold = TableReadBase.ParseInt(pair.Value.ValueStr[5]);
                pair.Value.MissionRewardDiamond = TableReadBase.ParseInt(pair.Value.ValueStr[6]);
                if(!string.IsNullOrEmpty(pair.Value.ValueStr[7]))
                {
                   pair.Value.FinishCheck.Add( TableReadBase.ParseMultiTable(pair.Value.ValueStr[7]));
                }
                if(!string.IsNullOrEmpty(pair.Value.ValueStr[8]))
                {
                   pair.Value.FinishCheck.Add( TableReadBase.ParseMultiTable(pair.Value.ValueStr[8]));
                }
                if(!string.IsNullOrEmpty(pair.Value.ValueStr[9]))
                {
                   pair.Value.FinishCheck.Add( TableReadBase.ParseMultiTable(pair.Value.ValueStr[9]));
                }
                pair.Value.FailCheck = TableReadBase.ParseInt(pair.Value.ValueStr[10]);
                pair.Value.GoToUI = TableReadBase.ParseString(pair.Value.ValueStr[11]);
                pair.Value.IconName = TableReadBase.ParseString(pair.Value.ValueStr[12]);
            }
        }
    }

}