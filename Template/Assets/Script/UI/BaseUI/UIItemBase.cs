using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

using GameBase;
using System;

namespace GameUI
{
    public class UIItemBase : UIBase, IPointerClickHandler
    {
        public virtual void Refresh()
        {

        }
       
        #region click

        public delegate void ItemClick(object initInfo);
        public delegate void PanelClick(UIItemBase initInfo);

        public object _InitInfo;
        public ItemClick _ClickEvent;
        public PanelClick _PanelClickEvent;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnItemClick();
        }

        public virtual void OnItemClick()
        {
            if (_ClickEvent != null)
            {
                _ClickEvent(_InitInfo);
            }

            if (_PanelClickEvent != null)
            {
                _PanelClickEvent(this);
            }
        }

        #endregion
    }
}
