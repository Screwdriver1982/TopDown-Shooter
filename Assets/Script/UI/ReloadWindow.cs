using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadWindow : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public GameObject deathWindow;

    Player player;

    // Start is called before the first frame update

    void Start()
    {
        player = FindObjectOfType<Player>();
        player.onDeath += onPlayerDeath;
    }

    // Update is called once per frame
    void onPlayerDeath()
    {
        deathWindow.SetActive(true);
    }

}
