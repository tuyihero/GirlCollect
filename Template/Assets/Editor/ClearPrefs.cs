using UnityEngine;
using UnityEditor;
using System.Collections;

public class ClearPrefs : MonoBehaviour
{

    [MenuItem("ProTool/ClearPref")]
    public static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
