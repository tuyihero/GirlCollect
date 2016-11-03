using UnityEngine;
using System.Collections;

using Tables;
namespace GameLogic
{
    public class ItemType_AddDiamond : ItemType_Base
    {

        #region 
        //private ItemType_AddGoldRecord _ItemTypeRecord;

        public override bool Init(string table, string id)
        {
            //_ItemTypeRecord = TableReader.ItemType_AddGold.GetRecord(id);
            return true;
        }

        public override bool Use(int addNum)
        {
            return true;
        }

        #endregion

    }
}