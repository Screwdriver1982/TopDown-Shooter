using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieUI : MonoBehaviour
{
    public Zombie zombie;
    public Slider slider;


    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = zombie.maxHealth;
        zombie.onHealthChanged += UpdateSlider;        
    }

    void UpdateSlider()
    {
        slider.value = zombie.health;
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity; 
    }
}
