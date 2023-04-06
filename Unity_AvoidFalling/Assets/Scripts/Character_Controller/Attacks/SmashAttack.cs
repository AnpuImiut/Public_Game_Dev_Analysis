using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashAttack : MonoBehaviour
{
    // Ability variables
    [SerializeField] private float cd;
    [SerializeField] private float smash_prob;


    // Control variables
    [SerializeField] private KeyCode button;
    [SerializeField] private bool player_control;
    private Rigidbody object_rb;


    // Jumping variables
    private bool available = true;
    private bool jumping = false;
    [SerializeField] private float jump_strength;
    [SerializeField] private float falling_strength;
    [SerializeField] private float max_height;
    

    // Force-field variables
    [SerializeField] private GameObject force_field;
    [SerializeField] private float push_strength;
    // size of the force-field expansion
    [SerializeField] private float[] collider_sizes;
    

    // Others
    [SerializeField] private AudioClip smash_sound;
    private AudioSource audio_source;
    [SerializeField] private GameObject particle_animation;

    void Start()
    {
        object_rb = transform.GetComponent<Rigidbody>();
        audio_source = transform.GetComponent<AudioSource>();
    }

    void Update()
    {
        if(available && !jumping)
        {
            // player smash attack
            if(player_control && Input.GetKeyDown(button))
            {
                do_smash_attack();
            }
            // enemy smash attack
            else if(!player_control && RandomGenerator.get_instance().NextDouble() < smash_prob)
            {
                do_smash_attack();
            }
        }
        if(jumping)
        {
            cap_max_height();
        }
    }

    void cap_max_height()
    {
        // make object fall after reaching max height
        if(transform.position.y >= max_height)
        {
            object_rb.velocity = Vector3.zero;
            object_rb.useGravity = true;
            // faster falling
            object_rb.AddForce(Vector3.down * falling_strength, ForceMode.Force);
        }
    }

    void do_smash_attack()
    {
        jumping = true;
        available = false;
        object_rb.useGravity = false;
        object_rb.velocity = Vector3.up * jump_strength;
    }

    void smash_impact()
    {
        // spawn_position for particle animation has to be adjusted slightly
        Vector3 spawn_pos = new Vector3(transform.position.x, -0.5f, transform.position.z) + 
            object_rb.velocity * Time.deltaTime * 10f;
        Instantiate(particle_animation, spawn_pos, particle_animation.transform.rotation);
        audio_source.PlayOneShot(smash_sound, 0.5f);
    }

    private void OnCollisionEnter(Collision other) 
    {
        // on contact with the ground the smash attack enters spawns a force-field
        if(other.gameObject.CompareTag("Ground") && jumping)
        {
            jumping = false;
            smash_impact();
            create_force_field();
            Invoke("reset_availability", cd);
        }
    }

    // cool-down on smash attack
    private void reset_availability()
    {
        available = true;
    }

    // create the force-field that can push objects
    private void create_force_field()
    {
        GameObject tmp = Instantiate(force_field, transform.position, force_field.transform.rotation);
        // set parent b/c force-field needs to know some parameters from the parent
        tmp.transform.parent = gameObject.transform;
    }

    public bool is_player_controlled() {return player_control;}
    public float get_push_strength() {return push_strength;}
    public float[] get_collider_sizes() {return collider_sizes;} 
    public void set_availability(bool val) {available = val;}
}
