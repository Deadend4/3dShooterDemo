using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private IUseObject useObject;

    public void SetInteraction(IUseObject useObjectLocal)
    {
        useObject = useObjectLocal;
    }

    public void UseInteraction(Animator interactionAnimator, bool isInteracted, AudioSource audioSource, Cue audioCue)
    {
        useObject.DoInteraction(interactionAnimator, isInteracted);
        if (audioCue != null)
            audioSource.PlayOneShot(audioCue.PlayAudio());
    }
}
