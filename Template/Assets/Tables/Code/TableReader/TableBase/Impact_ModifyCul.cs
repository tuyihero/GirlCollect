using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

using UnityEngine;

namespace Tables
{
    public partial class Impact_ModifyCulRecord  : TableRecordBase
    {
        public DataRecord ValueStr;

        public override string Id { get; set; }        public string Name { get; set; }
        public string Desc { get; set; }
        public IMPACT_MODIFY_TARGET ActTarget { get; set; }
        public float ActPersent { get; set; }
        public Impact_ModifyCulRecord(DataRecord dataRecord)
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
            recordStrList.Add(((int)ActTarget).ToString());
            recordStrList.Add(TableWriteBase.GetWriteStr(ActPersent));

            return recordStrList.ToArray();
        }
    }

    public partial class Impact_ModifyCul : TableFileBase
    {
        public Dictionary<string, Impact_ModifyCulRecord> Records { get; internal set; }

        public bool ContainsKey(string key)
        {
             return Records.ContainsKey(key);
        }

        public Impact_ModifyCulRecord GetRecord(string id)
        {
            try
            {
                return Records[id];
            }
            catch (Exception ex)
            {
                throw new Exception("Impact_ModifyCul" + ": " + id, ex);
            }
        }

        public Impact_ModifyCul(string pathOrContent,bool isPath = true)
        {
            Records = new Dictionary<string, Impact_ModifyCulRecord>();
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

                    Impact_ModifyCulRecord record = new Impact_ModifyCulRecord(data);
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
                pair.Value.ActTarget =  (IMPACT_MODIFY_TARGET)TableReadBase.ParseInt(pair.Value.ValueStr[3]);
                pair.Value.ActPersent = TableReadBase.ParseFloat(pair.Value.ValueStr[4]);
            }
        }
    }

}