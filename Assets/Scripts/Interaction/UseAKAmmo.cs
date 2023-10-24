using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAKAmmo : MonoBehaviour, IUseObject
{
    // Start is called before the first frame update
    string IUseObject.actionText
    {
        get => "Патроны для автомата";
    }

    public void DoInteraction(Animator animator, bool doorIsOpened)
    {
        animator.SetBool("isSwitched", doorIsOpened);
    }
}
