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

    public static int LevelPlay
    {
        get => PrefManager.GetInt(KeyManager.KeyLevelPlay, 1);
        set => PrefManager.SetInt(KeyManager.KeyLevelPlay, value);  
    }
}
