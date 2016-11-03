using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

using GameBase;
using GameLogic;

using Tables;
namespace GameUI
{
    public class UIFightCostumAttrItem : UIItemBase
    {
        #region 

        public Image _BG;
        public Text _AttrName;
        
        public string _AttrStr;

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

            _AttrStr = str;

            _AttrName.text = _AttrStr;

        }

    }
}
