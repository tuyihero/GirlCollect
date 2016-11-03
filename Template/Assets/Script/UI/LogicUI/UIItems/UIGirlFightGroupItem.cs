using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

using GameBase;
using GameLogic;

using Tables;
namespace GameUI
{
    public class UIGirlFightGroupItem : UIItemBase
    {
        #region 

        public Image _GirlFightGroupIcon;
        public Text _GirlFightGroupName;
        public Text _GirlFightGroupDesc;
        
        public GirlMemberInfo _FightGirlInfo;

        #endregion

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            var record = (GirlMemberInfo)hash["InitObj"];
            if (record == null)
                return;

            InitInfo(record);
        }

        public virtual void InitInfo(GirlMemberInfo record)
        {
            if (record == null)
                return;

            _FightGirlInfo = record;

            if (_GirlFightGroupIcon != null)
            {
                _GirlFightGroupIcon.sprite = ResourceManager.Instance.GetSprite(_FightGirlInfo.GirlInfoRecord.Icon);
            }

            if (_GirlFightGroupName != null)
            {
                _GirlFightGroupName.text = _FightGirlInfo.GirlInfoRecord.Name;
            }

            if (_GirlFightGroupDesc != null)
            {
                _GirlFightGroupDesc.text = _FightGirlInfo.GirlInfoRecord.Desc;
            }
            
        }

        public void ShowIsLock()
        {
            
        }
    }
}
