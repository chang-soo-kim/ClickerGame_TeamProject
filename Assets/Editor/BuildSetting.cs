using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class BuildSetting
{
    [MenuItem("MyTools/Build")]
    public static void MyBuild()
    {
        string desktop = "C:/MetaTrend_2";
        string buildPath = desktop + "/MiniGame_2/";
        string[] scene = { "Assets/Scenes/SampleScene.unity" };
        string folder = "";

        FileInfo buildInfo = new FileInfo(buildPath);

        if(buildInfo.Exists == false)
        {
            Directory.CreateDirectory(buildPath);
        }

        folder = buildPath + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + "/";

        FileInfo folderInfo = new FileInfo(folder);

        if (folderInfo.Exists == false) 
        {
            Directory.CreateDirectory(folder);
        }


        BuildPipeline.BuildPlayer(scene, folder + "build.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
    }
}
