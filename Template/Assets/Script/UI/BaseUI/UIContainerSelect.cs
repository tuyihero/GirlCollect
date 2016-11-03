using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GameUI
{
    public class UIContainerSelect : UIContainerBase
    {
        public bool _IsMultiSelect = false;
        private List<ContentPos> _Selecteds = new List<ContentPos>();

        public T GetSelected<T>()
        {
            if (!_IsMultiSelect && _Selecteds.Count > 0)
            {
                return (T)_Selecteds[0].Obj;
            }
            return default(T);
        }

        public List<T> GetSelecteds<T>()
        {
            List<T> selectedObjs = new List<T>();
            foreach (var selectedPos in _Selecteds)
            {
                selectedObjs.Add((T)selectedPos.Obj);
            }
            return selectedObjs;
        }

        public delegate void SelectedObjCallBack(object obj);
        private SelectedObjCallBack _SelectedCallBack;
        private SelectedObjCallBack _DisSelectedCallBack;

        public void OnSelectedObj(object obj)
        {
            ContentPos selectPos = _ValueList.Find((pos) =>
            {
                if (pos.Obj == obj)
                    return true;
                return false;
            });

            if (_IsMultiSelect)
            {
                if (_Selecteds.Contains(selectPos))
                {
                    if (selectPos.ShowItem != null)
                    {
                        ((UIItemSelect)selectPos.ShowItem).UnSelected();
                    }
                    _Selecteds.Remove(selectPos);
                }
                else
                {
                    if (selectPos.ShowItem != null)
                    {
                        ((UIItemSelect)selectPos.ShowItem).Selected();
                    }
                    _Selecteds.Add(selectPos);
                }
            }
            else
            {
                if (_Selecteds.Count > 0)
                {
                    ((UIItemSelect)_Selecteds[0].ShowItem).UnSelected();
                }
                _Selecteds.Clear();

                _Selecteds.Add(selectPos);
                ((UIItemSelect)_Selecteds[0].ShowItem).Selected();
            }

            if (_SelectedCallBack != null && _Selecteds.Contains(selectPos))
            {
                _SelectedCallBack(obj);
            }
            else if (_DisSelectedCallBack != null && !_Selecteds.Contains(selectPos))
            {
                _DisSelectedCallBack(obj);
            }
        }

        #region 

        public void InitSelectContent(ICollection list, ICollection selectedList, SelectedObjCallBack onSelect = null, SelectedObjCallBack onDisSelect = null, Hashtable exhash = null)
        {
            _SelectedCallBack = onSelect;
            _DisSelectedCallBack = onDisSelect;

            base.InitContentItem(list, OnSelectedObj, exhash);

            _Selecteds.Clear();
            if (selectedList != null)
            {
                foreach (var selectItem in selectedList)
                {
                    ContentPos selectPos = _ValueList.Find((pos) =>
                    {
                        if (pos.Obj == selectItem)
                            return true;
                        return false;
                    });
                    if (selectPos != null)
                        _Selecteds.Add(selectPos);
                }
            }
            ShowItems();
        }

        public override void ShowItems()
        {
            base.ShowItems();

            foreach (var shoItem in _ItemPrefabList)
            {
                var showObj = shoItem._InitInfo;
                ContentPos selectPos = _Selecteds.Find((pos) =>
                {
                    if (pos.Obj == showObj)
                        return true;
                    return false;
                });

                if (selectPos != null)
                {
                    ((UIItemSelect)shoItem).Selected();
                }
                else
                {
                    ((UIItemSelect)shoItem).UnSelected();
                }
            }
        }

        #endregion


    }
}
