using System.Collections;
using System.Collections.Generic;

using GameBase;
using Tables;
namespace GameLogic
{
    public class MissionPack: DataPackBase
    {
        #region 单例

        private static MissionPack _Instance;
        public static MissionPack Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MissionPack();
                }
                return _Instance;
            }
        }

        private MissionPack() { }

        #endregion

        #region mission infos

        [SaveField(1)]
        private List<MissionInfo> _DaylyMissions = new List<MissionInfo>();
        public List<MissionInfo> DaylyMissions { get { return _DaylyMissions; } }

        private List<MissionInfo> _AchieveMissions = new List<MissionInfo>();
        public List<MissionInfo> AchieveMissions { get { return _AchieveMissions; } }

        public Dictionary<string, MissionInfo> GetSeriesAchieve()
        {
            Dictionary<string, MissionInfo> seriesAchieves = new Dictionary<string, MissionInfo>();

            foreach (var mission in _AchieveMissions)
            {
                if (!seriesAchieves.ContainsKey(mission.MissionRecord.Series))
                {
                    seriesAchieves.Add(mission.MissionRecord.Series, mission);
                }
                else
                {
                    if (seriesAchieves[mission.MissionRecord.Series].MissionStatues == MissionInfo.MISSION_STATUES.MISSION_GETTED_REWARD
                        && mission.MissionStatues == MissionInfo.MISSION_STATUES.MISSION_GETTED_REWARD)
                    {
                        seriesAchieves[mission.MissionRecord.Series] = mission;
                    }
                }
            }

            return seriesAchieves;
        }

        public List<MissionInfo> GetNotFinishDaylyMission()
        {
            List<MissionInfo> missions = new List<MissionInfo>();
            foreach (var mission in _DaylyMissions)
            {
                if (mission.MissionStatues != MissionInfo.MISSION_STATUES.MISSION_GETTED_REWARD)
                {
                    missions.Add(mission);
                }
            }

            return missions;
        }

        public List<MissionInfo> GetNotFinishAchieveMission()
        {
            List<MissionInfo> missions = new List<MissionInfo>();
            foreach (var mission in _AchieveMissions)
            {
                if (mission.MissionStatues != MissionInfo.MISSION_STATUES.MISSION_GETTED_REWARD)
                {
                    missions.Add(mission);
                }
            }

            return missions;
        }

        public bool CheckIfAnyDailyMissionDone()
        {
            foreach (var mission in _DaylyMissions)
            {
                if (mission.CheckIfMissionDone())
                {
                    return true;
                }
            }
            return false;
        }

        public void CheckAllDailyMissionDone()
        {
            foreach (var mission in _DaylyMissions)
            {
                mission.CheckIfMissionDone();
            }
        }

        public bool CheckIfAnyAchieveMissionDone()
        {
            foreach (var mission in _AchieveMissions)
            {
                if (mission.CheckIfMissionDone())
                {
                    return true;
                }
            }
            return false;
        }

        public void CheckAllAchieveMissionDone()
        {
            foreach (var mission in _AchieveMissions)
            {
                mission.CheckIfMissionDone();
            }
        }

        #endregion

        #region event

        public override void InitEvents()
        {
            base.InitEvents();

            GameCore.RegisterEvent(EVENT_TYPE.EVENT_TIMER_REGISTER_FIRST_LOGIN, InitAllMission);
            GameCore.RegisterEvent(EVENT_TYPE.EVENT_TIMER_TODAY_FIRST_LOGIN, RefreshDailyMission);
        }

        public void InitAllMission(object sender, Hashtable hash)
        {
            InitAchieveMission();
            InitDaylyMission();
        }

        public void RefreshDailyMission(object sender, Hashtable hash)
        {
            InitDaylyMission();
        }

        #endregion

        #region init mission

        private int _DaylyMissionCount = 5;

        private void InitAchieveMission()
        {
            foreach (var missionRecord in TableReader.MissionInfo.Records)
            {
                if (missionRecord.Value.MissionType == MISSION_TYPE.ACHIEVE_MISSION)
                {
                    //if (!_AchieveMissions.ContainsKey(missionRecord.Value.Id))
                    {
                        _AchieveMissions.Add(new MissionInfo(missionRecord.Value, MissionInfo.MISSION_STATUES.MISSION_ACCEPT));
                    }
                }
            }
        }

        private void InitDaylyMission()
        {
            var randomIdxs = GameRandom.GetIndependentRandoms(0, TableReader.MissionInfo.DaylyMissionRecords.Count, _DaylyMissionCount);
            _DaylyMissions.Clear();
            foreach (var randomIdx in randomIdxs)
            {
                _DaylyMissions.Add(new MissionInfo(TableReader.MissionInfo.DaylyMissionRecords[randomIdx], MissionInfo.MISSION_STATUES.MISSION_ACCEPT));
            }

        }

        public bool CheckIfMissionDone()
        {
            bool isMissionDone = false;
            foreach (var mission in _DaylyMissions)
            {
                isMissionDone |= mission.CheckIfMissionDone();
            }

            foreach (var mission in _AchieveMissions)
            {
                isMissionDone |= mission.CheckIfMissionDone();
            }
            //Save();

            return isMissionDone;
        }

        #endregion

    }
}
