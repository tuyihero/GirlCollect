using UnityEngine;
using System.Collections;

using Tables;
using GameBase;

namespace GameLogic
{
    public class MissionCheck_SpecilFight : MissionCheck_Base
    {
        MissionCheck_SpecilFightRecord _CheckRecord;

        public override void Init(string ID, MissionInfo mission, int checkID)
        {
            base.Init(ID, mission, checkID);

            _CheckRecord = TableReader.MissionCheck_SpecilFight.GetRecord(ID);
            _NeedCheckCount = 1;
            CheckedCount = 0;
        }

        protected override void InitEvent()
        {
            base.InitEvent();

            RegisteEvent(EVENT_TYPE.EVENT_LOGIC_SPECIL_FIGHT, CheckEvent);
        }

        #region event

        public void CheckEvent(object sender, Hashtable hash)
        {
            ++CheckedCount;
        }

        #endregion
    }
}
