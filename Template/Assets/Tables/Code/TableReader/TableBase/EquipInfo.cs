using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

using UnityEngine;

namespace Tables
{
    public partial class EquipInfoRecord  : TableRecordBase
    {
        public DataRecord ValueStr;

        public override string Id { get; set; }        public string Name { get; set; }
        public string Desc { get; set; }
        public string Icon { get; set; }
        public GirlInfoRecord BelongGirl { get; set; }
        public ITEM_QUALITY Quality { get; set; }
        public List<int> AddAttrs { get; set; }
        public SkillInfoRecord AddSkills { get; set; }
        public EquipInfoRecord(DataRecord dataRecord)
        {
            if (dataRecord != null)
            {
                ValueStr = dataRecord;
                Id = ValueStr[0];

            }
            AddAttrs = new List<int>();
        }
        public override string[] GetRecordStr()
        {
            List<string> recordStrList = new List<string>();
            recordStrList.Add(TableWriteBase.GetWriteStr(Id));
            recordStrList.Add(TableWriteBase.GetWriteStr(Name));
            recordStrList.Add(TableWriteBase.GetWriteStr(Desc));
            recordStrList.Add(TableWriteBase.GetWriteStr(Icon));
            if (BelongGirl != null)
            {
                recordStrList.Add(BelongGirl.Id);
            }
            else
            {
                recordStrList.Add("");
            }
            recordStrList.Add(((int)Quality).ToString());
            foreach (var testTableItem in AddAttrs)
            {
                recordStrList.Add(TableWriteBase.GetWriteStr(testTableItem));
            }
            if (AddSkills != null)
            {
                recordStrList.Add(AddSkills.Id);
            }
            else
            {
                recordStrList.Add("");
            }

            return recordStrList.ToArray();
        }
    }

    public partial class EquipInfo : TableFileBase
    {
        public Dictionary<string, EquipInfoRecord> Records { get; internal set; }

        public bool ContainsKey(string key)
        {
             return Records.ContainsKey(key);
        }

        public EquipInfoRecord GetRecord(string id)
        {
            try
            {
                return Records[id];
            }
            catch (Exception ex)
            {
                throw new Exception("EquipInfo" + ": " + id, ex);
            }
        }

        public EquipInfo(string pathOrContent,bool isPath = true)
        {
            Records = new Dictionary<string, EquipInfoRecord>();
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

                    EquipInfoRecord record = new EquipInfoRecord(data);
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
                pair.Value.Icon = TableReadBase.ParseString(pair.Value.ValueStr[3]);
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[4]))
                {
                    pair.Value.BelongGirl =  TableReader.GirlInfo.GetRecord(pair.Value.ValueStr[4]);
                }
                else
                {
                    pair.Value.BelongGirl = null;
                }
                pair.Value.Quality =  (ITEM_QUALITY)TableReadBase.ParseInt(pair.Value.ValueStr[5]);
                pair.Value.AddAttrs.Add(TableReadBase.ParseInt(pair.Value.ValueStr[6]));
                pair.Value.AddAttrs.Add(TableReadBase.ParseInt(pair.Value.ValueStr[7]));
                pair.Value.AddAttrs.Add(TableReadBase.ParseInt(pair.Value.ValueStr[8]));
                pair.Value.AddAttrs.Add(TableReadBase.ParseInt(pair.Value.ValueStr[9]));
                pair.Value.AddAttrs.Add(TableReadBase.ParseInt(pair.Value.ValueStr[10]));
                pair.Value.AddAttrs.Add(TableReadBase.ParseInt(pair.Value.ValueStr[11]));
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[12]))
                {
                    pair.Value.AddSkills =  TableReader.SkillInfo.GetRecord(pair.Value.ValueStr[12]);
                }
                else
                {
                    pair.Value.AddSkills = null;
                }
            }
        }
    }

}