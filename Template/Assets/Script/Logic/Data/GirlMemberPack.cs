using System.Collections;
using System.Collections.Generic;

using GameBase;
using Tables;
namespace GameLogic
{
    public class GirlMemberPack : DataPackBase
    {
        #region 单例

        private static GirlMemberPack _Instance;
        public static GirlMemberPack Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new GirlMemberPack();
                }
                return _Instance;
            }
        }

        private GirlMemberPack() { }

        #endregion

        #region members

        [SaveField(1)]
        public List<GirlMemberInfo> GirlList = new List<GirlMemberInfo>();

        public List<GirlMemberInfo> SortedGirlGirlList = new List<GirlMemberInfo>();

        public List<GirlMemberInfo> GetSortedGirlList(List<int> filterList)
        {
            SortedGirlGirlList.Sort((girlA, girlB) =>
            {
                int levelA = girlA.GetFilterLevel(filterList);
                int levelB = girlB.GetFilterLevel(filterList);
                if (levelA > levelB)
                {
                    return 1;
                }
                else if (levelA < levelB)
                {
                    return -1;
                }
                else
                {
                    if (girlA.GirlInfoRecord.Star > girlB.GirlInfoRecord.Star)
                        return 1;
                    else if (girlA.GirlInfoRecord.Star < girlB.GirlInfoRecord.Star)
                        return -1;
                    else
                        return 0;
                }
            });

            return SortedGirlGirlList;
        }

        public List<GirlInfoRecord> GetGirlsNotInPack()
        {
            List<GirlInfoRecord> girls = new List<GirlInfoRecord>();

            foreach (var infoRecord in TableReader.GirlInfo.Records)
            {
                var finded = GirlList.Find((info) =>
                {
                    if (info.GirlInfoRecord.Id == infoRecord.Value.Id)
                        return true;
                    return false;
                });
                if (finded == null)
                {
                    girls.Add(infoRecord.Value);
                }
            }

            return girls;
        }

        public List<GirlMemberInfo> GetGirlsNotInFight()
        {
            List<GirlMemberInfo> girls = new List<GirlMemberInfo>();

            foreach (var girlInfo in GirlList)
            {
                if (!_NormalFightGroup.Contains(girlInfo))
                {
                    girls.Add(girlInfo);
                }
            }

            return girls;
        }

        public List<GirlMemberInfo> GetGirls()
        {
            return GirlList;
        }

        public GirlMemberInfo GetGirlByID(string id)
        {
            var findInfo = GirlList.Find((girlInfo) =>
            {
                if (girlInfo.GirlInfoRecord.Id == id)
                    return true;
                return false;
            });

            return findInfo;
        }

        public void AddNewGirl(GirlInfoRecord girlRecord)
        {
            GirlList.Add(new GirlMemberInfo(girlRecord.Id));
            LogManager.Log("AddNewGirl:" + girlRecord.Id);
        }

        public void AddNewGirl(GirlMemberInfo girlInfo)
        {
            GirlList.Add(girlInfo);
            LogManager.Log("AddNewGirl:" + girlInfo.GirlInfoRecord.Id);
        }

        public void AddNewGirls(List<GirlInfoRecord> girlRecords)
        {
            foreach (var girlRecord in girlRecords)
            {
                AddNewGirl(girlRecord);
            }
        }

        public void UpdateMember()
        {
            var timeDelay = GameCore.Instance.TimeController.GetLastDate(TIMER_TYPE.TIMER_LAST_VITALITY_UPDATE);
            var timeNow = System.DateTime.Now;
            int delta = (int)(timeNow - timeDelay).TotalSeconds;

            foreach (var member in GirlList)
            {
                member.Update(delta);
            }

            GameCore.Instance.TimeController.SetTimer(TIMER_TYPE.TIMER_LAST_VITALITY_UPDATE);
        }

        #endregion

        #region  random girl

        public List<GirlInfoRecord> GetRandomGirlInStar(int star, int num)
        {
            var girlList = Tables.TableReader.GirlInfo.GetStarGirls(star);
            var randomList = GameRandom.GetIndependentRandoms(0, girlList.Count, num);

            List<GirlInfoRecord> randomGirlList = new List<GirlInfoRecord>();
            foreach (var randomIdx in randomList)
            {
                randomGirlList.Add(girlList[randomIdx]);
            }

            return randomGirlList;
        }

        #endregion

        #region init

        public override void InitEvents()
        {
            base.InitEvents();

            GameCore.RegisterEvent(EVENT_TYPE.EVENT_TIMER_REGISTER_FIRST_LOGIN, RegisterFirstLogin);
        }

        public void RegisterFirstLogin(object sender, Hashtable hash)
        {
            GirlList.Clear();
            for (int i = 104; i < 115; ++i)
            {
                GirlList.Add(new GirlMemberInfo(i.ToString()));
            }
        }

        #endregion events

        #region sort

        public static void SortByFilter(ref List<GirlMemberInfo> girls, List<int> filters)
        {
            girls.Sort((girlA, girlB) =>
            {
                int levelA = girlA.GetFilterLevel(filters);
                int levelB = girlB.GetFilterLevel(filters);
                if (levelA > levelB)
                {
                    return -1;
                }
                else if (levelA < levelB)
                {
                    return 1;
                }
                else
                {
                    if (girlA.GirlInfoRecord.Star > girlB.GirlInfoRecord.Star)
                        return -1;
                    else if (girlA.GirlInfoRecord.Star < girlB.GirlInfoRecord.Star)
                        return 1;
                    else
                        return 0;
                }
            });
        }

        public static bool IsAttrLarge(GirlMemberInfo girl1, GirlMemberInfo girl2, List<int> attrs)
        {
            int AFiltNum = 0;
            int BFiltNum = 0;
            int AValue = 0;
            int BValue = 0;
            foreach (int attr in attrs)
            {
                if (girl1.GetAttr(attr) > girl1.GetInverseAttr(attr))
                {
                    ++AFiltNum;
                }
                AValue += girl1.GetAttr(attr);

                if (girl2.GetAttr(attr) > girl2.GetInverseAttr(attr))
                {
                    ++BFiltNum;
                }
                BValue += girl2.GetAttr(attr);
            }

            if (AFiltNum == BFiltNum)
                return AValue > BValue;

            return AFiltNum > BFiltNum;
        }

        #endregion

        #region fightGroup

        private int _GroupGirlCount = 5;
        public int GroupGirlCount { get { return _GroupGirlCount; } }

        private List<GirlMemberInfo> _NormalFightGroup = new List<GirlMemberInfo>();
        public List<GirlMemberInfo> NormalFightGroup { get { return _NormalFightGroup; } }

        public bool AddToNormalFightGroup(GirlMemberInfo girlInfo)
        {
            if (_NormalFightGroup.Contains(girlInfo))
            {
                ErrorManager.PushAndDisplayError("Girl already in group!");
                return false;
            }

            _NormalFightGroup.Add(girlInfo);

            return true;
        }

        public bool SwapGroupGirl(GirlMemberInfo orgGirl, GirlMemberInfo newGirl)
        {
            if (_NormalFightGroup.Remove(orgGirl))
            {
                _NormalFightGroup.Add(newGirl);

                return true;
            }

            return false;
        }

        public bool AddGroupGirl(GirlMemberInfo orgGirl)
        {
            _NormalFightGroup.Add(orgGirl);
            return true;
        }
        #endregion

    }
}
