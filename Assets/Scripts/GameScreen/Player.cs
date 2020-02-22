using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 9f;
    private float _upperBound = 0f;
    private float _lowerBound = -3.9f;
    private float _leftBound = -11.3f;
    private float _rightBound = 11.3f;
    [SerializeField]
    private GameObject _laser;
    private Vector3 _laserOffset = new Vector3(0f, 1.05f);
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _trippleShotEnable = false;
    [SerializeField]
    private bool _shieldEnable = false;
    [SerializeField]
    private bool _speedBoostEnable = false;
    [SerializeField]
    private GameObject _trippleShot;
    [SerializeField]
    private float _trippleShotDuration = 5f;
    [SerializeField]
    private float _speedBoostDuration = 5f;
    [SerializeField]
    private float _speedMultiplier = 2f;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private int _score = 0;
    [SerializeField]
    private UI_Manager _uiManager; 
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private AudioClip _laserAudioClip;
    private AudioSource _audioSource;
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UI_Manager>();
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _laserAudioClip;
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is Null");
        }
    }
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            ShootLaser();
        }
    }
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputController = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(inputController * _speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _lowerBound , _upperBound));
        if (transform.position.x < _leftBound)
        {
            transform.position = new Vector3(_rightBound, transform.position.y);
        }
        else if (transform.position.x > _rightBound)
        {
            transform.position = new Vector3(_leftBound, transform.position.y);
        }       
    }
    void ShootLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_trippleShotEnable == true)
        {
            Instantiate(_trippleShot, transform.position , Quaternion.identity);
        }
        else
        {
            Instantiate(_laser, transform.position + _laserOffset, Quaternion.identity);
        }
        _audioSource.Play();
    }
    public void Damage()
    {
        if (_shieldEnable == true)
        {
            ShieldDisable();
        }
        else
        {
            _lives -= 1;
            _uiManager.UpdateLives(_lives);
            if (_lives == 2)
            {
                _rightEngine.SetActive(true);
            }
            else if (_lives == 1)
            {
                _leftEngine.SetActive(true);
            }
            else if (_lives < 1)
            {
                _gameManager.GameOver();
                _spawnManager.onPLayerDeath();
                Destroy(this.gameObject);
            }
        }
    }
    public void TrippleShotActive()
    {
        _trippleShotEnable = true;
        StartCoroutine(TrippleShotPowerDown());
    }
    public void SpeedBoostActivate()
    {
        _speedBoostEnable = true;
        StartCoroutine(SpeedBoostPowerDown());
        _speed *= _speedMultiplier;
    }
    IEnumerator TrippleShotPowerDown()
    {
        {
            yield return new WaitForSeconds(_trippleShotDuration);
            _trippleShotEnable = false;
        }
    }
    IEnumerator SpeedBoostPowerDown()
    {
        yield return new WaitForSeconds(_speedBoostDuration);
        _speedBoostEnable = false;
        _speed /= _speedMultiplier;
    }
    public void ShieldEnable()
    {
        _shieldEnable = true;
        _shield.SetActive(true);
    }
    public void ShieldDisable()
    {
        _shieldEnable = false;
        _shield.SetActive(false);
    }
    public void AddPointsToScore()
    {
        _score += 10;
        _uiManager.UpdateScore(_score);
    }
}
