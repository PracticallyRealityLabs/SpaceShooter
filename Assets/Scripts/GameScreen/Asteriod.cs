using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteriod : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 15f;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _explosionGameObject;
    private Vector3 _asteroidLocation;
    private void Start()
    {
        _spawnManager = GameObject.FindWithTag("SpawnManager").transform.GetComponent<SpawnManager>();
    }
    void Update()
    {
        transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Laser")
        {
            Destroy(collision.gameObject);
            _spawnManager.enabled = true;
            Instantiate(_explosionGameObject, transform.position, Quaternion.identity);
            Destroy(this.gameObject,0.3f);
        }
    }
}