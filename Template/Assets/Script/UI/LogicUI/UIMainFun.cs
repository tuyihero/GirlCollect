using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using GameBase;
using GameLogic;
namespace GameUI
{
    public class UIMainFun : UIConflictBase
    {

        #region static funs

        public static List<EVENT_TYPE> GetShowEvent()
        {
            List<EVENT_TYPE> showEvents = new List<EVENT_TYPE>();

            showEvents.Add(EVENT_TYPE.EVENT_LOGIC_LOGIC_START);

            return showEvents;
        }

        #endregion

        #region 

        public Text[] _Currencies;

        #endregion

        #region 

        public override void Show(Hashtable hash)
        {
            base.Show(hash);
        }

        public void Update()
        {
            UpdateCurrency();
        }

        #endregion

        #region logic

        public void UpdateCurrency()
        {
            _Currencies[0].text = PlayerData.Instance.GetCurrency(Tables.CURRENCY_TYPE.GOLD).ToString();
            _Currencies[1].text = PlayerData.Instance.GetCurrency(Tables.CURRENCY_TYPE.LUXURY).ToString();
            _Currencies[2].text = PlayerData.Instance.GetCurrency(Tables.CURRENCY_TYPE.DIAMOND).ToString();
        }


        #endregion

        #region event

        //fight
        public void BtnFight()
        {
            //GameCore.Instance.UIManager.ShowUI("LogicUI/UIModeSelect");
            UIFightCapterPack.ShowAsyn();
        }

        //lottery
        public void BtnLottery()
        {
            GameCore.Instance.UIManager.ShowUI("LogicUI/UILottery");
        }

        //shop
        public void BtnShop()
        {
            GameCore.Instance.UIManager.ShowUI("LogicUI/UIShopRes");
        }

        //mission
        public void BtnMission()
        {
            UIMissionPack.ShowAsyn();
        }

        //mission
        public void BtnGirlPack()
        {
            UIGirlMemberPack.ShowAsyn();
        }

        //capture
        public void BtnCapture()
        {
            UICaptureScenePack.ShowAsyn();
        }

        //SpecialFight
        public void BtnSpecialFight()
        {
            UISpecilFightInfo.ShowAsyn();
        }

        //webcamFight
        public void BtnWebcamFight()
        {
            UIWebcamFightInfo.ShowAsyn();
        }

        //EliteFight
        public void BtnEliteFight()
        {
            UIFightStagePack.ShowAsyn(Tables.TableReader.FightCapter.EliteCapter, Vector3.zero);
        }

        #endregion

        #region 

        int clickCount = 0;
        public void BtnTest()
        {
            ++clickCount;
            UIErrorTips.ShowAsyn("Error:" + clickCount.ToString());
        }

        public void BtnAddRes()
        {
            PlayerData.Instance.AddCurrency(Tables.CURRENCY_TYPE.DIAMOND, 50);
            PlayerData.Instance.AddCurrency(Tables.CURRENCY_TYPE.LUXURY, 500);
            PlayerData.Instance.AddCurrency(Tables.CURRENCY_TYPE.GOLD, 5000);
        }

        #endregion
    }
}
