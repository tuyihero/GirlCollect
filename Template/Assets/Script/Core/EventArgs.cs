using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using Tables;
using GameLogic;

namespace GameBase
{
    /// <summary>
    /// 事件定义
    /// </summary>
    public enum EVENT_TYPE
    {
        EVENT_NONE = 0,

        EVENT_SYSTEM_START,

        EVENT_LOGIC_LOAD,
        EVENT_LOGIC_LOAD_FINISH,

        EVENT_LOGIC_LOGIC_START,
        EVENT_LOGIC_FIGHT_START,
        EVENT_LOGIC_FIGHT_FINISH,
        EVENT_LOGIC_FIGHT_WIN,
        EVENT_LOGIC_FIGHT_LOSE,
        EVENT_LOGIC_CAPTURE_GIRL,
        EVENT_LOGIC_PASS_STAGE,
        EVENT_LOGIC_SPECIL_FIGHT,
        EVENT_LOGIC_WEBCAM_FIGHT,

        EVENT_TIMER_REGISTER_FIRST_LOGIN,
        EVENT_TIMER_TODAY_FIRST_LOGIN,

        EVENT_UI_CREATE,
        EVENT_UI_SHOWED,
        EVENT_UI_HIDED,

        EVENT_UI_MISSION_REFRESH,



    }

   
 
}
