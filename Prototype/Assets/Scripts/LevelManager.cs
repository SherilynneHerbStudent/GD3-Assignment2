using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public GameObject GameScene;
    public GameObject BurrowScene;
    public GameObject Burrow;
    public GameObject GameCamera;

    public void SceneChanger(int level)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(level);
    }
    public void ExitGame()
    {
        Application.Quit();
    }


   


}