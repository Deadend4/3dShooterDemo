using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUseObject
{
    public string actionText { get; }
    public void DoInteraction(Animator animator, bool isAnimated);
}

