using System.Collections;
using System;
using System.Collections.Generic;

using Tables;

namespace GameLogic
{
    public class MissionInfo : IComparable<MissionInfo>
    {
        public enum MISSION_STATUES
        {
            MISSION_ACCEPT,
            MISSION_DONE,
            MISSION_GETTED_REWARD,
        }

        #region Construct

        public MissionInfo(MissionInfoRecord record, MISSION_STATUES statues)
        {
            _MissionRecord = record;
            _MissionStatues = statues;
            ID = _MissionRecord.Id;

            if (_MissionStatues == MISSION_STATUES.MISSION_ACCEPT)
            {
                foreach (var checkRecord in _MissionRecord.FinishCheck)
                {
                    if (string.IsNullOrEmpty(checkRecord.TableName))
                        continue;

                    AddCheck(checkRecord.TableName, checkRecord.ID);
                }
            }
        }

        public MissionInfo()
        {

        }

        public void InitFromLoad()
        {
            if (_MissionRecord == null)
                return;

            for(int i = 0; i< _MissionRecord.FinishCheck.Count; ++i)
            {
                if (string.IsNullOrEmpty(_MissionRecord.FinishCheck[i].TableName))
                    continue;

                AddCheck(_MissionRecord.FinishCheck[i].TableName, _MissionRecord.FinishCheck[i].ID, _CheckData[i]);
            }
        }
        #endregion

        #region 

        private string ID;
        [SaveField(1)]
        private MissionInfoRecord _MissionRecord;
        public MissionInfoRecord MissionRecord { get { return _MissionRecord; } }

        [SaveField(2)]
        private MISSION_STATUES _MissionStatues;
        public MISSION_STATUES MissionStatues { get { return _MissionStatues; } }

        [SaveField(3)]
        private List<int> _CheckData = new List<int>();
        public void SetCheckData(int idx, int value)
        {
            _CheckData[idx] = value;
        }
        public int GetCheckData(int idx)
        {
            return _CheckData[idx];
        }
        
        private List<MissionCheck_Base> _CheckList = new List<MissionCheck_Base>();
        public List<MissionCheck_Base> CheckList { get { return _CheckList; } }

        #endregion

        #region check

        public void AddCheck(string checkName, string checkID, int checkData = 0)
        {
            var checkBase = (MissionCheck_Base)System.Activator.CreateInstance(System.Type.GetType("GameLogic." + checkName));
            _CheckData.Add(0);
            checkBase.Init(checkID, this, _CheckList.Count);
            _CheckList.Add(checkBase);

        }

        public bool CheckIfMissionDone()
        {
            if (_MissionStatues == MISSION_STATUES.MISSION_DONE)
                return true;

            if (_MissionStatues == MISSION_STATUES.MISSION_ACCEPT)
            {
                if (IsChecksDone())
                {
                    _MissionStatues = MISSION_STATUES.MISSION_DONE;
                    foreach (var check in _CheckList)
                    {
                        check.FinishCheck();
                    }
                    return true;
                }
            }
            return false;
        }

        public bool IsChecksDone()
        {
            bool allCheckDone = true;
            foreach (var check in _CheckList)
            {
                check.CheckInData();
                if (!check.IfCheckPass())
                {
                    allCheckDone = false;
                    break;
                }
            }

            return allCheckDone;
        }

        public bool MissionComplate()
        {
            if (_MissionStatues == MISSION_STATUES.MISSION_DONE)
            {
                CulMissionReward();
                _MissionStatues = MISSION_STATUES.MISSION_GETTED_REWARD;
                return true;
            }
            return false;
        }

        private void CulMissionReward()
        {
            //todo:
        }
        #endregion

        #region 

        public int CompareTo(MissionInfo obj)
        {
            if (obj.MissionStatues == MISSION_STATUES.MISSION_DONE)
                return 1;

            if (obj.MissionStatues == MISSION_STATUES.MISSION_GETTED_REWARD)
                return -1;

            if (MissionStatues == MISSION_STATUES.MISSION_DONE)
                return -1;

            if (MissionStatues == MISSION_STATUES.MISSION_GETTED_REWARD)
                return 1;

            return 0;
            //throw new NotImplementedException();
        }

        #endregion
    }
}