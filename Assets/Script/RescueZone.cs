using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RescueZone : MonoBehaviour
{
    public float nextLevelTime;

    public bool activated = true;
    public Color activeColor;
    public Color deactiveColor;
    
    public SceneLoader sceneLoader;

    SpriteRenderer sR;
    // Start is called before the first frame update
    void Awake()
    {
        
        sR = GetComponent<SpriteRenderer>();
        sR.color = activeColor;
    }

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Activate(bool onOff)
    {
        activated = onOff;
        if (onOff)
        {
            sR.color = activeColor;
        }
        else
        {
            sR.color = deactiveColor;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null & activated)
        {
            sceneLoader.LoadNextLevel(nextLevelTime);
        }

    }
    
}
