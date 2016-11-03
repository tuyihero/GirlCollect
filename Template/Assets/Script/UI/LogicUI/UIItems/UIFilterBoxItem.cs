using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

using GameBase;
using GameLogic;

using Tables;
namespace GameUI
{
    public class UIFilterBoxItem : UIItemSelect
    {
        #region 

        public Image _FilterBoxIcon;
        public Text _FilterBoxName;
        public Text _FilterBoxDesc;
        
        public string _FilterStr;

        #endregion

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            var str = (string)hash["InitObj"];
            if (str == null)
                return;

            InitItemInfo(str);
        }

        public virtual void InitItemInfo(string str)
        {
            if (string.IsNullOrEmpty(str))
                return;

            _FilterStr = str;

            //if (_FilterBoxIcon != null)
            //{
            //    _FilterBoxIcon.sprite = ResourceManager.Instance.GetSprite(_FightCapterRecord.Icon);
            //}

            if (_FilterBoxName != null)
            {
                _FilterBoxName.text = _FilterStr;
            }

            //if (_FilterBoxDesc != null)
            //{
            //    _FilterBoxDesc.text = _FightCapterRecord.Desc;
            //}
            
        }

        public void ShowIsLock()
        {
            
        }
    }
}
