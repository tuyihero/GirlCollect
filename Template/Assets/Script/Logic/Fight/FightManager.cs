﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using Tables;
using GameBase;
namespace GameLogic
{
    public class FightManager
    {
        #region 唯一

        private static FightManager _Instance = null;
        public static FightManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new FightManager();
                }
                return _Instance;
            }
        }

        private FightManager() { }

        #endregion

        #region param

        public FightInfoRecord _FightRecord;
        public FightStageInfo _FightStage;

        private List<FightObjPlan_Base> _FightGuestPlans = new List<FightObjPlan_Base>();

        private List<GuestInfoRecord> _WaveGuests = new List<GuestInfoRecord>();
        private List<FightGirlInfo> _EnemyGirls = new List<FightGirlInfo>();

        private int _FightingWave = 0;
        public int FightWave { get { return _FightingWave; } }

        public FightObj_Player SelfPlayer;
        public FightObj_Player EnemyPlayer;

        public FightGirlInfo GetWaveEnemy()
        {
            if (_EnemyGirls.Count > _FightingWave)
                return _EnemyGirls[_FightingWave];

            return null;
        }

        public GuestInfoRecord GetWaveGuest()
        {
            if (_WaveGuests.Count > _FightingWave)
                return _WaveGuests[_FightingWave];

            return null;
        }
 
        #endregion

        #region StartFight

        public void StartFight(FightStageInfo fightStage)
        {
            _FightStage = fightStage;
            _FightRecord = fightStage.FigetRecord.FightInfo;
            
            InitFightInfo();

            LogicManager.Instance.EnterFight();
        }

        private void InitFightInfo()
        {
            _FightGuestPlans.Clear();
            _WaveGuests.Clear();
            _EnemyGirls.Clear();

            foreach (var objPlan in _FightRecord.ObjPlan)
            {
                if (objPlan == null)
                    continue;

                if (string.IsNullOrEmpty(objPlan.TableName))
                    continue;

                var planBase = (FightObjPlan_Base)Activator.CreateInstance(Type.GetType("GameLogic." + objPlan.TableName));
                planBase.Init(objPlan.ID);
                _FightGuestPlans.Add(planBase);

                _EnemyGirls.Add(planBase.GetEnemyGirl());
                _WaveGuests.Add(planBase.GetGuest());
                Debug.Log("guestWave:" + _WaveGuests.Count + ", count:" + _WaveGuests[_WaveGuests.Count -1 ].Id);
            }

            _FightingWave = -1;
            Debug.Log("_EnemyGirls:" + _EnemyGirls.Count);

            EnemyPlayer = new FightObj_Player();
            EnemyPlayer.Init();

            SelfPlayer = new FightObj_Player();
            SelfPlayer.Init();

            _FightResult = new FightResult();

            InitNextRound();

            ClearAllGirlCD();
        }

        #endregion

        #region fight girl modify

        public const int FIGHT_GIRL_CD = 10;

        public List<GirlMemberInfo> _FightGirls = new List<GirlMemberInfo>();

        public void ClearAllGirlCD()
        {
            foreach (var girlInfo in GirlMemberPack.Instance.GirlList)
            {
                girlInfo._FightCD = 0;
            }
            _FightGirls.Clear();
        }

        public void SetFightGirlCD(GirlMemberInfo girlInfo)
        {
            girlInfo._FightCD = FIGHT_GIRL_CD;
            _FightGirls.Add(girlInfo);
        }

        public void DecFightGirlCD()
        {
            foreach (var girlInfo in GirlMemberPack.Instance.GirlList)
            {
                if (girlInfo._FightCD > 0)
                {
                    --girlInfo._FightCD;
                }
            }
        }

        public void DecGirlLimit()
        {
            foreach (var girlInfo in GirlMemberPack.Instance.GirlList)
            {
                girlInfo.AddVitality(-_FightRecord.VitalityDec);
                girlInfo.AddMood(-_FightRecord.MoodDec);
            }
            _FightGirls.Clear();
        }

        #endregion

        #region round

        public const int ROUND_CALCULATE_COUNT = 4;

        public enum SkillAnimType
        {
            StartBuff = 1,
            Attract1,
            Attract2,
            Attract3,
            Attract4,
        }

        public class BuffResult
        {
            public int BuffIdx;
            public bool IsBuffSelf;
            public int BuffValue;
            public int SkillID;
        }

        public class RoundResult
        {
            public GuestInfoRecord GuestInfo;
            public int SelfAttractNum = 0;
            public int EnemyAttractNum = 0;
            public int SelfPoint = 0;
            public int EnemyPoint = 0;
            public int[] SelfAttractBase;
            public int SelfAttractBaseTotal;
            public int[] SelfAttractFinal;
            public int SelfAttractFinalTotal;
            public int[] EnemyAttractBase;
            public int EnemyAttractBaseTotal;
            public int[] EnemyAttractFinal;
            public int EnemyAttractFinalTotal;
            public int[] SelfPointBase;
            public int SelfPointBaseTotal;
            public int[] SelfPointFinal;
            public int SelfPointFinalTotal;
            public int[] EnemyPointBase;
            public int EnemyPointBaseTotal;
            public int[] EnemyPointFinal;
            public int EnemyPointFinalTotal;
            public List<BuffResult> SelfBuffResult;
            public List<BuffResult> EnemyBuffResult;
            public bool IsFightFinish = false;
        }

        public class FightResult
        {
            public int selfPoint;
            public int enemyPoint;
            public bool isWin = false;
        }
        public FightResult _FightResult = null;

        public bool InitNextRound()
        {
            DecFightGirlCD();

            ++_FightingWave;
            if (_FightingWave >= _WaveGuests.Count)
            {
                FightFinish();
                return false;
            }

            EnemyPlayer.SetFightingGirl(GetWaveEnemy());
            SelfPlayer.SetMemberGirl(null);

            RoundInit();

            return true;

        }

        public void FightFinish()
        {
            
            DecGirlLimit();
            _FightStage.FinishFighting(SelfPlayer.EvaluationValue, EnemyPlayer.EvaluationValue);
        }

        public FightGirlInfo SelectedGirl(GirlMemberInfo girlInfo)
        {
            SelfPlayer.SetMemberGirl(girlInfo);
            return SelfPlayer.FightingGirl;
        }

        public RoundResult RoundCalculate()
        {

            if (SelfPlayer.FightingGirl == null)
            {
                ErrorManager.PushAndDisplayError("FightingGirl empty");
                return null;
            }

            RoundResult roundResult = new RoundResult();
            roundResult.GuestInfo = GetWaveGuest();
            RoundStart(ref roundResult);

            CalculateAttract(ref roundResult);

            CalculatePoint(ref roundResult);

            if (_FightingWave == _WaveGuests.Count - 1)
            {
                roundResult.IsFightFinish = true;
            }

            RoundEnd(ref roundResult);

            SetFightGirlCD(SelfPlayer.FightingGirl._GirlInfo);
            return roundResult;
        }

        private void CalculateAttract(ref RoundResult roundResult)
        {
            var guest = GetWaveGuest();

            int[] selfAttractValues = SelfPlayer.CalculateAttractValue(guest);
            int[] enemyAttractValues = EnemyPlayer.CalculateAttractValue(guest);

            roundResult.SelfAttractBase = selfAttractValues;
            roundResult.EnemyAttractBase = enemyAttractValues;
            roundResult.SelfAttractFinal = new int[ROUND_CALCULATE_COUNT];
            roundResult.EnemyAttractFinal = new int[ROUND_CALCULATE_COUNT];
            for (int i = 0; i < ROUND_CALCULATE_COUNT; ++i)
            {
                roundResult.SelfAttractFinal[i] = roundResult.SelfAttractBase[i];
                roundResult.EnemyAttractFinal[i] = roundResult.EnemyAttractBase[i];
                roundResult.SelfAttractBaseTotal += roundResult.SelfAttractBase[i];
                roundResult.EnemyAttractBaseTotal += roundResult.EnemyAttractFinal[i];
            }

            RoundAttract(ref roundResult);

            int selfAttractValue = 0;
            foreach (var finalValue in roundResult.SelfAttractFinal)
            {
                selfAttractValue += finalValue;
            }
            roundResult.SelfAttractFinalTotal = selfAttractValue;

            int enemyAttractValue = 0;
            foreach (var finalValue in roundResult.EnemyAttractFinal)
            {
                enemyAttractValue += finalValue;
            }
            roundResult.EnemyAttractFinalTotal = enemyAttractValue;

            int selfAttractNum = 0;
            int enemyAttractNum = 0;

            int guestNum = guest.GuestNum;
            int largeAttract = selfAttractValue > enemyAttractValue ? selfAttractValue : enemyAttractValue;
            int attractStep = largeAttract / guestNum;

            while (guestNum > 0)
            {
                --guestNum;

                if (selfAttractValue > enemyAttractValue)
                {
                    ++selfAttractNum;
                    selfAttractValue -= attractStep;
                }
                else
                {
                    ++enemyAttractNum;
                    enemyAttractValue -= attractStep;
                }
            }

            roundResult.SelfAttractNum = selfAttractNum;
            roundResult.EnemyAttractNum = enemyAttractNum;
        }

        private void CalculatePoint(ref RoundResult roundResult)
        {
            var guest = GetWaveGuest();
            var enemyGirl = GetWaveEnemy();

            roundResult.SelfPointBase = SelfPlayer.CalculatePointValue(guest, roundResult.SelfAttractNum);
            roundResult.EnemyPointBase = EnemyPlayer.CalculatePointValue(guest, roundResult.EnemyAttractNum);
            roundResult.SelfPointFinal = new int[ROUND_CALCULATE_COUNT];
            roundResult.EnemyPointFinal = new int[ROUND_CALCULATE_COUNT];
            for (int i = 0; i < ROUND_CALCULATE_COUNT; ++i)
            {
                roundResult.SelfPointFinal[i] = roundResult.SelfPointBase[i];
                roundResult.EnemyPointFinal[i] = roundResult.EnemyPointBase[i];
                roundResult.SelfPointBaseTotal += roundResult.SelfPointBase[i];
                roundResult.EnemyPointBaseTotal += roundResult.EnemyPointFinal[i];
            }

            RoundAttract(ref roundResult);

            int selfPointValue = 0;
            foreach (var finalValue in roundResult.SelfPointFinal)
            {
                selfPointValue += finalValue;
            }
            roundResult.SelfPointFinalTotal = selfPointValue;

            int enemyPointValue = 0;
            foreach (var finalValue in roundResult.EnemyPointFinal)
            {
                enemyPointValue += finalValue;
            }
            roundResult.EnemyPointFinalTotal = enemyPointValue;

            SelfPlayer.EvaluationValue += roundResult.SelfPointFinalTotal;
            EnemyPlayer.EvaluationValue += roundResult.EnemyPointFinalTotal;
        }

        #endregion

        #region round event

        public void RoundInit()
        {
            SelfPlayer.RoundInit();
            EnemyPlayer.RoundInit();
        }

        public void RoundStart(ref RoundResult roundResult)
        {
            SelfPlayer.RoundStart(ref roundResult);
            EnemyPlayer.RoundStart(ref roundResult);
        }

        public void RoundEnd(ref RoundResult roundResult)
        {
            SelfPlayer.RoundEnd(ref roundResult);
            EnemyPlayer.RoundEnd(ref roundResult);
        }

        public void RoundAttract(ref RoundResult roundResult)
        {
            SelfPlayer.RoundAttract(ref roundResult);
            EnemyPlayer.RoundAttract(ref roundResult);
        }

        public void RoundPoint(ref RoundResult roundResult)
        {
            SelfPlayer.RoundPoint(ref roundResult);
            EnemyPlayer.RoundPoint(ref roundResult);
        }



        #endregion

    }
}