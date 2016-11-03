using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

using GameBase;
using GameLogic;

using Tables;
namespace GameUI
{
    public class UICaptureSceneCapturerItem : UIItemBase
    {
        #region 

        public Image _CaptureSceneIcon;
        public Text _CaptureSceneName;
        public Text _CaptureSceneDesc;
        
        public GirlCapturersRecord _CaptureRecord;

        #endregion

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            var info = (GirlCapturersRecord)hash["InitObj"];
            if (info == null)
                return;

            InitRecord(info);
        }

        public virtual void InitRecord(GirlCapturersRecord info)
        {
            if (info == null)
                return;

            _CaptureRecord = info;

            if (_CaptureSceneIcon != null)
            {
                _CaptureSceneIcon.sprite = ResourceManager.Instance.GetSprite(_CaptureRecord.Icon.ToString());
            }

            if (_CaptureSceneName != null)
            {
                _CaptureSceneName.text = _CaptureRecord.Name;
            }

            if (_CaptureSceneDesc != null)
            {
                _CaptureSceneDesc.text = _CaptureRecord.Desc;
            }
            
        }

        public void ShowIsLock()
        {
            
        }
    }
}
