using UnityEngine;

[System.Serializable]
public class GameSavePayload
{
    [SerializeField]
    private string _playerName;
    [SerializeField]
    private GameSave _save;

    public GameSavePayload(string playerName, GameSave save)
    {
        _playerName = playerName;
        _save = save;
    }
}
