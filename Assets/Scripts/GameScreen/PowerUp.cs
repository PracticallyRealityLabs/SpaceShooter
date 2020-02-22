using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _powerupSpeed = 3f;
    private float _upperBound = 06.9f;
    private float _lowerBound = -5.5f;
    private float _leftBound = -9f;
    private float _rightBound = 9f;
    [SerializeField]
    private AudioClip _audioClip;
    [SerializeField]
    private int powerupID;

    
    void Update()
    {
        transform.Translate(Vector3.down * _powerupSpeed * Time.deltaTime); 
        if (transform.position.y < _lowerBound)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch(powerupID)
                {
                    case 0:
                        player.TrippleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActivate();
                        break;
                    case 2:
                        player.ShieldEnable();
                        break;
                }
            }
            AudioSource.PlayClipAtPoint(_audioClip, transform.position);
            Destroy(this.gameObject);
        }
        
    }
}
