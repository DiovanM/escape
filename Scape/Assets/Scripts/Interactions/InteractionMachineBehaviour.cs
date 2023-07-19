using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionMachineBehaviour : StateMachineBehaviour
{

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var interactable = animator.GetComponent<AnimatedInteractable>();
        if (interactable != null)
            interactable.Perform();
        else
            Debug.LogWarning("Tried to perform an animated interaction but found no interactable");
    }

}
