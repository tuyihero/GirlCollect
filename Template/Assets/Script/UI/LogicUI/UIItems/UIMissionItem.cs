using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

using GameBase;
using GameLogic;

using Tables;
namespace GameUI
{
    public class UIMissionItem : UIItemBase
    {
        #region 

        public Image _MissionIcon;
        public Text _MissionName;
        public Text _MissionDesc;

        public Button _BtnGoto;
        public Button _BtnAchieve;
        public Button _BtnFinish;

        private GameLogic.MissionInfo _MissionInfo;
        #endregion

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            _MissionInfo = (GameLogic.MissionInfo)hash["InitObj"];

            InitItemInfo();
        }

        public virtual void InitItemInfo()
        {
            if (_MissionInfo == null)
                return;

            _MissionIcon.sprite = ResourceManager.Instance.GetSprite(_MissionInfo.MissionRecord.IconName);
            _MissionName.text = _MissionInfo.MissionRecord.Name;
            _MissionDesc.text = _MissionInfo.MissionRecord.Desc;

            if (_MissionInfo.MissionStatues == GameLogic.MissionInfo.MISSION_STATUES.MISSION_ACCEPT)
            {
                _BtnGoto.gameObject.SetActive(true);
                _BtnAchieve.gameObject.SetActive(false);
                _BtnFinish.gameObject.SetActive(false);
            }
            else if (_MissionInfo.MissionStatues == GameLogic.MissionInfo.MISSION_STATUES.MISSION_DONE)
            {
                _BtnGoto.gameObject.SetActive(false);
                _BtnAchieve.gameObject.SetActive(true);
                _BtnFinish.gameObject.SetActive(false);
            }
            else if (_MissionInfo.MissionStatues == GameLogic.MissionInfo.MISSION_STATUES.MISSION_GETTED_REWARD)
            {
                _BtnGoto.gameObject.SetActive(false);
                _BtnAchieve.gameObject.SetActive(false);
                _BtnFinish.gameObject.SetActive(true);
            }
        }

        public override void Refresh()
        {
            base.Refresh();

            if (_MissionInfo == null)
                return;

            if (_MissionInfo.MissionStatues == GameLogic.MissionInfo.MISSION_STATUES.MISSION_ACCEPT)
            {
                _BtnGoto.gameObject.SetActive(true);
                _BtnAchieve.gameObject.SetActive(false);
                _BtnFinish.gameObject.SetActive(false);
            }
            else if (_MissionInfo.MissionStatues == GameLogic.MissionInfo.MISSION_STATUES.MISSION_DONE)
            {
                _BtnGoto.gameObject.SetActive(false);
                _BtnAchieve.gameObject.SetActive(true);
                _BtnFinish.gameObject.SetActive(false);
            }
            else if (_MissionInfo.MissionStatues == GameLogic.MissionInfo.MISSION_STATUES.MISSION_GETTED_REWARD)
            {
                _BtnGoto.gameObject.SetActive(false);
                _BtnAchieve.gameObject.SetActive(false);
                _BtnFinish.gameObject.SetActive(true);
            }
        }

        public void ShowIsLock()
        {
            
        }

        #region 

        public void BtnGoto()
        {
            Hashtable hash = new Hashtable();
            GameCore.Instance.UIManager.ShowUI(_MissionInfo.MissionRecord.GoToUI, hash);
        }

        public void BtnAchieve()
        {
            _MissionInfo.MissionComplate();
            Refresh();
        }
        #endregion
    }
}
