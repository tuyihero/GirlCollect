using System.Collections;
using System.Collections.Generic;

using GameBase;
using Tables;
namespace GameLogic
{
    public class EquipPack : DataPackBase
    {
        #region 单例

        private static EquipPack _Instance;
        public static EquipPack Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new EquipPack();
                }
                return _Instance;
            }
        }

        private EquipPack() { }

        #endregion

        #region equips

        [SaveField(1)]
        private List<EquipInfo> _EquipList = new List<EquipInfo>();

        public List<EquipInfo> EquipList { get { return _EquipList; } }

        public void AddEquip(EquipInfoRecord equipRecord)
        {
            var equipInfo = new EquipInfo(equipRecord);
            _EquipList.Add(equipInfo);
        }

        public bool RemoveEquip(EquipInfo equip)
        {
            _EquipList.Remove(equip);
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
            foreach (var equip in Tables.TableReader.EquipInfo.Records)
            {
                AddEquip(equip.Value);
            }
        }

        #endregion events

    }
}
