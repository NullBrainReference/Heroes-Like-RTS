using UnityEngine;

[System.Serializable]
public class GameSavePayload
{
    [SerializeField]
    private string _playerName;
    [SerializeField]
    private string _save;
    //[SerializeField]
    //private GameSave _save;

    public GameSavePayload(string playerName, string save)
    {
        _playerName = playerName;
        _save = save;
    }

    public GameSave GetSaveData()
    {
        return JsonUtility.FromJson<GameSave>(_save);
    }
}
