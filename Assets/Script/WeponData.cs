using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class WeponData : MonoBehaviour
{
    string path = Directory.GetCurrentDirectory() + "/Assets/Data/" + "WeponData.csv";

    // StreamReader sr = new StreamReader(Application.dataPath + "/Data/" + "WeponData.csv");

    string data;

    private void Awake()
    {
        StreamReader sr = new StreamReader(path);


        data = sr.ReadToEnd();
        sr.Close();

        Debug.Log(data);
        // 경로 디버그 찍어보기
        // 
        // Directory.GetCurrentDirectory();
    }
    
}
