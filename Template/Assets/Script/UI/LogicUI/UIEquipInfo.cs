using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

using GameBase;
using GameLogic;
namespace GameUI
{

    public class UIEquipInfo : UIBase
    {
        #region static funs

        public static void ShowAsyn(EquipInfo equipInfo)
        {
            Hashtable hash = new Hashtable();
            hash.Add("EquipInfo", equipInfo);
            GameCore.Instance.UIManager.ShowUI("LogicUI/UIEquipInfo", hash);
        }

        #endregion

        #region params

        private UIEquipItem _EquipItem;

        #endregion

        #region show funs

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            var info = (EquipInfo)hash["EquipInfo"];
            InitEquipInfo(info);
        }

        public void InitEquipInfo(EquipInfo info)
        {
            if (info == null)
                return;



        }
        #endregion

        #region logic event

        protected override void InitEvent()
        {
            base.InitEvent();

            //GameCore.Instance.EventController.RegisteEvent(EVENT_TYPE.EVENT_UI_HIDED, UIHide_Event);
        }

        private void UIHide_Event(object sender, Hashtable hash)
        {
            
        }

        #endregion

        #region inact



        #endregion
    }
}
