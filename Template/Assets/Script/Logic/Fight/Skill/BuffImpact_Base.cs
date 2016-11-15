using UnityEngine;
using System.Collections;

using Tables;
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

        public virtual void Calculate(GirlMemberInfo girl, GuestInfoRecord guest, BuffInfoRecord buff, int orgAttract, int orgPoint, out int outAttract, out int outPoint)
        {
            outAttract = orgAttract;
            outPoint = orgPoint;
        }

        #endregion
    }
}
