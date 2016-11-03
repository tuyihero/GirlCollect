using UnityEngine;
using System.Collections;

namespace GameLogic
{
    public class FightGirlInfo
    {
        public const int MIN_ATTR_VALUE = 0;
        public const int MAX_ATTR_VALUE = 999999;

        public FightGirlInfo(GirlMemberInfo girlInfo)
        {
            _GirlInfo = girlInfo;

            Attr1A = _GirlInfo.Attr1A;
            Attr1B = _GirlInfo.Attr1B;
            Attr2A = _GirlInfo.Attr2A;
            Attr2B = _GirlInfo.Attr2B;
            Attr3A = _GirlInfo.Attr3A;
            Attr3B = _GirlInfo.Attr3B;
        }
        #region Attr

        public GirlMemberInfo _GirlInfo;

        public int ColdDownRound;

        private int _Attr1A = 0;
        public int Attr1A
        {
            get
            {
                return _Attr1A;
            }
            set
            {
                _Attr1A = Mathf.Clamp(value, MIN_ATTR_VALUE, MAX_ATTR_VALUE);
            }
        }

        private int _Attr1B = 0;
        public int Attr1B
        {
            get
            {
                return _Attr1B;
            }
            set
            {
                _Attr1B = Mathf.Clamp(value, MIN_ATTR_VALUE, MAX_ATTR_VALUE);
            }
        }

        private int _Attr2A = 0;
        public int Attr2A
        {
            get
            {
                return _Attr2A;
            }
            set
            {
                _Attr2A = Mathf.Clamp(value, MIN_ATTR_VALUE, MAX_ATTR_VALUE);
            }
        }

        private int _Attr2B = 0;
        public int Attr2B
        {
            get
            {
                return _Attr2B;
            }
            set
            {
                _Attr2B = Mathf.Clamp(value, MIN_ATTR_VALUE, MAX_ATTR_VALUE);
            }
        }

        private int _Attr3A = 0;
        public int Attr3A
        {
            get
            {
                return _Attr3A;
            }
            set
            {
                _Attr3A = Mathf.Clamp(value, MIN_ATTR_VALUE, MAX_ATTR_VALUE);
            }
        }

        private int _Attr3B = 0;
        public int Attr3B
        {
            get
            {
                return _Attr3B;
            }
            set
            {
                _Attr3B = Mathf.Clamp(value, MIN_ATTR_VALUE, MAX_ATTR_VALUE);
            }
        }

        #endregion Attr

        #region Show

        public bool _HasMask = false;

        #endregion
    }
}