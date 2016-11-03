using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GameBase;
using GameLogic;

namespace GameUI
{
    public class UIGirlMemberPack : UIBase
    {
        #region static funs

        public static void ShowAsyn()
        {
            Hashtable hash = new Hashtable();
            GameCore.Instance.UIManager.ShowUI("LogicUI/UIGirlMemberPack", hash);
        }

        //public static List<EVENT_TYPE> GetShowEvent()
        //{
        //    List<EVENT_TYPE> showEvents = new List<EVENT_TYPE>();

        //    showEvents.Add(EVENT_TYPE.EVENT_LOGIC_LOGIC_START);

        //    return showEvents;
        //}

        #endregion

        #region params

        public UIContainerBase _Container;

        #endregion

        #region show funs

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            var showList = GirlMemberPack.Instance.GetGirls();
            ShowList(showList);

            GirlMemberPack.Instance.UpdateMember();
        }

        public void ShowList(List<GirlMemberInfo> showList)
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

        public void SelectItem(object item)
        {
            
            GirlMemberInfo girlInfo = (GirlMemberInfo)item;

            UIGirlMemberInfo.ShowAsyn(girlInfo);
            
        }

        #endregion

        #region filter

        public void BtnShowFilterPanel()
        {
            UIFilterBoxPack.ShowAsyn(EnsureFilter);
        }

        public void EnsureFilter(List<int> filters)
        {
            var showList = GirlMemberPack.Instance.GetGirls();
            GirlMemberPack.SortByFilter(ref showList, filters);

            ShowList(showList);
        }

        #endregion


    }
}
