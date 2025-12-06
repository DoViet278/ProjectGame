using System;
using UnityEngine;

public static class PrefManager
{
    public static int GetInt(string key, int defaultValue = 0) => PlayerPrefs.GetInt(key, defaultValue);
    public static void SetInt(string key, int val) 
    { 
        PlayerPrefs.SetInt(key, val);
        PlayerPrefs.Save();
    }
    
    public static float GetFloat(string key, float defaultValue = 0) => PlayerPrefs.GetFloat(key, defaultValue);
    public static void SetFloat(string key, float val) 
    { 
        PlayerPrefs.SetFloat(key, val);
        PlayerPrefs.Save();
    }
    
    public static string GetString(string key, string defaultValue = "") => PlayerPrefs.GetString(key, defaultValue);
    public static void SetString(string key, string val) 
    { 
        PlayerPrefs.SetString(key, val);
        PlayerPrefs.Save();
    }
    
    public static bool GetBool(string key, bool defaultValue = false) => GetInt(key, defaultValue ? 1 : 0) == 1;
    public static void SetBool(string key, bool val) => SetInt(key, val ? 1 : 0);
}
