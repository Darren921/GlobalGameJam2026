using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader: MonoBehaviour
{
    public void Loadscene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
