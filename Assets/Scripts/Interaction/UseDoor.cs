using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseDoor : MonoBehaviour, IUseObject
{
    string IUseObject.actionText
    {
        get => "Использовать дверь";
    }

    public void DoInteraction(Animator animator, bool doorIsOpened)
    {
        animator.SetBool("isSwitched", doorIsOpened);
    }

}
