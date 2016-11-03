using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

using UnityEngine;

namespace Tables
{
    public partial class GuestInfoRecord : TableRecordBase
    {
        private List<SkillInfoRecord> _NotNullSkills;
        private void InitNotNullSkills()
        {
            if (_NotNullSkills != null)
                return;

            _NotNullSkills = new List<SkillInfoRecord>();
            foreach (var skillrecord in LikeSkill)
            {
                if (skillrecord != null)
                    _NotNullSkills.Add(skillrecord);
            }
        }

        public List<SkillInfoRecord> GetNotNullSkills()
        {
            InitNotNullSkills();

            return _NotNullSkills;
        }
    }

    public partial class GuestInfo : TableFileBase
    {
        private Dictionary<int, List<GuestInfoRecord>> _SpecilGuests;

        private void InitSpecilGuests()
        {
            if (_SpecilGuests != null)
                return;

            _SpecilGuests = new Dictionary<int, List<GuestInfoRecord>>();
            foreach (var guestRecord in Records.Values)
            {
                if (_SpecilGuests.ContainsKey(guestRecord.SpecilGuest))
                {
                    _SpecilGuests[guestRecord.SpecilGuest].Add(guestRecord);
                }
                else
                {
                    List<GuestInfoRecord> records = new List<GuestInfoRecord>();
                    records.Add(guestRecord);
                    _SpecilGuests.Add(guestRecord.SpecilGuest, records);
                }
            }
        }

        public List<GuestInfoRecord> GetSpecilGuests(int typeID)
        {
            InitSpecilGuests();

            return _SpecilGuests[typeID];
        }

        public List<GuestInfoRecord> GetSpecilGuests()
        {
            InitSpecilGuests();

            return _SpecilGuests[1];
        }

    }

}