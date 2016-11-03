using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

using GameBase;
using GameLogic;

using Tables;
namespace GameUI
{
    public class UITemplateItem : UIItemBase
    {
        #region 

        public Image _TemplateIcon;
        public Text _TemplateName;
        public Text _TemplateDesc;
        
        public FightCapterRecord _FightCapterRecord;

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

            if (_TemplateIcon != null)
            {
                _TemplateIcon.sprite = ResourceManager.Instance.GetSprite(_FightCapterRecord.Icon);
            }

            if (_TemplateName != null)
            {
                _TemplateName.text = _FightCapterRecord.Name;
            }

            if (_TemplateDesc != null)
            {
                _TemplateDesc.text = _FightCapterRecord.Desc;
            }
            
        }

        public void ShowIsLock()
        {
            
        }
    }
}
