using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Tables;
namespace GameLogic
{
    public class SkillManager
    {
        public SkillManager(FightObj_Player owner)
        {
            Init(owner);
        }

        #region buff

        private BuffManager _BuffManager;

        #endregion

        #region skill

        private FightObj_Player _SkillOwner;

        public void Init(FightObj_Player owner)
        {
            _SkillOwner = owner;
            _BuffManager = new BuffManager(_SkillOwner);
        }

        public void UseSkill(Tables.SkillInfoRecord skillRecord)
        {
            var target = GetSkillTarget(skillRecord);

            foreach (var buff in skillRecord.Buffs)
            {
                if (buff != null)
                {
                    _BuffManager.PatchBuff(buff, _SkillOwner, target);
                }
            }
        }

        public FightObj_Player GetSkillTarget(Tables.SkillInfoRecord skillRecord)
        {
            if (skillRecord.Target == Tables.SKILL_TARGET_TYPE.Self)
            {
                return _SkillOwner;
            }
            else
            {
                if (_SkillOwner == FightManager.Instance.SelfPlayer)
                    return FightManager.Instance.EnemyPlayer;
                else
                    return FightManager.Instance.SelfPlayer;
            }
        }

        public static bool CanGirlSkillUse(GirlMemberInfo useSkillGirl, SkillInfoRecord skillInfo, List<GirlMemberInfo> fightGirls)
        {
            switch (skillInfo.ActTerm)
            {
                case SKILL_ACT_TERM.NEED_LARGE1:
                    return IsFightGirlLarge1(useSkillGirl, fightGirls);
                case SKILL_ACT_TERM.NEED_LARGE2:
                    return IsFightGirlLarge2(useSkillGirl, fightGirls);
                case SKILL_ACT_TERM.NEED_SMALL1:
                    return IsFightGirlSmall1(useSkillGirl, fightGirls);
                case SKILL_ACT_TERM.NEED_SMALL2:
                    return IsFightGirlSmall2(useSkillGirl, fightGirls);
                case SKILL_ACT_TERM.NEED_SAME1:
                    return IsFightGirlSame1(useSkillGirl, fightGirls);
                case SKILL_ACT_TERM.NEED_SAME2:
                    return IsFightGirlSame2(useSkillGirl, fightGirls);
                case SKILL_ACT_TERM.NEED_SERIES:
                    return IsFightGirlSeries(useSkillGirl, fightGirls);

            }

            return false;
        }

        #endregion

        #region skillTerm

        public static bool IsFightGirlLarge1(GirlMemberInfo useSkillGirl, List<GirlMemberInfo> fightGirls)
        {
            int star = useSkillGirl.GirlInfoRecord.Star;
            int conditionCount = 0;
            foreach (var girl in fightGirls)
            {
                if (girl == useSkillGirl)
                    continue;
                if (girl.GirlInfoRecord.Star > star)
                    ++conditionCount;
            }

            if (conditionCount >= 1)
                return true;
            return false;
        }

        public static bool IsFightGirlLarge2(GirlMemberInfo useSkillGirl, List<GirlMemberInfo> fightGirls)
        {
            int star = useSkillGirl.GirlInfoRecord.Star;
            int conditionCount = 0;
            foreach (var girl in fightGirls)
            {
                if (girl == useSkillGirl)
                    continue;
                if (girl.GirlInfoRecord.Star > star)
                    ++conditionCount;
            }

            if (conditionCount >= 2)
                return true;
            return false;
        }

        public static bool IsFightGirlSmall1(GirlMemberInfo useSkillGirl, List<GirlMemberInfo> fightGirls)
        {
            int star = useSkillGirl.GirlInfoRecord.Star;
            int conditionCount = 0;
            foreach (var girl in fightGirls)
            {
                if (girl == useSkillGirl)
                    continue;
                if (girl.GirlInfoRecord.Star < star)
                    ++conditionCount;
            }

            if (conditionCount >= 1)
                return true;
            return false;
        }

        public static bool IsFightGirlSmall2(GirlMemberInfo useSkillGirl, List<GirlMemberInfo> fightGirls)
        {
            int star = useSkillGirl.GirlInfoRecord.Star;
            int conditionCount = 0;
            foreach (var girl in fightGirls)
            {
                if (girl == useSkillGirl)
                    continue;
                if (girl.GirlInfoRecord.Star < star)
                    ++conditionCount;
            }

            if (conditionCount >= 2)
                return true;
            return false;
        }

        public static bool IsFightGirlSame1(GirlMemberInfo useSkillGirl, List<GirlMemberInfo> fightGirls)
        {
            int star = useSkillGirl.GirlInfoRecord.Star;
            int conditionCount = 0;
            foreach (var girl in fightGirls)
            {
                if (girl == useSkillGirl)
                    continue;
                if (girl.GirlInfoRecord.Star == star)
                    ++conditionCount;
            }

            if (conditionCount >= 1)
                return true;
            return false;
        }

        public static bool IsFightGirlSame2(GirlMemberInfo useSkillGirl, List<GirlMemberInfo> fightGirls)
        {
            int star = useSkillGirl.GirlInfoRecord.Star;
            int conditionCount = 0;
            foreach (var girl in fightGirls)
            {
                if (girl == useSkillGirl)
                    continue;
                if (girl.GirlInfoRecord.Star == star)
                    ++conditionCount;
            }

            if (conditionCount >= 2)
                return true;
            return false;
        }

        public static bool IsFightGirlSeries(GirlMemberInfo useSkillGirl, List<GirlMemberInfo> fightGirls)
        {
            if (fightGirls.Count != 3)
                return false;

            List<int> stars = new List<int>();
            foreach (var girl in fightGirls)
            {
                stars.Add(girl.GirlInfoRecord.Star);
            }

            stars.Sort();
            for (int i = 1; i < stars.Count; ++i)
            {
                if (stars[i] - stars[i - 1] > 1)
                    return false;
            }
            return true;
        }

        #endregion

        #region round event

        public void RoundInit()
        {
            _BuffManager.RoundInit();
        }

        public void RoundStart(ref FightManager.RoundResult roundResult)
        {
            _BuffManager.RoundStart(ref roundResult);
        }

        public void RoundEnd(ref FightManager.RoundResult roundResult)
        {
            _BuffManager.RoundEnd(ref roundResult);
        }

        public void RoundAttract(ref FightManager.RoundResult roundResult)
        {
            _BuffManager.RoundAttract(ref roundResult);
        }

        public void RoundPoint(ref FightManager.RoundResult roundResult)
        {
            _BuffManager.RoundPoint(ref roundResult);
        }

        #endregion
    }
}
