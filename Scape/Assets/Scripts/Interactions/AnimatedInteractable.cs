using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedInteractable : Interactable
{

    public override bool Available => base.Available && !busy;

    private bool busy;

    [SerializeField] private Animator animator;

    public override void Interact()
    {
        if(Available)
        {
            busy = true;
            animator.SetTrigger("Activate");
        }
    }

    public void Perform()
    {
        busy = false;
        base.Interact();
    }

}
