using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public float maxHealth = 200f;
    public float health;
    public string[] upgrade;
    public float buildTime = 100f;
    public bool build = false;

    public Image healthBar;


    private void Start()
    {
        healthBar.fillAmount = 0;
    }

    public void Update()
    {
        if(build)
        {
            IncHealth();
        }
    }
    public void IncHealth()
    {
        if (health < maxHealth)
        {
            health += maxHealth / buildTime * Time.deltaTime;
            healthBar.fillAmount = health / (maxHealth / 100) / 100;
        }
        else
        {
            build = false;
            health = maxHealth;
        }
    }
}
