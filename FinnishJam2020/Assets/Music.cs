using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip Sound;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<AudioSource>().clip = Sound;
        gameObject.GetComponent<AudioSource>().Play();   
    }

    // Update is called once per frame
}
