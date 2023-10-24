using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseBox : MonoBehaviour, IUseObject
{
    string IUseObject.actionText
    {
        get => "Использовать ящик";
    }
    public void DoInteraction(Animator animator, bool isUsed)
    {
        Debug.Log("Puck-puck!");
    }
}

