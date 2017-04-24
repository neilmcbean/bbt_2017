using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.Security.Cryptography;


[ExecuteInEditMode]
public class TestData : MonoBehaviour
{
    #if UNITY_EDITOR
    public bool generate;

    public string fileName;
    public SentenceObject data;

    // Use this for initialization
    void Update()
    {
        if (generate)
        {
            generate = false;
            string jsonData = JsonUtility.ToJson(data);
            File.WriteAllText("Assets/" + fileName + ".txt", jsonData);
            AssetDatabase.Refresh();
        }
    }
    #endif
}
