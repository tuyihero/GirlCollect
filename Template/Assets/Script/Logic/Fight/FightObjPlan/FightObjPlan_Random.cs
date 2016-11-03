using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Tables;
namespace GameLogic
{
    public class FightObjPlan_Random : FightObjPlan_Base
    {
        private FightObjPlan_RandomRecord _ObjRecord;

        public override void Init(string id)
        {
            _ObjRecord = TableReader.FightObjPlan_Random.GetRecord(id);
        }

        public override GuestInfoRecord GetGuest()
        {
            //var girls = GirlMemberPack.Instance.GetRandomGirlInStar(_ObjRecord.GirlStar, 1);
            //return girls[0];
            return null;
        }

        public override FightGirlInfo GetEnemyGirl()
        {
            var girls = GirlMemberPack.Instance.GetRandomGirlInStar(_ObjRecord.GirlStar, 1);

            FightGirlInfo fightGirl = new FightGirlInfo(new GirlMemberInfo(girls[0].Id));
            fightGirl._HasMask = true;

            return fightGirl;
        }

        private GuestInfoRecord GetRandomGuest(GirlInfoRecord girlInfo)
        {
            return null;
        }
    }
}
