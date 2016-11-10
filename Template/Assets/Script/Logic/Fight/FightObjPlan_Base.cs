using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Tables;
namespace GameLogic
{
    public class FightObjPlan_Base
    {
        public virtual void Init(string id)
        { }

        public virtual GuestInfoRecord GetGuest()
        {
            return null;
        }

        public virtual List<GirlMemberInfo> GetEnemyGirl()
        {
            return null;
        }
    }
}
