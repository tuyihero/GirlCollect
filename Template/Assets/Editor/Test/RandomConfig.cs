using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using GameLogic;

public class RandomConfig
{

    [MenuItem("ProTool/AutoScript/RandomGirlInfo")]
    public static void RandomGirlInfo()
    {
        string randomConfigPath = Application.dataPath + "Editor/RandomConfig/RandomGirlInfo.txt";

    }

    #region Girl Info

    [MenuItem("ProTool/AutoScript/RandomGirl")]
    public static void RandomGirl()
    {
        string randomConfigPath = Application.dataPath + "/Editor/Test/RandomGirlInfo.txt";
        StreamWriter streamWriter = new StreamWriter(randomConfigPath);

        foreach (var girl in Tables.TableReader.GirlInfo.Records)
        {
            streamWriter.WriteLine(GetAttrStr(girl.Value));
        }

        streamWriter.Close();
    }

    public static string GetAttrStr(Tables.GirlInfoRecord girlInfo)
    {
        List<int> starAttrs = null;
        switch (girlInfo.Star)
        {
            case 1:
                starAttrs = GameBase.GameRandom.GetTotalRange(30, 5, 3);
                break;
            case 2:
                starAttrs = GameBase.GameRandom.GetTotalRange(40, 7, 3);
                break;
            case 3:
                starAttrs = GameBase.GameRandom.GetTotalRange(50, 9, 3);
                break;
            case 4:
                starAttrs = GameBase.GameRandom.GetTotalRange(60, 10, 3);
                break;
            case 5:
                starAttrs = GameBase.GameRandom.GetTotalRange(70, 12, 3);
                break;
        }

        List<int> attrValues = new List<int>();

        if (girlInfo.Attr1A > 0)
            attrValues.Add(starAttrs[0]);
        else
            attrValues.Add(0);

        if (girlInfo.Attr1B > 0)
            attrValues.Add(starAttrs[0]);
        else
            attrValues.Add(0);

        if (girlInfo.Attr2A > 0)
            attrValues.Add(starAttrs[1]);
        else
            attrValues.Add(0);

        if (girlInfo.Attr2B > 0)
            attrValues.Add(starAttrs[1]);
        else
            attrValues.Add(0);

        if (girlInfo.Attr3A > 0)
            attrValues.Add(starAttrs[2]);
        else
            attrValues.Add(0);

        if (girlInfo.Attr3B > 0)
            attrValues.Add(starAttrs[2]);
        else
            attrValues.Add(0);

        string attrStr = "";
        foreach (var attrValue in attrValues)
        {
            attrStr += attrValue.ToString() + "\t";
        }

        return attrStr;
    }

    #endregion

    #region random guest 

    private class GuestRandom
    {
        public int GuestCount;
        public List<int> Attracts;
        public List<int> Points;
    }

    [MenuItem("ProTool/AutoScript/RandomGuest")]
    public static void RandomGuest()
    {
        string randomConfigPath = Application.dataPath + "/Editor/Test/RandomGirlInfo.txt";
        StreamWriter streamWriter = new StreamWriter(randomConfigPath);

        for (int i = 1; i < 10; ++i)
        {
            for (int j = 1; j < 10; ++j)
            {
                //for (int k = 1; k < 7; ++k)
                {
                    var guestRandom = GetRandomGuest();
                    string guestStr = guestRandom.GuestCount + "\t"
                        + guestRandom.Attracts[0] + "\t"
                        + guestRandom.Points[0] + "\t"
                        + guestRandom.Attracts[1] + "\t"
                        + guestRandom.Points[1] + "\t"
                        + guestRandom.Attracts[2] + "\t"
                        + guestRandom.Points[2] + "\t"
                        + guestRandom.Attracts[3] + "\t"
                        + guestRandom.Points[3] + "\t"
                        + guestRandom.Attracts[4] + "\t"
                        + guestRandom.Points[4] + "\t"
                        + guestRandom.Attracts[5] + "\t"
                        + guestRandom.Points[5] + "\t";
                    streamWriter.WriteLine(guestStr);
                }
                //streamWriter.WriteLine();
            }
        }

        streamWriter.Close();
        
    }

    private static GuestRandom GetRandomGuest()
    {
        GuestRandom guestRandom = new GuestRandom();
        guestRandom.GuestCount = Random.Range(1, 6);
        guestRandom.Attracts = new List<int>();
        guestRandom.Points = new List<int>();

        var attractList = GameBase.GameRandom.GetTotalRange(1000, 150, 3);
        
        var pointList = GameBase.GameRandom.GetTotalRange(1000, 200, 3);

        for (int i = 0; i < 3; ++i)
        {
            int randomFirst = Random.Range(0, 2);
            if (randomFirst > 0)
            {
                guestRandom.Attracts.Add(attractList[i]);
                guestRandom.Attracts.Add(0);

                guestRandom.Points.Add(pointList[i]);
                guestRandom.Points.Add(0);
            }
            else
            {
                guestRandom.Attracts.Add(0);
                guestRandom.Attracts.Add(attractList[i]);

                guestRandom.Points.Add(0);
                guestRandom.Points.Add(pointList[i]);
            }
        }

        return guestRandom;
    }

    #endregion

    #region Fight Plan

    [MenuItem("ProTool/AutoScript/RandomFightPlan")]
    public static void RandomFightPlan()
    {
        string randomConfigPath = Application.dataPath + "/Editor/Test/RandomGirlInfo.txt";
        StreamWriter streamWriter = new StreamWriter(randomConfigPath);

        //int star = 1;
        //for (int i = 1; i < 10; ++i)
        //{
        //    if (i ==3 || i == 5 || i ==7 || i == 8)
        //        ++star;

        //    for (int j = 1; j < 10; ++j)
        //    {
        //        for (int k = 1; k < 7; ++k)
        //        {
        //            string id = i + "." + j + "." + k;
        //            var guestInfo = Tables.TableReader.GuestInfo.GetRecord(id);

        //            string girlStr = "";
        //            if (j == 1)
        //            {
        //                girlStr = GetGirlByStar(star, guestInfo);
        //            }
        //            else if (j == 2)
        //            {
        //                if(k <6)
        //                    girlStr = GetGirlByStar(star, guestInfo);
        //                else
        //                    girlStr = GetGirlByStar(star + 1, guestInfo);
        //            }
        //            else if (j == 3)
        //            {
        //                if (k < 5)
        //                    girlStr = GetGirlByStar(star, guestInfo);
        //                else
        //                    girlStr = GetGirlByStar(star + 1, guestInfo);
        //            }
        //            else if (j == 4)
        //            {
        //                if (k < 4)
        //                    girlStr = GetGirlByStar(star, guestInfo);
        //                else
        //                    girlStr = GetGirlByStar(star + 1, guestInfo);
        //            }
        //            else if (j == 5)
        //            {
        //                if (k < 3)
        //                    girlStr = GetGirlByStar(star, guestInfo);
        //                else if (k <6)
        //                    girlStr = GetGirlByStar(star + 1, guestInfo);
        //                else
        //                    girlStr = GetGirlByStar(star + 2, guestInfo);
        //            }
        //            else if (j == 6)
        //            {
        //                if (k < 3)
        //                    girlStr = GetGirlByStar(star, guestInfo);
        //                else
        //                    girlStr = GetGirlByStar(star + 1, guestInfo);
        //            }
        //            else if (j == 7)
        //            {
        //                if (k < 2)
        //                    girlStr = GetGirlByStar(star, guestInfo);
        //                else
        //                    girlStr = GetGirlByStar(star + 1, guestInfo);
        //            }
        //            else if (j == 8)
        //            {
        //                if (k < 2)
        //                    girlStr = GetGirlByStar(star, guestInfo);
        //                else if (k < 6)
        //                    girlStr = GetGirlByStar(star + 1, guestInfo);
        //                else
        //                    girlStr = GetGirlByStar(star + 2, guestInfo);
        //            }
        //            else if (j == 9)
        //            {
        //                if (k < 2)
        //                    girlStr = GetGirlByStar(star, guestInfo);
        //                else if (k < 5)
        //                    girlStr = GetGirlByStar(star + 1, guestInfo);
        //                else
        //                    girlStr = GetGirlByStar(star + 2, guestInfo);
        //            }

        //            streamWriter.WriteLine(girlStr);
        //        }
        //        //streamWriter.WriteLine();
        //    }
        //}

        int star = 1;
        for (int j = 1; j < 10; ++j)
        {
            if (j == 3 || j == 5 || j == 7 || j == 8)
                ++star;

            for (int k = 1; k < 10; ++k)
            {
                //for (int k = 1; k < 7; ++k)
                {
                    string id = "sp" + j + "." + k;
                    var guestInfo = Tables.TableReader.GuestInfo.GetRecord(id);

                    string girlStr = "";
                    if (k < 4)
                        girlStr = GetGirlByStar(star, guestInfo);
                    else if (k < 7)
                        girlStr = GetGirlByStar(star + 1, guestInfo);
                    else
                        girlStr = GetGirlByStar(star + 2, guestInfo);

                    streamWriter.WriteLine(girlStr);
                }
                //streamWriter.WriteLine();
            }
        }

        streamWriter.Close();
    }

    private static string GetGirlByStar(int star, Tables.GuestInfoRecord guestInfo)
    {
        Debug.Log(guestInfo.Id + "," + star);
        if (star <= 3)
            return GetGirlByStar(star, false, guestInfo);

        if(star == 4)
            return GetGirlByStar(3, true, guestInfo);

        if (star == 5)
            return GetGirlByStar(4, false, guestInfo);

        if (star == 6)
            return GetGirlByStar(4, true, guestInfo);

        if (star == 7)
            return GetGirlByStar(5, false, guestInfo);

        if (star == 8)
            return GetGirlByStar(5, true, guestInfo);
        
        return "";
    }

    private static string GetGirlByStar(int star, bool isWithEquip, Tables.GuestInfoRecord guestInfo)
    {
        List<Tables.GirlInfoRecord> girls = new List<Tables.GirlInfoRecord>();
        foreach (var girlInfo in Tables.TableReader.GirlInfo.Records)
        {
            if (girlInfo.Value.Star != star)
                continue;

            if (guestInfo.Attr1AAttract > 0 && girlInfo.Value.Attr1A == 0)
                continue;
            if (guestInfo.Attr1BAttract > 0 && girlInfo.Value.Attr1B == 0)
                continue;
            if (guestInfo.Attr2AAttract > 0 && girlInfo.Value.Attr2A == 0)
                continue;
            if (guestInfo.Attr2BAttract > 0 && girlInfo.Value.Attr2B == 0)
                continue;
            if (guestInfo.Attr3AAttract > 0 && girlInfo.Value.Attr3A == 0)
                continue;
            if (guestInfo.Attr3BAttract > 0 && girlInfo.Value.Attr3B == 0)
                continue;

            girls.Add(girlInfo.Value);
        }

        int idx = Random.Range(0, girls.Count);
        
        var girl = girls[idx];

        int equip = isWithEquip ? 1 : 0;

        string girlStr = girl.Id.ToString() + "\t" + equip.ToString() + "\t" + star + "\t" + guestInfo.Id;
        return girlStr;
    }

    #endregion
}
