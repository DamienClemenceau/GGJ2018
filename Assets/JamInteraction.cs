using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamInteraction : MonoBehaviour {
    private bool isIntercated, isOpen;
    private bool canInteract = true;
    private float completion;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && canInteract)
        {
            // TODO launch effect
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            ExitInteraction();
        }
    }

    public void BeginInteraction()
    {
       // if(canInteract)
            isIntercated = true;
    }

    private void ExitInteraction()
    {
        isIntercated = false;
        completion = 0;
    }

    private void Update()
    {
        _animator.SetBool("interact", isIntercated);
        if (isIntercated)
        {

            if(Input.GetButton("Interact"))
            {
                completion += 10;
            }

            if(completion >= 30)
            {
                isOpen = true;
                _animator.SetBool("isOpen", isOpen);
                canInteract = false;
                ExitInteraction();
            }
        }
    }
}
