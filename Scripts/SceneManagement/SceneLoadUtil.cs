using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadUtil
{
    public static void LoadBattleSync(MapGroup player1, MapGroup player2)
    {
        //TODO: replace with better solution
        PlayerPrefs.SetString("player1", JsonUtility.ToJson(player1));
        PlayerPrefs.SetString("player2", JsonUtility.ToJson(player2));

        SceneManager.LoadScene("BattleScene", LoadSceneMode.Single);
    }
}
