using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GameBase;
using GameLogic;

namespace GameUI
{
    public class UIFilterGirls : UIBase
    {
        #region params

        public UIContainerBase _Container;

        public UIFilterBoxPack _FilterBox;

        #endregion

        #region show funs

        public void ShowGirls(List<GirlMemberInfo> showGirls)
        {
            _Container.InitContentItem(showGirls);
            
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
            Tables.FightCapterRecord record = (Tables.FightCapterRecord)item;

            Debug.Log("SelectCapter:" + record.Id);
            
        }

        #endregion


    }
}
