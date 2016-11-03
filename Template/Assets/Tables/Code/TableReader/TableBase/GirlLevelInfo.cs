using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

using UnityEngine;

namespace Tables
{
    public partial class GirlLevelInfoRecord  : TableRecordBase
    {
        public DataRecord ValueStr;

        public override string Id { get; set; }        public string Name { get; set; }
        public string Desc { get; set; }
        public string TypeID { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public int GoldCost { get; set; }
        public int Attr1A { get; set; }
        public int Attr1B { get; set; }
        public int Attr2A { get; set; }
        public int Attr2B { get; set; }
        public int Attr3A { get; set; }
        public int Attr3B { get; set; }
        public GirlLevelInfoRecord(DataRecord dataRecord)
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
            recordStrList.Add(TableWriteBase.GetWriteStr(TypeID));
            recordStrList.Add(TableWriteBase.GetWriteStr(Level));
            recordStrList.Add(TableWriteBase.GetWriteStr(Exp));
            recordStrList.Add(TableWriteBase.GetWriteStr(GoldCost));
            recordStrList.Add(TableWriteBase.GetWriteStr(Attr1A));
            recordStrList.Add(TableWriteBase.GetWriteStr(Attr1B));
            recordStrList.Add(TableWriteBase.GetWriteStr(Attr2A));
            recordStrList.Add(TableWriteBase.GetWriteStr(Attr2B));
            recordStrList.Add(TableWriteBase.GetWriteStr(Attr3A));
            recordStrList.Add(TableWriteBase.GetWriteStr(Attr3B));

            return recordStrList.ToArray();
        }
    }

    public partial class GirlLevelInfo : TableFileBase
    {
        public Dictionary<string, GirlLevelInfoRecord> Records { get; internal set; }

        public bool ContainsKey(string key)
        {
             return Records.ContainsKey(key);
        }

        public GirlLevelInfoRecord GetRecord(string id)
        {
            try
            {
                return Records[id];
            }
            catch (Exception ex)
            {
                throw new Exception("GirlLevelInfo" + ": " + id, ex);
            }
        }

        public GirlLevelInfo(string pathOrContent,bool isPath = true)
        {
            Records = new Dictionary<string, GirlLevelInfoRecord>();
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

                    GirlLevelInfoRecord record = new GirlLevelInfoRecord(data);
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
                pair.Value.TypeID = TableReadBase.ParseString(pair.Value.ValueStr[3]);
                pair.Value.Level = TableReadBase.ParseInt(pair.Value.ValueStr[4]);
                pair.Value.Exp = TableReadBase.ParseInt(pair.Value.ValueStr[5]);
                pair.Value.GoldCost = TableReadBase.ParseInt(pair.Value.ValueStr[6]);
                pair.Value.Attr1A = TableReadBase.ParseInt(pair.Value.ValueStr[7]);
                pair.Value.Attr1B = TableReadBase.ParseInt(pair.Value.ValueStr[8]);
                pair.Value.Attr2A = TableReadBase.ParseInt(pair.Value.ValueStr[9]);
                pair.Value.Attr2B = TableReadBase.ParseInt(pair.Value.ValueStr[10]);
                pair.Value.Attr3A = TableReadBase.ParseInt(pair.Value.ValueStr[11]);
                pair.Value.Attr3B = TableReadBase.ParseInt(pair.Value.ValueStr[12]);
            }
        }
    }

}