using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

using GameBase;
using GameLogic;

using Tables;
namespace GameUI
{
    public class UIFightGirlItem : UIItemSelect
    {
        #region 

        public Image _GirlMemberIcon;
        public Text _GirlMemberName;
        public Text _GirlMemberDesc;

        public Text[] _AttrText;
        public Text[] _AttrValue;
        public GameObject[] Skills;
        public Text[] _SkillNames;
        public Text Star;

        public GameObject _MaskGO;
        
        public GirlMemberInfo _GirlMenberInfo;
        
        #endregion

        public virtual void InitGirl(GirlMemberInfo info)
        {
            if (info == null)
            {
                _GirlMenberInfo = null;
                //_GirlMemberIcon.sprite = ResourceManager.Instance.GetSprite(_GirlMenberInfo.GirlInfoRecord.Icon);
                _GirlMemberName.text = "";
                _GirlMemberDesc.text = "";
                for (int i = 0; i < _AttrValue.Length; ++i)
                {
                    _AttrText[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < _SkillNames.Length; ++i)
                {
                    Skills[i].SetActive(false);
                }
                Star.text = "";
                //_MaskGO.SetActive(false);
                return;
            }

            _GirlMenberInfo = info;

            if (_GirlMemberIcon != null)
            {
                //_GirlMemberIcon.sprite = ResourceManager.Instance.GetSprite(_GirlMenberInfo.GirlInfoRecord.Icon);
            }

            if (_GirlMemberName != null)
            {
                _GirlMemberName.text = _GirlMenberInfo.GirlInfoRecord.Name;
            }

            if (_GirlMemberDesc != null)
            {
                _GirlMemberDesc.text = _GirlMenberInfo.GirlInfoRecord.Desc;
            }

            Star.text = _GirlMenberInfo.GirlInfoRecord.Star.ToString();

            for (int i = 0; i < _SkillNames.Length; ++i)
            {
                if (_GirlMenberInfo.GirlInfoRecord.Skills.Count <= i
                    || _GirlMenberInfo.GirlInfoRecord.Skills[i] == null)
                {
                    Skills[i].SetActive(false);
                }
                else
                {
                    Skills[i].SetActive(true);
                    _SkillNames[i].text = _GirlMenberInfo.GirlInfoRecord.Skills[i].Name;
                }

            }

            var attrDic = _GirlMenberInfo.GetAdvantageAttrs();
            int idx = 0;
            foreach (var attr in attrDic)
            {
                _AttrText[idx].gameObject.SetActive(true);
                _AttrText[idx].text = attr.Key;
                _AttrValue[idx].text = attr.Value.ToString();
                ++idx;
            }

            for (int i = idx; i < _AttrValue.Length; ++i)
            {
                _AttrText[i].gameObject.SetActive(false);
            }

            //if (_GirlMenberInfo._HasMask)
            //{
            //    _MaskGO.SetActive(true);
            //}
            //else
            //{
            //    _MaskGO.SetActive(false);
            //}
        }

        public void ShowWithOutMask()
        {
            _MaskGO.SetActive(false);
        }

        public void ShowIsLock()
        {
            
        }

        public void SetSkillCanUse(int idx, bool isCan)
        {
            if(isCan)
                _SkillNames[idx].color = new Color(0, 1, 0);
            else
                _SkillNames[idx].color = new Color(0, 0, 0);
        }
    }
}
