using UnityEngine;

public static class DataManager
{
    public static bool IsFirstPlayTime
    {
        get => PrefManager.GetBool(KeyManager.KeyIsFirstPlayTime, true);
        set => PrefManager.SetBool(KeyManager.KeyIsFirstPlayTime, value);
    }

    public static int CoinInGame
    {
        get => PrefManager.GetInt(KeyManager.KeyCoinInGame, 1000);
        set => PrefManager.SetInt(KeyManager.KeyCoinInGame, value);
    }

    public static string PlayerName
    {
        get => PrefManager.GetString(KeyManager.KeyPlayerName, "PlayerName");
        set => PrefManager.SetString(KeyManager.KeyPlayerName, value);  
    }

    public static int LevelPlayUnlocked
    {
        get => PrefManager.GetInt(KeyManager.KeyLevelPlayUnlocked, 1);
        set => PrefManager.SetInt(KeyManager.KeyLevelPlayUnlocked, value);  
    }

    public static int LevelPlaying
    {
        get => PrefManager.GetInt(KeyManager.KeyLevelPlaying, 1);
        set => PrefManager.SetInt(KeyManager.KeyLevelPlaying, value);
    }
}
