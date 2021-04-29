using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private int powerupID; //0=triple shot, 1 = speed boost, 2 = shield

    [SerializeField]
    private AudioClip _clip;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed *  Time.deltaTime);

        if(transform.position.y < -8)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //access the player
            Player player = other.GetComponent<Player>();

            if (player != null)
            {   
                if (powerupID == 0)
                {
                    //enable triple shot
                    player.TripleShotPowerUpOn();
                    AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
                }

                else if (powerupID == 1)
                {
                    player.SpeedBoostPowerUpOn();
                    AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
                }
                else if (powerupID == 2)
                {
                    player.EnableShields();
                    AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
                }

            }

            //destroy the powerUp
            Destroy(this.gameObject);
        }

    }
}
