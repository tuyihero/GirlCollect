using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

using UnityEngine;

namespace Tables
{
    public partial class BuffImpact_ModifyValueLastRecord  : TableRecordBase
    {
        public DataRecord ValueStr;

        public override string Id { get; set; }        public string Name { get; set; }
        public string Desc { get; set; }
        public PLAYER_VALUE_TYPE TargetValue { get; set; }
        public SKILL_TARGET_TYPE SourceType { get; set; }
        public PLAYER_VALUE_TYPE SourceValue { get; set; }
        public IMPACT_MODIFY_TYPE ModifyType { get; set; }
        public float ModifyValue { get; set; }
        public BuffImpact_ModifyValueLastRecord(DataRecord dataRecord)
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
            recordStrList.Add(((int)TargetValue).ToString());
            recordStrList.Add(((int)SourceType).ToString());
            recordStrList.Add(((int)SourceValue).ToString());
            recordStrList.Add(((int)ModifyType).ToString());
            recordStrList.Add(TableWriteBase.GetWriteStr(ModifyValue));

            return recordStrList.ToArray();
        }
    }

    public partial class BuffImpact_ModifyValueLast : TableFileBase
    {
        public Dictionary<string, BuffImpact_ModifyValueLastRecord> Records { get; internal set; }

        public bool ContainsKey(string key)
        {
             return Records.ContainsKey(key);
        }

        public BuffImpact_ModifyValueLastRecord GetRecord(string id)
        {
            try
            {
                return Records[id];
            }
            catch (Exception ex)
            {
                throw new Exception("BuffImpact_ModifyValueLast" + ": " + id, ex);
            }
        }

        public BuffImpact_ModifyValueLast(string pathOrContent,bool isPath = true)
        {
            Records = new Dictionary<string, BuffImpact_ModifyValueLastRecord>();
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

                    BuffImpact_ModifyValueLastRecord record = new BuffImpact_ModifyValueLastRecord(data);
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
                pair.Value.TargetValue =  (PLAYER_VALUE_TYPE)TableReadBase.ParseInt(pair.Value.ValueStr[3]);
                pair.Value.SourceType =  (SKILL_TARGET_TYPE)TableReadBase.ParseInt(pair.Value.ValueStr[4]);
                pair.Value.SourceValue =  (PLAYER_VALUE_TYPE)TableReadBase.ParseInt(pair.Value.ValueStr[5]);
                pair.Value.ModifyType =  (IMPACT_MODIFY_TYPE)TableReadBase.ParseInt(pair.Value.ValueStr[6]);
                pair.Value.ModifyValue = TableReadBase.ParseFloat(pair.Value.ValueStr[7]);
            }
        }
    }

}