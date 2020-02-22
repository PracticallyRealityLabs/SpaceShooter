using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 8f;
    private float destroyHeight = 8f; 
    void Update()
    {
        Shoot();
        Destroy();
    }
    void Shoot()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);
    }
    private void Destroy()
    {
        if (transform.position.y > destroyHeight)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
    }
}
