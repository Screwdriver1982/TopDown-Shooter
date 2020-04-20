using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(int sceneNumber, float reloadTime)
    {
        Coroutine SceneReload = StartCoroutine(SceneLoadCoroutine(sceneNumber, reloadTime));
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
