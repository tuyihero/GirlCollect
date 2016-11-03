using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

using GameBase;
using GameLogic;

using Tables;
namespace GameUI
{
    public class UICaptureSceneGirlItem : UIItemSelect
    {
        #region 

        public Image _CaptureSceneIcon;
        public Text _CaptureSceneName;
        public Text _CaptureSceneDesc;
        
        public GirlInfoRecord _GirlRecord;

        #endregion

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            var info = (GirlInfoRecord)hash["InitObj"];
            if (info == null)
                return;

            InitRecord(info);
        }

        public virtual void InitRecord(GirlInfoRecord info)
        {
            if (info == null)
                return;

            _GirlRecord = info;

            if (_CaptureSceneIcon != null)
            {
                _CaptureSceneIcon.sprite = ResourceManager.Instance.GetSprite(_GirlRecord.Icon.ToString());
            }

            if (_CaptureSceneName != null)
            {
                _CaptureSceneName.text = _GirlRecord.Name;
            }

            if (_CaptureSceneDesc != null)
            {
                _CaptureSceneDesc.text = _GirlRecord.Desc;
            }
            
        }

        public void ShowIsLock()
        {
            
        }
    }
}
