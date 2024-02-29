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
    private AK.Wwise.Event dragSound = new AK.Wwise.Event();
    [SerializeField]
    private AK.Wwise.Event sniffSound = new AK.Wwise.Event();
    [SerializeField]
    private float walkFrequency = 0.0f;
    [SerializeField]
    private float sniffFrequency = 0.0f;

    DogController dog;
    CharacterController characterController;
    bool groundedLastUpdate = true;
    private float walkCount;
    private float sniffCount;
    private bool playingDrag = false;

    // Start is called before the first frame update
    void Start()
    {
        walkCount = walkFrequency;
        sniffCount = sniffFrequency;
        dog = GetComponent<DogController>();
        characterController = GetComponent<CharacterController>();
    }

    
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (dog.JumpedThisUpdate())
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

        // Sniff audio
        if (dog.GetState() == STATE.Sniff)
        {
            if (sniffCount <= 0.0f)
            {
                Debug.Log("Play sniff sound");
                sniffSound.Post(gameObject);
                sniffCount = sniffFrequency;
            }
            else
            {
                sniffCount -= Time.deltaTime;
            }
        }
        else
        {
            sniffCount = sniffFrequency;
        }

        //Debug.Log("CCV : " + velocity.magnitude);
        //Debug.Log(input.magnitude);
        if (dog.GetState() == STATE.PushPull && input.magnitude != 0 && !playingDrag)
        {
            Debug.Log("Playing drag");
            playingDrag = true;
            dragSound.Post(gameObject);
        } 
        else if((dog.GetState() != STATE.PushPull || input.magnitude == 0) && playingDrag)
        {
            Debug.Log("Stop playing drag");
            playingDrag = false;
            dragSound.Stop(this.gameObject, 500, AkCurveInterpolation.AkCurveInterpolation_Constant);
        }

        groundedLastUpdate = characterController.isGrounded;
    }
}
