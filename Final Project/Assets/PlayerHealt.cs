using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealt : MonoBehaviour
{
    // public Slider slider;
    public float healt = 100;
    
    //public void SetMaxHealth(int health)
    //{
    //    slider.maxValue = health;
    //    slider.value = health;
    //}

    //public void SetHealth(int health)
    //{
    //    slider.value = health;
    //}

    public void TakeDamage(int damage)
    {
        healt -= damage;
    }

}
