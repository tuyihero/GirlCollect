using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

using UnityEngine;

namespace Tables
{
    public partial class GirlInfoRecord : TableRecordBase
    {
       
    }

    public partial class GirlInfo : TableFileBase
    {
        private Dictionary<int, List<GirlInfoRecord>> _StarGirls;

        private void InitStar()
        {
            if (_StarGirls != null)
                return;

            _StarGirls = new Dictionary<int, List<GirlInfoRecord>>();
            foreach (var starRecord in Records.Values)
            {
                if (_StarGirls.ContainsKey(starRecord.Star))
                {
                    _StarGirls[starRecord.Star].Add(starRecord);
                }
                else
                {
                    List<GirlInfoRecord> records = new List<GirlInfoRecord>();
                    records.Add(starRecord);
                    _StarGirls.Add(starRecord.Star, records);
                }
            }
        }

        public List<GirlInfoRecord> GetStarGirls(int star)
        {
            InitStar();

            if(_StarGirls.ContainsKey(star))
                return _StarGirls[star];
            return null;
        }
    }

}