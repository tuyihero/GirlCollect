using UnityEngine;
using System.Collections;

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
