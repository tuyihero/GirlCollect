using UnityEngine;
using System.Collections;

using Tables;
namespace GameLogic
{
    public class Impact_ModifyCul : BuffImpact_Base
    {
        

        #region 

        public override void InitBuff(Tables.BuffInfoRecord buffRecord, FightObj_Player sender, FightObj_Player reciver)
        {

        }

        public override void DispatchBuff(FightObj_Player buffOwner)
        {

        }

        public override void RoundAttract(ref FightManager.RoundResult roundResult, FightObj_Player sender, FightObj_Player reciver)
        {

        }

        public override void RoundPoint(ref FightManager.RoundResult roundResult, FightObj_Player sender, FightObj_Player reciver)
        {

        }

        public override void Calculate(GirlMemberInfo girl, GuestInfoRecord guest, BuffInfoRecord buff, int orgAttract, int orgPoint, out int outAttract, out int outPoint)
        {
            Impact_ModifyCulRecord impactRecord = TableReader.Impact_ModifyCul.GetRecord(buff.Impact.ID);

            int valueAttract = 0;
            int valuePoint = 0;
            outAttract = orgAttract;
            outPoint = orgPoint;
            switch (impactRecord.ActTarget)
            {
                case IMPACT_MODIFY_TARGET.ATTR1A:
                    valueAttract = (int)(girl.Attr1A * ((impactRecord.ActPersent) * 0.0001f) * guest.Attr1AAttract);
                    valuePoint = (int)(girl.Attr1A * ((impactRecord.ActPersent) * 0.0001f) * guest.Attr1APoint);
                    outAttract += valueAttract;
                    outPoint += valuePoint;
                    break;
                case IMPACT_MODIFY_TARGET.ATTR1B:
                    valueAttract = (int)(girl.Attr1B * ((impactRecord.ActPersent) * 0.0001f) * guest.Attr1BAttract);
                    valuePoint = (int)(girl.Attr1B * ((impactRecord.ActPersent) * 0.0001f) * guest.Attr1BPoint);
                    outAttract += valueAttract;
                    outPoint += valuePoint;
                    break;
                case IMPACT_MODIFY_TARGET.ATTR2A:
                    valueAttract = (int)(girl.Attr2A * ((impactRecord.ActPersent) * 0.0001f) * guest.Attr2AAttract);
                    valuePoint = (int)(girl.Attr2A * ((impactRecord.ActPersent) * 0.0001f) * guest.Attr2APoint);
                    outAttract += valueAttract;
                    outPoint += valuePoint;
                    break;
                case IMPACT_MODIFY_TARGET.ATTR2B:
                    valueAttract = (int)(girl.Attr2B * ((impactRecord.ActPersent) * 0.0001f) * guest.Attr2BAttract);
                    valuePoint = (int)(girl.Attr2B * ((impactRecord.ActPersent) * 0.0001f) * guest.Attr2BPoint);
                    outAttract += valueAttract;
                    outPoint += valuePoint;
                    break;
                case IMPACT_MODIFY_TARGET.ATTR3A:
                    valueAttract = (int)(girl.Attr2A * ((impactRecord.ActPersent) * 0.0001f) * guest.Attr2AAttract);
                    valuePoint = (int)(girl.Attr2A * ((impactRecord.ActPersent) * 0.0001f) * guest.Attr2APoint);
                    outAttract += valueAttract;
                    outPoint += valuePoint;
                    break;
                case IMPACT_MODIFY_TARGET.ATTR3B:
                    valueAttract = (int)(girl.Attr3B * ((impactRecord.ActPersent) * 0.0001f) * guest.Attr3BAttract);
                    valuePoint = (int)(girl.Attr3B * ((impactRecord.ActPersent) * 0.0001f) * guest.Attr3BPoint);
                    outAttract += valueAttract;
                    outPoint += valuePoint;
                    break;
                case IMPACT_MODIFY_TARGET.ATTRACK:
                    valueAttract = (int)(orgAttract * ((impactRecord.ActPersent + 10000) * 0.0001f));
                    outAttract = valueAttract;
                    break;
                case IMPACT_MODIFY_TARGET.POINT:
                    valuePoint = (int)(orgPoint * ((impactRecord.ActPersent + 10000) * 0.0001f));
                    outPoint = valuePoint;
                    break;
            }
        }

        #endregion
    }
}
