using System.Collections;
using System.Collections.Generic;
using System;

using GameBase;
using Tables;
namespace GameLogic
{
    public class FightStagePack: DataPackBase
    {
        #region 单例

        private static FightStagePack _Instance;
        public static FightStagePack Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new FightStagePack();
                }
                return _Instance;
            }
        }

        private FightStagePack() { }

        #endregion

        #region stages

        [SaveField(1)]
        public List<FightStageInfo> _FightStageList = new List<FightStageInfo>();

        private FightStageInfo _FightingStage = null;
        public FightStageInfo FightingStage
        {
            get
            {
                return _FightingStage;
            }
        }

        public List<FightStageInfo> GetCapterStages(FightCapterRecord capterRecord)
        {
            List<FightStageInfo> resultList = new List<FightStageInfo>();
            foreach (var stageRecord in capterRecord.StageNotNull)
            {
                if (stageRecord == null)
                    continue;
                var stageInfo = _FightStageList.Find((stage) =>
                {
                    if (stage.FigetRecord == stageRecord)
                        return true;
                    return false;
                });
                if (stageInfo != null)
                {
                    resultList.Add(stageInfo);
                }
            }

            return resultList;
        }

        public int CulFightGoldAward(FightStageInfo stageInfo, int point)
        {
            int goldAward = 0;
            if (point > 0)
            {
                goldAward = ((point / 10000) + 1) * 200;
            }
            else
            {
                goldAward = 200;
            }

            PlayerData.Instance.AddCurrency(CURRENCY_TYPE.GOLD, goldAward);
            return goldAward;
        }

        public bool IsCanCapterFight(FightCapterRecord capterRecord)
        {
            if (capterRecord == TableReader.FightCapter.EliteCapter)
            {
                return true;
            }
            else
            {
                int idx = TableReader.FightCapter.NormalCapter.IndexOf(capterRecord);
                if (idx == 0)
                {
                    return true;
                }
                else if (idx > 0)
                {
                    var capter = TableReader.FightCapter.NormalCapter[idx - 1];
                    string stageID = capter.StageNotNull[capter.StageNotNull.Count - 1].Id;

                    var findStage = GetStageByID(stageID);
                    if (findStage == null)
                        return false;

                    return findStage.MaxPassPoint > 0;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsCanStageFight(FightStageInfo stageInfo)
        {
            if (TableReader.FightCapter.EliteCapter.StageNotNull.Contains(stageInfo.FigetRecord))
            {
                var idx = TableReader.FightCapter.EliteCapter.StageNotNull.IndexOf(stageInfo.FigetRecord);
                if (idx < 0)
                    return false;

                string stageID = TableReader.FightCapter.NormalCapter[TableReader.FightCapter.NormalCapter.Count - 1].Id;
                var findStage = GetStageByID(stageID);
                if (findStage == null)
                    return false;

                return findStage.MaxPassPoint > 0;
            }
            else
            {
                var idx = _FightStageList.IndexOf(stageInfo);
                if (idx > 0)
                {
                    return _FightStageList[idx - 1].MaxPassPoint > 0;
                }

                return true;
            }
        }

        public FightStageInfo GetStageByID(string stageID)
        {
            var findStage = _FightStageList.Find((stage) =>
            {
                if (stage.FigetRecord.Id == stageID)
                    return true;
                return false;
            });

            return findStage;
        }

        #endregion

        #region specil stage

        public const int SPECIL_GUEST_REFRESH_TIME = 18000;

        public const int COMPLATE_COST_DIAMOND = 50;

        
        private string _SpecilGuestID;
        [SaveField(2)]
        private GuestInfoRecord _SpecilGuest;

        [SaveField(3)]
        private List<string> _FightingGrilIDs;

        private List<GirlMemberInfo> _FightingGirls;
        public List<GirlMemberInfo> FightingGirls
        {
            get
            {
                if (_FightingGirls == null)
                {
                    _FightingGirls = new List<GirlMemberInfo>();
                    if (_FightingGrilIDs != null)
                    {
                        foreach (var fightID in _FightingGrilIDs)
                        {
                            var findGirl = GirlMemberPack.Instance.GetGirlByID(fightID);
                            if (findGirl != null)
                                _FightingGirls.Add(findGirl);
                        }
                    }
                }
                return _FightingGirls;
            }
            set
            {
                _FightingGirls = value;
                _FightingGrilIDs = new List<string>();
                foreach (var fightGirl in _FightingGirls)
                {
                    _FightingGrilIDs.Add(fightGirl.GirlInfoRecord.Id);
                }
            }
        }

        public bool IsGirlInSpecilFight(GirlMemberInfo girl)
        {
            return FightingGirls.Contains(girl);
        }

        public GuestInfoRecord GetSpecilGuest()
        {
            if (_SpecilGuest == null && string.IsNullOrEmpty(_SpecilGuestID))
            {
                InitSpecilStage();
            }
            else if (_SpecilGuest == null && !string.IsNullOrEmpty(_SpecilGuestID))
            {
                _SpecilGuest = TableReader.GuestInfo.GetRecord(_SpecilGuestID);
            }

            return _SpecilGuest;
        }

        private void InitSpecilStage()
        {
            var spGuests = TableReader.GuestInfo.GetSpecilGuests();
            if (spGuests.Count <= 0)
                return;

            var randomIdx = UnityEngine.Random.Range(0, spGuests.Count);
            _SpecilGuest = spGuests[randomIdx];

            if(_FightingGrilIDs != null)
                _FightingGrilIDs.Clear();

            if(_FightingGirls != null)
                _FightingGirls.Clear();
        }

        public bool IsFightFinish()
        {
            var timeDelay = GameCore.Instance.TimeController.GetTimeDelay(TIMER_TYPE.TIMER_SPECIL_STAGE_REFRESH);
            if (timeDelay.TotalSeconds >= SPECIL_GUEST_REFRESH_TIME)
            {
                return true;
            }
            return false;
        }

        public bool IsFighting()
        {
            var timeDelay = GameCore.Instance.TimeController.GetTimeDelay(TIMER_TYPE.TIMER_SPECIL_STAGE_REFRESH);
            if (timeDelay.TotalSeconds == 0)
            {
                return false;
            }
            return true;
        }

        public void StartFightSpecil(List<GirlMemberInfo> fightMember)
        {
            FightingGirls = fightMember;
            GameCore.Instance.TimeController.SetTimer(TIMER_TYPE.TIMER_SPECIL_STAGE_REFRESH);

            GameCore.PushEvent(EVENT_TYPE.EVENT_LOGIC_SPECIL_FIGHT, null);
        }

        public void ComplateFight()
        {
            if (IsFightFinish())
            {
                GetSpecialAward();

                _FightingGrilIDs.Clear();

                foreach (var girl in FightingGirls)
                {
                    girl.AddVitality(-girl.Vitality);
                    girl.AddMood(GirlMemberInfo.MAX_MOOD);
                }

                FightingGirls.Clear();
            }
        }

        public void ComplateFightImmediately()
        {
            if (IsFightFinish())
            {
                return;
            }

            if (!PlayerData.Instance.TryDecCurrency(CURRENCY_TYPE.DIAMOND, COMPLATE_COST_DIAMOND))
            {
                return;    
            }

            GameCore.Instance.TimeController.RemoveTimer(TIMER_TYPE.TIMER_SPECIL_STAGE_REFRESH);

            GetSpecialAward();

            _FightingGrilIDs.Clear();

            foreach (var girl in _FightingGirls)
            {
                girl.AddVitality(-girl.Vitality);
                girl.AddMood(GirlMemberInfo.MAX_MOOD);
            }

            _FightingGirls.Clear();
        }

        public void GetSpecialAward()
        {
            int awardPoint = 0;
            foreach (var girl in FightingGirls)
            {
                awardPoint += (int)(girl.Attr1A * GetSpecilGuest().Attr1APoint);
                awardPoint += (int)(girl.Attr1B * GetSpecilGuest().Attr1BPoint);
                awardPoint += (int)(girl.Attr2A * GetSpecilGuest().Attr2APoint);
                awardPoint += (int)(girl.Attr2B * GetSpecilGuest().Attr2BPoint);
                awardPoint += (int)(girl.Attr3A * GetSpecilGuest().Attr3APoint);
                awardPoint += (int)(girl.Attr3B * GetSpecilGuest().Attr3BPoint);
            }

            int goldAward = ((awardPoint / 10000) + 1) * 200;
            PlayerData.Instance.AddCurrency(CURRENCY_TYPE.GOLD, goldAward);

            //equip
            int rateQ0 = Math.Max(10000 - awardPoint, 0);
            int rateQ1 = Math.Max(awardPoint - 40000, 0);
            int rateQ2 = Math.Max(awardPoint - 60000, 0);
            int rateQ3 = Math.Max(awardPoint - 80000, 0);

            int equipLevel = GameRandom.GetRandomLevel(rateQ0, rateQ1, rateQ2, rateQ3);
            if (equipLevel > 0)
            {
                var equipRecord = DropManager.GetRandomEquip(equipLevel);
                EquipPack.Instance.AddEquip(equipRecord);
            }
        }

        #endregion

        #region webcam stage

        public const int WEBCAM_REWARD_INTERVAL = 60;

        [SaveField(4)]
        private List<string> _WebcamGrilIDs;

        private List<GirlMemberInfo> _WebcamGirls;
        public List<GirlMemberInfo> WebcamGirls
        {
            get
            {
                if (_WebcamGirls == null)
                {
                    _WebcamGirls = new List<GirlMemberInfo>();
                    if (_WebcamGrilIDs != null)
                    {
                        foreach (var fightID in _WebcamGrilIDs)
                        {
                            var findGirl = GirlMemberPack.Instance.GetGirlByID(fightID);
                            if (findGirl != null)
                                _WebcamGirls.Add(findGirl);
                        }
                    }
                }
                return _WebcamGirls;
            }
            set
            {
                _WebcamGirls = value;
                _WebcamGrilIDs = new List<string>();
                foreach (var fightGirl in _WebcamGirls)
                {
                    _WebcamGrilIDs.Add(fightGirl.GirlInfoRecord.Id);
                }
            }
        }

        public bool IsGirlInWebcamFight(GirlMemberInfo girl)
        {
            return WebcamGirls.Contains(girl);
        }

        public bool IsCanGetAward()
        {
            if (WebcamGirls.Count == 0)
                return false;

            var timeDelay = GameCore.Instance.TimeController.GetTimeDelay(TIMER_TYPE.TIMER_SPECIL_STAGE_REFRESH);
            if (timeDelay.TotalSeconds < WEBCAM_REWARD_INTERVAL)
                return false;

            return true;
        }

        public void StartWebcamGirl(List<GirlMemberInfo> girls)
        {
            WebcamGirls = girls;
            GetWebcamReward();

            GameCore.PushEvent(EVENT_TYPE.EVENT_LOGIC_WEBCAM_FIGHT, null);
        }

        public void GetWebcamReward()
        {
            var timeDelay = GameCore.Instance.TimeController.GetTimeDelay(TIMER_TYPE.TIMER_WEBCAM_REWARD);
            int rewardTimes = (int)(timeDelay.TotalSeconds % WEBCAM_REWARD_INTERVAL);

            int awardPoint = 0;
            foreach (var girl in FightingGirls)
            {
                awardPoint += (int)(girl.Attr1A);
                awardPoint += (int)(girl.Attr1B);
                awardPoint += (int)(girl.Attr2A);
                awardPoint += (int)(girl.Attr2B);
                awardPoint += (int)(girl.Attr3A);
                awardPoint += (int)(girl.Attr3B);
            }

            int goldAward = ((awardPoint / 10000) + 1) * 200;
            PlayerData.Instance.AddCurrency(CURRENCY_TYPE.GOLD, goldAward);

            int luxuryAward = ((awardPoint / 50000) + 1) * 200;
            PlayerData.Instance.AddCurrency(CURRENCY_TYPE.LUXURY, luxuryAward);

            GameCore.Instance.TimeController.SetTimer(TIMER_TYPE.TIMER_WEBCAM_REWARD);
        }

        #endregion

        #region events

        public override void InitEvents()
        {
            base.InitEvents();

            GameCore.RegisterEvent(EVENT_TYPE.EVENT_TIMER_REGISTER_FIRST_LOGIN, RegisterFirstLogin);
        }

        public void RegisterFirstLogin(object sender, Hashtable hash)
        {
            _FightStageList.Clear();
            foreach (var record in Tables.TableReader.FightScene.Records)
            {
                _FightStageList.Add(new FightStageInfo(record.Value));
            }
            
        }

        #endregion 

    }
}
