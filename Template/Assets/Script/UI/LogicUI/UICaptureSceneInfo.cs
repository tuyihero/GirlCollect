using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

using GameBase;
using GameLogic;
using Tables;
namespace GameUI
{

    public class UICaptureSceneInfo : UIBase
    {
        #region static funs

        public static void ShowAsyn(GameLogic.GirlCaptureScene captureScene)
        {
            Hashtable hash = new Hashtable();
            hash.Add("CaptureScene", captureScene);
            GameCore.Instance.UIManager.ShowUI("LogicUI/UICaptureSceneInfo", hash);
        }

        #endregion

        #region params

        public UIContainerBase _GirlsContainer;

        private GameLogic.GirlCaptureScene _GirlCaptureScene;
        private GirlInfoRecord _SelectedGirl;

        private Dictionary<GirlCapturersRecord, GirlInfoRecord> _CapturerDict = new Dictionary<GirlCapturersRecord, GirlInfoRecord>();
        #endregion

        #region show funs

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            var captureScene = (GameLogic.GirlCaptureScene)hash["CaptureScene"];

            InitCapture(captureScene);
        }

        public void InitCapture(GameLogic.GirlCaptureScene captureScene)
        {
            if (captureScene == null)
                return;

            _GirlCaptureScene = captureScene;

            var showGirls = _GirlCaptureScene.AppearGirls;
            _GirlsContainer.InitContentItem(showGirls, CaptureGirlClick);
        }

        #endregion

        #region logic event

        protected override void InitEvent()
        {
            base.InitEvent();

            //GameCore.Instance.EventController.RegisteEvent(EVENT_TYPE.EVENT_UI_HIDED, UIHide_Event);
        }

        #endregion

        #region 

        public void CaptureGirlClick(object obj)
        {
            GirlMemberInfo girlInfo = (GirlMemberInfo)obj;

            if (girlInfo != null)
            {
                UICaptureGirlInfo.ShowAsyn(girlInfo, _GirlCaptureScene);
            }
        }

        #endregion

    }
}
