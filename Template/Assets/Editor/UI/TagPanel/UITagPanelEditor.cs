using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(UITagPanel))]
public class UITagPanelEditor : Editor
{
    #region add
    [MenuItem("GameObject/UI/MyUI/UITagPanel")]
    public static void CreateText()
    {
        var textObj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets\\Editor\\UI\\TagPanel\\UITagPanel.prefab");

        InstantiateAsChild(textObj);
    }

    private static void InstantiateAsChild(GameObject prefab)
    {
        GameObject itemGO = GameObject.Instantiate(prefab);

        itemGO.name = itemGO.name.Replace("(Clone)", "");

        Object[] selection = Selection.GetFiltered(typeof(GameObject), SelectionMode.ExcludePrefab);

        if (selection.Length > 0 && selection[0] is GameObject)
        {
            var parentGO = selection[0] as GameObject;

            itemGO.transform.parent = parentGO.transform;
            itemGO.transform.localScale = new Vector3(1,1,1);
        }
    }
    #endregion

    private SerializedObject _TagPanel;
    private SerializedProperty _Tags, _TagPanels;

    private GameObject _TagPrefab;
    private GameObject _TagPanelPrefab;

    private UITagPanel TargetTagPanel
    {
        get
        {
            return target as UITagPanel;
        }
    }

    private string _TagStr = "2";
    // Update is called once per frame
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); 

        string errorMsg = "";

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Tag Count");
        _TagStr = GUILayout.TextField(TargetTagPanel._Tags.Count.ToString());
        errorMsg = ModifyTagCount(_TagStr);
        EditorGUILayout.EndHorizontal();

        if (!string.IsNullOrEmpty(errorMsg))
            GUILayout.Label(errorMsg);
    }

    private string ModifyTagCount(string tagStr)
    {
        int tempCount = 0;
        if (int.TryParse(tagStr, out tempCount))
        {
            if (tempCount > TargetTagPanel._TagPanels.Count)
            {
                GameObject tagPrefab = TargetTagPanel._Tags[0].gameObject;
                GameObject tagPanelPrefab = TargetTagPanel._TagPanels[0];

                int addCount = tempCount - TargetTagPanel._TagPanels.Count;
                for (int i = 0; i < addCount; ++i)
                {
                    var tagGO = GameObject.Instantiate(tagPrefab);
                    tagGO.transform.parent = TargetTagPanel._Tags[0].transform.parent;
                    tagGO.transform.localScale = TargetTagPanel._Tags[0].transform.localScale;
                    var toggle = tagGO.GetComponent<Toggle>();
                    toggle.onValueChanged.GetPersistentMethodName(0);
                    TargetTagPanel._Tags.Add(toggle);

                    tagGO = GameObject.Instantiate(tagPanelPrefab);
                    tagGO.transform.parent = TargetTagPanel._TagPanels[0].transform.parent;
                    tagGO.transform.localScale = TargetTagPanel._Tags[0].transform.localScale;
                    TargetTagPanel._TagPanels.Add(tagGO);
                }
                
            }
            else if (tempCount < TargetTagPanel._TagPanels.Count)
            {
                if (tempCount == 0)
                {
                    return "TagCount could not be 0";
                }

                int delCount = TargetTagPanel._TagPanels.Count - tempCount;
                for (int i = 0; i < delCount; ++i)
                {
                    GameObject.DestroyImmediate(TargetTagPanel._Tags[TargetTagPanel._Tags.Count - 1].gameObject);
                    TargetTagPanel._Tags.RemoveAt(TargetTagPanel._Tags.Count - 1);
                    GameObject.DestroyImmediate(TargetTagPanel._TagPanels[TargetTagPanel._TagPanels.Count - 1]);
                    TargetTagPanel._TagPanels.RemoveAt(TargetTagPanel._TagPanels.Count - 1);
                }
                
            }
            return "";
        }

        return "TagCount pls input a num!";
    }
}
