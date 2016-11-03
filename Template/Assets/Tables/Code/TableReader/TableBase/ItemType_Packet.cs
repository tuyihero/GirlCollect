﻿using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

using UnityEngine;

namespace Tables
{
    public partial class ItemType_PacketRecord  : TableRecordBase
    {
        public DataRecord ValueStr;

        public override string Id { get; set; }        public string Name { get; set; }
        public string Desc { get; set; }
        public List<DropGroupRecord> DropGroup { get; set; }
        public ItemType_PacketRecord(DataRecord dataRecord)
        {
            if (dataRecord != null)
            {
                ValueStr = dataRecord;
                Id = ValueStr[0];

            }
            DropGroup = new List<DropGroupRecord>();
        }
        public override string[] GetRecordStr()
        {
            List<string> recordStrList = new List<string>();
            recordStrList.Add(TableWriteBase.GetWriteStr(Id));
            recordStrList.Add(TableWriteBase.GetWriteStr(Name));
            recordStrList.Add(TableWriteBase.GetWriteStr(Desc));
            foreach (var testTableItem in DropGroup)
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

    public partial class ItemType_Packet : TableFileBase
    {
        public Dictionary<string, ItemType_PacketRecord> Records { get; internal set; }

        public bool ContainsKey(string key)
        {
             return Records.ContainsKey(key);
        }

        public ItemType_PacketRecord GetRecord(string id)
        {
            try
            {
                return Records[id];
            }
            catch (Exception ex)
            {
                throw new Exception("ItemType_Packet" + ": " + id, ex);
            }
        }

        public ItemType_Packet(string pathOrContent,bool isPath = true)
        {
            Records = new Dictionary<string, ItemType_PacketRecord>();
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

                    ItemType_PacketRecord record = new ItemType_PacketRecord(data);
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
                    pair.Value.DropGroup.Add( TableReader.DropGroup.GetRecord(pair.Value.ValueStr[3]));
                }
                else
                {
                    pair.Value.DropGroup.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[4]))
                {
                    pair.Value.DropGroup.Add( TableReader.DropGroup.GetRecord(pair.Value.ValueStr[4]));
                }
                else
                {
                    pair.Value.DropGroup.Add(null);
                }
                if (!string.IsNullOrEmpty(pair.Value.ValueStr[5]))
                {
                    pair.Value.DropGroup.Add( TableReader.DropGroup.GetRecord(pair.Value.ValueStr[5]));
                }
                else
                {
                    pair.Value.DropGroup.Add(null);
                }
            }
        }
    }

}