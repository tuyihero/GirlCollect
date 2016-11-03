using UnityEngine;
using System.Collections;

public class testStr : MonoBehaviour {

	// Use this for initialization
	void Start () {

        string testStr = "1,2,3,4,5,\"6,7,8\",9";
        string[] testSplit = testStr.Split(',');
        string testStrCombine = "";
        foreach (var str in testSplit)
        {
            testStrCombine += str + ";";
        }
        Debug.Log("testStrCombine:" + testStrCombine);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
