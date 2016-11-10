using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Tables;
using GameBase;

namespace GameLogic
{
    public class GirlCaptureScene
    {
        private string _CaptureSceneID = "";
        [SaveField(1)]
        private GirlCaptureSceneRecord _CaptureSceneRecord;
        public GirlCaptureSceneRecord CaptureSceneRecord
        {
            get
            {
                if (_CaptureSceneRecord == null)
                {
                    _CaptureSceneRecord = TableReader.GirlCaptureScene.GetRecord(_CaptureSceneID);
                }
                return _CaptureSceneRecord;
            }
        }

        public GirlCaptureScene(GirlCaptureSceneRecord record)
        {
            _CaptureSceneID = record.Id;
            _CaptureSceneRecord = record;
        }

        public GirlCaptureScene()
        {

        }

        #region

        [SaveField(2)]
        private List<GirlMemberInfo> _AppearGirls = new List<GirlMemberInfo>();
        public List<GirlMemberInfo> AppearGirls
        {
            get
            {
                return _AppearGirls;
            }
        }

        public void InitGirls(List<GirlInfoRecord> selectGirls)
        {
            _AppearGirls.Clear();

            int level = GetMaxLevelOfGirls(selectGirls);
            int randomMaxLevel;
            if (level == 4)
            {
                float randomRate = Random.Range(1, 10000);
                if (randomRate < 2000)
                {
                    randomMaxLevel = 4;
                }
                else
                {
                    randomMaxLevel = 3;
                }
            }
            else if (level == 5)
            {
                float randomRate = Random.Range(1, 10000);
                if (randomRate < 1000)
                {
                    randomMaxLevel = 5;
                }
                else if (randomRate < 3000)
                {
                    randomMaxLevel = 4;
                }
                else
                {
                    randomMaxLevel = 3;
                }
            }
            else
            {
                randomMaxLevel = 3;
            }

            var girlMax = GetRandomGirl(selectGirls, randomMaxLevel);
            _AppearGirls.Add(new GirlMemberInfo(girlMax.Id));
            selectGirls.Remove(girlMax);

            for (int i = 0; i < 2; ++i)
            {
                var girl = GetRandomGirl(selectGirls, 2);
                _AppearGirls.Add(new GirlMemberInfo(girl.Id));
                selectGirls.Remove(girl);
            }

            for (int i = 0; i < 3; ++i)
            {
                var girl = GetRandomGirl(selectGirls, 1);
                _AppearGirls.Add(new GirlMemberInfo(girl.Id));
                selectGirls.Remove(girl);
            }
        }

        public bool TryCaptureGirl(GirlMemberInfo girlInfo, int gold, int luxury, int diamond)
        {
            if (!PlayerData.Instance.TryDecAll(new int[] { gold, luxury, diamond }))
            {
                ErrorManager.PushAndDisplayError("resource not enough");
                return false;
            }

            int captureRate = CulCaptureGirlRate(girlInfo, gold, luxury, diamond);
            int randomRate = Random.Range(0, 10001);

            Hashtable hash = new Hashtable();
            if (randomRate < captureRate)
            {
                GirlMemberPack.Instance.AddNewGirl(girlInfo);

                hash.Add("Sucess", true);
                GameCore.PushEvent(EVENT_TYPE.EVENT_LOGIC_CAPTURE_GIRL, hash);

                return true;
            }
            else
            {
                hash.Add("Sucess", false);
                GameCore.PushEvent(EVENT_TYPE.EVENT_LOGIC_CAPTURE_GIRL, hash);

                return false;
            }

        }

        public int GetMaxLevelOfGirls(List<GirlInfoRecord> selectGirls)
        {
            return selectGirls[0].Star;
        }

        private GirlInfoRecord GetRandomGirl(List<GirlInfoRecord> selectGirls, int level)
        {
            int idxStart = selectGirls.FindIndex((girl) =>
            {
                if (girl.Star == level)
                    return true;
                return false;
            });

            int idxEnd = selectGirls.FindLastIndex((girl) =>
            {
                if (girl.Star == level)
                    return true;
                return false;
            });

            int idx = Random.Range(idxStart, idxEnd);
            return selectGirls[idx];

        }

        public int CulCaptureGirlRate(GirlMemberInfo girlInfo, int gold, int luxury, int diamond)
        {
            int captureRate = 0;

            if (girlInfo.GirlInfoRecord.Star == 1)
            {
                captureRate += luxury * 50;
                captureRate += gold * 10;
                if (captureRate > 9999)
                {
                    captureRate = 9999;
                }

                captureRate += diamond * 1000;
            }
            else if (girlInfo.GirlInfoRecord.Star == 2)
            {
                captureRate += luxury * 50;
                captureRate += gold * 10;
                if (captureRate > 8888)
                {
                    captureRate = 8888;
                }

                captureRate += diamond * 500;
            }
            else if (girlInfo.GirlInfoRecord.Star ==3)
            {
                captureRate += luxury * 50;
                captureRate += gold * 10;
                if (captureRate > 5555)
                {
                    captureRate = 5555;
                }

                captureRate += diamond * 200;
            }
            else if (girlInfo.GirlInfoRecord.Star == 4)
            {
                captureRate += luxury * 50;
                captureRate += gold * 10;
                if (captureRate > 3333)
                {
                    captureRate = 3333;
                }

                captureRate += diamond * 150;
            }
            else if (girlInfo.GirlInfoRecord.Star == 5)
            {
                captureRate += luxury * 50;
                captureRate += gold * 10;
                if (captureRate > 1111)
                {
                    captureRate = 1111;
                }

                captureRate += diamond * 100;
            }

            return captureRate;

        }
        
        #endregion

    }
}
