using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GameBase;
using GameLogic;

namespace GameUI
{
    public class UICaptureScenePack : UIBase
    {
        #region static funs

        public static void ShowAsyn()
        {
            Hashtable hash = new Hashtable();
            GameCore.Instance.UIManager.ShowUI("LogicUI/UICaptureScenePack", hash);
        }

        #endregion

        #region params

        public UIContainerBase _Container;

        #endregion

        #region show funs

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            GirlCapturePack.Instance.InitCaptureScenes();
            var showList = GirlCapturePack.Instance.CaptureList;
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
            GirlCaptureScene captureScene = (GirlCaptureScene)item;

            Debug.Log("SelectCapter:" + captureScene.CaptureSceneRecord.Id);

            UICaptureSceneInfo.ShowAsyn(captureScene);
            
        }

        #endregion


    }
}
