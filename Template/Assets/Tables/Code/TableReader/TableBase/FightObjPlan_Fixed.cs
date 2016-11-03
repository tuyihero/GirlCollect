using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

using UnityEngine;

namespace Tables
{
    public partial class FightObjPlan_FixedRecord  : TableRecordBase
    {
        public DataRecord ValueStr;

        public override string Id { get; set; }        public string Name { get; set; }
        public string Desc { get; set; }
        public GirlInfoRecord GirlInfo { get; set; }
        public EquipInfoRecord GirlEquip { get; set; }
        public int GirlLevel { get; set; }
        public GuestInfoRecord GuestInfo { get; set; }
        public FightObjPlan_FixedRecord(DataRecord dataRecord)
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
            if (GirlInfo != null)
            {
                recordStrList.Add(GirlInfo.Id);
            }
            else
            {
                recordStrList.Add("");
            }
            if (GirlEquip != null)
            {
                recordStrList.Add(GirlEquip.Id);
            }
            else
            {
                recordStrList.Add("");
            }
            recordStrList.Add(TableWriteBase.GetWriteStr(GirlLevel));
            if (GuestInfo != null)
            {
                recordStrList.Add(GuestInfo.Id);
            }
            else
            {
                recordStrList.Add("");
            }

            return recordStrList.ToArray();
        }
    }

    public partial class FightObjPlan_Fixed : TableFileBase
    {
        public Dictionary<string, FightObjPlan_FixedRecord> Records { get; internal set; }

        public bool ContainsKey(string key)
        {
             return Records.ContainsKey(key);
        }

        public FightObjPlan_FixedRecord GetRecord(string id)
        {
            try
            {
                return Records[id];
            }
            catch (Exception ex)
            {
                throw new Exception("FightObjPlan_Fixed" + ": " + id, ex);
            }
        }

        public FightObjPlan_Fixed(string pathOrContent,bool isPath = true)
        {
            Records = new Dictionary<string, FightObjPlan_FixedRecord>();
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

                    FightObjPlan_FixedRecord record = new FightObjPlan_FixedRecord(data);
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
                    pair.Value.GirlInfo =  TableReader.GirlInfo.GetRecord(pair.Value.ValueStr[3]);
                }
                else
                {
                    pair.Value.GirlInfo = null;
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[4]))
                {
                    pair.Value.GirlEquip =  TableReader.EquipInfo.GetRecord(pair.Value.ValueStr[4]);
                }
                else
                {
                    pair.Value.GirlEquip = null;
                }
                pair.Value.GirlLevel = TableReadBase.ParseInt(pair.Value.ValueStr[5]);
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[6]))
                {
                    pair.Value.GuestInfo =  TableReader.GuestInfo.GetRecord(pair.Value.ValueStr[6]);
                }
                else
                {
                    pair.Value.GuestInfo = null;
                }
            }
        }
    }

}