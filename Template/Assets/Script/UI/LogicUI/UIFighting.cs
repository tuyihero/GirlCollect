using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

using GameBase;
using GameLogic;
namespace GameUI
{

    public class UIFighting : UIBase
    {
        #region static funs

        public static void ShowAsyn()
        {
            Hashtable hash = new Hashtable();
            GameCore.Instance.UIManager.ShowUI("LogicUI/UIFighting", hash);
        }

        public static List<EVENT_TYPE> GetShowEvent()
        {
            List<EVENT_TYPE> showEvents = new List<EVENT_TYPE>();

            showEvents.Add(EVENT_TYPE.EVENT_LOGIC_FIGHT_START);

            return showEvents;
        }

        #endregion

        #region params

        public UIGirlMemberItem[] _SelfGirls;
        public UIGirlMemberItem[] _EnemyGirls;

        public UIFightCustom _CustomInfo;

        public Animator _Animator;

        private List<GirlMemberInfo> SelectedGirls = new List<GirlMemberInfo>();
        #endregion

        #region show funs

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            InitFight();
        }

        public void Update()
        {
            ResultAnimUpdate();
        }

        public void InitFight()
        {
            RefreshShow();

            foreach (var girlItem in _SelfGirls)
            {
                girlItem._PanelClickEvent = BtnShowSelect;
            }
        }

        public void RefreshShow()
        {
            for (int i = 0; i < _SelfGirls.Length; ++i)
            {
                //if (FightManager.Instance.SelfPlayer.FightingGirls.Count > i)
                //{
                //    _SelfGirls[i].InitGirl(FightManager.Instance.SelfPlayer.FightingGirls[i]);
                //}
                //else
                {
                    _SelfGirls[i].InitGirl(null);
                }

                if (FightManager.Instance.EnemyPlayer.FightingGirls.Count > i)
                {
                    _EnemyGirls[i].InitGirl(FightManager.Instance.EnemyPlayer.FightingGirls[i]);
                }
                else
                {
                    _EnemyGirls[i].InitGirl(null);
                }
            }

            _CustomInfo.ShowCustom(FightManager.Instance.GetWaveGuest());
        }
        #endregion

        #region inact

        public void BtnShowSelect(UIItemBase itemBase)
        {
            UIGirlMemberItem selectItem = itemBase as UIGirlMemberItem;
            SelectedGirls.Remove(selectItem._GirlMenberInfo);
            RefreshGirlItems();

            UIGirlSelectPanel.ShowAsyn(OnSelectMember, OnUnSelectMember, SelectedGirls, false);
        }

        private void OnSelectMember(GirlMemberInfo girlInfo)
        {
            SelectedGirls.Add(girlInfo);
            RefreshGirlItems();

            if (SelectedGirls.Count == _SelfGirls.Length)
            {
                UIGirlSelectPanel.HideAsyn();
            }
        }

        private void OnUnSelectMember(GirlMemberInfo girlInfo)
        {
            SelectedGirls.Remove(girlInfo);
            RefreshGirlItems();
        }

        public void RefreshGirlItems()
        {
            for (int i = 0; i < _SelfGirls.Length; ++i)
            {
                if (SelectedGirls.Count > i)
                    _SelfGirls[i].InitGirl(SelectedGirls[i]);
                else
                    _SelfGirls[i].InitGirl(null);
            }
            RefreshSkill();
        }

        public void RoundFight()
        {
            FightManager.Instance.SelfPlayer.SetMemberGirl(SelectedGirls);

            _RoundResult = FightManager.Instance.RoundCalculate();
            if (_RoundResult == null)
                return;

            StartRecultAnim();

            SelectedGirls.Clear();
        }

        private void StartNextRound()
        {
            Debug.Log("self :" + _RoundResult.SelfAttractNum + ", " + _RoundResult.SelfPoint);
            Debug.Log("enemy :" + _RoundResult.EnemyAttractNum + ", " + _RoundResult.EnemyPoint);

            if (!_RoundResult.IsFightFinish)
            {
                FightManager.Instance.InitNextRound();
                RefreshShow();
            }
            else
            {
                FightManager.Instance.FightFinish();
            }
        }

        #endregion

        #region skill

        private void RefreshSkill()
        {
            for (int i = 0; i < _SelfGirls.Length; ++i)
            {
                if (_SelfGirls[i]._GirlMenberInfo == null)
                    continue;
                for (int j = 0; j < _SelfGirls[i]._GirlMenberInfo.GirlInfoRecord.Skills.Count; ++j)
                {
                    if (_SelfGirls[i]._GirlMenberInfo.GirlInfoRecord.Skills[j] == null)
                        continue;

                    if (SkillManager.CanGirlSkillUse(_SelfGirls[i]._GirlMenberInfo, _SelfGirls[i]._GirlMenberInfo.GirlInfoRecord.Skills[j], SelectedGirls))
                    {
                        _SelfGirls[i].SetSkillCanUse(j, true);
                    }
                    else
                    {
                        _SelfGirls[i].SetSkillCanUse(j, false);
                    }

                }
                
            }
        }

        #endregion

        #region Result Anim

        public GameObject _ResultPanel;
        public GameObject _InfoPanel;
        public Image _ResultBackGround;
        public Text _ResultTitle;
        public Text[] _ResultAttrs;
        public Text[] _SelfPointTx;
        public Text[] _EnemyPointTx;
        public Image[] _SelfBuffImg;
        public Image[] _EnemyBuffImg;
        public Text _SelfTotalPoint;
        public Text _EnemyTotalPoint;
        public Text _SelfTotalNum;
        public Text _EnemyTotalNum;

        public float RESULT_MOVE_DISTANCE = 400;
        public float RESULT_MOVE_SPEED = 700;
        public float RESULT_NUM_ACT1_TIME = 0.5f;
        public float RESULT_NUM_ACT2_TIME = 0.5f;
        public float RESULT_NUM_ACT_TOTAL_TIME = 0.1f;
        public float RESULT_NUM_ACT_GUEST_TIME = 0.1f;
        public float RESULT_ATTRACT_END_WAIT_TIME = 0.3f;

        private float _ResultNumAct1StartTime = 0;
        private float _ResultNumAct2StartTime = 0;
        private float _ResultNumTotalStartTime = 0;
        private float _ResultNumGuestStartTime = 0;
        private float _ResultAttractEndWaitTime = 0;

        private FightManager.RoundResult _RoundResult;
        private enum ResultAnimStage
        {
            None,
            AttractMoveIn,
            AttractNum1,
            AttractNum2,
            AttractNumTotal,
            AttractNumGuest,
            AttractEnd,
            AttractGuestMove,
            PointMoveIn,
            PointNum1,
            PointNum2,
            PointNumTotal,
            PointEnd,
            Finish
        }
        private ResultAnimStage _ResultAnimStage = ResultAnimStage.None;

        private void ResultAnimUpdate()
        {
            switch (_ResultAnimStage)
            {
                case ResultAnimStage.None:
                    return;
                case ResultAnimStage.AttractMoveIn:
                    ShowAttactResultBGAnim();
                    break;
                case ResultAnimStage.AttractNum1:
                    ShowAttractNumAnim1();
                    break;
                case ResultAnimStage.AttractNum2:
                    ShowAttractNumAnim2();
                    break;
                case ResultAnimStage.AttractNumTotal:
                    ShowAttractNumTotal();
                    break;
                case ResultAnimStage.AttractNumGuest:
                    ShowAttractNumGuest();
                    break;
                case ResultAnimStage.AttractEnd:
                    ShowAttactEnd();
                    break;
                case ResultAnimStage.AttractGuestMove:
                    ShowGuestMove();
                    break;
                case ResultAnimStage.PointMoveIn:
                    ShowPointBGAnim();
                    break;
                case ResultAnimStage.PointNum1:
                    ShowPointNumAnim1();
                    break;
                case ResultAnimStage.PointNum2:
                    ShowPointNumAnim2();
                    break;
                case ResultAnimStage.PointNumTotal:
                    ShowPointNumTotal();
                    break;
                case ResultAnimStage.PointEnd:
                    ShowPointEnd();
                    break;

            }

        }

        private void StartRecultAnim()
        {
            InitResultPanel();
            _ResultAnimStage = ResultAnimStage.AttractMoveIn;
        }

        private void InitResultPanel()
        {
            _ResultPanel.SetActive(true);

            Vector3 resultBGMovePos = _ResultBackGround.transform.localPosition + new Vector3(0, RESULT_MOVE_DISTANCE, 0);
            _ResultBackGround.transform.localPosition = resultBGMovePos;

            //foreach (var go in _ResultAttrs)
            //{
            //    go.gameObject.SetActive(false);
            //}

            //foreach (var go in _SelfPointTx)
            //{
            //    go.gameObject.SetActive(false);
            //}

            //foreach (var go in _EnemyPointTx)
            //{
            //    go.gameObject.SetActive(false);
            //}

            //foreach (var go in _SelfBuffImg)
            //{
            //    go.gameObject.SetActive(false);
            //}

            //foreach (var go in _EnemyBuffImg)
            //{
            //    go.gameObject.SetActive(false);
            //}

            _ResultAttrs[0].text = _RoundResult.GuestInfo.Attr1AAttract > _RoundResult.GuestInfo.Attr1BAttract ? GirlMemberInfo.ATTR_NAMES[0] : GirlMemberInfo.ATTR_NAMES[1];
            _ResultAttrs[1].text = _RoundResult.GuestInfo.Attr2AAttract > _RoundResult.GuestInfo.Attr2BAttract ? GirlMemberInfo.ATTR_NAMES[2] : GirlMemberInfo.ATTR_NAMES[3];
            _ResultAttrs[2].text = _RoundResult.GuestInfo.Attr3AAttract > _RoundResult.GuestInfo.Attr3BAttract ? GirlMemberInfo.ATTR_NAMES[4] : GirlMemberInfo.ATTR_NAMES[5];
            _SelfTotalPoint.text = "";
            _EnemyTotalPoint.text = "";
            _SelfTotalNum.text = "";
            _EnemyTotalNum.text = "";

            _InfoPanel.SetActive(false);
        }

        private void ShowAttactResultBGAnim()
        {
            _ResultBackGround.gameObject.transform.localPosition += new Vector3(0, RESULT_MOVE_SPEED * Time.deltaTime, 0);

            if (_ResultBackGround.gameObject.transform.localPosition.y < 0)
            {
                _ResultBackGround.gameObject.transform.localPosition = Vector3.zero;
                _ResultAnimStage = ResultAnimStage.AttractNum1;
            }
        }

        private void ShowAttractNumAnim1()
        {
            if (_ResultNumAct1StartTime == 0)
            {
                _ResultNumAct1StartTime = RESULT_NUM_ACT1_TIME;
                _InfoPanel.SetActive(true);

                _SelfTotalPoint.text = "";
                _EnemyTotalPoint.text = "";
                _SelfTotalNum.text = "";
                _EnemyTotalNum.text = "";
            }
            else
            {
                _ResultNumAct1StartTime -= Time.deltaTime;
                if (_ResultNumAct1StartTime < 0)
                {
                    _ResultNumAct1StartTime = 0;
                    for (int i = 0; i < _SelfPointTx.Length; ++i)
                    {
                        _SelfPointTx[i].text = _RoundResult.SelfAttractBase[i].ToString();
                        _EnemyPointTx[i].text = _RoundResult.EnemyAttractBase[i].ToString();
                    }

                    _ResultAnimStage = ResultAnimStage.AttractNum2;
                }
            }

            for (int i = 0; i < _SelfPointTx.Length; ++i)
            {
                _SelfPointTx[i].text = ((int)((_RoundResult.SelfAttractBase[i] / RESULT_NUM_ACT1_TIME) * (RESULT_NUM_ACT1_TIME - _ResultNumAct1StartTime))).ToString();
                _EnemyPointTx[i].text = ((int)((_RoundResult.EnemyAttractBase[i] / RESULT_NUM_ACT1_TIME) * (RESULT_NUM_ACT1_TIME - _ResultNumAct1StartTime))).ToString();
            }
        }

        private void ShowAttractNumAnim2()
        {
            if (_ResultNumAct2StartTime == 0)
            {
                _ResultNumAct2StartTime = RESULT_NUM_ACT2_TIME;
                for (int i = 0; i < _SelfPointTx.Length; ++i)
                {
                    string valueStr = _RoundResult.SelfAttractBase[i].ToString() + "+<color=#00ff00ff>" + (_RoundResult.SelfAttractFinal[i] - _RoundResult.SelfAttractBase[i]).ToString() + "</color>";
                    _SelfPointTx[i].text = valueStr;
                    valueStr = _RoundResult.EnemyAttractBase[i].ToString() + "+<color=#00ff00ff>" + (_RoundResult.EnemyAttractFinal[i] - _RoundResult.EnemyAttractBase[i]).ToString() + "</color>";
                    _EnemyPointTx[i].text = valueStr;
                }
            }
            else
            {
                _ResultNumAct2StartTime -= Time.deltaTime;
                if (_ResultNumAct2StartTime <= 0)
                {
                    _ResultNumAct2StartTime = 0;

                    _ResultAnimStage = ResultAnimStage.AttractNumTotal;

                }
            }
        }

        private void ShowAttractNumTotal()
        {
            if (_ResultNumTotalStartTime == 0)
            {
                _ResultNumTotalStartTime = RESULT_NUM_ACT_TOTAL_TIME;
                _SelfTotalPoint.text = _RoundResult.SelfAttractFinalTotal.ToString();
                _EnemyTotalPoint.text = _RoundResult.EnemyAttractFinalTotal.ToString();
            }
            else
            {
                _ResultNumTotalStartTime -= Time.deltaTime;
                if (_ResultNumTotalStartTime <= 0)
                {
                    _ResultNumTotalStartTime = 0;
                    
                    _ResultAnimStage = ResultAnimStage.AttractNumGuest;

                }
            }
        }

        private void ShowAttractNumGuest()
        {
            if (_ResultNumTotalStartTime == 0)
            {
                _ResultNumTotalStartTime = RESULT_NUM_ACT_TOTAL_TIME;

                _SelfTotalNum.text = _RoundResult.SelfAttractNum.ToString();
                _EnemyTotalNum.text = _RoundResult.EnemyAttractNum.ToString();
            }
            else
            {
                _ResultNumTotalStartTime -= Time.deltaTime;
                if (_ResultNumTotalStartTime <= 0)
                {
                    _ResultNumTotalStartTime = 0;
                    _ResultAnimStage = ResultAnimStage.AttractEnd;

                    

                    _InfoPanel.SetActive(false);

                }
            }
        }

        private void ShowAttactEnd()
        {
            _ResultBackGround.gameObject.transform.localPosition += new Vector3(0, -RESULT_MOVE_SPEED * Time.deltaTime, 0);

            if (_ResultBackGround.gameObject.transform.localPosition.y > RESULT_MOVE_DISTANCE)
            {
                _ResultBackGround.gameObject.transform.localPosition = new Vector3(_ResultBackGround.gameObject.transform.localPosition.x, RESULT_MOVE_DISTANCE, _ResultBackGround.gameObject.transform.localPosition.y);
                _ResultAnimStage = ResultAnimStage.AttractGuestMove;
            }
        }

        private void ShowGuestMove()
        {
            _CustomInfo.PlayCustomMove(_RoundResult.SelfAttractNum, _RoundResult.EnemyAttractNum, MoveAnimCallBack);
        }

        private void MoveAnimCallBack()
        {
            if (_ResultAttractEndWaitTime == 0)
            {
                _ResultAttractEndWaitTime = RESULT_ATTRACT_END_WAIT_TIME;
            }
            else
            {
                _ResultAttractEndWaitTime -= Time.deltaTime;
                if (_ResultAttractEndWaitTime <= 0)
                {
                    _ResultAttractEndWaitTime = 0;
                    _ResultAnimStage = ResultAnimStage.PointMoveIn;
                }
            }
        }

        private void ShowPointBGAnim()
        {
            _ResultBackGround.gameObject.transform.localPosition += new Vector3(0, RESULT_MOVE_SPEED * Time.deltaTime, 0);

            if (_ResultBackGround.gameObject.transform.localPosition.y < 0)
            {
                _ResultBackGround.gameObject.transform.localPosition = Vector3.zero;
                _ResultAnimStage = ResultAnimStage.PointNum1;
                
            }
        }

        private void ShowPointNumAnim1()
        {
            if (_ResultNumAct1StartTime == 0)
            {
                _ResultNumAct1StartTime = RESULT_NUM_ACT1_TIME;
                _InfoPanel.SetActive(true);
                _SelfTotalPoint.text = "";
                _EnemyTotalPoint.text = "";
                _SelfTotalNum.text = "";
                _EnemyTotalNum.text = "";
            }
            else
            {
                _ResultNumAct1StartTime -= Time.deltaTime;
                if (_ResultNumAct1StartTime < 0)
                {
                    _ResultNumAct1StartTime = 0;
                    for (int i = 0; i < _SelfPointTx.Length; ++i)
                    {
                        _SelfPointTx[i].text = _RoundResult.SelfPointBase[i].ToString();
                        _EnemyPointTx[i].text = _RoundResult.EnemyPointBase[i].ToString();
                    }
                    
                    _ResultAnimStage = ResultAnimStage.PointNum2;
                }
            }

            for (int i = 0; i < _SelfPointTx.Length; ++i)
            {
                _SelfPointTx[i].text = ((int)((_RoundResult.SelfPointBase[i] / RESULT_NUM_ACT1_TIME) * (RESULT_NUM_ACT1_TIME - _ResultNumAct1StartTime))).ToString();
                _EnemyPointTx[i].text = ((int)((_RoundResult.EnemyPointBase[i] / RESULT_NUM_ACT1_TIME) * (RESULT_NUM_ACT1_TIME - _ResultNumAct1StartTime))).ToString();
            }
        }

        private void ShowPointNumAnim2()
        {
            if (_ResultNumAct2StartTime == 0)
            {
                _ResultNumAct2StartTime = RESULT_NUM_ACT2_TIME;
                for (int i = 0; i < _SelfPointTx.Length; ++i)
                {
                    string valueStr = _RoundResult.SelfPointBase[i].ToString() + "+<color=#00ff00ff>" + (_RoundResult.SelfPointFinal[i] - _RoundResult.SelfPointBase[i]).ToString() + "</color>";
                    _SelfPointTx[i].text = valueStr;
                    valueStr = _RoundResult.EnemyPointBase[i].ToString() + "+<color=#00ff00ff>" + (_RoundResult.EnemyPointFinal[i] - _RoundResult.EnemyPointBase[i]).ToString() + "</color>";
                    _EnemyPointTx[i].text = valueStr;
                }
            }
            else
            {
                _ResultNumAct2StartTime -= Time.deltaTime;
                if (_ResultNumAct2StartTime <= 0)
                {
                    _ResultNumAct2StartTime = 0;

                    _ResultAnimStage = ResultAnimStage.PointNumTotal;

                }
            }
            
        }

        private void ShowPointNumTotal()
        {
            if (_ResultNumTotalStartTime == 0)
            {
                _ResultNumTotalStartTime = RESULT_NUM_ACT_TOTAL_TIME;
                _SelfTotalPoint.text = _RoundResult.SelfPointFinalTotal.ToString();
                _EnemyTotalPoint.text = _RoundResult.EnemyPointFinalTotal.ToString();
            }
            else
            {
                _ResultNumTotalStartTime -= Time.deltaTime;
                if (_ResultNumTotalStartTime <= 0)
                {
                    _ResultNumTotalStartTime = 0;
                    
                    _ResultAnimStage = ResultAnimStage.PointEnd;
                    _InfoPanel.SetActive(false);

                }
            }
        }

        private void ShowPointEnd()
        {
            _ResultBackGround.gameObject.transform.localPosition += new Vector3(0, -RESULT_MOVE_SPEED * Time.deltaTime, 0);

            if (_ResultBackGround.gameObject.transform.localPosition.y > RESULT_MOVE_DISTANCE)
            {
                _ResultBackGround.gameObject.transform.localPosition = new Vector3(_ResultBackGround.gameObject.transform.localPosition.x, RESULT_MOVE_DISTANCE, _ResultBackGround.gameObject.transform.localPosition.y);
                _ResultAnimStage = ResultAnimStage.None;
                StartNextRound();
            }
        }

        #endregion

        
    }
}
