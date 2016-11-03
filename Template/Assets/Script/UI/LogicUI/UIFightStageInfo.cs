using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

using GameBase;
using GameLogic;
namespace GameUI
{

    public class UIFightStageInfo : UIBase
    {
        #region static funs

        public static void ShowAsyn(FightStageInfo stageInfo)
        {
            Hashtable hash = new Hashtable();
            hash.Add("InitObj", stageInfo);
            GameCore.Instance.UIManager.ShowUI("LogicUI/UIFightStageInfo", hash);
        }

        #endregion

        #region params

        private FightStageInfo _StageInfo;

        #endregion

        #region show funs

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            var info = (FightStageInfo)hash["InitObj"];
            InitInfo(info);
        }

        public void InitInfo(FightStageInfo info)
        {
            if (info == null)
                return;

            _StageInfo = info;
        }

        #endregion

        #region inact

        public void BtnStartFight()
        {
            _StageInfo.StartFighting();
        }

        #endregion
    }
}
