using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CreateScriptFromTable
{

    [MenuItem("ProTool/AutoScript/CreateItemsScript")]
    public static void CreateScript()
    {
        Object[] selection = Selection.GetFiltered(typeof(TextAsset), SelectionMode.Assets);
        List<string> tableNames = new List<string>();

        foreach (var selectObj in selection)
        {
            var texAsset = selectObj as TextAsset;
            if (texAsset == null)
                continue;

            tableNames.Add(texAsset.name);
            CreateScript(texAsset.name);
        }
    }

    

    private static void CreateScript(string packName)
    {
        string packScriptPath = Application.dataPath + "\\Script\\Logic\\Fight\\Stage\\StageChecks\\" + packName + ".cs";
        TextAsset packScript = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets\\Editor\\Logic\\Table\\ScriptTemplate\\FightStageCheck_Template.txt");
        using (StreamWriter streamWriter = new StreamWriter(packScriptPath))
        {
            string funName = packName.Substring(packName.IndexOf("_") + 1);
            streamWriter.Write(packScript.text.Replace("Template", funName));
            streamWriter.Close();
        }
    }
}

