using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

using UnityEngine;

namespace Tables
{
    public partial class FightCapterRecord : TableRecordBase
    {
        private List<FightSceneRecord> _StageNotNull;
        public List<FightSceneRecord> StageNotNull
        {
            get
            {
                InitStageNotNull();
                return _StageNotNull;
            }
        }

        private void InitStageNotNull()
        {
            if (_StageNotNull != null)
                return;

            _StageNotNull = new List<FightSceneRecord>();
            foreach (var stageRecord in FightScenes)
            {
                if (stageRecord != null)
                {
                    _StageNotNull.Add(stageRecord);
                }
            }
        }

    }

    public partial class FightCapter : TableFileBase
    {
        private List<FightCapterRecord> _NormalCapter;
        public List<FightCapterRecord> NormalCapter
        {
            get
            {
                InitNormalCapter();
                return _NormalCapter;
            }
        }

        private FightCapterRecord _EliteCapter;
        public FightCapterRecord EliteCapter
        {
            get
            {
                InitNormalCapter();
                return _EliteCapter;
            }
        }



        private void InitNormalCapter()
        {
            if (_NormalCapter != null)
                return;

            _NormalCapter = new List<FightCapterRecord>();
            foreach (var record in Records.Values)
            {
                if (!record.Name.Contains("SP"))
                {
                    _NormalCapter.Add(record);
                }
                else
                {
                    _EliteCapter = record;
                }
            }
        }

        
    }

}