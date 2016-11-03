using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

using GameBase;
using GameLogic;

using Tables;
namespace GameUI
{
    public class UIFightCostumSkillItem : UIItemBase
    {
        #region 

        public Image _BG;
        public Text _SkillName;
        
        public SkillInfoRecord _SkillRecord;

        #endregion

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            var record = (SkillInfoRecord)hash["InitObj"];
            if (record == null)
                return;

            InitItemInfo(record);
        }

        public virtual void InitItemInfo(SkillInfoRecord record)
        {
            if (record == null)
                return;

            _SkillRecord = record;

            _SkillName.text = _SkillRecord.Name;

        }

    }
}
