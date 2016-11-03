using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

using GameBase;
using GameLogic;

using Tables;
namespace GameUI
{
    public class UIFightCapterItem : UIItemBase
    {
        #region 

        public Image _FightCapterIcon;
        public Text _FightCapterName;
        public Text _FightCapterDesc;
        public GameObject _DisableGO;
        
        public FightCapterRecord _FightCapterRecord;
        private bool _IsCanFight;

        #endregion

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            var record = (FightCapterRecord)hash["InitObj"];
            if (record == null)
                return;

            InitItemInfo(record);
        }

        public virtual void InitItemInfo(FightCapterRecord record)
        {
            if (record == null)
                return;

            _FightCapterRecord = record;

            if (_FightCapterIcon != null)
            {
                _FightCapterIcon.sprite = ResourceManager.Instance.GetSprite(_FightCapterRecord.Icon);
            }

            if (_FightCapterName != null)
            {
                _FightCapterName.text = _FightCapterRecord.Name;
            }

            if (_FightCapterDesc != null)
            {
                _FightCapterDesc.text = _FightCapterRecord.Desc;
            }

            if (_DisableGO != null)
            {
                if (!FightStagePack.Instance.IsCanCapterFight(record))
                {
                    _DisableGO.SetActive(true);
                    _IsCanFight = false;
                }
                else
                {
                    _DisableGO.SetActive(false);
                    _IsCanFight = true;
                }
            }
        }

        public void ShowIsLock()
        {
            
        }

        public override void OnItemClick()
        {
            if(_IsCanFight)
                base.OnItemClick();
        }
    }
}
