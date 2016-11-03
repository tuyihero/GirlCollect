using System.Collections;
using System.Collections.Generic;

using GameBase;
using Tables;
namespace GameLogic
{
    public class GirlCapturePack : DataPackBase
    {
        #region 单例

        private static GirlCapturePack _Instance;
        public static GirlCapturePack Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new GirlCapturePack();
                }
                return _Instance;
            }
        }

        private GirlCapturePack() { }

        #endregion

        #region members

        [SaveField(1)]
        private List<GirlCaptureScene> _CaptureList = new List<GirlCaptureScene>();
        public List<GirlCaptureScene> CaptureList
        {
            get
            {
                return _CaptureList;
            }
        }

        public void InitCaptureScenes()
        {
            if (!IsNeedRefresh())
                return;
            SetRefreshTimer();

            _CaptureList.Clear();
            int randomModel = UnityEngine.Random.Range(0, 2);
            var girlList = GirlMemberPack.Instance.GetGirlsNotInPack();
            if (randomModel > 0)
            {
                AddCaptureSceneByID(girlList, "1", "2", "5");
            }
            else
            {
                AddCaptureSceneByID(girlList, "3", "4", "6");
            }
        }

        public void AddCaptureSceneByID(List<GirlInfoRecord> girlList, params string[] ids)
        {
            foreach (var id in ids)
            {
                var record = TableReader.GirlCaptureScene.GetRecord(id);
                var captureScene = new GirlCaptureScene(record);
                captureScene.InitGirls(girlList);
                _CaptureList.Add(captureScene);
            }
        }

        public const int REFRESH_SECOND = 60;
        public bool IsNeedRefresh()
        {
            var data = System.DateTime.Now - GameCore.Instance.TimeController.GetLastDate(TIMER_TYPE.TIMER_CAPTURE_REFRESH);
            //if (data.TotalSeconds > REFRESH_SECOND)
            {
                
                return true;
            }
            return false;
        }

        public void SetRefreshTimer()
        {
            GameCore.Instance.TimeController.SetTimer(TIMER_TYPE.TIMER_CAPTURE_REFRESH);
        }

        #endregion

        #region capture

        public void CalculateCapture(Dictionary<GirlCapturersRecord, GirlInfoRecord> captureDict)
        {
            List<GirlInfoRecord> captedGirls = new List<GirlInfoRecord>();
            foreach (var capture in captureDict)
            {
                if (!captedGirls.Contains(capture.Value))
                {
                    captedGirls.Add(capture.Value);
                }
            }

            GirlMemberPack.Instance.AddNewGirls(captedGirls);
        }

        #endregion



    }
}
