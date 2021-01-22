using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{ 
 
    public float maxHealth;
    public float curHealth;

    // Use this for initialization
    void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        AddjustCurrentHealth(0);
    }

    public void AddjustCurrentHealth(float Damage)
    {
        curHealth += Damage;

        if (curHealth < 0)
            curHealth = 0;

        if (maxHealth < 1)
            maxHealth = 1;

    }

    
}
