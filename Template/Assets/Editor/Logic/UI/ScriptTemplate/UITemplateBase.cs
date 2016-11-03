using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

using GameBase;
namespace GameUI
{

    public class UITemplate : UIBase
    {
        #region static funs

        public static void ShowAsyn()
        {
            Hashtable hash = new Hashtable();
            GameCore.Instance.UIManager.ShowUI("TemplatePath", hash);
        }

        public static List<EVENT_TYPE> GetShowEvent()
        {
            List<EVENT_TYPE> showEvents = new List<EVENT_TYPE>();

            showEvents.Add(EVENT_TYPE.EVENT_LOGIC_LOGIC_START);

            return showEvents;
        }

        #endregion

        #region params

        

        #endregion

        #region show funs

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

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
