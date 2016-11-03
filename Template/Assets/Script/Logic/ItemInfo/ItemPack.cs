using System.Collections;
using System.Collections.Generic;

using Tables;
using GameBase;
namespace GameLogic
{
    public class ItemPack 
    {

        #region 单例

        private static ItemPack m_Instance;
        public static ItemPack Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new ItemPack();
                }
                return m_Instance;
            }
        }

        private ItemPack() { }

        #endregion

        #region 

        private List<ItemInfo> _ItemList = new List<ItemInfo>();

        public void CreateItem(string id, int num)
        {
            ItemInfo itemInfo = new ItemInfo();
            itemInfo.Init(id, num);
            _ItemList.Add(itemInfo);
        }

        public void CreateAndUseItem(ItemInfoRecord itemRecord, int num = 1)
        {
            ItemInfo itemInfo = new ItemInfo();
            itemInfo.Init(itemRecord.Id, 1);
            itemInfo.Use(num);

        }
        public void CreateAndUseItem(string id, int num = 1)
        {
            ItemInfo itemInfo = new ItemInfo();
            itemInfo.Init(id, 1);
            itemInfo.Use(num);

        }

        public ItemInfo GetItemByType(string itemType)
        {
            foreach (var itemInfo in _ItemList)
            {
                if (itemInfo.ItemRecord.Type.TableName == itemType)
                {
                    return itemInfo;
                }
            }
            return null;
        }

        #endregion

        #region save/load

        public void SavePack()
        {
            string packDataStr = "";
            foreach (var infoPair in _ItemList)
            {
                packDataStr += infoPair.ID + "," + infoPair.Num + ";";
            }
            LocalSave.Save(LocalSaveType.ITEM_PACK, packDataStr);
        }

        public void LoadPack()
        {
            int num = 0;
            string packDataStr = LocalSave.LoadString(LocalSaveType.ITEM_PACK);
            string[] dataStrs = packDataStr.Split(';');
            foreach (string dataStr in dataStrs)
            {
                string[] attrStrs = dataStr.Split(',');
                if (attrStrs.Length == 2)
                {
                    if (int.TryParse(attrStrs[1], out num))
                    {
                        CreateItem(attrStrs[0], num);
                    }
                }
            }
        }

        public void ClearData()
        {
            _ItemList.Clear();
        }
        #endregion

    }
}