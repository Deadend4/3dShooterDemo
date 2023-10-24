using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAkmShutter : MonoBehaviour
{
    [SerializeField] private AudioClip shutterShound;
    [SerializeField] AudioSource audioSource;
    private void Start() { }
    public void PlayShutter()
    {
        audioSource.PlayOneShot(shutterShound, 0.2f);
    }
}
