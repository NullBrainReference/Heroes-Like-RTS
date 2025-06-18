using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadUtil
{
    public static void LoadBattleSync(MapGroup player1, MapGroup player2)
    {
        EventBus.Instance.Invoke(EventType.Save);

        //TODO: replace with better solution
        PlayerPrefs.SetString("player1", JsonUtility.ToJson(player1));
        PlayerPrefs.SetString("player2", JsonUtility.ToJson(player2));

        SceneManager.LoadScene("BattleScene", LoadSceneMode.Single);
    }

    public static void LoadMapSync(MapGroup player1, MapGroup player2)
    {
        player1.RemoveDead();
        player2.RemoveDead();

        //TODO: replace with better solution
        PlayerPrefs.SetString("player1", JsonUtility.ToJson(player1));
        PlayerPrefs.SetString("player2", JsonUtility.ToJson(player2));

        PlayerPrefs.SetString("loadmap", "load");

        SceneManager.LoadScene("MapScene", LoadSceneMode.Single);
    }
}
