using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Interact : MonoBehaviour
{

    private Animator animator;
    private AudioSource audioSource;
    [Header("Settings")]
    public IUseObject currentObject;
    [SerializeField] Cue audioInteract;
    public bool interactIsAnimated = false;
    //[Header("Box Settings")]

    void Start()
    {
        InitialSetup();
        audioSource = GetComponent<AudioSource>();
    }

    public void UseInteraction()
    {
        Interaction interaction = new Interaction();

        interactIsAnimated = !interactIsAnimated;

        interaction.SetInteraction(currentObject);
        interaction.UseInteraction(animator, interactIsAnimated, audioSource, audioInteract);
    }

    private IUseObject GetObject(string tag)
    {
        switch (tag)
        {
            case "door":
                return new UseDoor();
            case "box":
                return new UseBox();
            default:
                return null;
        }
    }
    private void InitialSetup()
    {
        if (TryGetComponent<Animator>(out animator))
        {
            animator.SetBool("isSwitched", interactIsAnimated);
        }
        currentObject = GetObject(transform.tag);
    }

}
