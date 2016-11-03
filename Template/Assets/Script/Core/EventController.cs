using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GameBase
{
    /// <summary>
    /// 事件管理类
    /// </summary>
    public class EventController : MonoBehaviour
    {
        /// <summary>
        /// 事件发送参数
        /// </summary>
        private class EventParam
        {
            /// <summary>
            /// 事件
            /// </summary>
            public EVENT_TYPE EventType;
            /// <summary>
            /// 事件发送者
            /// </summary>
            public object Sender;
            /// <summary>
            /// 事件参数
            /// </summary>
            public Hashtable EventArgs;

            public EventParam(EVENT_TYPE eventType, object sender, Hashtable eventArgs)
            {
                EventType = eventType;
                Sender = sender;
                EventArgs = eventArgs;
            }
        }

        #region 一般事件

        public delegate void EventDelegate(object go, Hashtable eventArgs);
        /// <summary>
        /// 事件处理者列表
        /// </summary>        
        private Dictionary<EVENT_TYPE, List<EventDelegate>> _HandleList = new Dictionary<EVENT_TYPE, List<EventDelegate>>();

        private Dictionary<EVENT_TYPE, List<EventDelegate>> _UnRegisteHandleList = new Dictionary<EVENT_TYPE, List<EventDelegate>>();

        /// <summary>
        /// 事件列表
        /// </summary>
        private List<EventParam> _EventList = new List<EventParam>();

        private bool _IsSendedEvent;

        public void RegisteEvent(EVENT_TYPE eventType, EventDelegate handler)
        {
            if (_HandleList.ContainsKey(eventType))
            {
                if (!_HandleList[eventType].Contains(handler))
                {
                    _HandleList[eventType].Add(handler);
                }
            }
            else
            {
                List<EventDelegate> newHandle = new List<EventDelegate>();
                newHandle.Add(handler);
                _HandleList.Add(eventType, newHandle);
            }
        }

        public void UnRegisteEvent(EVENT_TYPE eventType, EventDelegate handler)
        {
            if (_HandleList.ContainsKey(eventType))
            {
                if (_UnRegisteHandleList.ContainsKey(eventType))
                {
                    if (!_UnRegisteHandleList[eventType].Contains(handler))
                    {
                        _UnRegisteHandleList[eventType].Add(handler);
                    }
                }
                else
                {
                    List<EventDelegate> newHandle = new List<EventDelegate>();
                    newHandle.Add(handler);
                    _UnRegisteHandleList.Add(eventType, newHandle);
                }
            }
            Invoke("UnRegisteEventInner", 0);
        }

        private void UnRegisteEventInner()
        {
            foreach (var unregisterHandle in _UnRegisteHandleList)
            {
                foreach (var eventHandle in unregisterHandle.Value)
                {
                    if (_HandleList.ContainsKey(unregisterHandle.Key))
                    {
                        _HandleList[unregisterHandle.Key].Remove(eventHandle);
                        if (_HandleList[unregisterHandle.Key].Count == 0)
                        {
                            _HandleList.Remove(unregisterHandle.Key);
                        }
                    }
                }
            }
        }

        public void PushEvent(EVENT_TYPE EventType, object sender)
        {
            PushEvent(EventType, sender, new Hashtable());
        }

        public void PushEvent(EVENT_TYPE EventType, object sender, Hashtable eventArgs)
        {
            _EventList.Add(new EventParam(EventType, sender, eventArgs));
            if (!_IsSendedEvent)
            {
                Invoke("DispatchEvent", 0);
                _IsSendedEvent = true;
            }
        }

        public void DispatchEvent()
        {
            for (int i = 0; i < _EventList.Count; ++i)
            {
                if (_HandleList.ContainsKey(_EventList[i].EventType))
                {
                    _EventList[i].EventArgs.Add("EVENT_TYPE", _EventList[i].EventType);
                    for (int j = 0; j < _HandleList[_EventList[i].EventType].Count; ++j)
                    {
                        EventDelegate handler = _HandleList[_EventList[i].EventType][j];
                        try
                        {
                            handler(_EventList[i].Sender, _EventList[i].EventArgs);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError("SkillEventController DispatchEvent Exception EventType:" + _EventList[i].EventType +
                                " handleName:" + handler.ToString() + " e:" + ex);
                        }
                    }
                }
            }
            _EventList.Clear();
            _IsSendedEvent = false;
        }
        #endregion
    }
}
