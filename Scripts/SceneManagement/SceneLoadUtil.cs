using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadUtil
{
    public static void LoadBattleSync(MapGroup player1, MapGroup player2)
    {
        SceneManager.LoadScene("BattleScene", LoadSceneMode.Single);
    }
}
