using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

using UnityEngine;

namespace Tables
{
    public partial class MissionInfoRecord : TableRecordBase
    {
       
    }

    public partial class MissionInfo : TableFileBase
    {
        private List<MissionInfoRecord> _DaylyMissionRecords;

        public List<MissionInfoRecord> DaylyMissionRecords
        {
            get
            {
                if (_DaylyMissionRecords == null)
                {
                    _DaylyMissionRecords = new List<MissionInfoRecord>();
                    foreach (var record in Records)
                    {
                        if (record.Value.MissionType == MISSION_TYPE.DAYLY_MISSION)
                        {
                            _DaylyMissionRecords.Add(record.Value);
                        }
                    }
                }
                return _DaylyMissionRecords;
            }
        }
    }

}