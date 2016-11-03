using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CreateUIScript
{

    [MenuItem("ProTool/AutoScript/CreateUIScript")]
    public static void CreateScript()
    {
        Object[] selection = Selection.GetFiltered(typeof(object), SelectionMode.Assets);

        foreach (var selectObj in selection)
        {
            CreateScript(selectObj);
        }
    }

    

    private static void CreateScript(Object uiObject)
    {
        TextAsset packScript = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets\\Editor\\Logic\\UI\\ScriptTemplate\\UITemplateBase.cs");

        string uiObjPath = GetUIObjPath(uiObject);

        string uiObjName = GetUIObjName(uiObject);

        string uiScriptPath = Application.dataPath +  "/Script/UI/" + uiObjPath + ".cs";

        using (StreamWriter streamWriter = new StreamWriter(uiScriptPath))
        {
            string script = packScript.text.Replace("TemplatePath", uiObjPath);
            script = script.Replace("Template", uiObjName);
            streamWriter.Write(script);
            streamWriter.Close();
        }
    }

    private static string GetUIObjPath(Object uiObject)
    {
        string uiObjPath = AssetDatabase.GetAssetPath(uiObject);
        string[] pathStrs = uiObjPath.Split('/');
        string uiPath = "";
        for (int i = 3; i < pathStrs.Length - 1; ++i)
        {
            uiPath += pathStrs[i] + '/';
        }
        uiPath += GetUIObjName(uiObject);
        return uiPath;
    }

    private static string GetUIObjName(Object uiObject)
    {
        return Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(uiObject));
    }
}

