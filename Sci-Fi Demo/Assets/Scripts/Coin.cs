using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private AudioClip _coinSound;
    private UI_Manager _uiManager;
    // Start is called before the first frame update
    void Start()
    {

        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    //check for collision
    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {

            _uiManager.actionText("Press E to pick up the coin");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();
                if(player != null)
                {

                    player.hasCoin = true;
                    AudioSource.PlayClipAtPoint(_coinSound, Camera.main.transform.position, 1.0f);

                    if(_uiManager != null)
                    {
                        _uiManager.CollectedCoin();
                    }

                    Destroy(this.gameObject);
                }
                _uiManager.actionDone();


            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        _uiManager.actionDone();
    }


    //check if player is near coin
    //check for e key press
    //give player de coin
    //play coin sound!
    //destroy de coin


}
