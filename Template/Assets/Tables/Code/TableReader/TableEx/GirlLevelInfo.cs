using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

using UnityEngine;

namespace Tables
{
    public partial class GirlLevelInfoRecord : TableRecordBase
    {
       
    }

    public partial class GirlLevelInfo : TableFileBase
    {
        private Dictionary<string, List<GirlLevelInfoRecord>> _LevelTypes;

        private void InitTypes()
        {
            if (_LevelTypes != null)
                return;

            _LevelTypes = new Dictionary<string, List<GirlLevelInfoRecord>>();
            foreach (var levelRecord in Records.Values)
            {
                if (_LevelTypes.ContainsKey(levelRecord.TypeID))
                {
                    _LevelTypes[levelRecord.TypeID].Add(levelRecord);
                }
                else
                {
                    List<GirlLevelInfoRecord> records = new List<GirlLevelInfoRecord>();
                    records.Add(levelRecord);
                    _LevelTypes.Add(levelRecord.TypeID, records);
                }
            }

            foreach (var levelType in _LevelTypes)
            {
                levelType.Value.Sort((record1, record2) =>
                {
                    if (record1.Level > record2.Level)
                        return 1;
                    else if (record1.Level < record2.Level)
                        return -1;
                    return 0;
                });
            }
        }

        public List<GirlLevelInfoRecord> GetTypeLevels(string typeID)
        {
            InitTypes();

            if(_LevelTypes.ContainsKey(typeID))
                return _LevelTypes[typeID];
            return null;
        }

        public GirlLevelInfoRecord GetTypeLevel(string typeID, int level)
        {
            InitTypes();

            if (_LevelTypes.ContainsKey(typeID)
                && _LevelTypes[typeID].Count > (level))
                return _LevelTypes[typeID][level];
            return null;
        }
    }

}