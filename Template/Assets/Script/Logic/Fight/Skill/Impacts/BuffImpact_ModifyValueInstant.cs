using UnityEngine;
using System.Collections;

using Tables;
using GameBase;
namespace GameLogic
{
    public class BuffImpact_ModifyValueInstant : BuffImpact_Base
    {
        private BuffImpact_ModifyValueInstantRecord _ImpactRecord;

        #region 

        public override void InitBuff(BuffInfoRecord buffRecord, FightObj_Player sender, FightObj_Player reciver)
        {
            _ImpactRecord = TableReader.BuffImpact_ModifyValueInstant.GetRecord(buffRecord.Impact.ID);

            if (_ImpactRecord == null)
                return;

            if (_ImpactRecord.TargetValue != PLAYER_VALUE_TYPE.EVALUATION)
            {
                ErrorManager.PushAndDisplayError("BuffImpact_ModifyValueInstant TargetValue Cant modify instant!! " + _ImpactRecord.TargetValue.ToString());
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

            if (_ImpactRecord.TargetValue == PLAYER_VALUE_TYPE.EVALUATION)
            {
                reciver.EvaluationValue += sourceValue;

            }

        }

        #endregion
    }
}
