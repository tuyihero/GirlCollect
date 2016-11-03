using UnityEngine;
using System.Collections;

using Tables;
using GameBase;
namespace GameLogic
{
    public class BuffImpact_ModifyAttract : BuffImpact_Base
    {
        private int _ModifiedValue = 0;
        private BuffImpact_ModifyAttractRecord _ImpactRecord;

        #region 

        public override void InitBuff(BuffInfoRecord buffRecord, FightObj_Player sender, FightObj_Player reciver)
        {
            _ImpactRecord = TableReader.BuffImpact_ModifyAttract.GetRecord(buffRecord.Impact.ID);
        }

        public override void RoundAttract(ref FightManager.RoundResult roundResult, FightObj_Player sender, FightObj_Player reciver)
        {
            FightManager.BuffResult buffResult = new FightManager.BuffResult();

            if (reciver == FightManager.Instance.SelfPlayer)
            {

            }
        }

        #endregion
    }
}
