using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAudio : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip ascend;
    [SerializeField] private AudioClip descend;

    private AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        aud = this.GetComponent<AudioSource>();
    }

    public void PlayCollectionAudio(bool ascending)
    {
        if(ascending == true)
        {
            aud.PlayOneShot(ascend);
        }
        else
        {
            aud.PlayOneShot(descend);
        }
    }
}
