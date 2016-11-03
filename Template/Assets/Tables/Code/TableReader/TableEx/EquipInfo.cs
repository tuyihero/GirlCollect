using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

using UnityEngine;

namespace Tables
{
    public partial class EquipInfoRecord : TableRecordBase
    {
       
    }

    public partial class EquipInfo : TableFileBase
    {
        private Dictionary<ITEM_QUALITY, List<EquipInfoRecord>> _QualityEquip;

        private void InitEquip()
        {
            if (_QualityEquip != null)
                return;

            _QualityEquip = new Dictionary<ITEM_QUALITY, List<EquipInfoRecord>>();
            foreach (var starRecord in Records.Values)
            {
                if (_QualityEquip.ContainsKey(starRecord.Quality))
                {
                    _QualityEquip[starRecord.Quality].Add(starRecord);
                }
                else
                {
                    List<EquipInfoRecord> records = new List<EquipInfoRecord>();
                    records.Add(starRecord);
                    _QualityEquip.Add(starRecord.Quality, records);
                }
            }
        }

        public List<EquipInfoRecord> GetQualityEquips(int star)
        {
            InitEquip();

            if(_QualityEquip.ContainsKey((ITEM_QUALITY)star))
                return _QualityEquip[(ITEM_QUALITY)star];
            return null;
        }
    }

}