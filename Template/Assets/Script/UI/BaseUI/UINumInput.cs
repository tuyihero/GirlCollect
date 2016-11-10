using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

using GameBase;
using System;

namespace GameUI
{
    public class UINumInput : UIItemBase
    {

        #region param

        public InputField _InputField;

        private int _Value;
        public int Value
        {
            get
            {

                if (!int.TryParse(_InputField.text, out _Value))
                    _Value = 0;
                return _Value;
            }
            set
            {
                _Value = value;
                _InputField.text = _Value.ToString();
            }
        }

        private int _MaxValue = -1;
        private int _MinValue = 0;
        #endregion

        #region 

        public void Init(int initValue, int minValue, int maxValue)
        {
            Value = initValue;
            _MaxValue = maxValue;
            _MinValue = minValue;
        }

        #endregion

        #region 

        public void BtnAdd(int stepValue)
        {
            int resValue = Value + stepValue;
            if (_MaxValue > _MinValue && _MaxValue > 0 && _MinValue >= 0)
            {
                Value = Mathf.Clamp(resValue, _MinValue, _MaxValue);
            }
            else if(_MinValue >= 0)
            {
                Value = Mathf.Min(resValue, _MinValue);
            }

            ValueChange();
        }

        public void BtnDec(int stepValue)
        {
            int resValue = Value - stepValue;
            if (_MaxValue > _MinValue && _MaxValue > 0 && _MinValue >= 0)
            {
                Value = Mathf.Clamp(resValue, _MinValue, _MaxValue);
            }
            else if (_MinValue >= 0)
            {
                Value = Mathf.Min(resValue, _MinValue);
            }

            ValueChange();
        }

        public void InputChange()
        {
            if (!int.TryParse(_InputField.text, out _Value))
                _Value = 0;
            ValueChange();
        }
        #endregion

        #region value change

        [System.Serializable]
        public class OnChangeEvent : UnityEvent<int>
        {
            public OnChangeEvent() { }
        }

        [SerializeField]
        public OnChangeEvent OnValueChange;

        public void ValueChange()
        {
            if (OnValueChange != null)
            {
                OnValueChange.Invoke(_Value);
            }
        }


        #endregion
    }
}
