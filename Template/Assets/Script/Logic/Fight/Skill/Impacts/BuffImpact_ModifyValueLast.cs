using UnityEngine;
using System.Collections;

using Tables;
using GameBase;
namespace GameLogic
{
    public class BuffImpact_ModifyValueLast :BuffImpact_Base
    {
        private int _ModifiedValue = 0;
        private BuffImpact_ModifyValueLastRecord _ImpactRecord;

        #region 

        public override void InitBuff(BuffInfoRecord buffRecord, FightObj_Player sender, FightObj_Player reciver)
        {
            _ImpactRecord = TableReader.BuffImpact_ModifyValueLast.GetRecord(buffRecord.Impact.ID);

            if (_ImpactRecord == null)
                return;

            if (_ImpactRecord.TargetValue == PLAYER_VALUE_TYPE.EVALUATION)
            {
                ErrorManager.PushAndDisplayError("BuffImpact_ModifyValueLast TargetValue Cant modify last!! " + _ImpactRecord.TargetValue.ToString());
                return;
            }

            int sourceValue = 0;
            if (_ImpactRecord.SourceType == SKILL_TARGET_TYPE.Self)
            {
                sourceValue = sender.GetValue(_ImpactRecord.SourceValue);
            }
            else if (_ImpactRecord.SourceType == SKILL_TARGET_TYPE.Enemy)
            {
                sourceValue = sender.GetValue(_ImpactRecord.SourceValue);
            }
            else if(_ImpactRecord.ModifyType == IMPACT_MODIFY_TYPE.FIXED)
            {
                sourceValue = (int)_ImpactRecord.ModifyValue;
            }

            if (_ImpactRecord.ModifyType == IMPACT_MODIFY_TYPE.PERSEND)
            {
                sourceValue = (int)(sourceValue * _ImpactRecord.ModifyValue);
            }

            if (_ImpactRecord.TargetValue != PLAYER_VALUE_TYPE.EVALUATION)
            {
                _ModifiedValue = reciver.AddValue(_ImpactRecord.TargetValue, sourceValue);
            }

        }

        public override void DispatchBuff(FightObj_Player buffOwner)
        {
            base.DispatchBuff(buffOwner);

            buffOwner.AddValue(_ImpactRecord.TargetValue, -_ModifiedValue);
        }

        #endregion
    }
}
