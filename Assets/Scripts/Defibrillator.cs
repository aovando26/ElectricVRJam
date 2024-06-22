using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defibrillator : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isUsing = false;
    public AudioClip defibrillatorClip;

    void Start()
    {
        Debug.Log("This is Defibrillator script");
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.Log("Audio component is missing from gameobject");
        }
    }

    void Update()
    {
        if (isUsing)
        {
            audioSource.PlayOneShot(defibrillatorClip);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DefibrillatorRight"))
        {
            Debug.Log("The other object you are colliding with is: " + other.gameObject.name);
            isUsing = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DefibrillatorRight"))
        {
            Debug.Log("The other object you are colliding with is: " + other.gameObject.name);
            isUsing = false;
            audioSource.Stop();
        }
    }
}
