using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    [SerializeField] private float speed;
    [SerializeField] private GameObject focalPoint;
    [SerializeField] private GameObject powerupIndicator;
    [SerializeField] private bool powered;
    [SerializeField] private int powerUpDuration = 5;
    [SerializeField] private float[] strength; // how hard to hit enemy without and with power-up
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        input_move();
        powerUpInd_follow();
    }

    void input_move()
    {
        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime); 
    }

    void powerUpInd_follow()
    {
        // Set power-up indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);
    }

    // If Player collides with powerup, activate powerup
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            buff();
            Invoke("power_up_cd", (float) powerUpDuration);
        }
    }  
    void buff()
    {
        powered = true;
        speed *= 2;
        powerupIndicator.SetActive(true);
    }

    void power_up_cd()
    {
        powered = false;
        speed /= 2;
        powerupIndicator.SetActive(false);
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer =  (other.gameObject.transform.position - transform.position).normalized; 
           
            if (powered) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * strength[0], ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * strength[1], ForceMode.Impulse);
            }
        }
    }
}
