/****
 * Created by: William Gulick
 * Date Created: 9/20/2021
 * 
 * Last edited by:
 * Last updated: 9/20/2021
 * 
 * Description: Controls player health, manages health bar
 ****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] private float _health = 0f;

    public bool destroyOnDeath = false;
    public GameObject deathEffect = null;
    public Image healthBar;
    public GameManager gm;

    public float HP
    {
        get { return _health; }
        set
        {
            _health = value;
            if (HP >= 100f)
            {
                if (deathEffect != null)
                {
                    Instantiate(deathEffect, transform.position, transform.rotation);
                }

                if (destroyOnDeath)
                {
                    Destroy(gameObject);
                }
            }
        } //end set
    } // end public HP

    // Update is called once per frame
    void Update()
    {
        if(healthBar != null)
        {
            healthBar.fillAmount = 0 + ( HP / 100);
        }
    }
}
