using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSwitcher : MonoBehaviour
{
    public AudioClip switchBGM;
    public AudioSource source;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (source.clip != switchBGM)
            {
                source.clip = switchBGM;
                source.Play();
            }
        }
    }
}
