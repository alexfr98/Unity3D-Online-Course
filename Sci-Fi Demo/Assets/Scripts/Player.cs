using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{ 
    private CharacterController _controller;
    private float _speed = 3.5f;
    private float gravity = 9.81f;
    [SerializeField]
    private GameObject _muzzleFlash;
    [SerializeField]
    private GameObject _hitMarketPrefab;
    [SerializeField]
    private AudioSource _shootSound;
    [SerializeField]
    private int currentAmo;
    private int maxAmo = 50;

    private bool _isReloading;

    private UI_Manager _uiManager;

    [SerializeField]
    private GameObject _weapon;

    //variable bool to knpow if player have the coin
    public bool hasCoin = false;
    public bool hasWeapon = false;

    // Start is called before the first frame update
    void Start()
    {
        //hide mouse course
        _controller = GetComponent<CharacterController>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentAmo = maxAmo;

        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();

    }

    // Update is called once per frame
    void Update()
    {
        //case ray from centger point of main camera

        if(Input.GetMouseButton(0) && currentAmo == 0)
        {
            _uiManager.actionText("Press R to reload");
        }

        if (Input.GetMouseButton(0) && currentAmo >0)
        {

            Shoot();

        }
        else
        {
            _shootSound.Stop();
            _muzzleFlash.SetActive(false);
        }
        //if escpae key is pressed unhide mouse curse
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.R) && !_isReloading)
        {
            _isReloading = true;
            StartCoroutine(Reload());
        }

    }


    void Shoot()
    {
        _muzzleFlash.SetActive(true);
        currentAmo--;
        _uiManager.UpdateAmmo(currentAmo);
        //if audio is noit playing
        //play audio
        if (!_shootSound.isPlaying)
        {
            _shootSound.Play();
        }

        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, out hitInfo))
        {

            GameObject hitMarker = Instantiate(_hitMarketPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(hitMarker, 1.5f);

            //check if we hit the crate
            Destructable crate = hitInfo.transform.GetComponent<Destructable>();
            if(crate != null)
            {
                //Destroy crate
                crate.DestroyCrate();
            }
        }
    }
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * _speed;
        velocity.y -= gravity;

        velocity = transform.transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);
    }

    public void EnablwWeapons()
    {
        _weapon.SetActive(true);
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.5f);
        currentAmo = maxAmo;
        _uiManager.UpdateAmmo(currentAmo);
        _isReloading = false;
        _uiManager.actionText("");
    }

}
