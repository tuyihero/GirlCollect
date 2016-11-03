using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

using GameBase;
using GameLogic;
namespace GameUI
{

    public class UIErrorTips : UIBase
    {
        #region static funs

        public static void ShowAsyn(string errorMsg)
        {
            Hashtable hash = new Hashtable();
            hash.Add("ErrorMsg", errorMsg);
            GameCore.Instance.UIManager.ShowUI("LogicUI/UIErrorTips", hash);
        }

        #endregion

        #region params

        public Text[] _Texts;

        #endregion

        #region show funs

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            string errorMsg = (string)hash["ErrorMsg"];
            PushMessage(errorMsg);
        }

        public override void Hide()
        {
            base.Hide();

            foreach (var text in _Texts)
            {
                text.text = "";
            }
        }

        public void PushMessage(string msg)
        {
            if (string.IsNullOrEmpty(msg))
                return;

            int textCount = _Texts.Length;
            for (int i = 0; i < textCount - 1; ++i)
            {
                _Texts[i].text = _Texts[i + 1].text;
            }

            _Texts[textCount - 1].text = msg;
            ShowLast(2);
        }

        #endregion

    }
}
