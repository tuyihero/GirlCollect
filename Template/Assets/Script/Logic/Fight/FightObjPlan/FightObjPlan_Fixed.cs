using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Tables;
namespace GameLogic
{
    public class FightObjPlan_Fixed : FightObjPlan_Base
    {
        private FightObjPlan_FixedRecord _ObjRecord;

        public override void Init(string id)
        {
            _ObjRecord = TableReader.FightObjPlan_Fixed.GetRecord(id);
        }

        //public override GuestInfoRecord GetGuest()
        //{
        //    return _ObjRecord.GuestInfo;
        //}

        //public override List<FightGirlInfo> GetEnemyGirl()
        //{

        //    FightGirlInfo fightGirl = new FightGirlInfo(_ObjRecord.GirlInfo.Id);
        //    List<FightGirlInfo> fightgirls = new List<FightGirlInfo>();
        //    fightgirls.Add(fightGirl);
        //    return fightgirls;
        //}
        public override GuestInfoRecord GetGuest()
        {
            GuestInfoRecord guestRecord = new GuestInfoRecord(null);

            int num = Random.Range(1, 6);
            guestRecord.GuestNum = num;

            int randomAttr = Random.Range(0, 3);
            for (int i = 0; i < 3; ++i)
            {
                if (i == randomAttr)
                    continue;

                int isA = Random.Range(0, 2);
                if (i == 0 && isA == 1)
                {
                    guestRecord.Attr1AAttract = 10;
                }
                else if (i == 0 && isA == 0)
                {
                    guestRecord.Attr1BAttract = 10;
                }
                else if (i == 1 && isA == 1)
                {
                    guestRecord.Attr2AAttract = 10;
                }
                else if (i == 1 && isA == 0)
                {
                    guestRecord.Attr2BAttract = 10;
                }
                else if (i == 2 && isA == 1)
                {
                    guestRecord.Attr3AAttract = 10;
                }
                else if (i == 2 && isA == 0)
                {
                    guestRecord.Attr3BAttract = 10;
                }
            }

            randomAttr = Random.Range(0, 3);
            for (int i = 0; i < 3; ++i)
            {
                if (i == randomAttr)
                    continue;

                int isA = Random.Range(0, 2);
                if (i == 0 && isA == 1)
                {
                    guestRecord.Attr1APoint= 10;
                }
                else if (i == 0 && isA == 0)
                {
                    guestRecord.Attr1BPoint = 10;
                }
                else if (i == 1 && isA == 1)
                {
                    guestRecord.Attr2APoint = 10;
                }
                else if (i == 1 && isA == 0)
                {
                    guestRecord.Attr2BPoint = 10;
                }
                else if (i == 2 && isA == 1)
                {
                    guestRecord.Attr3APoint = 10;
                }
                else if (i == 2 && isA == 0)
                {
                    guestRecord.Attr3BPoint = 10;
                }
            }

            return guestRecord;
        }

        public override List<GirlMemberInfo> GetEnemyGirl()
        {
            var randomIds = GameBase.GameRandom.GetIndependentRandoms(0, TableReader.GirlInfo.Records.Count, 3);

            
            List<GirlMemberInfo> fightgirls = new List<GirlMemberInfo>();

            foreach (var randomID in randomIds)
            {
                GirlMemberInfo fightGirl = new GirlMemberInfo(randomID.ToString());
                fightgirls.Add(fightGirl);
            }
            
            return fightgirls;
        }
    }
}
