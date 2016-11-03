using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GameBase;
using GameLogic;

namespace GameUI
{
    public class UIMissionPack : UIBase
    {
        #region static funs

        public static void ShowAsyn()
        {
            Hashtable hash = new Hashtable();
            GameCore.Instance.UIManager.ShowUI("LogicUI/UIMissionPack", hash);
        }

        public static void Refresh()
        {
            Hashtable hash = new Hashtable();
            GameCore.PushEvent(EVENT_TYPE.EVENT_UI_MISSION_REFRESH, hash);
        }

        #endregion

        #region params

        public UIContainerBase _DailyMissionContainer;
        public UIContainerBase _AchieveMissionContainer;

        public UITagPanel _TagPanel;

        #endregion

        #region show funs

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            MissionPack.Instance.CheckAllDailyMissionDone();
            var showList = MissionPack.Instance.DaylyMissions;
            _DailyMissionContainer.InitContentItem(showList, null);

            var achieveList = MissionPack.Instance.GetSeriesAchieve();
            _AchieveMissionContainer.InitContentItem(achieveList.Values, null);

            _TagPanel.ShowPage(0);
        }

        public override void PreLoad()
        {
            base.PreLoad();

            _DailyMissionContainer.PreLoadItem(10);
        }

        #endregion

        #region logic event

        protected override void InitEvent()
        {
            base.InitEvent();

            GameCore.Instance.EventController.RegisteEvent(EVENT_TYPE.EVENT_UI_MISSION_REFRESH, EventRefresh);
        }

        private void EventRefresh(object sender, Hashtable hash)
        {
            var showList = MissionPack.Instance.DaylyMissions;
            _DailyMissionContainer.InitContentItem(showList, SelectItem);
        }

        #endregion

        #region selectItem

        public void SelectItem(object item)
        {
            Tables.FightCapterRecord record = (Tables.FightCapterRecord)item;

            Debug.Log("SelectCapter:" + record.Id);
            
        }

        #endregion


    }
}
