using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GameBase;
using GameLogic;

namespace GameUI
{
    public class UIGirlSelectPanel : UIBase
    {
        #region static funs

        public static void ShowAsyn(SelectGirlDelegate selectDelegate, SelectGirlDelegate disSelectDelegate, List<GirlMemberInfo> selectedList, bool isCloseAfterSelect)
        {
            Hashtable hash = new Hashtable();
            hash.Add("SelectGirlDelegate", selectDelegate);
            hash.Add("isCloseAfterSelect", isCloseAfterSelect);
            hash.Add("SelectedList", selectedList);
            hash.Add("DisselectDelegate", disSelectDelegate);
            GameCore.Instance.UIManager.ShowUI("LogicUI/UIGirlSelectPanel", hash);
        }

        public static void ShowFilter(SelectGirlDelegate selectDelegate, List<int> filter)
        {
            Hashtable hash = new Hashtable();
            hash.Add("SelectGirlDelegate", selectDelegate);
            hash.Add("Filter", filter);
            hash.Add("isCloseAfterSelect", true);
            hash.Add("SelectedList", null);
            hash.Add("DisselectDelegate", null);
            GameCore.Instance.UIManager.ShowUI("LogicUI/UIGirlSelectPanel", hash);
        }

        public static void HideAsyn()
        {
            GameCore.Instance.UIManager.HideUI("LogicUI/UIGirlSelectPanel");
        }

        #endregion

        #region params

        public UIContainerSelect _ContainerGroup;

        public GameObject _HideBtn;

        public Animator _Animator;

        #endregion

        #region show funs

        public override void Show(Hashtable hash)
        {
            base.Show(hash);
            transform.SetAsLastSibling();

            _SelectGirlCallBack = (SelectGirlDelegate)hash["SelectGirlDelegate"];
            _CloseAfterSelect = (bool)hash["isCloseAfterSelect"];
            var selectedList = (List<GirlMemberInfo>)hash["SelectedList"];
            _DisSelectGirlCallBack = (SelectGirlDelegate)hash["DisselectDelegate"];
            

            var group = GirlMemberPack.Instance.GirlList;
            if (hash.ContainsKey("Filter"))
            {
                List<int> filter = (List<int>)hash["Filter"];
                GirlMemberPack.SortByFilter(ref group, filter);
            }
            _ContainerGroup.InitSelectContent(group, selectedList, SelectGroupGirl, UnSelectGroupGirl);

            _Animator.Play("MoveIn", 0);

            _HideBtn.SetActive(true);

        }

        public void ShowList(List<GirlMemberInfo> showList)
        {
            var selecteds = _ContainerGroup.GetSelecteds<GirlMemberInfo>();
            _ContainerGroup.InitSelectContent(showList, selecteds, SelectGroupGirl, UnSelectGroupGirl);
        }

        public override void Hide()
        {
            //base.Hide();

            _Animator.Play("MoveOut", 0);

            _HideBtn.SetActive(false);
        }

        public override void PreLoad()
        {
            base.PreLoad();

            _ContainerGroup.PreLoadItem(10);
        }

        public void Refresh()
        {

        }
        #endregion

        #region select girl

        public delegate void SelectGirlDelegate(GirlMemberInfo selectGirl);
        private SelectGirlDelegate _SelectGirlCallBack;
        private SelectGirlDelegate _DisSelectGirlCallBack;

        private bool _CloseAfterSelect = true;

        public void SelectGroupGirl(object item)
        {
            GirlMemberInfo record = (GirlMemberInfo)item;
            if (_SelectGirlCallBack != null)
            {
                _SelectGirlCallBack(record);
            }

            if (_CloseAfterSelect)
            {
                Hide();
            }
            else
            {
                //_ContainerGroup.RefreshItems();
            }
        }

        public void UnSelectGroupGirl(object item)
        {
            GirlMemberInfo record = (GirlMemberInfo)item;
            if (_DisSelectGirlCallBack != null)
            {
                _DisSelectGirlCallBack(record);
            }

            //_ContainerGroup.RefreshItems();
        }

        #endregion

        #region filter

        public void BtnShowFilterPanel()
        {
            UIFilterBoxPack.ShowAsyn(EnsureFilter);
        }

        public void EnsureFilter(List<int> filters)
        {
            var showList = GirlMemberPack.Instance.GetGirls();
            GirlMemberPack.SortByFilter(ref showList, filters);

            ShowList(showList);
        }

        #endregion
    }
}
