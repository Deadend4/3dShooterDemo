using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAI : MonoBehaviour
{
    [SerializeField] AudioClip dyingSound;
    private AudioSource audioSource;
    private Animator animator;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    public void Die()
    {
        animator.SetTrigger("die");
        if (!dyingSound.Equals(null))
        {
            audioSource.PlayOneShot(dyingSound);
        }
    }
}
