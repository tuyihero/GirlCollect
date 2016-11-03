using UnityEngine;
using System.Collections;
using System;

using GameBase;
using Tables;
namespace GameLogic
{
    public class LogicManager
    {
        #region 唯一

        private static LogicManager _Instance = null;
        public static LogicManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new LogicManager();
                }
                return _Instance;
            }
        }

        private LogicManager() { }

        #endregion

        #region
       
        #endregion


        #region 接口

        public AsyncOperation StartLoadLogic()
        {
            var sceneLoader = GameCore.Instance.SceneManager.ChangeLogicScene();

            DataManager.Instance.Load();
            DataManager.Instance.InitEvent();

            GameCore.Instance.EventController.PushEvent(EVENT_TYPE.EVENT_LOGIC_LOAD_FINISH, this, new Hashtable());

            return sceneLoader;
        }

        public void StartLogic()
        {
            GameCore.Instance.EventController.PushEvent(EVENT_TYPE.EVENT_LOGIC_LOGIC_START, this, new Hashtable());
        }

        public void EnterFight()
        {


            var sceneLoader = GameCore.Instance.SceneManager.ChangeFightScene();

            GameCore.Instance.UIManager.HideAllUI();

            Hashtable hash = new Hashtable();
            hash.Add("SceneLoader", sceneLoader);
            hash.Add("SceneName", GameCore.Instance.SceneManager.GetFightSceneName());
            GameCore.Instance.EventController.PushEvent(EVENT_TYPE.EVENT_LOGIC_LOAD, this, hash);


        }

        public void EnterFightFinish()
        {
            GameCore.Instance.EventController.PushEvent(EVENT_TYPE.EVENT_LOGIC_FIGHT_START, this, new Hashtable());
        }

        public void ExitFight()
        {
            var sceneLoader = GameCore.Instance.SceneManager.ChangeLogicScene();


            GameCore.Instance.UIManager.HideAllUI();

            Hashtable hash = new Hashtable();
            hash.Add("SceneLoader", sceneLoader);
            hash.Add("SceneName", GameDefine.GAMELOGIC_SCENE_NAME);
            GameCore.Instance.EventController.PushEvent(EVENT_TYPE.EVENT_LOGIC_LOAD, this, hash);
        }

        public void SaveGame()
        {
            DataManager.Instance.Save();
        }

        public void QuitGame()
        {
            try
            {
                SaveGame();
                DataLog.StopLog();
                Application.Quit();
            }
            catch (Exception e)
            {
                Application.Quit();
            }
        }

        public void CleanUpSave()
        {
            
        }
        #endregion
    }
}
