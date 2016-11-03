using UnityEngine;
using System.Collections;

using Tables;
using GameBase;

namespace GameLogic
{
    public class MissionCheck_PassStage : MissionCheck_Base
    {
        MissionCheck_PassStageRecord _CheckRecord;

        public override void Init(string ID, MissionInfo mission, int checkID)
        {
            base.Init(ID, mission, checkID);

            _CheckRecord = TableReader.MissionCheck_PassStage.GetRecord(ID);
            _NeedCheckCount = 1;
            CheckedCount = 0;
        }

        protected override void InitEvent()
        {
            base.InitEvent();

            RegisteEvent(EVENT_TYPE.EVENT_LOGIC_PASS_STAGE, CheckEvent);
        }

        #region event

        public void CheckEvent(object sender, Hashtable hash)
        {
            bool isWin = (bool)hash["IsWin"];
            if (!isWin)
                return;

            var passStage = (FightStageInfo)hash["PassStage"];

            if (_CheckRecord.StageInfo != null)
            {
                if (_CheckRecord.StageInfo.Id == passStage.FigetRecord.Id)
                {
                    ++CheckedCount;
                }
            }
            else 
            {
                if (_CheckRecord.StageTypeElite && Tables.TableReader.FightCapter.EliteCapter.StageNotNull.Contains(passStage.FigetRecord))
                {
                    ++CheckedCount;
                }
                else if (!_CheckRecord.StageTypeElite && !Tables.TableReader.FightCapter.EliteCapter.StageNotNull.Contains(passStage.FigetRecord))
                {
                    ++CheckedCount;
                }
            }
        }

        #endregion
    }
}
