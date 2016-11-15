using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Tables;
namespace GameLogic
{
    public class BuffManager
    {
        private FightObj_Player _BuffOwner;
        public BuffManager(FightObj_Player owner)
        {
            _BuffOwner = owner;
        }

        #region 

        private List<BuffImpact_Base> _BuffList = new List<BuffImpact_Base>();

        public void PatchBuff(Tables.BuffInfoRecord buffRecord, FightObj_Player sender, FightObj_Player reciver)
        {
            var buffBase = (BuffImpact_Base)System.Activator.CreateInstance(System.Type.GetType("GameLogic." + buffRecord.Impact.TableName));
            buffBase.InitBuff(buffRecord, sender, reciver);
            if (buffBase != null && buffRecord.Last > 0)
            {
                buffBase.LastRound = buffRecord.Last;
                _BuffList.Add(buffBase);
            }
        }

        #endregion

        #region round event

        public void RoundInit()
        {
        }

        public void RoundStart(ref FightManager.RoundResult roundResult)
        { }

        private List<BuffImpact_Base> _DispatchBuff = new List<BuffImpact_Base>();
        public void RoundEnd(ref FightManager.RoundResult roundResult)
        {
            foreach (var buffImpact in _BuffList)
            {
                --buffImpact.LastRound;
                if (buffImpact.LastRound == 0)
                {
                    _DispatchBuff.Add(buffImpact);
                }
            }

            foreach (var dispatchBuff in _DispatchBuff)
            {
                dispatchBuff.DispatchBuff(_BuffOwner);
                _BuffList.Remove(dispatchBuff);
            }
        }

        public void RoundAttract(ref FightManager.RoundResult roundResult)
        { }

        public void RoundPoint(ref FightManager.RoundResult roundResult)
        { }

        #endregion

        #region ex calculate

        public void Calculate(GirlMemberInfo girl, GuestInfoRecord guest, BuffInfoRecord buffRecord, int orgAttract, int orgPoint, out int outAttract, out int outPoint)
        {
            var buffBase = (BuffImpact_Base)System.Activator.CreateInstance(System.Type.GetType("GameLogic." + buffRecord.Impact.TableName));
            buffBase.Calculate(girl, guest, buffRecord, orgAttract, orgPoint, out outAttract,  out outPoint);
        }

        #endregion
    }
}
