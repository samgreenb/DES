using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAudio : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.Event footstepSound = new AK.Wwise.Event();
    [SerializeField]
    private AK.Wwise.Event jumpSound = new AK.Wwise.Event();
    [SerializeField]
    private AK.Wwise.Event jumpLandSound = new AK.Wwise.Event();
    [SerializeField]
    private float walkFrequency = 0.0f;

    DogController dog;
    CharacterController characterController;
    bool groundedLastUpdate = true;
    private float walkCount;

    // Start is called before the first frame update
    void Start()
    {
        walkCount = walkFrequency;
        dog = GetComponent<DogController>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(dog.JumpedThisUpdate())
        {
            jumpSound.Post(gameObject);
        }
        if(characterController.isGrounded &&  groundedLastUpdate == false) 
        {
            jumpLandSound.Post(gameObject);
        }
        
        Vector3 velocity = characterController.velocity;
        velocity.y = 0;
        if (characterController.isGrounded && characterController.velocity.magnitude > 0.5f)
        {
            if(walkCount <= 0.0f) {
                footstepSound.Post(gameObject);
                walkCount = walkFrequency;
            }
            else
            {
                walkCount -= Time.deltaTime;
            }
        }
        else
        {
            walkCount = walkFrequency;
        }

        groundedLastUpdate = characterController.isGrounded;
    }
}
