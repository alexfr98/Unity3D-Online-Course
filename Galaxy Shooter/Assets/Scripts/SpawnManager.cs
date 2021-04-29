using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject[] powerUps;

    private GameManager _gameManager;

    //create a corotuine to spawn the Enemy every 5 seconds

    public void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    IEnumerator EnemySpawnRoutine()
    {
        while(!_gameManager.gameOver)
        {

            float randomX = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomX, 6.0f, 0);
            Instantiate(enemyShipPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
        
    }

    

    IEnumerator PowerUpSpawnRoutine()
    {
        while (!_gameManager.gameOver)
        {

            float randomX = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomX, 6.0f, 0);
            Instantiate(powerUps[Random.Range(0,3)], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }

    }

    public void startSpawning()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }


}
