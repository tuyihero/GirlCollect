using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

using UnityEngine;

namespace Tables
{
    public partial class GirlInfoRecord  : TableRecordBase
    {
        public DataRecord ValueStr;

        public override string Id { get; set; }        public string Name { get; set; }
        public string Desc { get; set; }
        public string Icon { get; set; }
        public int Star { get; set; }
        public FightSceneRecord CatchStagePass { get; set; }
        public string LevelInfoTypeID { get; set; }
        public int Attr1A { get; set; }
        public int Attr1B { get; set; }
        public int Attr2A { get; set; }
        public int Attr2B { get; set; }
        public int Attr3A { get; set; }
        public int Attr3B { get; set; }
        public List<SkillInfoRecord> Skills { get; set; }
        public GirlInfoRecord(DataRecord dataRecord)
        {
            if (dataRecord != null)
            {
                ValueStr = dataRecord;
                Id = ValueStr[0];

            }
            Skills = new List<SkillInfoRecord>();
        }
        public override string[] GetRecordStr()
        {
            List<string> recordStrList = new List<string>();
            recordStrList.Add(TableWriteBase.GetWriteStr(Id));
            recordStrList.Add(TableWriteBase.GetWriteStr(Name));
            recordStrList.Add(TableWriteBase.GetWriteStr(Desc));
            recordStrList.Add(TableWriteBase.GetWriteStr(Icon));
            recordStrList.Add(TableWriteBase.GetWriteStr(Star));
            if (CatchStagePass != null)
            {
                recordStrList.Add(CatchStagePass.Id);
            }
            else
            {
                recordStrList.Add("");
            }
            recordStrList.Add(TableWriteBase.GetWriteStr(LevelInfoTypeID));
            recordStrList.Add(TableWriteBase.GetWriteStr(Attr1A));
            recordStrList.Add(TableWriteBase.GetWriteStr(Attr1B));
            recordStrList.Add(TableWriteBase.GetWriteStr(Attr2A));
            recordStrList.Add(TableWriteBase.GetWriteStr(Attr2B));
            recordStrList.Add(TableWriteBase.GetWriteStr(Attr3A));
            recordStrList.Add(TableWriteBase.GetWriteStr(Attr3B));
            foreach (var testTableItem in Skills)
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

    public partial class GirlInfo : TableFileBase
    {
        public Dictionary<string, GirlInfoRecord> Records { get; internal set; }

        public bool ContainsKey(string key)
        {
             return Records.ContainsKey(key);
        }

        public GirlInfoRecord GetRecord(string id)
        {
            try
            {
                return Records[id];
            }
            catch (Exception ex)
            {
                throw new Exception("GirlInfo" + ": " + id, ex);
            }
        }

        public GirlInfo(string pathOrContent,bool isPath = true)
        {
            Records = new Dictionary<string, GirlInfoRecord>();
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

                    GirlInfoRecord record = new GirlInfoRecord(data);
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
                pair.Value.Star = TableReadBase.ParseInt(pair.Value.ValueStr[4]);
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[5]))
                {
                    pair.Value.CatchStagePass =  TableReader.FightScene.GetRecord(pair.Value.ValueStr[5]);
                }
                else
                {
                    pair.Value.CatchStagePass = null;
                }
                pair.Value.LevelInfoTypeID = TableReadBase.ParseString(pair.Value.ValueStr[6]);
                pair.Value.Attr1A = TableReadBase.ParseInt(pair.Value.ValueStr[7]);
                pair.Value.Attr1B = TableReadBase.ParseInt(pair.Value.ValueStr[8]);
                pair.Value.Attr2A = TableReadBase.ParseInt(pair.Value.ValueStr[9]);
                pair.Value.Attr2B = TableReadBase.ParseInt(pair.Value.ValueStr[10]);
                pair.Value.Attr3A = TableReadBase.ParseInt(pair.Value.ValueStr[11]);
                pair.Value.Attr3B = TableReadBase.ParseInt(pair.Value.ValueStr[12]);
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[13]))
                {
                    pair.Value.Skills.Add( TableReader.SkillInfo.GetRecord(pair.Value.ValueStr[13]));
                }
                else
                {
                    pair.Value.Skills.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[14]))
                {
                    pair.Value.Skills.Add( TableReader.SkillInfo.GetRecord(pair.Value.ValueStr[14]));
                }
                else
                {
                    pair.Value.Skills.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[15]))
                {
                    pair.Value.Skills.Add( TableReader.SkillInfo.GetRecord(pair.Value.ValueStr[15]));
                }
                else
                {
                    pair.Value.Skills.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[16]))
                {
                    pair.Value.Skills.Add( TableReader.SkillInfo.GetRecord(pair.Value.ValueStr[16]));
                }
                else
                {
                    pair.Value.Skills.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[17]))
                {
                    pair.Value.Skills.Add( TableReader.SkillInfo.GetRecord(pair.Value.ValueStr[17]));
                }
                else
                {
                    pair.Value.Skills.Add(null);
                }
            }
        }
    }

}