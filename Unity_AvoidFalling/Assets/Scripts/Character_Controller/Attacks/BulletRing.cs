using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRing : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private int num_bullets;
    [SerializeField] private float push_strength;
    [SerializeField] private float forward_offset;

    /*      
        BulletRing is an attack where X bullets are 
        evenly spawned around the attacker.                         
    */
    public void trigger_bullet_ring(string target, Vector3 center_pos)
    {
        float degree_step = 360f / num_bullets;
        for(int i = 0; i < num_bullets; i++)
        {
            GameObject tmp = Instantiate(bullet, center_pos, bullet.transform.rotation);
            // translate bullet by off-set and rotate accordingly
            tmp.transform.Rotate(new Vector3(i * degree_step, 0, 0));
            tmp.transform.Translate(Vector3.up * forward_offset);
            tmp.GetComponent<BulletController>().set_pushStrength_and_target(target, push_strength);
        }
    }
}
