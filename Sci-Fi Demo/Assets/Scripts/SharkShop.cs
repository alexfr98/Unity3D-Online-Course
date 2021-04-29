using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{

    [SerializeField]
    private AudioClip _saleSound;
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

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                if (player.hasCoin == true)
                {
                    _uiManager.actionText("Press 'E' to buy a weapon");
                }
                else if (player.hasWeapon == false && player.hasCoin == false)
                {
                    _uiManager.actionText("SHARK: You have to have a coin to buy a wapon. Get out of here!");
                }
                else if(player.hasCoin == false && player.hasWeapon == true)
                {
                    _uiManager.actionText("SHARK: I don't have more weapons. See you soon!");
                }
                   
            }

            if (Input.GetKeyDown(KeyCode.E) && player.hasCoin ==true )
            {
                if(player.hasWeapon== false)
                {
                    player.hasWeapon = true;
                    player.hasCoin = false;
                    _uiManager.giveCoin();
                    AudioSource.PlayClipAtPoint(_saleSound, Camera.main.transform.position, 1.0f);
                    player.EnablwWeapons();
                    _uiManager.actionDone();
                }
                
            }

            

        }
    }
    private void OnTriggerExit(Collider other)
    {
        _uiManager.actionDone();
    }

}
