using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using Tables;
using GameLogic;

namespace GameUI
{
    public class UIFightCustomAnimation : UIItemBase
    {

        #region 

        public GridLayoutGroup  _CustomImgPanel;
        public GameObject[] _CustomImgs;
        public Transform _MoveToLeftTran;
        public Transform _MoveToRightTran;

        #endregion

        void Update()
        {
            PlaySwing();
            MoveAnimUpdate();
        }

        #region 

        public void ShowCustomCount(int count)
        {
            _CustomImgPanel.transform.localPosition = new Vector3(0, _CustomImgPanel.transform.localPosition.y, _CustomImgPanel.transform.localPosition.z);
            _CustomImgPanel.enabled = true;
            for (int i = 0; i < _CustomImgs.Length; ++i)
            {
                if (i < count)
                {
                    _CustomImgs[i].transform.parent.gameObject.SetActive(true);
                    _CustomImgs[i].SetActive(true);
                    _CustomImgs[i].transform.localPosition = Vector3.zero;
                }
                else
                {
                    _CustomImgs[i].transform.parent.gameObject.SetActive(false);
                    _CustomImgs[i].SetActive(false);
                }
            }
        }

        #endregion

        #region Animation Swing

        private bool _PlaySwing = false;
        private bool _SwingRight = true;
        public float _SwingPosZ = 400f;
        public float _SwingSpeed = 1000f;

        public void StartPlaySwing()
        {
            _PlaySwing = true;
        }

        private void PlaySwing()
        {
            if (!_PlaySwing)
                return;

            if (_SwingRight)
            {
                _CustomImgPanel.transform.localPosition += new Vector3(_SwingSpeed * Time.deltaTime, 0, 0);
                if (_CustomImgPanel.transform.localPosition.x > _SwingPosZ)
                {
                    _SwingRight = false;
                }
            }
            else
            {
                _CustomImgPanel.transform.localPosition += new Vector3(-_SwingSpeed * Time.deltaTime, 0, 0);
                if (_CustomImgPanel.transform.localPosition.x < -_SwingPosZ)
                {
                    _SwingRight = true;
                }
            }
        }

        #endregion

        #region Animation Move

        public float _MoveSpeed = 1;
        public float _MoveToLeft = -400;
        public float _MoveToRight = 400;

        private bool _IsPlayMove = false;
        private int _LeftCount = 0;
        private int _RightCount = 0;

        public delegate void MoveAnimCallBack();
        MoveAnimCallBack _MoveAnimCallBack;

        public void StartMoveAnim(int leftCount, int rightCount, MoveAnimCallBack moveAnimCallBack)
        {
            //_CustomImgPanel.enabled = false;
            _PlaySwing = false;
            _IsPlayMove = true;

            _LeftCount = leftCount;
            _RightCount = rightCount;

            _MoveAnimCallBack = moveAnimCallBack;
        }

        public void MoveAnimUpdate()
        {
            if (!_IsPlayMove)
                return;

            int inactiveCount = 0;
            for (int i = 0; i < _CustomImgs.Length; ++i)
            {
                if (!_CustomImgs[i].activeInHierarchy)
                {
                    ++inactiveCount;
                    continue;
                }

                if (i < _LeftCount)
                {
                    _CustomImgs[i].transform.position += new Vector3(-_MoveSpeed * Time.deltaTime, 0, 0);
                    if (_CustomImgs[i].transform.position.x < _MoveToLeftTran.transform.position.x)
                    {
                        _CustomImgs[i].gameObject.SetActive(false);
                    }
                }
                else
                {
                    _CustomImgs[i].transform.position += new Vector3(_MoveSpeed * Time.deltaTime, 0, 0);
                    if (_CustomImgs[i].transform.position.x > _MoveToRightTran.transform.position.x)
                    {
                        _CustomImgs[i].gameObject.SetActive(false);
                    }
                }
            }

            if (inactiveCount == _CustomImgs.Length)
            {
                if (_MoveAnimCallBack != null)
                    _MoveAnimCallBack();

                _IsPlayMove = false;
            }
        }

        #endregion


    }
}
