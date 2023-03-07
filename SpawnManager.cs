using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] GameObject[] powerups;
    [SerializeField] GameObject _enemyContainer;
    
    private bool _stopSpawning = false;
   
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

   
    void Update()
    {

    }
    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            float randomx = Random.Range(-9.4f, 9.4f);
            Vector3 randomSpawn = new Vector3(randomx, 7.5f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, randomSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(4f);
        }
    }
    IEnumerator SpawnPowerUpRoutine()
    {
        while (_stopSpawning == false)
        {
            float randomTime = Random.Range(7.0f, 15.0f);
            float randomx = Random.Range(-9.4f, 9.4f);
            Vector3 randomSpawn = new Vector3(randomx, 7.5f, 0);
            int randomPowerUp = Random.Range(0, 3);
            GameObject newPowerUp = Instantiate(powerups[randomPowerUp], randomSpawn, Quaternion.identity);
            yield return new WaitForSeconds(randomTime);
        }


    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}
