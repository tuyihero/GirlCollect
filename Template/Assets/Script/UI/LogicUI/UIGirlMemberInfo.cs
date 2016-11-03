using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

using GameBase;
using GameLogic;
namespace GameUI
{

    public class UIGirlMemberInfo : UIBase
    {
        #region static funs

        public static void ShowAsyn(GirlMemberInfo girlInfo)
        {
            Hashtable hash = new Hashtable();
            hash.Add("GirlInfo", girlInfo);
            GameCore.Instance.UIManager.ShowUI("LogicUI/UIGirlMemberInfo", hash);
        }

        #endregion

        #region params

        public UIGirlMemberItem _UIGirlItem;
        public UIEquipItem _EquipItem;

        private GirlMemberInfo _GirlInfo;

        #endregion

        #region show funs

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            var girlInfo = (GirlMemberInfo)hash["GirlInfo"];
            InitGirlInfo(girlInfo);
        }

        public void InitGirlInfo(GirlMemberInfo girlInfo)
        {
            if (girlInfo == null)
                return;

            _GirlInfo = girlInfo;

            _UIGirlItem.InitGirl(_GirlInfo);

            if (_GirlInfo.EquipInfo != null)
            {
                _EquipItem.InitItemInfo(_GirlInfo.EquipInfo);
            }
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

        public void BtnLevelUp()
        {
            _GirlInfo.LevelUp();
        }

        public void BtnEquip()
        {
            UIEquipPack.ShowAsyn(SelectEquip);
        }


        #endregion

        #region 

        public bool SelectEquip(EquipInfo equipInfo)
        {
            return _GirlInfo.PutOnEquip(equipInfo);
        }

        #endregion
    }
}
