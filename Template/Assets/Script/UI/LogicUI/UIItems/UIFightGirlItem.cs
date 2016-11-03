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

        public GameObject _MaskGO;
        
        public FightGirlInfo _GirlMenberInfo;
        
        #endregion

        public virtual void InitGirl(FightGirlInfo info)
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
                _MaskGO.SetActive(false);
                return;
            }

            _GirlMenberInfo = info;

            if (_GirlMemberIcon != null)
            {
                //_GirlMemberIcon.sprite = ResourceManager.Instance.GetSprite(_GirlMenberInfo.GirlInfoRecord.Icon);
            }

            if (_GirlMemberName != null)
            {
                _GirlMemberName.text = _GirlMenberInfo._GirlInfo.GirlInfoRecord.Name;
            }

            if (_GirlMemberDesc != null)
            {
                _GirlMemberDesc.text = _GirlMenberInfo._GirlInfo.GirlInfoRecord.Desc;
            }

            var attrDic = _GirlMenberInfo._GirlInfo.GetAdvantageAttrs();
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

            if (_GirlMenberInfo._HasMask)
            {
                _MaskGO.SetActive(true);
            }
            else
            {
                _MaskGO.SetActive(false);
            }
        }

        public void ShowWithOutMask()
        {
            _MaskGO.SetActive(false);
        }

        public void ShowIsLock()
        {
            
        }
    }
}
