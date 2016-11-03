using System.Collections;
using System.Collections.Generic;
using System;

using GameBase;
namespace GameLogic
{
    public class MissionCheck_Base
    {

        #region 

        protected int _NeedCheckCount;
        public int NeedCheckCount { get { return _NeedCheckCount; } }

        public int CheckedCount
        {
            get
            {
                return _MissionInfo.GetCheckData(_CheckID);
            }
            set
            {
                _MissionInfo.SetCheckData(_CheckID, value);
            }
        }

        private MissionInfo _MissionInfo;
        private int _CheckID; 

        #endregion

        #region init

        public virtual void Init(string ID, MissionInfo missionInfo, int checkID)
        {
            _MissionInfo = missionInfo;
            _CheckID = checkID;
            InitEvent();
        }

        public virtual void CheckInData()
        {

        }

        public virtual void FinishCheck()
        {
            UnRegisteEvents();
        }

        public bool IfCheckPass()
        {
            if (_NeedCheckCount > CheckedCount)
                return false;

            FinishCheck();
            return true;
        }

        #endregion

        #region Check Event

        protected Dictionary<EVENT_TYPE, EventController.EventDelegate> _HandleList = new Dictionary<EVENT_TYPE, EventController.EventDelegate>();

        protected virtual void InitEvent()
        {

        }

        protected void RegisteEvent(EVENT_TYPE eventType, EventController.EventDelegate handle)
        {
            _HandleList.Add(eventType, handle);
            GameCore.Instance.EventController.RegisteEvent(eventType, handle);
        }

        protected void UnRegisteEvents()
        {
            foreach (var eventHandle in _HandleList)
            {
                GameCore.Instance.EventController.UnRegisteEvent(eventHandle.Key, eventHandle.Value);
            }
            _HandleList.Clear();
        }
        #endregion
    }
}