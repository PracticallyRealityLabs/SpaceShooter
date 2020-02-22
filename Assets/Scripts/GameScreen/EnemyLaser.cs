using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 8f;
    private float destroyHeight = -8f;
    private void Start()
    {
        this.gameObject.GetComponent<AudioSource>().Play();
    }
    void Update()
    {
        Shoot();
        Destroy();
    }
    void Shoot()
    {
        transform.Translate(Vector3.down * _laserSpeed * Time.deltaTime);
    }
    private void Destroy()
    {
        if (transform.position.y < destroyHeight)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().Damage();
            Destroy(this.gameObject);
        }
    }
}
