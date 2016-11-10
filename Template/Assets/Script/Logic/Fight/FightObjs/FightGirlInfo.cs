using UnityEngine;
using System.Collections;

namespace GameLogic
{
    public class FightGirlInfo : GirlMemberInfo
    {
        public const int MIN_ATTR_VALUE = 0;
        public const int MAX_ATTR_VALUE = 999999;

        public FightGirlInfo(string recordID) :base(recordID)
        {
            
        }

        #region Show

        public bool _HasMask = false;

        #endregion
    }
}