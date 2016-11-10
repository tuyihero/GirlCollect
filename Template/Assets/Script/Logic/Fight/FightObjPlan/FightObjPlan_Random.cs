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

        public override List<GirlMemberInfo> GetEnemyGirl()
        {
            var girls = GirlMemberPack.Instance.GetRandomGirlInStar(_ObjRecord.GirlStar, 1);

            GirlMemberInfo fightGirl = new GirlMemberInfo(girls[0].Id);

            List<GirlMemberInfo> fightgirls = new List<GirlMemberInfo>();
            fightgirls.Add(fightGirl);
            return fightgirls;
        }

        private GuestInfoRecord GetRandomGuest(GirlInfoRecord girlInfo)
        {
            return null;
        }
    }
}
