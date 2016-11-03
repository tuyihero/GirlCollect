using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GameBase;
using GameLogic;

using Tables;
namespace GameUI
{
    public class UIFightStagePack : UIBase
    {
        #region static funs

        public static void ShowAsyn(FightCapterRecord capterRecord, Vector3 initPos)
        {
            Hashtable hash = new Hashtable();
            hash.Add("InitObj", capterRecord);
            hash.Add("InitPos", initPos);
            GameCore.Instance.UIManager.ShowUI("LogicUI/UIFightStagePack", hash);
        }

        #endregion

        #region params

        public UIContainerBase _Container;
        public GameObject _ContentPanel;
        public UIFightCapterItem _CapterItem;

        #endregion

        #region show funs

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            var record = (FightCapterRecord)hash["InitObj"];
            var initPos = (Vector3)hash["InitPos"];
            if (record == null)
                return;

            var showList = FightStagePack.Instance.GetCapterStages(record);
            _Container.InitContentItem(showList, SelectItem);
            _CapterItem.InitItemInfo(record);
            _CapterItem.transform.position = initPos;

            StartAnimation();
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

        #endregion

        #region selectItem

        public void SelectItem(object item)
        {
            FightStageInfo info = (FightStageInfo)item;

            //UIFightStageInfo.ShowAsyn(info);
            info.StartFighting();

        }

        #endregion

        #region Animation

        public Vector3 ContentPanelInitPos;
        public Vector3 ContentPanelMoveToPos;
        public Vector3 CapterItemMoveToPos;
        public float CpaterMoveToSpeed;

        public float START_WAIT_TIME = 0.5f;
        public float START_MOVE_TIME = 0.5f;

        private void StartAnimation()
        {
            _ContentPanel.transform.localPosition = ContentPanelInitPos;
            _ContentPanel.gameObject.SetActive(false);
            //Invoke("StartAnimation2", START_WAIT_TIME);
            StartAnimation2();
        }

        private void StartAnimation2()
        {
            _ContentPanel.gameObject.SetActive(true);

            Hashtable hashContent = new Hashtable();
            hashContent.Add("position", ContentPanelMoveToPos);
            hashContent.Add("time", START_MOVE_TIME);
            hashContent.Add("islocal", true);
            iTween.MoveTo(_ContentPanel, hashContent);

            float distance = Mathf.Abs( _CapterItem.transform.position.x - CapterItemMoveToPos.x);
            float speed = distance / START_MOVE_TIME;

            Hashtable hash = new Hashtable();
            hash.Add("position", CapterItemMoveToPos);
            hash.Add("speed", speed);
            hash.Add("islocal", true);
            iTween.MoveTo(_CapterItem.gameObject, hash);
        }

        #endregion


    }
}
