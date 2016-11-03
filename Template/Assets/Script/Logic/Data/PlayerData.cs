using System.Collections;
using System.Collections.Generic;

using GameBase;
using Tables;
namespace GameLogic
{
    public class PlayerData : DataPackBase
    {
        #region 单例

        private static PlayerData _Instance;
        public static PlayerData Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new PlayerData();
                }
                return _Instance;
            }
        }

        private PlayerData() { }

        #endregion

        #region currency

        [SaveField(1)]
        private List<int> _Currencies = new List<int>();

        public int GetCurrency(CURRENCY_TYPE type)
        {
            return _Currencies[((int)type) - 1];
        }

        public void AddCurrency(CURRENCY_TYPE type, int value)
        {
            _Currencies[((int)type) - 1] += value;
        }

        public bool CanDecCurrency(CURRENCY_TYPE type, int value)
        {
            if (_Currencies[(int)type - 1] < value)
                return false;

            return true;
        }

        public bool TryDecCurrency(CURRENCY_TYPE type, int value)
        {
            if (_Currencies[(int)type - 1] < value)
                return false;

            _Currencies[(int)type - 1] -= value;
            return true;
        }

        public bool TryDecAll(int[] value)
        {
            for (int i = 0; i < value.Length; ++i)
            {
                if (_Currencies[i] < value[i])
                    return false;
            }

            for (int i = 0; i < value.Length; ++i)
            {
                _Currencies[i] -= value[i];
            }
            return true;
        }

        #endregion

        #region init

        public override void InitEvents()
        {
            base.InitEvents();

            GameCore.RegisterEvent(EVENT_TYPE.EVENT_TIMER_REGISTER_FIRST_LOGIN, RegisterFirstLogin);
        }

        public void RegisterFirstLogin(object sender, Hashtable hash)
        {
            _Currencies.Clear();
            for (int i = 0; i < (int)CURRENCY_TYPE.DIAMOND; ++i)
            {
                _Currencies.Add(50);
            }
        }

        #endregion

    }
}
