using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Tables;

namespace GameLogic
{
    public class DropManager
    {
        public const int TOTAL_RATE = 10000;

        public static List<DropItemRecord> GetAndCreateDropItem(DropGroupRecord dropGroup)
        {
            var dropItems = GetDropItems(dropGroup);
            foreach (var item in dropItems)
            {
                DropItem.CreateDropItem(item);
                Debug.Log("dropItem:" + item.Id);
            }
            return dropItems;
        }

        public static List<DropItemRecord> GetDropItems(DropGroupRecord dropGroup)
        {
            switch (dropGroup.DropGroupType)
            {
                case DROP_GROUP_TYPE.SINGLE:
                    return GetDropSingleRate(dropGroup);
                case DROP_GROUP_TYPE.TOTAL:
                    return GetDropTotalRate(dropGroup);
            }

            return new List<DropItemRecord>();
        }

        public static List<DropItemRecord> GetContainItems(DropGroupRecord dropGroup)
        {
            List<DropItemRecord> items = new List<DropItemRecord>();

            foreach (var item in dropGroup.DropItem)
            {
                if (item != null)
                    items.Add(item);
            }

            return items;
        }

        private static List<DropItemRecord> GetDropSingleRate(DropGroupRecord dropGroup)
        {
            List<DropItemRecord> dropList = new List<DropItemRecord>();
            for (int i = 0; i < dropGroup.DropItem.Count; ++i)
            {
                if (dropGroup.DropItem[i] != null)
                {
                    if (dropGroup.DropRate[i] >= TOTAL_RATE)
                    {
                        dropList.Add(dropGroup.DropItem[i]);
                    }
                    else
                    {
                        int randomRate = Random.Range(0, TOTAL_RATE);
                        if (randomRate < dropGroup.DropRate[i])
                        {
                            dropList.Add(dropGroup.DropItem[i]);
                        }
                    }
                }
            }

            return dropList;
        }

        private static List<DropItemRecord> GetDropTotalRate(DropGroupRecord dropGroup)
        {
            List<DropItemRecord> dropList = new List<DropItemRecord>();
            float totalRate = 0;
            int randomRate = Random.Range(0, TOTAL_RATE);
            for (int i = 0; i < dropGroup.DropItem.Count; ++i)
            {
                if (dropGroup.DropItem[i] != null)
                {
                    totalRate += dropGroup.DropRate[i];
                    if (randomRate < totalRate)
                    {
                        dropList.Add(dropGroup.DropItem[i]);
                        break;
                    }
                }
            }
            return dropList;
        }

        #region 

        public static EquipInfoRecord GetRandomEquip(int level)
        {
            var levelEquips = Tables.TableReader.EquipInfo.GetQualityEquips(level);
            var randomIdx = Random.Range(0, levelEquips.Count);
            return levelEquips[randomIdx];
        }

        #endregion
    }
}