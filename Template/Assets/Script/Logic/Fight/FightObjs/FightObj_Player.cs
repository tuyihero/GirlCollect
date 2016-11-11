using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Tables;
namespace GameLogic
{
    public class FightObj_Player : _FightObjBase
    {
        #region
        public const int MIN_EVALUATION_VALUE = 0;
        public const int MAX_EVALUATION_VALUE = 9999999;
        public const int INIT_EVALUATION_VALUE = 1000;

        private int _EvaluationValue = INIT_EVALUATION_VALUE;
        public int EvaluationValue
        {
            get
            {
                return _EvaluationValue;
            }
            set
            {
                _EvaluationValue = Mathf.Clamp(value, MIN_EVALUATION_VALUE, MAX_EVALUATION_VALUE);
            }
        }

        private const int MAX_FIGHT_GIRL_COUNT = 3;
        private List<GirlMemberInfo> _FightingGirls = new List<GirlMemberInfo>();
        public List<GirlMemberInfo> FightingGirls { get { return _FightingGirls; } }

        public void SetMemberGirl(List<GirlMemberInfo> girls)
        {
            if (girls == null)
            {
                return;
            }
            _FightingGirls = girls;
        }

        public void SetFightGirl(List<GirlMemberInfo> girls)
        {
            if (girls == null)
            {
                return;
            }
            _FightingGirls = girls;
        }

        public void Init()
        {
            if (_SkillManager == null)
            {
                _SkillManager = new SkillManager(this);
            }
        }

        #endregion

        #region skill

        private SkillManager _SkillManager;

        public void UseSkill(SkillInfoRecord skillRecord)
        {
            _SkillManager.UseSkill(skillRecord);
        }

        public int GetValue(int girlIdx, Tables.PLAYER_VALUE_TYPE valueType)
        {
            var fightingGirl = _FightingGirls[girlIdx];
            switch (valueType)
            {
                case PLAYER_VALUE_TYPE.EVALUATION:
                    return EvaluationValue;
                case PLAYER_VALUE_TYPE.ATTR1A:
                    return fightingGirl.Attr1A;
                case PLAYER_VALUE_TYPE.ATTR1B:
                    return fightingGirl.Attr1B;
                case PLAYER_VALUE_TYPE.ATTR2A:
                    return fightingGirl.Attr2A;
                case PLAYER_VALUE_TYPE.ATTR2B:
                    return fightingGirl.Attr2B;
                case PLAYER_VALUE_TYPE.ATTR3A:
                    return fightingGirl.Attr3A;
                case PLAYER_VALUE_TYPE.ATTR3B:
                    return fightingGirl.Attr3B;
            }

            return 0;
        }

        //return delta between origin and new
        
        #endregion

        #region cal result

        public int[] CalculateAttractValue(GuestInfoRecord guestInfo)
        {
            int attractValue = 0;
            int[] attractBase = new int[MAX_FIGHT_GIRL_COUNT + 1];

            for(int i = 0; i< MAX_FIGHT_GIRL_COUNT; ++i)
            {
                if (_FightingGirls.Count > i)
                {
                    attractValue += (int)(guestInfo.Attr1AAttract * _FightingGirls[i].Attr1A);
                    attractValue += (int)(guestInfo.Attr1BAttract * _FightingGirls[i].Attr1B);
                    attractValue += (int)(guestInfo.Attr2AAttract * _FightingGirls[i].Attr2A);
                    attractValue += (int)(guestInfo.Attr2BAttract * _FightingGirls[i].Attr2B);
                    attractValue += (int)(guestInfo.Attr3AAttract * _FightingGirls[i].Attr3A);
                    attractValue += (int)(guestInfo.Attr3BAttract * _FightingGirls[i].Attr3B);
                }
                attractBase[i] = attractValue;
            }

            for (int i = 0; i < 3; ++i)
            {
                attractBase[3] += attractBase[i];
            }

            return attractBase;
        }

        public int[] CalculatePointValue(GuestInfoRecord guestInfo, int guestCount)
        {
            int pointValue = 0;

            int[] pointBase = new int[FightManager.ROUND_CALCULATE_COUNT + 1];

            for (int i = 0; i < MAX_FIGHT_GIRL_COUNT; ++i)
            {
                if (_FightingGirls.Count > i)
                {
                    pointValue += (int)(guestInfo.Attr1APoint * _FightingGirls[i].Attr1A);
                    pointValue += (int)(guestInfo.Attr1BPoint * _FightingGirls[i].Attr1B);
                    pointValue += (int)(guestInfo.Attr2APoint * _FightingGirls[i].Attr2A);
                    pointValue += (int)(guestInfo.Attr2BPoint * _FightingGirls[i].Attr2B);
                    pointValue += (int)(guestInfo.Attr3APoint * _FightingGirls[i].Attr3A);
                    pointValue += (int)(guestInfo.Attr3BPoint * _FightingGirls[i].Attr3B);
                }
                pointBase[i] = pointValue;
            }
            for (int i = 0; i < 3; ++i)
            {
                pointBase[3] += pointBase[i];
            }

            return pointBase;
        }

        #endregion

        #region round event

        public void RoundInit()
        {
            _SkillManager.RoundInit();
        }

        public void RoundStart(ref FightManager.RoundResult roundResult)
        {
            _SkillManager.RoundStart(ref roundResult);
        }

        public void RoundEnd(ref FightManager.RoundResult roundResult)
        {
            _SkillManager.RoundEnd(ref roundResult);
        }

        public void RoundAttract(ref FightManager.RoundResult roundResult)
        {
            _SkillManager.RoundAttract(ref roundResult);
        }

        public void RoundPoint(ref FightManager.RoundResult roundResult)
        {
            _SkillManager.RoundPoint(ref roundResult);
        }

        #endregion

    }
}
