using UnityEngine;
using System.Collections;

namespace GameLogic
{
    public class BuffImpact_Base
    {
        public int LastRound;

        #region 

        public virtual void InitBuff(Tables.BuffInfoRecord buffRecord, FightObj_Player sender, FightObj_Player reciver)
        {

        }

        public virtual void DispatchBuff(FightObj_Player buffOwner)
        {

        }

        public virtual void RoundAttract(ref FightManager.RoundResult roundResult, FightObj_Player sender, FightObj_Player reciver)
        {

        }

        public virtual void RoundPoint(ref FightManager.RoundResult roundResult, FightObj_Player sender, FightObj_Player reciver)
        {

        }

        #endregion
    }
}
