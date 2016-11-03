using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

using UnityEngine;

namespace Tables
{
    public partial class GlobalDefineRecord  : TableRecordBase
    {
       
    }

    public partial class GlobalDefine : TableFileBase
    {
        public static GlobalDefineRecord _TheRecord = null;
        public static GlobalDefineRecord TheRecord
        {
            get
            {
                if (_TheRecord == null)
                {
                    _TheRecord = TableReader.GlobalDefine.GetRecord("9999999");
                }
                return _TheRecord;
            }
        }
    }

}