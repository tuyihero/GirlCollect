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

            var randoms = GameBase.GameRandom.GetIndependentRandoms(0, selectGirls.Count, CaptureSceneRecord.AppearGirlCount);
            for (int i = 0; i < randoms.Count; ++i)
            {
                _AppearGirls.Add(new GirlMemberInfo(selectGirls[randoms[i]].Id));
            }
        }

        public bool TryCaptureGirl(GirlMemberInfo girlInfo, int gold, int luxury, int diamond)
        {
            if (!PlayerData.Instance.TryDecAll(new int[] { gold, luxury, diamond }))
            {
                ErrorManager.PushAndDisplayError("resource not enough");
                return false;
            }

            GirlMemberPack.Instance.AddNewGirl(girlInfo);

            GameCore.PushEvent(EVENT_TYPE.EVENT_LOGIC_CAPTURE_GIRL, null);
            return true;
        }
        #endregion

    }
}
