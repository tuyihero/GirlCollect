using UnityEngine;
using System.Collections;

namespace GameLogic
{
    public class ItemType_Base
    {
        public virtual bool Init(string table, string id) { return false; }

        public virtual bool Use() { return false; }

        public virtual bool Use(int num) { return false; }
    }
}