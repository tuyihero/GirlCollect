using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

using GameBase;
using GameLogic;
namespace GameUI
{

    public class UICaptureGirlInfo : UIBase
    {
        #region static funs

        public static void ShowAsyn(GirlMemberInfo girlInfo, GirlCaptureScene captureSceneInfo)
        {
            Hashtable hash = new Hashtable();
            hash.Add("GirlInfo", girlInfo);
            hash.Add("CaptureScene", captureSceneInfo);
            GameCore.Instance.UIManager.ShowUI("LogicUI/UICaptureGirlInfo", hash);
        }

        #endregion

        #region params

        public UIGirlMemberItem _GirlMemberItem;

        public UINumInput[] _Currencys;

        private GirlMemberInfo _GirlInfo;
        private GirlCaptureScene _CaptureScene;

        #endregion

        #region show funs

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            var girlInfo = (GirlMemberInfo)hash["GirlInfo"];
            _CaptureScene = (GirlCaptureScene)hash["CaptureScene"];

            InitGirlInfo(girlInfo);
        }

        public void InitGirlInfo(GirlMemberInfo girlInfo)
        {
            if (girlInfo == null)
                return;

            _GirlInfo = girlInfo;
            _GirlMemberItem.InitGirl(_GirlInfo);

            if(_Currencys.Length == 3)
            {
                _Currencys[0].Init(0, 0, PlayerData.Instance.GetCurrency(Tables.CURRENCY_TYPE.GOLD));
                _Currencys[1].Init(0, 0, PlayerData.Instance.GetCurrency(Tables.CURRENCY_TYPE.LUXURY));
                _Currencys[2].Init(0, 0, PlayerData.Instance.GetCurrency(Tables.CURRENCY_TYPE.DIAMOND));
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

        public void BtnOK()
        {
            if (_CaptureScene != null)
            {
                if (_CaptureScene.TryCaptureGirl(_GirlInfo, _Currencys[0].Value, _Currencys[1].Value, _Currencys[2].Value))
                    Hide();
            }
        }

        #endregion
    }
}
