using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GameBase;
using GameLogic;

namespace GameUI
{
    public class UIEquipPack : UIBase
    {
        #region static funs

        public static void ShowAsyn(SelectEquip selectEquip)
        {
            Hashtable hash = new Hashtable();
            hash.Add("SelectEquip", selectEquip);
            GameCore.Instance.UIManager.ShowUI("LogicUI/UIEquipPack", hash);
        }

        public static void HideAsyn()
        {

        }

        #endregion

        #region params

        public UIContainerBase _Container;

        #endregion

        #region show funs

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            if (hash.Contains("SelectEquip"))
            {
                _SelectEquip = (SelectEquip)hash["SelectEquip"];
            }

            var showList = EquipPack.Instance.EquipList;
            ShowList(showList);
        }

        public void ShowList(List<EquipInfo> showList)
        {
            _Container.InitContentItem(showList, SelectItem);
        }

        public override void PreLoad()
        {
            base.PreLoad();

            _Container.PreLoadItem(1);
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

        #region selectItem

        public delegate bool SelectEquip(EquipInfo equipInfo);
        public SelectEquip _SelectEquip;

        public void SelectItem(object item)
        {

            EquipInfo equipInfo = (EquipInfo)item;

            if (_SelectEquip != null)
            {
                if (_SelectEquip(equipInfo))
                {
                    Hide();
                }
            }
            
        }

        #endregion


    }
}
