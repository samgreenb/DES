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
    void Update()
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

        groundedLastUpdate = characterController.isGrounded;
    }
}
