using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class DestroyOutOfBound : MonoBehaviour
{
    void Update()
    {
        // pushing out of ring -> falling -> negative y
        if(transform.position.y < -2)
        {
            // destroy enemy object
            if(transform.CompareTag("Enemy"))
            {
                EventManager.trigger_event("EnemyDestroyed");
                if(transform.name.Contains("Boss"))
                {
                    EventManager.trigger_event("BossDestroyed");
                }
                Destroy(gameObject);
            }
            else if(transform.CompareTag("Player"))
            {
                EventManager.trigger_event("PlayerDestroyed");
            }
        }
        // to catch out of the bound objects that are fast -> destroy these
        if(Math.Abs(transform.position.x) >= 22f || Math.Abs(transform.position.z) >= 22f)
        {
            if(transform.CompareTag("Enemy"))
            {
                EventManager.trigger_event("EnemyDestroyed");
            }
            Destroy(gameObject);
        }
    }
}
