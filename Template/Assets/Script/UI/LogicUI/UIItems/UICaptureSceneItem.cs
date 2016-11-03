using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

using GameBase;
using GameLogic;

using Tables;
namespace GameUI
{
    public class UICaptureSceneItem : UIItemBase
    {
        #region 

        public Image _CaptureSceneIcon;
        public Text _CaptureSceneName;
        public Text _CaptureSceneDesc;
        
        public GameLogic.GirlCaptureScene _CaptureScene;

        #endregion

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            var info = (GameLogic.GirlCaptureScene)hash["InitObj"];
            if (info == null)
                return;

            InitCaptureScene(info);
        }

        public virtual void InitCaptureScene(GameLogic.GirlCaptureScene info)
        {
            if (info == null)
                return;

            _CaptureScene = info;

            if (_CaptureSceneIcon != null)
            {
                _CaptureSceneIcon.sprite = ResourceManager.Instance.GetSprite(_CaptureScene.CaptureSceneRecord.Icon.ToString());
            }

            if (_CaptureSceneName != null)
            {
                _CaptureSceneName.text = _CaptureScene.CaptureSceneRecord.Name;
            }

            if (_CaptureSceneDesc != null)
            {
                _CaptureSceneDesc.text = _CaptureScene.CaptureSceneRecord.Desc;
            }
            
        }

        public void ShowIsLock()
        {
            
        }
    }
}
