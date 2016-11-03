using UnityEngine;
using System.Collections;

using Tables;
using GameBase;

namespace GameLogic
{
    public class MissionCheck_WebcamFight : MissionCheck_Base
    {
        MissionCheck_WebcamFightRecord _CheckRecord;

        public override void Init(string ID, MissionInfo mission, int checkID)
        {
            base.Init(ID, mission, checkID);

            _CheckRecord = TableReader.MissionCheck_WebcamFight.GetRecord(ID);
            _NeedCheckCount = 1;
            CheckedCount = 0;
        }

        protected override void InitEvent()
        {
            base.InitEvent();

            RegisteEvent(EVENT_TYPE.EVENT_LOGIC_WEBCAM_FIGHT, CheckEvent);
        }

        #region event

        public void CheckEvent(object sender, Hashtable hash)
        {
            ++CheckedCount;
        }

        #endregion
    }
}
