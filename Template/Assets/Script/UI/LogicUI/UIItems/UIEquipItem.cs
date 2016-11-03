using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

using GameBase;
using GameLogic;

using Tables;
namespace GameUI
{
    public class UIEquipItem : UIItemBase
    {
        #region 

        public Image _EquipIcon;
        public Text _EquipName;
        public Text _EquipDesc;

        private GameLogic.EquipInfo _EqiupInfo;
        #endregion

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            var equipInfo = (GameLogic.EquipInfo)hash["InitObj"];
            InitItemInfo(equipInfo);
        }

        public void InitItemInfo(GameLogic.EquipInfo equipInfo)
        {
            if (equipInfo == null)
                return;

            _EqiupInfo = equipInfo;
            _EquipIcon.sprite = ResourceManager.Instance.GetSprite(_EqiupInfo.EquipRecord.Icon);
            _EquipName.text = _EqiupInfo.EquipRecord.Name;
            _EquipDesc.text = _EqiupInfo.EquipRecord.Desc;

        }

        public void ShowIsLock()
        {
            
        }
    }
}
