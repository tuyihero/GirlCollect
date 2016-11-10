using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Tables;
using GameLogic;

namespace GameUI
{
    public class UIFightCustom : UIItemBase
    {
        #region

        public UIFightCustomAnimation _CustomAnimation;
        public UIContainerBase _CustomAtractAttrs;
        public UIContainerBase _CustomPointAttrs;
        public UIContainerBase _CustomLikeSkill;

        private GuestInfoRecord _ShowGuest;
        #endregion

        #region 

        public void ShowCustom(GuestInfoRecord guestInfo)
        {
            InitCustom(guestInfo);
        }

        public void InitCustom(GuestInfoRecord guestInfo)
        {
            _ShowGuest = guestInfo;

            if (_ShowGuest != null)
            {
                gameObject.SetActive(true);

                _CustomAtractAttrs.InitContentItem(GetSortedAtractAttr());
                _CustomPointAttrs.InitContentItem(GetSortedPointAttr());

                _CustomLikeSkill.InitContentItem(guestInfo.GetNotNullSkills());

                if (_CustomAnimation != null)
                {
                    _CustomAnimation.ShowCustomCount(guestInfo.GuestNum);

                    _CustomAnimation.StartPlaySwing();
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public void PlayCustomMove(int leftCount, int rightCount, UIFightCustomAnimation.MoveAnimCallBack moveAnimCallBack)
        {
            _CustomAnimation.StartMoveAnim(leftCount, rightCount, moveAnimCallBack);
        }

        #endregion

        private class AttrShow
        {
            public int AttrValue;
            public string AttrName;
        }

        private List<string> GetSortedAtractAttr()
        {
            List<AttrShow> attrList = new List<AttrShow>();

            if (_ShowGuest.Attr1AAttract > _ShowGuest.Attr1BAttract)
            {
                AttrShow attr = new AttrShow();
                attr.AttrValue = (int)_ShowGuest.Attr1AAttract;
                attr.AttrName = GirlMemberInfo.ATTR_NAMES[0];
                attrList.Add(attr);
            }
            else if (_ShowGuest.Attr1AAttract < _ShowGuest.Attr1BAttract)
            {
                AttrShow attr = new AttrShow();
                attr.AttrValue = (int)_ShowGuest.Attr1BAttract;
                attr.AttrName = GirlMemberInfo.ATTR_NAMES[1];
                attrList.Add(attr);
            }

            if (_ShowGuest.Attr2AAttract > _ShowGuest.Attr2BAttract)
            {
                AttrShow attr = new AttrShow();
                attr.AttrValue = (int)_ShowGuest.Attr2AAttract;
                attr.AttrName = GirlMemberInfo.ATTR_NAMES[2];
                attrList.Add(attr);
            }
            else if (_ShowGuest.Attr2AAttract < _ShowGuest.Attr2BAttract)
            {
                AttrShow attr = new AttrShow();
                attr.AttrValue = (int)_ShowGuest.Attr2BAttract;
                attr.AttrName = GirlMemberInfo.ATTR_NAMES[3];
                attrList.Add(attr);
            }

            if (_ShowGuest.Attr3AAttract > _ShowGuest.Attr3BAttract)
            {
                AttrShow attr = new AttrShow();
                attr.AttrValue = (int)_ShowGuest.Attr3AAttract;
                attr.AttrName = GirlMemberInfo.ATTR_NAMES[4];
                attrList.Add(attr);
            }
            else if (_ShowGuest.Attr3AAttract < _ShowGuest.Attr3BAttract)
            {
                AttrShow attr = new AttrShow();
                attr.AttrValue = (int)_ShowGuest.Attr3BAttract;
                attr.AttrName = GirlMemberInfo.ATTR_NAMES[5];
                attrList.Add(attr);
            }

            attrList.Sort((attr1, attr2) =>
            {
                if (attr1.AttrValue > attr2.AttrValue)
                    return 1;
                else if (attr1.AttrValue < attr2.AttrValue)
                    return -1;
                return 0;
            });

            List<string> attrStrs = new List<string>();
            foreach (var attrInfo in attrList)
            {
                attrStrs.Add(attrInfo.AttrName);
            }
            return attrStrs;
        }

        private List<string> GetSortedPointAttr()
        {
            List<AttrShow> attrList = new List<AttrShow>();

            if (_ShowGuest.Attr1APoint > _ShowGuest.Attr1BPoint)
            {
                AttrShow attr = new AttrShow();
                attr.AttrValue = (int)_ShowGuest.Attr1APoint;
                attr.AttrName = GirlMemberInfo.ATTR_NAMES[0];
                attrList.Add(attr);
            }
            else if (_ShowGuest.Attr1APoint < _ShowGuest.Attr1BPoint)
            {
                AttrShow attr = new AttrShow();
                attr.AttrValue = (int)_ShowGuest.Attr1BPoint;
                attr.AttrName = GirlMemberInfo.ATTR_NAMES[1];
                attrList.Add(attr);
            }

            if (_ShowGuest.Attr2APoint > _ShowGuest.Attr2BPoint)
            {
                AttrShow attr = new AttrShow();
                attr.AttrValue = (int)_ShowGuest.Attr2APoint;
                attr.AttrName = GirlMemberInfo.ATTR_NAMES[2];
                attrList.Add(attr);
            }
            else if (_ShowGuest.Attr2APoint < _ShowGuest.Attr2BPoint)
            {
                AttrShow attr = new AttrShow();
                attr.AttrValue = (int)_ShowGuest.Attr2BPoint;
                attr.AttrName = GirlMemberInfo.ATTR_NAMES[3];
                attrList.Add(attr);
            }

            if (_ShowGuest.Attr3APoint > _ShowGuest.Attr3BPoint)
            {
                AttrShow attr = new AttrShow();
                attr.AttrValue = (int)_ShowGuest.Attr3APoint;
                attr.AttrName = GirlMemberInfo.ATTR_NAMES[4];
                attrList.Add(attr);
            }
            else if (_ShowGuest.Attr3APoint < _ShowGuest.Attr3BPoint)
            {
                AttrShow attr = new AttrShow();
                attr.AttrValue = (int)_ShowGuest.Attr3BPoint;
                attr.AttrName = GirlMemberInfo.ATTR_NAMES[5];
                attrList.Add(attr);
            }

            attrList.Sort((attr1, attr2) =>
            {
                if (attr1.AttrValue > attr2.AttrValue)
                    return 1;
                else if (attr1.AttrValue < attr2.AttrValue)
                    return -1;
                return 0;
            });

            List<string> attrStrs = new List<string>();
            foreach (var attrInfo in attrList)
            {
                attrStrs.Add(attrInfo.AttrName);
            }
            return attrStrs;
        }


    }
}
