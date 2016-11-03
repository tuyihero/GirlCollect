using UnityEngine;
using System.Collections;

using Tables;

namespace GameLogic
{
    public class DropItem
    {
        public static void CreateDropItem(DropItemRecord dropItem)
        {
            var dropType = System.Type.GetType("GameLogic." + dropItem.DropType.TableName);
            var dropFun = dropType.GetMethod("CreateDropItem", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            dropFun.Invoke(null, new object[] { dropItem });
        }
    }
}