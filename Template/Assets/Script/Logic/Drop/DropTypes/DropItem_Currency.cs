using UnityEngine;
using System.Collections;

using Tables;

namespace GameLogic
{
    public class DropItem_Currency
    {
        public static void CreateDropItem(DropItemRecord dropItem)
        {
            DropItem_CurrencyRecord record = TableReader.DropItem_Currency.GetRecord(dropItem.DropType.ID);

            if (record == null)
            {
                GameBase.ErrorManager.PushAndDisplayError("DropItem_Currency record error:" + dropItem.Id);
                return;
            }

            int currencyValue = 0;
            if (record.CurrencyValueMin >= record.CurrencyValueMax)
            {
                currencyValue = record.CurrencyValueMin;
            }
            else
            {
                currencyValue = Random.Range(record.CurrencyValueMin, record.CurrencyValueMax);
            }

            PlayerData.Instance.AddCurrency(record.CurrencyType, currencyValue);
            
        }
    }
}