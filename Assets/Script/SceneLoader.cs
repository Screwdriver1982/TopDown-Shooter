using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        LoadNextLevel(0);
    }
    public void ReloadScene(float reloadTime)
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Coroutine SceneReload = StartCoroutine(SceneLoadCoroutine(activeSceneIndex, reloadTime));
    }

    public void LoadNextLevel(float reloadTime)
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Coroutine NextLevel = StartCoroutine(SceneLoadCoroutine(activeSceneIndex+1, reloadTime));
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    
    IEnumerator SceneLoadCoroutine(int sceneNumber, float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);
        SceneManager.LoadScene(sceneNumber);
    }



}
