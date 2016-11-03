using UnityEngine;
using System.Collections;

using Tables;
using System.Collections.Generic;

namespace GameLogic
{

    public class GirlMemberInfo
    {
        
        private string ID;
        [SaveField(1)]
        private GirlInfoRecord _GirlInfoRecord;
        public GirlInfoRecord GirlInfoRecord
        {
            get
            {
                if (_GirlInfoRecord == null)
                    _GirlInfoRecord = TableReader.GirlInfo.GetRecord(ID);
                return _GirlInfoRecord;
            }
        }

        public GirlMemberInfo(string recordID)
        {
            ID = recordID;

            CalculateAttr();
        }

        public GirlMemberInfo()
        {

        }

        public void InitFromLoad()
        {
            CalculateAttr();
        }

        #region level

        private const int MAX_LEVEL = 50;

        [SaveField(2)]
        private long _TotalExp = 0;

        private int _Level = -1;
        public int Level
        {
            get
            {
                if (_Level < 0)
                {
                    CalculateLevel();
                }
                return _Level;
            }
            set
            {
                _Level = value;
            }
        }

        public bool CanLevelUp()
        {
            if (_Level == MAX_LEVEL)
            {
                GameBase.ErrorManager.PushAndDisplayError("Level Max");
                return false;
            }

            var levelInfo = TableReader.GirlLevelInfo.GetTypeLevel(GirlInfoRecord.LevelInfoTypeID, _Level + 1);
            if (levelInfo == null)
            {
                GameBase.ErrorManager.PushAndDisplayError("LevelInfo Error");
                return false;
            }

            if(PlayerData.Instance.CanDecCurrency(CURRENCY_TYPE.GOLD, levelInfo.GoldCost))
            {
                GameBase.ErrorManager.PushAndDisplayError("Money not enough");
                return false;
            }

            return true;
        }

        public void LevelUp()
        {
            if (!CanLevelUp())
                return;

            var levelInfo = TableReader.GirlLevelInfo.GetTypeLevel(GirlInfoRecord.LevelInfoTypeID, _Level + 1);
            if (levelInfo == null)
                return;

            if (!PlayerData.Instance.TryDecCurrency(CURRENCY_TYPE.GOLD, levelInfo.GoldCost))
                return;

            _TotalExp += levelInfo.Exp;
            CalculateLevel();
        }

        private void CalculateLevel()
        {
            long exp = _TotalExp;
            if (GirlInfoRecord == null)
                return;

            var levelInfos = TableReader.GirlLevelInfo.GetTypeLevels(GirlInfoRecord.LevelInfoTypeID);
            if (levelInfos == null || levelInfos.Count == 0)
                return;

            int level = 0;
            foreach (var levelInfo in levelInfos)
            {
                exp -= levelInfo.Exp;
                if (exp >= 0)
                {
                    level = levelInfo.Level;
                }
                else
                {
                    break;
                }
            }

            if (_Level != level)
            {
                _Level = level;
                CalculateAttr();
            }
        }

        

        #endregion

        #region equip

        [SaveField(3)]
        private EquipInfo _EquipInfo;
        public EquipInfo EquipInfo { get { return _EquipInfo; } }

        public bool PutOnEquip(EquipInfo equipInfo)
        {
            if (!CanPutOnEquip(equipInfo))
                return false;

            if (EquipPack.Instance.RemoveEquip(equipInfo))
            {
                _EquipInfo = equipInfo;
                return true;
            }
            return false;
        }

        public bool CanPutOnEquip(EquipInfo equipInfo)
        {
            if (equipInfo.EquipRecord.BelongGirl == null
                || equipInfo.EquipRecord.BelongGirl == GirlInfoRecord)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Attrs
        public static string[] ATTR_NAMES = new string[]
        {
            "Lively",
            "Elegant",
            "Cute",
            "Mature",
            "Modern",
            "Classic",
        };

        private int _Attr1A = 0;
        public int Attr1A { get { return _Attr1A; } }

        private int _Attr1B = 0;
        public int Attr1B { get { return _Attr1B; } }

        private int _Attr2A = 0;
        public int Attr2A { get { return _Attr2A; } }

        private int _Attr2B = 0;
        public int Attr2B { get { return _Attr2B; } }

        private int _Attr3A = 0;
        public int Attr3A { get { return _Attr3A; } }

        private int _Attr3B = 0;
        public int Attr3B { get { return _Attr3B; } }

        public void CalculateAttr()
        {
            //base
            _Attr1A = GirlInfoRecord.Attr1A;
            _Attr1B = GirlInfoRecord.Attr1B;
            _Attr2A = GirlInfoRecord.Attr2A;
            _Attr2B = GirlInfoRecord.Attr2B;
            _Attr3A = GirlInfoRecord.Attr3A;
            _Attr3B = GirlInfoRecord.Attr3B;

            //level
            var levelInfos = TableReader.GirlLevelInfo.GetTypeLevels(GirlInfoRecord.LevelInfoTypeID);
            if (levelInfos == null || levelInfos.Count == 0)
                return;

            foreach (var levelInfo in levelInfos)
            {
                if (levelInfo.Level <= _Level)
                {
                    _Attr1A += levelInfo.Attr1A;
                    _Attr1B += levelInfo.Attr1B;
                    _Attr2A += levelInfo.Attr2A;
                    _Attr2B += levelInfo.Attr2B;
                    _Attr3A += levelInfo.Attr3A;
                    _Attr3B += levelInfo.Attr3B;
                }
            }

            //equip
            if (_EquipInfo != null && _EquipInfo.EquipRecord != null)
            {
                _Attr1A += _EquipInfo.EquipRecord.AddAttrs[0];
                _Attr1B += _EquipInfo.EquipRecord.AddAttrs[1];
                _Attr2A += _EquipInfo.EquipRecord.AddAttrs[2];
                _Attr2B += _EquipInfo.EquipRecord.AddAttrs[3];
                _Attr3A += _EquipInfo.EquipRecord.AddAttrs[4];
                _Attr3B += _EquipInfo.EquipRecord.AddAttrs[5];
            }

        }

        public int GetAttr(int idx)
        {
            switch (idx)
            {
                case 0:
                    return _Attr1A;
                case 1:
                    return _Attr1B;
                case 2:
                    return _Attr2A;
                case 3:
                    return _Attr2B;
                case 4:
                    return _Attr3A;
                case 5:
                    return _Attr3B;
            }
            return 0;
        }

        public int GetInverseAttr(int idx)
        {
            switch (idx)
            {
                case 0:
                    return _Attr1B;
                case 1:
                    return _Attr1A;
                case 2:
                    return _Attr2B;
                case 3:
                    return _Attr2A;
                case 4:
                    return _Attr3B;
                case 5:
                    return _Attr3A;
            }
            return 0;
        }

        public Dictionary<string, int> GetAdvantageAttrs()
        {
            var attrDict = new Dictionary<string, int>();
            if (_Attr1A > _Attr1B)
            {
                attrDict.Add(ATTR_NAMES[0], _Attr1A);
            }
            else
            {
                attrDict.Add(ATTR_NAMES[1], _Attr1B);
            }

            if (_Attr2A > _Attr2B)
            {
                attrDict.Add(ATTR_NAMES[2], _Attr2A);
            }
            else
            {
                attrDict.Add(ATTR_NAMES[3], _Attr2B);
            }

            if (_Attr3A > _Attr3B)
            {
                attrDict.Add(ATTR_NAMES[4], _Attr3A);
            }
            else
            {
                attrDict.Add(ATTR_NAMES[5], _Attr3B);
            }

            return attrDict;
        }

        public int GetFilterLevel(List<int> filter)
        {
            int level = 0;
            if (_Attr1A > _Attr1B && filter.Contains(0))
            {
                ++level;
            }
            else if(_Attr1A < _Attr1B && filter.Contains(1))
            {
                ++level;
            }

            if (_Attr2A > _Attr2B && filter.Contains(2))
            {
                ++level;
            }
            else if (_Attr2A < _Attr2B && filter.Contains(3))
            {
                ++level;
            }

            if (_Attr3A > _Attr3B && filter.Contains(4))
            {
                ++level;
            }
            else if (_Attr3A < _Attr3B && filter.Contains(5))
            {
                ++level;
            }

            return level;
        }
        #endregion

        #region ActLimit

        public const int VITALITY_RESUME_PER_SECOND = 5;
        public const int MAX_VITALITY = 10000;
        [SaveField(4)]
        private int _Vitality = MAX_VITALITY;
        public int Vitality { get { return _Vitality; } }
        public float GetVitalityRate()
        {
            return (float)_Vitality / MAX_VITALITY;
        }

        public const int MAX_MOOD = 10000;
        [SaveField(5)]
        private int _Mood = MAX_MOOD;
        public int Mood { get { return _Mood; } }
        public float GetMoodRate()
        {
            return (float)_Mood / MAX_MOOD;
        }

        public void AddVitality(int value)
        {
            _Vitality = Mathf.Clamp(_Vitality + value, 0, MAX_VITALITY);
        }

        public void AddMood(int value)
        {
            _Mood = Mathf.Clamp(_Mood + value, 0, MAX_MOOD);
        }

        public void Update(int delta)
        {
            _Vitality += VITALITY_RESUME_PER_SECOND * delta;
            if (_Vitality > MAX_VITALITY)
            {
                _Vitality = MAX_VITALITY;
            }
        }


        #endregion

        #region FightTemp

        public int _FightCD = 0;

        #endregion
    }
}
