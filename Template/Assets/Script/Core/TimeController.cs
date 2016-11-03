using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GameBase
{
    public class TimeController : MonoBehaviour
    {
        //void Start()
        //{
        //    var date = System.DateTime.Now;
        //    Init();
        //}

        void Update()
        {
            CallBackUpdate();
        }

        #region TimeRecord

        /// <summary>
        /// 事件处理者列表
        /// </summary>        
        private Dictionary<TIMER_TYPE, DateTime> _TimerList = new Dictionary<TIMER_TYPE, DateTime>();

        public void Init()
        {
            Load();
            InitEvent();
        }

        public void SetTimer(TIMER_TYPE type)
        {
            var dataTime = DateTime.Now;
            if (_TimerList.ContainsKey(type))
            {
                _TimerList[type] = dataTime;
            }
            else
            {
                _TimerList.Add(type, dataTime);
            }
        }
        
        public void RemoveTimer(TIMER_TYPE type)
        {
            if (_TimerList.ContainsKey(type))
            {
                _TimerList.Remove(type);
            }
        }

        public TimeSpan GetTimeDelay(TIMER_TYPE type)
        {
            if (_TimerList.ContainsKey(type))
            {
                var date = System.DateTime.Now - _TimerList[type];
                return date;
            }
            return System.TimeSpan.Zero;
        }

        public DateTime GetLastDate(TIMER_TYPE type)
        {
            if (_TimerList.ContainsKey(type))
            {
                return _TimerList[type];
            }
            return new DateTime(0);
        }

        #endregion

        #region SpecilTime event

        public void InitEvent()
        {
            GameCore.RegisterEvent(EVENT_TYPE.EVENT_LOGIC_LOAD_FINISH, LoginEventHandle);
        }

        public void LoginEventHandle(object sender, Hashtable hash)
        {
            IsRegisterFirstLogin();
            IsTodayFirstLogin();
        }

        public bool IsRegisterFirstLogin()
        {
            var lastDate = GetLastDate(TIMER_TYPE.TIMER_LOGIN);
            if (lastDate.Year == 0001)
            {
                GameCore.PushEvent(EVENT_TYPE.EVENT_TIMER_REGISTER_FIRST_LOGIN, new Hashtable());
                Debug.Log("EVENT_TIMER_REGISTER_FIRST_LOGIN");
                return true;
            }
            return false;
        }

        public bool IsTodayFirstLogin()
        {
            var lastDate = GetLastDate(TIMER_TYPE.TIMER_LOGIN);
            if (lastDate.Year == 1 || lastDate.DayOfYear != System.DateTime.Today.DayOfYear)
            {
                GameCore.PushEvent(EVENT_TYPE.EVENT_TIMER_TODAY_FIRST_LOGIN, new Hashtable());
                Debug.Log("EVENT_TIMER_TODAY_FIRST_LOGIN");
                SetTimer(TIMER_TYPE.TIMER_LOGIN);
                return true;
            }
            return false;
        }

        #endregion

        #region CallBack


        public delegate void TimeCallBackFun();

        public class TimeCallBackInfo
        {
            public TimeCallBackFun _CallBackFun;
            public float _Second;
            public float _StartTime;
        }

        private List<TimeCallBackInfo> _TimeCallBacks = new List<TimeCallBackInfo>();
        private List<TimeCallBackInfo> _TimeToDelete = new List<TimeCallBackInfo>();

        public void SetCallBack(TimeCallBackFun callBack, float seconds)
        {
            _TimeCallBacks.Add(new TimeCallBackInfo() { _CallBackFun = callBack, _Second = seconds, _StartTime = Time.time });

            //Invoke("CallBack", seconds);
        }

        public void CallBackUpdate()
        {
            foreach (var timeInfo in _TimeCallBacks)
            {
                if (Time.time - timeInfo._StartTime >= timeInfo._Second)
                {
                    try
                    {
                        timeInfo._CallBackFun();
                    }
                    catch (Exception e)
                    {
                        LogManager.LogError("TimeCallBackError:" + e.ToString());
                    }
                    _TimeToDelete.Add(timeInfo);
                }
            }

            foreach (var timeInfo in _TimeToDelete)
            {
                _TimeCallBacks.Remove(timeInfo);
            }

            _TimeToDelete.Clear();
        }

        #endregion

        #region save/load

        public void Save()
        {
            string packDataStr = "";
            foreach (var handle in _TimerList)
            {
                packDataStr += ((int)handle.Key).ToString() + ",";
                packDataStr += handle.Value.ToString() + ";";
            }
            LocalSave.Save(LocalSaveType.TIMER_PACK, packDataStr);
        }

        public void Load()
        {
            _TimerList.Clear();

            string packDataStr = LocalSave.LoadString(LocalSaveType.TIMER_PACK);
            if (string.IsNullOrEmpty(packDataStr))
                return;

            string[] dataStrs = packDataStr.Split(';');
            foreach (string dataStr in dataStrs)
            {
                if (string.IsNullOrEmpty(dataStr))
                    continue;

                string[] timeStrs = dataStr.Split(',');
                int timeType = int.Parse(timeStrs[0]);

                _TimerList.Add((TIMER_TYPE)timeType, DateTime.Parse(timeStrs[1]));
            }
        }

        public void Clear()
        {
            _TimerList.Clear();
        }
        #endregion
    }
}
