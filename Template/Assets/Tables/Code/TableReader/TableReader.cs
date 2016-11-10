using System.Collections;

namespace Tables
{
    public class TableReader
    {

        #region 唯一实例

        private TableReader() { }

        private TableReader _Instance = null;
        public TableReader Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new TableReader();

                return _Instance;
            }
        }

        #endregion
        #region Logic

//
        public static BuffInfo BuffInfo { get; internal set; }
//
        public static EquipInfo EquipInfo { get; internal set; }
//
        public static FightInfo FightInfo { get; internal set; }
//
        public static GirlCapturers GirlCapturers { get; internal set; }
//
        public static GirlCaptureScene GirlCaptureScene { get; internal set; }
//
        public static GirlInfo GirlInfo { get; internal set; }
//
        public static GirlLevelInfo GirlLevelInfo { get; internal set; }
//
        public static GuestInfo GuestInfo { get; internal set; }
//
        public static SkillInfo SkillInfo { get; internal set; }
//
        public static DropGroup DropGroup { get; internal set; }
//
        public static DropItem DropItem { get; internal set; }
//
        public static FightCapter FightCapter { get; internal set; }
//
        public static FightScene FightScene { get; internal set; }
//
        public static GlobalDefine GlobalDefine { get; internal set; }
//
        public static ItemInfo ItemInfo { get; internal set; }
//
        public static Lottery Lottery { get; internal set; }
//
        public static MissionInfo MissionInfo { get; internal set; }
//
        public static ShopItem ShopItem { get; internal set; }
//
        public static ShopTable ShopTable { get; internal set; }
//
        public static ShowInfo ShowInfo { get; internal set; }
//
        public static DropItem_Currency DropItem_Currency { get; internal set; }
//
        public static DropItem_Equip DropItem_Equip { get; internal set; }
//
        public static ItemType_AddDiamond ItemType_AddDiamond { get; internal set; }
//
        public static ItemType_AddGold ItemType_AddGold { get; internal set; }
//
        public static ItemType_AddTili ItemType_AddTili { get; internal set; }
//
        public static ItemType_Packet ItemType_Packet { get; internal set; }
//
        public static MissionCheck_CatchGirl MissionCheck_CatchGirl { get; internal set; }
//
        public static MissionCheck_Gold MissionCheck_Gold { get; internal set; }
//
        public static MissionCheck_PassStage MissionCheck_PassStage { get; internal set; }
//
        public static MissionCheck_SpecilFight MissionCheck_SpecilFight { get; internal set; }
//
        public static MissionCheck_WebcamFight MissionCheck_WebcamFight { get; internal set; }
//
        public static Impact_AttrChange Impact_AttrChange { get; internal set; }
//
        public static Impact_ModifyCul Impact_ModifyCul { get; internal set; }
//
        public static FightObjPlan_Fixed FightObjPlan_Fixed { get; internal set; }
//
        public static FightObjPlan_Random FightObjPlan_Random { get; internal set; }

        public static void ReadTables()
        {
            //读取所有表
            BuffInfo = new BuffInfo(TableReadBase.GetTableText("BuffInfo"), false);
            EquipInfo = new EquipInfo(TableReadBase.GetTableText("EquipInfo"), false);
            FightInfo = new FightInfo(TableReadBase.GetTableText("FightInfo"), false);
            GirlCapturers = new GirlCapturers(TableReadBase.GetTableText("GirlCapturers"), false);
            GirlCaptureScene = new GirlCaptureScene(TableReadBase.GetTableText("GirlCaptureScene"), false);
            GirlInfo = new GirlInfo(TableReadBase.GetTableText("GirlInfo"), false);
            GirlLevelInfo = new GirlLevelInfo(TableReadBase.GetTableText("GirlLevelInfo"), false);
            GuestInfo = new GuestInfo(TableReadBase.GetTableText("GuestInfo"), false);
            SkillInfo = new SkillInfo(TableReadBase.GetTableText("SkillInfo"), false);
            DropGroup = new DropGroup(TableReadBase.GetTableText("BaseFunc/DropGroup"), false);
            DropItem = new DropItem(TableReadBase.GetTableText("BaseFunc/DropItem"), false);
            FightCapter = new FightCapter(TableReadBase.GetTableText("BaseFunc/FightCapter"), false);
            FightScene = new FightScene(TableReadBase.GetTableText("BaseFunc/FightScene"), false);
            GlobalDefine = new GlobalDefine(TableReadBase.GetTableText("BaseFunc/GlobalDefine"), false);
            ItemInfo = new ItemInfo(TableReadBase.GetTableText("BaseFunc/ItemInfo"), false);
            Lottery = new Lottery(TableReadBase.GetTableText("BaseFunc/Lottery"), false);
            MissionInfo = new MissionInfo(TableReadBase.GetTableText("BaseFunc/MissionInfo"), false);
            ShopItem = new ShopItem(TableReadBase.GetTableText("BaseFunc/ShopItem"), false);
            ShopTable = new ShopTable(TableReadBase.GetTableText("BaseFunc/ShopTable"), false);
            ShowInfo = new ShowInfo(TableReadBase.GetTableText("BaseFunc/ShowInfo"), false);
            DropItem_Currency = new DropItem_Currency(TableReadBase.GetTableText("BaseFunc/DropTypes/DropItem_Currency"), false);
            DropItem_Equip = new DropItem_Equip(TableReadBase.GetTableText("BaseFunc/DropTypes/DropItem_Equip"), false);
            ItemType_AddDiamond = new ItemType_AddDiamond(TableReadBase.GetTableText("BaseFunc/ItemTypes/ItemType_AddDiamond"), false);
            ItemType_AddGold = new ItemType_AddGold(TableReadBase.GetTableText("BaseFunc/ItemTypes/ItemType_AddGold"), false);
            ItemType_AddTili = new ItemType_AddTili(TableReadBase.GetTableText("BaseFunc/ItemTypes/ItemType_AddTili"), false);
            ItemType_Packet = new ItemType_Packet(TableReadBase.GetTableText("BaseFunc/ItemTypes/ItemType_Packet"), false);
            MissionCheck_CatchGirl = new MissionCheck_CatchGirl(TableReadBase.GetTableText("BaseFunc/MissionChecks/MissionCheck_CatchGirl"), false);
            MissionCheck_Gold = new MissionCheck_Gold(TableReadBase.GetTableText("BaseFunc/MissionChecks/MissionCheck_Gold"), false);
            MissionCheck_PassStage = new MissionCheck_PassStage(TableReadBase.GetTableText("BaseFunc/MissionChecks/MissionCheck_PassStage"), false);
            MissionCheck_SpecilFight = new MissionCheck_SpecilFight(TableReadBase.GetTableText("BaseFunc/MissionChecks/MissionCheck_SpecilFight"), false);
            MissionCheck_WebcamFight = new MissionCheck_WebcamFight(TableReadBase.GetTableText("BaseFunc/MissionChecks/MissionCheck_WebcamFight"), false);
            Impact_AttrChange = new Impact_AttrChange(TableReadBase.GetTableText("BuffImpact/Impact_AttrChange"), false);
            Impact_ModifyCul = new Impact_ModifyCul(TableReadBase.GetTableText("BuffImpact/Impact_ModifyCul"), false);
            FightObjPlan_Fixed = new FightObjPlan_Fixed(TableReadBase.GetTableText("FightObjPlan/FightObjPlan_Fixed"), false);
            FightObjPlan_Random = new FightObjPlan_Random(TableReadBase.GetTableText("FightObjPlan/FightObjPlan_Random"), false);

            //初始化所有表
            BuffInfo.CoverTableContent();
            EquipInfo.CoverTableContent();
            FightInfo.CoverTableContent();
            GirlCapturers.CoverTableContent();
            GirlCaptureScene.CoverTableContent();
            GirlInfo.CoverTableContent();
            GirlLevelInfo.CoverTableContent();
            GuestInfo.CoverTableContent();
            SkillInfo.CoverTableContent();
            DropGroup.CoverTableContent();
            DropItem.CoverTableContent();
            FightCapter.CoverTableContent();
            FightScene.CoverTableContent();
            GlobalDefine.CoverTableContent();
            ItemInfo.CoverTableContent();
            Lottery.CoverTableContent();
            MissionInfo.CoverTableContent();
            ShopItem.CoverTableContent();
            ShopTable.CoverTableContent();
            ShowInfo.CoverTableContent();
            DropItem_Currency.CoverTableContent();
            DropItem_Equip.CoverTableContent();
            ItemType_AddDiamond.CoverTableContent();
            ItemType_AddGold.CoverTableContent();
            ItemType_AddTili.CoverTableContent();
            ItemType_Packet.CoverTableContent();
            MissionCheck_CatchGirl.CoverTableContent();
            MissionCheck_Gold.CoverTableContent();
            MissionCheck_PassStage.CoverTableContent();
            MissionCheck_SpecilFight.CoverTableContent();
            MissionCheck_WebcamFight.CoverTableContent();
            Impact_AttrChange.CoverTableContent();
            Impact_ModifyCul.CoverTableContent();
            FightObjPlan_Fixed.CoverTableContent();
            FightObjPlan_Random.CoverTableContent();
        }

        #endregion
    }
}
