using UnityEngine;
using System.Collections;

using Tables;
using GameBase;

namespace GameLogic
{
    public class MissionCheck_CatchGirl : MissionCheck_Base
    {
        MissionCheck_CatchGirlRecord _CheckRecord;

        public override void Init(string ID, MissionInfo mission, int checkID)
        {
            base.Init(ID, mission, checkID);

            _CheckRecord = TableReader.MissionCheck_CatchGirl.GetRecord(ID);
            _NeedCheckCount = 1;
            CheckedCount = 0;
        }

        protected override void InitEvent()
        {
            base.InitEvent();

            RegisteEvent(EVENT_TYPE.EVENT_LOGIC_CAPTURE_GIRL, CheckEvent);
        }

        #region event

        public void CheckEvent(object sender, Hashtable hash)
        {
            CheckedCount = 1;
        }

        #endregion
    }
}
