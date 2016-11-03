using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class CreateUIPack
{

    [MenuItem("ProTool/AutoScript/PackScoll")]
    public static void CreatePackScoll()
    {
        string path = EditorUtility.SaveFilePanel("Save png", "", "Temp", "");
        string packName = Path.GetFileNameWithoutExtension(path);

        CreateScript(packName);
        CreatePrefab(packName);
    }

    #region 

    private static void CreateScript(string packName)
    {
        string packScriptPath = Application.dataPath + "\\Script\\UI\\LogicUI\\" + "UI" + packName + "Pack.cs";
        TextAsset packScript = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets\\Editor\\Logic\\UI\\ScriptTemplate\\UITemplatePack.cs");
        using (StreamWriter streamWriter = new StreamWriter(packScriptPath))
        {
            streamWriter.Write(packScript.text.Replace("Template", packName));
            streamWriter.Close();
        }

        string itemScriptPath = Application.dataPath + "\\Script\\UI\\LogicUI\\UIItems\\" + "UI" + packName + "Item.cs";
        TextAsset itemScript = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets\\Editor\\Logic\\UI\\ScriptTemplate\\UITemplateItem.cs");
        using (StreamWriter streamWriter = new StreamWriter(itemScriptPath))
        {
            streamWriter.Write(itemScript.text.Replace("Template", packName));
            streamWriter.Close();
        }
    }

    private static void CreatePrefab(string packName)
    {
        GameObject packTemp = AssetDatabase.LoadAssetAtPath<GameObject>("Assets\\Editor\\Logic\\UI\\PrefabTemplate\\UITemplatePack.prefab");
        GameObject packGO = GameObject.Instantiate<GameObject>(packTemp);
        packGO.name = "UI" + packName + "Pack";
        string packScriptPath = "Assets/Resources/Ui/LogicUI/" + packGO.name + ".prefab";

        PrefabUtility.CreatePrefab(packScriptPath, packGO);

        GameObject itemTemp = AssetDatabase.LoadAssetAtPath<GameObject>("Assets\\Editor\\Logic\\UI\\PrefabTemplate\\UITemplateItem.prefab");
        GameObject itemGO = GameObject.Instantiate<GameObject>(itemTemp);
        itemGO.name = "UI" + packName + "Item";
        string itemPrefabPath = "Assets/Resources/Ui/LogicUI/UIItems/" + itemGO.name + ".prefab";

        PrefabUtility.CreatePrefab(itemPrefabPath, itemGO);
    }

    #endregion
}
