using UnityEngine;

public static class SaveSystem
{
    public static void SavePlayerPosition(Vector3 pos)
    {
        PlayerPrefs.SetFloat("player_x", pos.x);
        PlayerPrefs.SetFloat("player_y", pos.y);
        PlayerPrefs.SetFloat("player_z", pos.z);
        PlayerPrefs.Save();
    }

    public static Vector3 LoadPlayerPosition(Vector3 fallback)
    {
        if (!PlayerPrefs.HasKey("player_x")) return fallback;
        return new Vector3(PlayerPrefs.GetFloat("player_x"), PlayerPrefs.GetFloat("player_y"), PlayerPrefs.GetFloat("player_z"));
    }
}
