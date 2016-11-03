using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

using GameBase;
using GameLogic;

using Tables;
namespace GameUI
{
    public class UIFightStageItem : UIItemBase
    {
        #region 

        public Image _FightStageIcon;
        public Text _FightStageName;
        public Text _FightStageDesc;
        public GameObject _DisableObj;

        private FightStageInfo _FightStage;
        private bool _IsCanClick = true;

        #endregion

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            var data = (FightStageInfo)hash["InitObj"];
            if (data == null)
                return;

            InitData(data);
        }

        public virtual void InitData(FightStageInfo data)
        {
            if (data == null)
                return;

            _FightStage = data;

            if (_FightStageIcon != null)
            {
                //_FightStageIcon.sprite = ResourceManager.Instance.GetSprite(_FightStage.FigetRecord);
            }

            if (_FightStageName != null)
            {
                _FightStageName.text = _FightStage.FigetRecord.Name;
            }

            if (_FightStageDesc != null)
            {
                _FightStageDesc.text = _FightStage.FigetRecord.Desc;
            }

            if (!data.IsCanFight())
            {
                _DisableObj.SetActive(true);
                _IsCanClick = false;
            }
            else
            {
                _DisableObj.SetActive(false);
                _IsCanClick = true;
            }
            
        }

        public void ShowIsLock()
        {
            
        }

        public override void OnItemClick()
        {
            if(_IsCanClick)
                base.OnItemClick();
        }
    }
}
