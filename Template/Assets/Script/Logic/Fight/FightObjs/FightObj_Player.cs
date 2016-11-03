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

        private FightGirlInfo _FightingGirl = null;
        public FightGirlInfo FightingGirl { get { return _FightingGirl; } }

        public void SetMemberGirl(GirlMemberInfo girl)
        {
            if (girl == null)
            {
                _FightingGirl = null;
                return;
            }
            _FightingGirl = new FightGirlInfo(girl);
        }

        public void SetFightingGirl(FightGirlInfo girl)
        {
            if (girl == null)
            {
                _FightingGirl = null;
                return;
            }
            _FightingGirl = girl;
        }

        public List<GirlMemberInfo> GetGroupGirls()
        {
            return GirlMemberPack.Instance.GirlList;
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

        public int GetValue(Tables.PLAYER_VALUE_TYPE valueType)
        {
            switch (valueType)
            {
                case PLAYER_VALUE_TYPE.EVALUATION:
                    return EvaluationValue;
                case PLAYER_VALUE_TYPE.ATTR1A:
                    return _FightingGirl.Attr1A;
                case PLAYER_VALUE_TYPE.ATTR1B:
                    return _FightingGirl.Attr1B;
                case PLAYER_VALUE_TYPE.ATTR2A:
                    return _FightingGirl.Attr2A;
                case PLAYER_VALUE_TYPE.ATTR2B:
                    return _FightingGirl.Attr2B;
                case PLAYER_VALUE_TYPE.ATTR3A:
                    return _FightingGirl.Attr3A;
                case PLAYER_VALUE_TYPE.ATTR3B:
                    return _FightingGirl.Attr3B;
            }

            return 0;
        }

        //return delta between origin and new
        public int AddValue(Tables.PLAYER_VALUE_TYPE valueType,int addValue)
        {
            int orgValue = 0;
            switch (valueType)
            {
                case PLAYER_VALUE_TYPE.EVALUATION:
                    orgValue = EvaluationValue;
                    EvaluationValue += addValue;
                    return EvaluationValue - orgValue;
                case PLAYER_VALUE_TYPE.ATTR1A:
                    orgValue = _FightingGirl.Attr1A;
                    _FightingGirl.Attr1A += addValue;
                    return _FightingGirl.Attr1A - orgValue;
                case PLAYER_VALUE_TYPE.ATTR1B:
                    orgValue = _FightingGirl.Attr1B;
                    _FightingGirl.Attr1B += addValue;
                    return _FightingGirl.Attr1B - orgValue;
                case PLAYER_VALUE_TYPE.ATTR2A:
                    orgValue = _FightingGirl.Attr2A;
                    _FightingGirl.Attr2A += addValue;
                    return _FightingGirl.Attr2A - orgValue;
                case PLAYER_VALUE_TYPE.ATTR2B:
                    orgValue = _FightingGirl.Attr2B;
                    _FightingGirl.Attr2B += addValue;
                    return _FightingGirl.Attr2B - orgValue;
                case PLAYER_VALUE_TYPE.ATTR3A:
                    orgValue = _FightingGirl.Attr3A;
                    _FightingGirl.Attr3A += addValue;
                    return _FightingGirl.Attr3A - orgValue;
                case PLAYER_VALUE_TYPE.ATTR3B:
                    orgValue = _FightingGirl.Attr3B;
                    _FightingGirl.Attr3B += addValue;
                    return _FightingGirl.Attr3B - orgValue;
            }

            return 0;

        }
        #endregion

        #region cal result

        public int[] CalculateAttractValue(GuestInfoRecord guestInfo)
        {
            int attractValue = 0;
            int[] attractBase = new int[FightManager.ROUND_CALCULATE_COUNT];

            attractValue = (int)(guestInfo.Attr1AAttract * _FightingGirl.Attr1A);
            if (attractValue > 0)
            {
                attractBase[0] = (int)(attractValue * _FightingGirl._GirlInfo.GetMoodRate());
            }
            attractValue = (int)(guestInfo.Attr1BAttract * _FightingGirl.Attr1B);
            if (attractValue > 0)
            {
                attractBase[0] = (int)(attractValue * _FightingGirl._GirlInfo.GetMoodRate());
            }

            attractValue = (int)(guestInfo.Attr2AAttract * _FightingGirl.Attr2A);
            if (attractValue > 0)
            {
                attractBase[1] = (int)(attractValue * _FightingGirl._GirlInfo.GetMoodRate());
            }
            attractValue = (int)(guestInfo.Attr2BAttract * _FightingGirl.Attr2B);
            if (attractValue > 0)
            {
                attractBase[1] = (int)(attractValue * _FightingGirl._GirlInfo.GetMoodRate());
            }

            attractValue = (int)(guestInfo.Attr3AAttract * _FightingGirl.Attr3A);
            if (attractValue > 0)
            {
                attractBase[2] = (int)(attractValue * _FightingGirl._GirlInfo.GetMoodRate());
            }
            attractValue = (int)(guestInfo.Attr3BAttract * _FightingGirl.Attr3B);
            if (attractValue > 0)
            {
                attractBase[2] = (int)(attractValue * _FightingGirl._GirlInfo.GetMoodRate());
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

            int[] pointBase = new int[FightManager.ROUND_CALCULATE_COUNT];

            pointValue = (int)(guestInfo.Attr1APoint * _FightingGirl.Attr1A * guestCount);
            if (pointValue > 0)
            {
                pointBase[0] = (int)(pointValue * _FightingGirl._GirlInfo.GetMoodRate());
            }
            pointValue = (int)(guestInfo.Attr1BAttract * _FightingGirl.Attr1B * guestCount);
            if (pointValue > 0)
            {
                pointBase[0] = (int)(pointValue * _FightingGirl._GirlInfo.GetMoodRate());
            }

            pointValue = (int)(guestInfo.Attr2APoint * _FightingGirl.Attr2A * guestCount);
            if (pointValue > 0)
            {
                pointBase[1] = (int)(pointValue * _FightingGirl._GirlInfo.GetMoodRate());
            }
            pointValue = (int)(guestInfo.Attr2BAttract * _FightingGirl.Attr2B * guestCount);
            if (pointValue > 0)
            {
                pointBase[1] = (int)(pointValue * _FightingGirl._GirlInfo.GetMoodRate());
            }

            pointValue = (int)(guestInfo.Attr3APoint * _FightingGirl.Attr3A * guestCount);
            if (pointValue > 0)
            {
                pointBase[2] = (int)(pointValue * _FightingGirl._GirlInfo.GetMoodRate());
            }
            pointValue = (int)(guestInfo.Attr3BAttract * _FightingGirl.Attr3B * guestCount);
            if (pointValue > 0)
            {
                pointBase[2] = (int)(pointValue * _FightingGirl._GirlInfo.GetMoodRate());
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
