using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using Tables;
using GameBase;
namespace GameUI
{
    public class UICurrencyItem :  UIItemBase
    {
        #region 

        public Image _CurrencyIcon;
        public Text _CurrencyValue;

        #endregion

        #region 

        public void ShowCurrency(CURRENCY_TYPE currencyType, int currencyValue)
        {
            _CurrencyIcon.sprite = GetCurrencySprite(currencyType);
            _CurrencyValue.text = currencyValue.ToString();
        }

        public Sprite GetCurrencySprite(CURRENCY_TYPE currencyType)
        {
            string spriteName = currencyType.ToString();
            return ResourceManager.Instance.GetSprite(spriteName);
        }

        #endregion
    }
}
