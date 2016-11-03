using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

using UnityEngine;

namespace Tables
{
    public partial class GuestInfoRecord  : TableRecordBase
    {
        public DataRecord ValueStr;

        public override string Id { get; set; }        public string Name { get; set; }
        public string Desc { get; set; }
        public string Icon { get; set; }
        public int SpecilGuest { get; set; }
        public int GuestNum { get; set; }
        public float Attr1AAttract { get; set; }
        public float Attr1APoint { get; set; }
        public float Attr1BAttract { get; set; }
        public float Attr1BPoint { get; set; }
        public float Attr2AAttract { get; set; }
        public float Attr2APoint { get; set; }
        public float Attr2BAttract { get; set; }
        public float Attr2BPoint { get; set; }
        public float Attr3AAttract { get; set; }
        public float Attr3APoint { get; set; }
        public float Attr3BAttract { get; set; }
        public float Attr3BPoint { get; set; }
        public List<SkillInfoRecord> LikeSkill { get; set; }
        public List<float> SkillAttract { get; set; }
        public List<float> SkillPoint { get; set; }
        public List<BuffInfoRecord> AddBuffs { get; set; }
        public GuestInfoRecord(DataRecord dataRecord)
        {
            if (dataRecord != null)
            {
                ValueStr = dataRecord;
                Id = ValueStr[0];

            }
            LikeSkill = new List<SkillInfoRecord>();
            SkillAttract = new List<float>();
            SkillPoint = new List<float>();
            AddBuffs = new List<BuffInfoRecord>();
        }
        public override string[] GetRecordStr()
        {
            List<string> recordStrList = new List<string>();
            recordStrList.Add(TableWriteBase.GetWriteStr(Id));
            recordStrList.Add(TableWriteBase.GetWriteStr(Name));
            recordStrList.Add(TableWriteBase.GetWriteStr(Desc));
            recordStrList.Add(TableWriteBase.GetWriteStr(Icon));
            recordStrList.Add(TableWriteBase.GetWriteStr(SpecilGuest));
            recordStrList.Add(TableWriteBase.GetWriteStr(GuestNum));
            recordStrList.Add(TableWriteBase.GetWriteStr(Attr1AAttract));
            recordStrList.Add(TableWriteBase.GetWriteStr(Attr1APoint));
            recordStrList.Add(TableWriteBase.GetWriteStr(Attr1BAttract));
            recordStrList.Add(TableWriteBase.GetWriteStr(Attr1BPoint));
            recordStrList.Add(TableWriteBase.GetWriteStr(Attr2AAttract));
            recordStrList.Add(TableWriteBase.GetWriteStr(Attr2APoint));
            recordStrList.Add(TableWriteBase.GetWriteStr(Attr2BAttract));
            recordStrList.Add(TableWriteBase.GetWriteStr(Attr2BPoint));
            recordStrList.Add(TableWriteBase.GetWriteStr(Attr3AAttract));
            recordStrList.Add(TableWriteBase.GetWriteStr(Attr3APoint));
            recordStrList.Add(TableWriteBase.GetWriteStr(Attr3BAttract));
            recordStrList.Add(TableWriteBase.GetWriteStr(Attr3BPoint));
            foreach (var testTableItem in LikeSkill)
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
            foreach (var testTableItem in SkillAttract)
            {
                recordStrList.Add(TableWriteBase.GetWriteStr(testTableItem));
            }
            foreach (var testTableItem in SkillPoint)
            {
                recordStrList.Add(TableWriteBase.GetWriteStr(testTableItem));
            }
            foreach (var testTableItem in AddBuffs)
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

    public partial class GuestInfo : TableFileBase
    {
        public Dictionary<string, GuestInfoRecord> Records { get; internal set; }

        public bool ContainsKey(string key)
        {
             return Records.ContainsKey(key);
        }

        public GuestInfoRecord GetRecord(string id)
        {
            try
            {
                return Records[id];
            }
            catch (Exception ex)
            {
                throw new Exception("GuestInfo" + ": " + id, ex);
            }
        }

        public GuestInfo(string pathOrContent,bool isPath = true)
        {
            Records = new Dictionary<string, GuestInfoRecord>();
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

                    GuestInfoRecord record = new GuestInfoRecord(data);
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
                pair.Value.SpecilGuest = TableReadBase.ParseInt(pair.Value.ValueStr[4]);
                pair.Value.GuestNum = TableReadBase.ParseInt(pair.Value.ValueStr[5]);
                pair.Value.Attr1AAttract = TableReadBase.ParseFloat(pair.Value.ValueStr[6]);
                pair.Value.Attr1APoint = TableReadBase.ParseFloat(pair.Value.ValueStr[7]);
                pair.Value.Attr1BAttract = TableReadBase.ParseFloat(pair.Value.ValueStr[8]);
                pair.Value.Attr1BPoint = TableReadBase.ParseFloat(pair.Value.ValueStr[9]);
                pair.Value.Attr2AAttract = TableReadBase.ParseFloat(pair.Value.ValueStr[10]);
                pair.Value.Attr2APoint = TableReadBase.ParseFloat(pair.Value.ValueStr[11]);
                pair.Value.Attr2BAttract = TableReadBase.ParseFloat(pair.Value.ValueStr[12]);
                pair.Value.Attr2BPoint = TableReadBase.ParseFloat(pair.Value.ValueStr[13]);
                pair.Value.Attr3AAttract = TableReadBase.ParseFloat(pair.Value.ValueStr[14]);
                pair.Value.Attr3APoint = TableReadBase.ParseFloat(pair.Value.ValueStr[15]);
                pair.Value.Attr3BAttract = TableReadBase.ParseFloat(pair.Value.ValueStr[16]);
                pair.Value.Attr3BPoint = TableReadBase.ParseFloat(pair.Value.ValueStr[17]);
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[18]))
                {
                    pair.Value.LikeSkill.Add( TableReader.SkillInfo.GetRecord(pair.Value.ValueStr[18]));
                }
                else
                {
                    pair.Value.LikeSkill.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[19]))
                {
                    pair.Value.LikeSkill.Add( TableReader.SkillInfo.GetRecord(pair.Value.ValueStr[19]));
                }
                else
                {
                    pair.Value.LikeSkill.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[20]))
                {
                    pair.Value.LikeSkill.Add( TableReader.SkillInfo.GetRecord(pair.Value.ValueStr[20]));
                }
                else
                {
                    pair.Value.LikeSkill.Add(null);
                }
                pair.Value.SkillAttract.Add(TableReadBase.ParseFloat(pair.Value.ValueStr[21]));
                pair.Value.SkillAttract.Add(TableReadBase.ParseFloat(pair.Value.ValueStr[22]));
                pair.Value.SkillAttract.Add(TableReadBase.ParseFloat(pair.Value.ValueStr[23]));
                pair.Value.SkillPoint.Add(TableReadBase.ParseFloat(pair.Value.ValueStr[24]));
                pair.Value.SkillPoint.Add(TableReadBase.ParseFloat(pair.Value.ValueStr[25]));
                pair.Value.SkillPoint.Add(TableReadBase.ParseFloat(pair.Value.ValueStr[26]));
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[27]))
                {
                    pair.Value.AddBuffs.Add( TableReader.BuffInfo.GetRecord(pair.Value.ValueStr[27]));
                }
                else
                {
                    pair.Value.AddBuffs.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[28]))
                {
                    pair.Value.AddBuffs.Add( TableReader.BuffInfo.GetRecord(pair.Value.ValueStr[28]));
                }
                else
                {
                    pair.Value.AddBuffs.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[29]))
                {
                    pair.Value.AddBuffs.Add( TableReader.BuffInfo.GetRecord(pair.Value.ValueStr[29]));
                }
                else
                {
                    pair.Value.AddBuffs.Add(null);
                }
            }
        }
    }

}