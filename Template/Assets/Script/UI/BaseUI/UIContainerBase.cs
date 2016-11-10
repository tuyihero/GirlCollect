using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GameUI
{
    public class UIContainerBase : UIBase
    {
        public class ContentPos
        {
            public Vector2 Pos;
            public object Obj;

            public UIItemBase ShowItem;
        }

        #region calculate rect

        private float _ViewPortWidth = 0;
        private float _ViewPortHeight = 0;
        private float _ContentCanvasWidth = 0;
        private float _ContentCanvasHeight = 0;
        private int _ContentRow = 0;
        private int _ContentColumn = 0;

        private int _ShowRowCount = 0;
        private int _ShowColumnCount = 0;

        private int _InitItemCount = 0;

        private void CalculateRect(ICollection items)
        {
            int itemCount = items.Count;

            _ViewPortWidth = _ScrollTransform.sizeDelta.x;
            _ViewPortHeight = _ScrollTransform.sizeDelta.y;

            _ContentColumn = 1;
            _ContentRow = 1;
            if (_LayoutGroup.constraint == GridLayoutGroup.Constraint.FixedRowCount)
            {
                _ContentColumn = _LayoutGroup.constraintCount;
                _ContentRow = itemCount / _ContentColumn;
                if (itemCount % _ContentColumn > 0)
                {
                    ++_ContentRow;
                }
            }
            else if (_LayoutGroup.constraint == GridLayoutGroup.Constraint.FixedColumnCount)
            {
                _ContentRow = _LayoutGroup.constraintCount;
                _ContentColumn = itemCount / _ContentRow;
                if (itemCount % _ContentRow > 0)
                {
                    ++_ContentColumn;
                }
            }

            _ValueList.Clear();
            int i = 0;
            var enumerator = items.GetEnumerator();
            enumerator.Reset();
            while (enumerator.MoveNext())
            //for (int i = 0; i < itemCount; ++i)
            {
                int column = i / _ContentRow;
                int row = i % _ContentRow;

                var contentPos = new ContentPos();
                contentPos.Pos = new Vector2(
                    row * (_LayoutGroup.cellSize.x + _LayoutGroup.spacing.x) + _LayoutGroup.cellSize.x * 0.5f,
                    - column * (_LayoutGroup.cellSize.y + _LayoutGroup.spacing.y) - _LayoutGroup.cellSize.y * 0.5f);
                contentPos.Obj = enumerator.Current;
                _ValueList.Add(contentPos);

                ++i;
            }


            _ContentCanvasWidth = _LayoutGroup.cellSize.x * _ContentRow + _LayoutGroup.spacing.x * (_ContentRow - 1);
            _ContentCanvasHeight = _LayoutGroup.cellSize.y * _ContentColumn + _LayoutGroup.spacing.y * (_ContentColumn - 1);

            _ShowRowCount = (int)Math.Ceiling((_ViewPortWidth + _LayoutGroup.spacing.x) / (_LayoutGroup.cellSize.x + _LayoutGroup.spacing.x)) + 2;
            _ShowColumnCount = (int)Math.Ceiling((_ViewPortHeight + _LayoutGroup.spacing.y) / (_LayoutGroup.cellSize.y + _LayoutGroup.spacing.y)) + 2;

            _InitItemCount = _ShowRowCount * 2 + _ShowColumnCount * 2 + 4;
            _InitItemCount = Math.Min(_InitItemCount, itemCount);

            _ContainerObj.sizeDelta = new Vector2(_ContentCanvasWidth, _ContentCanvasHeight);

            float containerPosX = 0;
            float containerPosY = 0;
            if (_ContainerObj.sizeDelta.x < _ScrollTransform.sizeDelta.x)
            {
                containerPosX = (_ScrollTransform.sizeDelta.x - _ContainerObj.sizeDelta.x) * 0.5f;
            }
            if (_ContainerObj.sizeDelta.y < _ScrollTransform.sizeDelta.y)
            {
                containerPosY = (_ContainerObj.sizeDelta.y - _ScrollTransform.sizeDelta.y) * 0.5f;
            }
            _ContainerObj.transform.localPosition = new Vector3(containerPosX, containerPosY, 0);

            Debug.Log("CalculateRect viewPortWidth:" + _ShowRowCount + "," + _ShowColumnCount + "," + _ContentCanvasWidth + "," + _ContentCanvasHeight);

        }

        #endregion

        #region 

        public Vector3 GetItemPosition(object item)
        {
            foreach (UIItemBase uiItem in _ItemPrefabList)
            {
                if (uiItem._InitInfo == item)
                {
                    return uiItem.transform.position;
                }
            }
            return Vector3.zero;
        }

        #endregion

        public ScrollRect _ScrollRect;
        public RectTransform _ScrollTransform;
        public GridLayoutGroup _LayoutGroup;
        public GameObject _ContainItemPre;
        public RectTransform _ContainerObj;

        protected List<UIItemBase> _ItemPrefabList = new List<UIItemBase>();
        protected List<ContentPos> _ValueList = new List<ContentPos>();
        protected UIItemBase.ItemClick _OnClickItem;

        public virtual void InitContentItem(ICollection valueList, UIItemBase.ItemClick onClick = null, Hashtable exhash = null)
        {
            CalculateRect(valueList);

            _OnClickItem = onClick;

            InitItems();

            _LayoutGroup.enabled = false;

            ShowItems();

            RefreshItems();
        }

        public virtual void ShowItems()
        {
            int showIdx = 0;
            foreach (var pos in _ValueList)
            {
                if (_ContainerObj.localPosition.x + pos.Pos.x >= -_LayoutGroup.cellSize.x
                    && _ContainerObj.localPosition.x + pos.Pos.x <= _ViewPortWidth + _LayoutGroup.cellSize.x
                    && -_ContainerObj.localPosition.y - pos.Pos.y >= -_LayoutGroup.cellSize.y
                    && -_ContainerObj.localPosition.y - pos.Pos.y <= _ViewPortHeight + _LayoutGroup.cellSize.y)
                {
                    _ItemPrefabList[showIdx].transform.localPosition = new Vector3(pos.Pos.x, pos.Pos.y, 0);
                    if (_ItemPrefabList[showIdx]._InitInfo != pos.Obj)
                    {
                        Hashtable hash = new Hashtable();
                        hash.Add("InitObj", pos.Obj);
                        _ItemPrefabList[showIdx].Show(hash);
                        _ItemPrefabList[showIdx]._InitInfo = pos.Obj;
                        _ItemPrefabList[showIdx]._ClickEvent = _OnClickItem;

                        pos.ShowItem = _ItemPrefabList[showIdx];
                    }
                    showIdx++;
                }
                else
                {
                    pos.ShowItem = null;
                }
            }
        }

        public virtual void RefreshItems()
        {
            foreach (var item in _ItemPrefabList)
            {
                item.Refresh();
            }
        }

        public void PreLoadItem(int preLoadCount)
        {
            InitItems();
        }

        private void InitItems()
        {
            if (_InitItemCount > _ItemPrefabList.Count)
            {
                for (int i = _ItemPrefabList.Count; i < _InitItemCount; ++i)
                {
                    GameObject obj = GameObject.Instantiate<GameObject>(_ContainItemPre);
                    obj.transform.parent = _ContainerObj.transform;
                    obj.transform.localScale = new Vector3(1, 1, 1);
                    obj.transform.localPosition = new Vector3(1000, 1000, 1);
                    var itemScript = obj.GetComponent<UIItemBase>();

                    _ItemPrefabList.Add(itemScript);
                }
            }
            else
            {
                for (int i = 0; i < _ItemPrefabList.Count; ++i)
                {
                    _ItemPrefabList[i].transform.localPosition = new Vector3(1000, 1000, 1);
                }
            }
        }

        public T GetContainItem<T>(int idx)
        {
            return _ItemPrefabList[idx].GetComponent<T>();
        }

        public void ForeachActiveItem<T>(Action<T> action)
        {
            for (int i = 0; i < _ItemPrefabList.Count; ++i)
            {
                if (_ItemPrefabList[i].gameObject.activeSelf)
                {
                    action((_ItemPrefabList[i].gameObject.GetComponent<T>()));
                }
            }
        }

        public Vector3 GetObjItemPos(object obj)
        {
            foreach (var item in _ItemPrefabList)
            {
                if (item._InitInfo == obj)
                {
                    return item.transform.position;
                }
            }
            return Vector3.zero;
        }
    }
}
