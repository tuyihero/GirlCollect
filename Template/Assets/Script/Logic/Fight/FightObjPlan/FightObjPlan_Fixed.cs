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

        public override GuestInfoRecord GetGuest()
        {
            return _ObjRecord.GuestInfo;
        }

        public override FightGirlInfo GetEnemyGirl()
        {
            GirlMemberInfo enemyInfo = new GirlMemberInfo(_ObjRecord.GirlInfo.Id);
            enemyInfo.Level = _ObjRecord.GirlLevel;

            FightGirlInfo fightGirl = new FightGirlInfo(enemyInfo);
            return fightGirl;
        }
    }
}
