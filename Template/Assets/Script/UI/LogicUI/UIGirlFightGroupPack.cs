using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GameBase;
using GameLogic;

namespace GameUI
{
    public class UIGirlFightGroupPack : UIBase
    {
        #region static funs

        public static void ShowAsyn()
        {
            Hashtable hash = new Hashtable();
            GameCore.Instance.UIManager.ShowUI("LogicUI/UIGirlFightGroupPack", hash);
        }

        #endregion

        #region params

        public UIContainerBase _ContainerGroup;
        public UIContainerBase _ContainerMember;

        public GameObject _MemberPanel;

        #endregion

        #region show funs

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            AutoAddGroup();

            var group = GirlMemberPack.Instance.NormalFightGroup;
            _ContainerGroup.InitContentItem(group, SelectGroupGirl);

            var member = GirlMemberPack.Instance.GetGirlsNotInFight();
            _ContainerMember.InitContentItem(member, SelectMemberGirl);
        }

        public override void PreLoad()
        {
            base.PreLoad();

            _ContainerGroup.PreLoadItem(10);
            _ContainerMember.PreLoadItem(10);
        }

        #endregion

        #region swap group girl

        private GirlMemberInfo _SwapingGril;

        public void SelectGroupGirl(object item)
        {
            GirlMemberInfo record = (GirlMemberInfo)item;
            _SwapingGril = record;
            _MemberPanel.SetActive(true);
            Debug.Log("SelectGroupGirl:" + record.GirlInfoRecord.Id);
        }

        public void SelectMemberGirl(object item)
        {
            GirlMemberInfo record = (GirlMemberInfo)item;

            if (_SwapingGril != null)
            {
                if (GirlMemberPack.Instance.SwapGroupGirl(_SwapingGril, record))
                {
                    _MemberPanel.SetActive(false);
                }
            }

            Debug.Log("SelectMemberGirl:" + record.GirlInfoRecord.Id);
        }

        public void AutoAddGroup()
        {
            if (GirlMemberPack.Instance.GroupGirlCount > GirlMemberPack.Instance.NormalFightGroup.Count)
            {
                var member = GirlMemberPack.Instance.GetGirlsNotInFight();
                for (int i = 0; i < GirlMemberPack.Instance.GroupGirlCount - GirlMemberPack.Instance.NormalFightGroup.Count; ++i)
                {
                    if (member.Count > i)
                    {
                        GirlMemberPack.Instance.AddGroupGirl(member[i]);
                    }
                }
            }
        }

        #endregion

        #region filter

        public void BtnShowFilterPanel()
        {
            UIFilterBoxPack.ShowAsyn(EnsureFilter);
        }

        public void EnsureFilter(List<int> filters)
        {
            string filterStr = "";
            foreach (var filte in filters)
            {
                filterStr += filte + ",";
            }

            Debug.Log("EnsureFilter:" + filterStr);
        }

        #endregion
    }
}
