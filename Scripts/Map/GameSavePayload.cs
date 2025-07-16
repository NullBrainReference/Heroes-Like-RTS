using UnityEngine;

[System.Serializable]
public class GameSavePayload
{
    [SerializeField]
    private string player_name;
    [SerializeField]
    private string save_data;
    //[SerializeField]
    //private GameSave _save;

    public GameSavePayload(string playerName, string save)
    {
        player_name = playerName;
        save_data = save;
    }

    public GameSave GetSaveData()
    {
        return JsonUtility.FromJson<GameSave>(save_data);
    }
}
