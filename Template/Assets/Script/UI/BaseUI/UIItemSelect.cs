using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

using GameBase;
using System;

namespace GameUI
{
    public class UIItemSelect : UIItemBase, IPointerClickHandler
    {
        public GameObject _SelectGO;

        #region select

        public virtual void Selected()
        {
            if (_SelectGO != null)
            {
                _SelectGO.SetActive(true);
            }
        }

        public virtual void UnSelected()
        {
            if (_SelectGO != null)
            {
                _SelectGO.SetActive(false);
            }
        }

        #endregion
    }
}
