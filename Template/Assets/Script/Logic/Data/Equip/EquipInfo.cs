using UnityEngine;
using System.Collections;

using Tables;

namespace GameLogic
{
    public class EquipInfo
    {
        
        private string ID;
        [SaveField(1)]
        private EquipInfoRecord _EquipRecord;
        public EquipInfoRecord EquipRecord { get { return _EquipRecord; } }

        public EquipInfo()
        {
            ID = "";
        }

        public EquipInfo(EquipInfoRecord record)
        {
            ID = record.Id;
            _EquipRecord = record;
        }

    }
}
