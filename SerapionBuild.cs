using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;
using System.Collections.Generic;
using System;
using System.IO;

// This is a script that implement build process with CLI for Unity3D projects.
// MAINTAINER Mirnes Halilovic <mirnes.halilovic@serapion.net>
// The pipeline is made up of 2 main steps:
//     1. Build Android App
//     2. Build IOS App

public class SerapionBuild : MonoBehaviour
{
    static private string[] collectBuildScenes()
    {
        var scenes = new List<string>();
        
        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (scene == null)
            continue;
            if (scene.enabled)
            scenes.Add(scene.path);
        }
        return scenes.ToArray();
    }
    
    [MenuItem("Build/Build iOS")]
    public static void IphoneApp()
    {
        
        string folder = Path.Combine(Application.persistentDataPath + @"/CI");
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }
        
        string file = Path.Combine(folder, "IOS-version.txt");
        string currentversion = Application.version;
        string oldversion = File.ReadAllText(file);
        if (!currentversion.Equals(oldversion)) {
            
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = collectBuildScenes();
            buildPlayerOptions.locationPathName = "build/iPhone/";
            buildPlayerOptions.target = BuildTarget.iOS;
            buildPlayerOptions.options = BuildOptions.None;
            
            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;
            
            if (summary.result == BuildResult.Succeeded)
            {
                Debug.Log("Serapion IOS Build succeeded: " + summary.totalSize + " bytes");
            }
            
            if (summary.result == BuildResult.Failed)
            {
                Debug.Log("Serapion IOS Build failed");
            }
            string getVersion = Application.version;
            File.WriteAllText(file, getVersion);
            
        } else {
            Debug.Log("There is no new version for build!");
        }
    }
    
    [MenuItem("Build/Build Android")]
    public static void AndroidApp()
    {
        string folder = Path.Combine(Application.persistentDataPath + @"/CI");
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }
        
        string file = Path.Combine(folder, "Android-version.txt");
        string currentversion = Application.version;
        string oldversion = File.ReadAllText(file);
        if (!currentversion.Equals(oldversion)) {
            
            
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = collectBuildScenes();
            buildPlayerOptions.locationPathName = "build/android/kennig-demo-android.apk";
            buildPlayerOptions.target = BuildTarget.Android;
            buildPlayerOptions.options = BuildOptions.None;
            
            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;
            
            if (summary.result == BuildResult.Succeeded)
            {
                Debug.Log("Serapion ANDROID Build succeeded: " + summary.totalSize + " bytes");
            }
            
            if (summary.result == BuildResult.Failed)
            {
                Debug.Log("Serapion ANDROID Build failed");
            }
            
            string getVersion = Application.version;
            File.WriteAllText(file, getVersion);
            
        } else {
            Debug.Log("There is no new version for build!");
        }
    }
}
