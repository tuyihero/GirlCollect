using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using GameBase;
using GameLogic;

namespace GameUI
{
    public class UIFilterBoxPack : UIBase
    {
        #region static funs

        public static void ShowAsyn(EnsureFilter ensureFilter)
        {
            Hashtable hash = new Hashtable();
            hash.Add("EnsureFilter", ensureFilter);
            GameCore.Instance.UIManager.ShowUI("LogicUI/UIFilterBoxPack", hash);
        }

        #endregion

        #region params

        public Toggle[] _Filters;

        #endregion

        #region show funs

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            _EnsureFilter = (EnsureFilter)hash["EnsureFilter"];
            transform.SetAsLastSibling();
        }

        public override void PreLoad()
        {
            base.PreLoad();
        }

        #endregion

        #region event

        public delegate void EnsureFilter(List<int> filters);
        private EnsureFilter _EnsureFilter;

        public void BtnOK()
        {
            if (_EnsureFilter != null)
            {
                List<int> filtes = new List<int>();
                for (int i = 0; i < _Filters.Length; ++i)
                {
                    if (_Filters[i].isOn)
                    {
                        filtes.Add(i);
                    }
                }
                _EnsureFilter(filtes);
            }

            Hide();
        }

        #endregion


    }
}
