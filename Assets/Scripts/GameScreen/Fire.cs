using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    void Start()
    {
        this.gameObject.GetComponent<AudioSource>().Play();
    }
}
