using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GameBase;
using GameLogic;

namespace GameUI
{
    public class UIFightCapterPack : UIBase
    {
        #region static funs

        public static void ShowAsyn()
        {
            Hashtable hash = new Hashtable();
            GameCore.Instance.UIManager.ShowUI("LogicUI/UIFightCapterPack", hash);
        }

        #endregion

        #region params

        public UIContainerBase _Container;

        #endregion

        #region show funs

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            var showList = Tables.TableReader.FightCapter.NormalCapter;
            _Container.InitContentItem(showList, SelectItem);

            iTween.FadeTo(gameObject, 1, 0);
        }

        public override void PreLoad()
        {
            base.PreLoad();

            _Container.PreLoadItem(1);
        }

        #endregion

        #region selectItem

        public void SelectItem(object item)
        {
            Tables.FightCapterRecord record = (Tables.FightCapterRecord)item;

            Debug.Log("SelectCapter:" + record.Id);

            UIFightStagePack.ShowAsyn(record, _Container.GetItemPosition(record));

            //FadeOut();
            Hide();
        }

        #endregion

        #region Animation

        private const float FADE_OUT_TIME = 0.5f;

        private void FadeOut()
        {
            iTween.FadeTo(gameObject, 0, FADE_OUT_TIME);
            ShowLast(FADE_OUT_TIME + 0.1f);
            
        }



        #endregion


    }
}
