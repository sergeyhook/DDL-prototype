using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPhrase : MonoBehaviour
{
    public AudioSource audioSource;
    public bool Played;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !audioSource.isPlaying&&Played==false)
        {
            Played = true;
            audioSource.Play();
        }
    }
}
