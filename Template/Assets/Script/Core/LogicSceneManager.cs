using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace GameBase
{
    /// <summary>
    /// 场景管理器
    /// </summary>
    public class LogicSceneManager
    {
        #region 唯一

        private static LogicSceneManager _Instance = null;
        public static LogicSceneManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new LogicSceneManager();
                }
                return _Instance;
            }
        }

        private LogicSceneManager() { }

        #endregion

        /// <summary>
        /// 切换逻辑场景
        /// </summary>
        public AsyncOperation ChangeLogicScene()
        {
            return SceneManager.LoadSceneAsync(GameDefine.GAMELOGIC_SCENE_NAME); ;
        }

        /// <summary>
        /// 切换战斗场景
        /// </summary>
        public AsyncOperation ChangeFightScene()
        {
            return SceneManager.LoadSceneAsync(GetFightSceneName()); ;
        }

        public string GetFightSceneName()
        {

            return GameDefine.FIGHT_SCENE_NAME1;
        }

        public bool IsFightScene(string sceneName)
        {
            if (GameDefine.FIGHT_SCENE_NAME1 == sceneName)
                return true;
            return false;
        }

        /// <summary>
        /// 加载进度
        /// </summary>
        public float CheckLoadingProcess()
        {
            //process TODO;
            return 1;
        }
    }
}
