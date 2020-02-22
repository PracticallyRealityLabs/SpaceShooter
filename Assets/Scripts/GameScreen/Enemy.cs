using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private float _upperBound = 06.9f;
    private float _lowerBound = -5.5f;
    private float _leftBound = -9f;
    private float _rightBound = 9f;
    private Player _player;
    private Animator _enemyAnimator;
    private Collider2D  _enemyCollider;
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _laser;
    private float _laserFireRate;
    private float _canFire = -1f;
    private Vector3 _laserLocation;
    private void Start()
    {
        _player = GameObject.FindWithTag("Player").transform.GetComponent<Player>();
        _enemyAnimator = GetComponent<Animator>();
        _enemyCollider = GetComponent<Collider2D>();
        _audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        EnemyMovementController();
        EnemyFireController();
    }
    private void EnemyMovementController()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y < _lowerBound)
        {     
            float randomX = Random.Range(_leftBound, _rightBound);
            transform.position = new Vector3(randomX, _upperBound);
        }
        
    }
    private void EnemyFireController()
    {
        if (Time.time > _canFire && _enemyCollider.enabled == true)
        {
            _laserLocation = new Vector3(transform.position.x, transform.position.y - 1.3f, transform.position.z);
            _laserFireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _laserFireRate;
            Instantiate(_laser, _laserLocation, Quaternion.identity);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            _enemyAnimator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _enemyCollider.enabled = false;
            _audioSource.Play();
            if (player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject, 2.8f);
        }
        else if (other.tag == "Laser")
        {          
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddPointsToScore();
            }
            _enemyAnimator.SetTrigger("OnEnemyDeath");
            _enemyCollider.enabled = false;
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.8f);
        }
    }
   

}
