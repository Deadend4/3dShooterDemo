using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationVoicesMC : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<AudioClip> runningCue;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void SoundRun()
    {
        audioSource.PlayOneShot(runningCue[Random.Range(0, runningCue.Count)]);
    }

}
