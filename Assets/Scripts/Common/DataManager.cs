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
}
