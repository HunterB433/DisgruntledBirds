// only Need 
using UnityEngine;
// Working withScenes
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Functions for each
    public void LoadMenu()
    {
        Debug.Log("Loading 0");
        SceneManager.LoadScene(0);
    }
    public void LoadFirst()
    {
        Debug.Log("Loading 1");
        SceneManager.LoadScene(1);
    }

    public void LoadSecond()
    {
        Debug.Log("Loading 2");
        SceneManager.LoadScene(2);
    }

    public void LoadThird()
    {
        Debug.Log("Loading 3");
        SceneManager.LoadScene(3);
    }
}
