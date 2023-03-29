using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    private int powered_up;
    [SerializeField] float power_up_strength = 20f;
    [SerializeField] float power_up_time = 4f;
    [SerializeField] private GameObject power_up_indicator = null;
    private Rigidbody player_rb;
    [SerializeField] private AudioClip power_up_sound;
    private AudioSource audio_source;

    void Start()
    {
        player_rb = transform.GetComponent<Rigidbody>();
        audio_source = transform.GetComponent<AudioSource>();
        powered_up = 0;
        EventManager.register("DestroyPlayer", destroy_player);
    }

    void Update()
    {
        if(power_up_indicator == null)
        {
            attach_power_up_indicator();
        }
        move_player();
        if(power_up_indicator.activeSelf)
        {
            power_indicator_update_position();
        }
    }

    void destroy_player()
    {
        power_up_indicator.SetActive(true);
        EventManager.unregister("DestroyPlayer", destroy_player);
        Destroy(power_up_indicator);
        Destroy(gameObject);
    }

    void move_player()
    {
        // horizontal movement
        player_rb.AddForce(Vector3.right * Input.GetAxis("Horizontal") * speed, ForceMode.Acceleration);
        // vertical movement
        player_rb.AddForce(Vector3.forward * Input.GetAxis("Vertical") * speed, ForceMode.Acceleration);
        // makes turning more smooth
        // player_rb.velocity = new Vector3(player_rb.velocity.x * 0.99f, player_rb.velocity.y, player_rb.velocity.z  * 0.99f);
    }

    void attach_power_up_indicator()
    {
        power_up_indicator = GameObject.Find("PowerUpIndicator");
        power_up_indicator.SetActive(false);
    }

    void power_indicator_update_position()
    {
        power_up_indicator.transform.position = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        // collect strength power-up and buff player temporarily
        if(other.transform.CompareTag("PowerUp") && other.GetComponent<Enemy_Info>().get_id() == "PowerUpStrength")
        {
            Destroy(other.gameObject);
            audio_source.PlayOneShot(power_up_sound, 0.6f);
            power_up_indicator.SetActive(true);
            powered_up += 6;
            Invoke("loose_power_up", power_up_time);
        }
        // collect bullet-shooting power-up and activate it once
        else if(other.transform.CompareTag("PowerUp") && other.GetComponent<Enemy_Info>().get_id() == "PowerUpShoot")
        {
            other.GetComponent<BulletRing>().trigger_bullet_ring(target: "Enemy", transform.position);
            Destroy(other.gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        // only when powered up, enemies can get pushed back
        if(other.transform.CompareTag("Enemy") && powered_up > 0)
        {
            enemy_push_back(other);
        }
    }

    void enemy_push_back(Collision other)
    {
        // pushes the enemy away relative to their direction and the player
        Vector3 away_dir = transform.GetComponent<Rigidbody>().velocity.normalized;
        Vector3 repel_dir = (other.transform.position - transform.position).normalized;
        Vector3 push_dir = (-away_dir + repel_dir);

        // depending on the enemy type, different levels of pushing strength is need
        float strength_factor = 1f;
        ForceMode force_mode = ForceMode.Force;
        switch(other.gameObject.GetComponent<Enemy_Info>().get_id())
        {
            default:
                break;
            case "Enemy_normal":
                force_mode = ForceMode.VelocityChange;
                break;
            case "Enemy_smasher":
                force_mode = ForceMode.VelocityChange;
                break;
            case "Enemy_fast":
                force_mode = ForceMode.VelocityChange;
                strength_factor = 0.5f;
                break;
            case "Boss":
                force_mode = ForceMode.VelocityChange;
                strength_factor = 0.5f;
                break;
        }
        // push the enemy
        other.transform.GetComponent<Rigidbody>().AddForce(
            push_dir * power_up_strength * strength_factor, force_mode
        );
    }

    void loose_power_up()
    {
        // power-up is a integer and works like a countdown instead of a switch
        powered_up -= 6;
        if(powered_up == 0)
        {
            power_up_indicator.SetActive(false);
        }
    }

    public int is_powered_up() {return powered_up;}
}
