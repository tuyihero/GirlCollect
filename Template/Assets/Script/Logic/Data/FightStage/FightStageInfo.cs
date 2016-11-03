using UnityEngine;
using System.Collections;

using Tables;
using GameBase;

namespace GameLogic
{
    public class FightStageInfo
    {
        private string ID;

        [SaveField(1)]
        private FightSceneRecord _FigetRecord;
        public FightSceneRecord FigetRecord
        {
            get
            {
                if (_FigetRecord == null)
                    _FigetRecord = TableReader.FightScene.GetRecord(ID);
                return _FigetRecord;
            }
        }

        [SaveField(2)]
        private int _MaxPassPoint = 0;
        public int MaxPassPoint { get { return _MaxPassPoint; } }
        public bool IsStagePassed()
        {
            return _MaxPassPoint > 0;
        }

        public FightStageInfo()
        {

        }

        public FightStageInfo(FightSceneRecord figetRecord)
        {
            _FigetRecord = figetRecord;
            ID = _FigetRecord.Id;
        }

        public void StartFighting()
        {
            FightManager.Instance.StartFight(this);
        }

        public void FinishFighting(int selfPoint, int enemyPoint)
        {
            int evalutionDelta = selfPoint - enemyPoint;
            Hashtable hash = new Hashtable();
            hash.Add("SelfPoint", selfPoint);
            hash.Add("EnemyPoint", enemyPoint);
            hash.Add("Evalute", GetFightEvalute(evalutionDelta));

            if (evalutionDelta > 0)
            {
                if (evalutionDelta > MaxPassPoint)
                {
                    _MaxPassPoint = evalutionDelta;
                    hash.Add("IsNewRecord", true);
                }

                hash.Add("IsWin", true);
            }
            else
            {
                hash.Add("IsWin", false);
            }

            int gold = FightStagePack.Instance.CulFightGoldAward(this, evalutionDelta);

            hash.Add("PassStage", this);
            hash.Add("GoldAward", gold);

            GameCore.PushEvent(EVENT_TYPE.EVENT_LOGIC_PASS_STAGE, this, hash);
        }

        public int GetFightEvalute(int delta)
        {
            if (delta > 0)
            {
                return (int)(delta / 100) + 1;
            }
            else
            {
                return 0;
            }
        }

        public bool IsCanFight()
        {
            return FightStagePack.Instance.IsCanStageFight(this);
        }
    }
}
