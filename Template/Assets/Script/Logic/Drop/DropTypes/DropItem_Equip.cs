using UnityEngine;
using System.Collections;

using Tables;

namespace GameLogic
{
    public class DropItem_Equip
    {
        public static void CreateDropItem(DropItemRecord dropItem)
        {
            DropItem_EquipRecord record = TableReader.DropItem_Equip.GetRecord(dropItem.DropType.ID);

            if (record == null)
            {
                GameBase.ErrorManager.PushAndDisplayError("DropItem_Currency record error:" + dropItem.Id);
                return;
            }

            EquipPack.Instance.AddEquip(record.EquipInfo);
            
        }
    }
}