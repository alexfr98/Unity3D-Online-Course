using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private GameObject _enemyExplotionPrefab;

    private UIManager _uiManager;

    [SerializeField]
    private AudioClip _clip;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);


        if (transform.position.y < -6.0f)
        {
            float randomX = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomX, 6.0f, 0);
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
                player.Damage();
                //destroy the enemy
                Instantiate(_enemyExplotionPrefab, transform.position, Quaternion.identity);
                AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
                Destroy(this.gameObject);
            }

        }

        if(other.tag == "Laser")
        {
            //destroy the enemy
            
            Destroy(other.gameObject);
            Instantiate(_enemyExplotionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject);
            _uiManager.UpdateScore();


        }


    }
}
