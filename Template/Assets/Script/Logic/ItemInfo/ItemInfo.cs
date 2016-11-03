using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Tables;

namespace GameLogic
{
    public class ItemInfo
    {

        #region

        private ItemInfoRecord _ItemRecord;
        public ItemInfoRecord ItemRecord { get { return _ItemRecord; } }
        public string ID
        {
            get
            {
                return _ItemRecord.Id;
            }
        }

        private ItemType_Base _ItemType;

        public void Init(string id, int num)
        {
            var record = TableReader.ItemInfo.GetRecord(id);
            Init(record, num);
        }

        public void Init(ItemInfoRecord record, int num)
        {
            _ItemRecord = record;
            Num = num;

            _ItemType = (ItemType_Base)System.Activator.CreateInstance(System.Type.GetType("GameLogic." + record.Type.TableName));
            _ItemType.Init(record.Type.TableName, record.Type.ID);
        }

        #endregion

        #region

        public int Num;

        public bool DecNum()
        {
            if (Num > 0)
            {
                --Num;
                return true;
            }
            return false;
        }

        public bool Use(int num = 1)
        {
            if(num == 1)
                return _ItemType.Use();
            return _ItemType.Use(num);
        }

        #endregion
    }
}