using System.Collections;

namespace Tables
{

    //
    public enum CURRENCY_TYPE
    {
        None = 0, //,枚举必须保留0值
        GOLD = 1, //,
        LUXURY = 2, //,
        DIAMOND = 3, //,
    }

    //
    public enum DROP_GROUP_TYPE
    {
        None = 1, //空,
        SINGLE = 2, //单独计算概率,
        TOTAL = 3, //整体计算概率,
    }

    //
    public enum DROP_ITEM_TYPE
    {
        None = 0, //空,
        ITEM = 1, //物品,
        CAR = 2, //车辆,
        DRIVER = 3, //车手,
        DIAMOND = 4, //钻石,
        GOLD = 5, //金币,
    }

    //
    public enum IMPACT_MODIFY_TARGET
    {
        None = 0, //,枚举必须保留0值
        ATTR1A = 1, //ATTR1A,ATTR1A
        ATTR1B = 2, //ATTR1B,ATTR1B
        ATTR2A = 3, //ATTR2A,ATTR2A
        ATTR2B = 4, //ATTR2B,ATTR2B
        ATTR3A = 5, //ATTR3A,ATTR3A
        ATTR3B = 6, //ATTR3B,ATTR3B
        ATTRACK = 7, //ATTRACK,ATTRACK
        POINT = 8, //POINT,POINT
    }

    //
    public enum IMPACT_MODIFY_TYPE
    {
        None = 0, //,枚举必须保留0值
        FIXED = 1, //,
        PERSEND = 2, //,
    }

    //
    public enum ITEM_QUALITY
    {
        None = 0, //空,
        WHITE = 1, //白,
        GREEN = 2, //绿,
        BLUE = 3, //蓝,
        PURPLE = 4, //紫,
        ORINGE = 5, //橙,
    }

    //
    public enum ITEM_TYPE
    {
        None = 0, //空,
        NORMAL = 1, //普通,
        MONEY = 2, //货币,
        PACKET = 3, //礼包,
    }

    //
    public enum MISSION_TYPE
    {
        None = 0, //空,
        DAYLY_MISSION = 1, //每日任务,
        ACHIEVE_MISSION = 2, //成就任务,
    }

    //
    public enum PLAYER_VALUE_TYPE
    {
        None = 0, //,枚举必须保留0值
        EVALUATION = 1, //,
        ATTR1A = 2, //,
        ATTR1B = 3, //,
        ATTR2A = 4, //,
        ATTR2B = 5, //,
        ATTR3A = 6, //,
        ATTR3B = 7, //,
    }

    //
    public enum SKILL_ACT_TERM
    {
        None = 0, //,枚举必须保留0值
        NEED_LARGE1 = 1, //NEED_LARGE1,
        NEED_LARGE2 = 2, //NEED_LARGE2,
        NEED_SMALL1 = 3, //NEED_SMALL1,
        NEED_SMALL2 = 4, //NEED_SMALL2,
        NEED_SAME1 = 5, //NEED_SAME1,
        NEED_SAME2 = 6, //NEED_SAME2,
        NEED_SERIES = 7, //NEED_SERIES,
    }

    //
    public enum SKILL_ACT_TIMING
    {
        None = 0, //,枚举必须保留0值
        ROUNT_START = 1, //ROUNT_START,
        ROUNT_ATTRACT = 2, //ROUNT_ATTRACT,
        ROUNT_POINT = 3, //ROUNT_POINT,
        ROUNT_FINISH = 4, //ROUNT_FINISH,
        FIGHT_START = 5, //FIGHT_START,
        FIGHT_FINISH = 6, //FIGHT_FINISH,
    }

    //
    public enum SKILL_TARGET_TYPE
    {
        None = 0, //,枚举必须保留0值
        Self = 1, //Self,
        Enemy = 2, //Enemy,
    }


}