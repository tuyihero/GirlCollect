using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

using GameBase;
using GameLogic;

using Tables;
namespace GameUI
{
    public class UIGirlMemberItem : UIItemSelect
    {
        #region 

        public Image _GirlMemberIcon;
        public Text _GirlMemberName;
        public Text _GirlMemberDesc;

        public Text[] _AttrText;
        public Text[] _AttrValue;
        public GameObject[] Skills;
        public Text[] _SkillNames;
        public Text _Desc;
        public Image _Star;
        public Text Star;

        public Image _LimitImg;
        
        public GirlMemberInfo _GirlMenberInfo;
        
        #endregion

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            var info = (GirlMemberInfo)hash["InitObj"];
            if (info == null)
                return;

            InitGirl(info);
        }

        public void InitGirl(GirlMemberInfo info)
        {
            if (info == null)
            {
                ClearGirlInfo();
                return;
            }

            _GirlMenberInfo = info;

            SetGirlInfo();
        }

        public override void Refresh()
        {
            base.Refresh();

            SetGirlInfo();
        }

        private void SetGirlInfo()
        {
            if (_GirlMenberInfo == null)
            {
                ClearGirlInfo();
                return;
            }

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

            if (_LimitImg != null)
            {
                _LimitImg.gameObject.SetActive(false);
                if (_GirlMenberInfo._FightCD > 0)
                {
                    _LimitImg.gameObject.SetActive(true);
                    _LimitImg.color = new Color(1, 0, 0);
                }
                //if (_GirlMenberInfo.GetVitalityRate() < 0.5f)
                //{
                //    _LimitImg.gameObject.SetActive(true);
                //    _LimitImg.color = new Color(0, 1, 0);
                //}
                //if (FightStagePack.Instance.IsGirlInSpecilFight(_GirlMenberInfo)
                //    || FightStagePack.Instance.IsGirlInWebcamFight(_GirlMenberInfo))
                //{
                //    _LimitImg.gameObject.SetActive(true);
                //    _LimitImg.color = new Color(0, 0, 1);
                //}
            }

            _Desc.text = _GirlMenberInfo.GirlInfoRecord.Desc;
        }

        public void ClearGirlInfo()
        {
            _GirlMenberInfo = null;

            if (_GirlMemberIcon != null)
            {
                _GirlMemberIcon.sprite = null;
            }

            if (_GirlMemberName != null)
            {
                _GirlMemberName.text = "";
            }

            if (_GirlMemberDesc != null)
            {
                _GirlMemberDesc.text = "";
            }

            for (int idx = 0; idx < _AttrText.Length; ++idx)
            {
                _AttrText[idx].gameObject.SetActive(true);
                _AttrText[idx].text = "";
                _AttrValue[idx].text = "";
            }

            for (int i = 0; i < _SkillNames.Length; ++i)
            {
                Skills[i].SetActive(false);
            }
            Star.text = "";
        }

        public void ShowIsLock()
        {
            
        }

        public void SetSkillCanUse(int idx, bool isCan)
        {
            if (isCan)
                _SkillNames[idx].color = new Color(0, 1, 0);
            else
                _SkillNames[idx].color = new Color(0, 0, 0);
        }

        #region select

        public override void OnItemClick()
        {
            if (_LimitImg != null && _LimitImg.gameObject.activeInHierarchy)
                return;

            base.OnItemClick();
        }

        #endregion
    }
}
