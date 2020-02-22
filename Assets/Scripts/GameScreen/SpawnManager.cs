using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyContainer;
    private float _upperBound = 6.9f;
    private float _leftBound = -9f;
    private float _rightBound = 9f;
    [SerializeField]
    private float _enemyWaitTime = 5f;
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject[] _powerup;
    [SerializeField]
    private float _minPowerupWaitTime = 3f;
    [SerializeField]
    private float _maxPowerupWaitTime = 7f;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerup());
    }    
    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            float randomX = Random.Range(_leftBound, _rightBound);
            Vector3 posToSpawn = new Vector3(randomX, _upperBound, 0);
            GameObject newEnenmy = Instantiate(_enemy, posToSpawn , Quaternion.identity);
            newEnenmy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_enemyWaitTime);
        }
    }
    IEnumerator SpawnPowerup()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false) 
        {
            float randomX = Random.Range(_leftBound, _rightBound);
            float randomTime = Random.Range(_minPowerupWaitTime, _maxPowerupWaitTime);
            int randomPowerup = Random.Range(0, 3);
            Vector3 posToSpawn = new Vector3(randomX, _upperBound, 0);
            Instantiate(_powerup[randomPowerup], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(randomTime);
        }
    }
    public void onPLayerDeath()
    {
        _stopSpawning = true;
    }
}
