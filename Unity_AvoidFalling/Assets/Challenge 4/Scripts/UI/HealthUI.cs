using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int max_health;
    // list containing current health images
    [SerializeField] private List<GameObject> health_imgs;
    // health prefab for creating new health objects
    [SerializeField] private GameObject health_prefab;
    [SerializeField] private Vector3 first_health_pos = new Vector3(37.1f, -24.4f, 0);
    [SerializeField] private float y_dist = 50;
    // Start is called before the first frame update
    void Start()
    {
        create_hearts();
        EventManager.register("WrongScored", health_lost);
    }

    void create_hearts()
    {
        health = max_health;
        // instantiate health GameObjects
        for (int i = 1; i <= max_health; i++)
        {
            Vector3 new_pos = first_health_pos + Vector3.down * (i-1) * y_dist;
            health_imgs.Add((GameObject) Instantiate(health_prefab, gameObject.transform, false));
            // changing the position in the overlay at runtime
            health_imgs[i-1].GetComponent<RectTransform>().anchoredPosition = new_pos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // method reduces current health, if 0 is reached method returns false
    void health_lost()
    {
        health -= 1;
        // destroy health object as highest index in the list
        GameObject lost_health_obj = health_imgs[health];
        health_imgs.RemoveAt(health);
        Destroy(lost_health_obj);
        // if health reaches zero, game is lost
        if(health == 0)
        {
            EventManager.trigger_event("GameOver");
            EventManager.unregister("WrongScored", health_lost);
        }
    }
}
