using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

using GameBase;
using GameLogic;
namespace GameUI
{

    public class UIFightFinish : UIBase
    {
        #region static funs

        public static void ShowAsyn()
        {
            Hashtable hash = new Hashtable();
            GameCore.Instance.UIManager.ShowUI("LogicUI/UIFightFinish", hash);
        }

        public static List<EVENT_TYPE> GetShowEvent()
        {
            List<EVENT_TYPE> showEvents = new List<EVENT_TYPE>();

            showEvents.Add(EVENT_TYPE.EVENT_LOGIC_PASS_STAGE);

            return showEvents;
        }

        #endregion

        #region params

        public GameObject _WinPanel;
        public GameObject _FailPanel;
        public Text _SelfPoint;
        public Text _EnemyPoint;
        public Text _EveluatPoin;
        public Image _EveluatImg;
        public Image _NewRecordImg;
        public UICurrencyItem _AwardCold;
        //public UICurrencyItem _AwardLuxury;


        #endregion

        #region show funs

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            var eventType = (EVENT_TYPE)hash["EVENT_TYPE"];
            int selfPoint = (int)hash["SelfPoint"];
            int enemyPoint = (int)hash["EnemyPoint"];
            int evalute = (int)hash["Evalute"];
            bool isNewRecord = false;
            if (hash.ContainsKey("IsNewRecord"))
            {
                isNewRecord = (bool)hash["IsNewRecord"];
            }
            bool isWin = (bool)hash["IsWin"];
            int goldAward = (int)hash["GoldAward"];
            InitInfo(isWin, selfPoint, enemyPoint, evalute, isNewRecord, goldAward);
        }

        public void InitInfo(bool isWin, int selfPoint, int enemyPoint, int evalut, bool isNewRecord, int goldAward)
        {
            if (isWin)
            {
                _WinPanel.SetActive(true);
                _FailPanel.SetActive(false);
            }
            else
            {
                _WinPanel.SetActive(false);
                _FailPanel.SetActive(true);
            }

            _SelfPoint.text = selfPoint.ToString();
            _EnemyPoint.text = enemyPoint.ToString();
            _EveluatPoin.text = (selfPoint - enemyPoint).ToString();
            _EveluatImg.sprite = GetEvaluteSprite(evalut);
            _NewRecordImg.gameObject.SetActive(isNewRecord);
            _AwardCold.ShowCurrency(Tables.CURRENCY_TYPE.GOLD, goldAward);
        }

        public Sprite GetEvaluteSprite(int evalute)
        {
            return ResourceManager.Instance.GetSprite("Evalute" + evalute);
        }

        #endregion

        #region inact

        public void BtnBack()
        {
            LogicManager.Instance.ExitFight();
            Hide();
        }

        #endregion
    }
}
