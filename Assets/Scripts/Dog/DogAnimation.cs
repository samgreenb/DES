using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAnimation : MonoBehaviour
{
    DogController dog;
    CharacterController characterController;
    [SerializeField]
    Animator animator;
    bool groundedLastUpdate = true;

    // Start is called before the first frame update
    void Start()
    {
        dog = GetComponent<DogController>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(dog.JumpedThisUpdate())
        {
            animator.SetTrigger("jump");
        }
        if(characterController.isGrounded &&  groundedLastUpdate == false) {
            animator.SetTrigger("landed");
        }
        
        Vector3 velocity = characterController.velocity;
        velocity.y = 0;
        if (characterController.isGrounded && characterController.velocity.magnitude > 0.5f)
        {
            animator.SetBool("walk", true);
        } 
        else
        {
            animator.SetBool("walk", false);
        }

        if (characterController.isGrounded && characterController.velocity.magnitude > 5.5f)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }

        groundedLastUpdate = characterController.isGrounded;
    }

    private void OnDisable()
    {
        animator.speed = 0.0f;
    }

    private void OnEnable()
    {
        animator.speed = 1.0f;
    }
}
