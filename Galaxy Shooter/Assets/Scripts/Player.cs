using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject _explotionPrefab;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleLaserPrefab;

    [SerializeField]
    private GameObject _shieldGameObject;

    [SerializeField]
    private GameObject[] _engines;

    //fireRate is 0.25f
    //canFire --  has the amount of time between firing passed?
    //Time.time
    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;

    public bool canTripleShoot = false;
    public bool canSpeedBoost = false;
    public bool shieldsActive = false;


    [SerializeField]
    private float _speed = 5.0f;

    public int lives = 3;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;

    private AudioSource _audioSource;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager != null)
        {
            _spawnManager.startSpawning(); 
        }

        if (_uiManager != null)
        {
            _uiManager.UpdateLives(lives);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        //if space key pressed
        //spawn laser at player position
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            LaserShooting();
        }

    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");



        //if position player is greater than 4.2
        //set player position to 4,2

        if (canSpeedBoost)
        {
            transform.Translate(Vector3.right * _speed * 1.5f  * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 1.5f * verticalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }

        if (transform.position.y > 4.2f)
        {
            transform.position = new Vector3(transform.position.x, 4.2f, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }

    private void LaserShooting()
    {
       
        if (Time.time > _canFire)
        {

            _audioSource.Play();

            if (canTripleShoot)
            {
                //spawn my laser
                Instantiate(_tripleLaserPrefab, transform.position + new Vector3(0, 1.15f, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.15f, 0), Quaternion.identity);
            }

            _canFire = Time.time + _fireRate;
        }
    }

    public void Damage()
    {
        if (shieldsActive)
        {
            shieldsActive = false;
            _shieldGameObject.SetActive(false);
            return;
        }

        lives--;
        if (_engines[0].activeSelf)
        {
            _engines[1].SetActive(true);
        }
        else if (_engines[1].activeSelf)
        {
            _engines[0].SetActive(true);
        }
        else
        {
            _engines[Random.Range(0, 2)].SetActive(true);
        }
        _uiManager.UpdateLives(lives);
        if (lives == 0)
        {
            Instantiate(_explotionPrefab, transform.position, Quaternion.identity);

            _gameManager.gameOver = true;
            _uiManager.showNewGameImage();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotPowerUpOn()
    {
        canTripleShoot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShoot = false;
    }

    public void SpeedBoostPowerUpOn()
    {
        canSpeedBoost = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canSpeedBoost = false;
    }

    public void EnableShields()
    {
        shieldsActive = true;
        _shieldGameObject.SetActive(true);
    }
}
