using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum STATE
{
    Default,
    Crouch,
    PushPull,
    Hold
}

public class Movement : MonoBehaviour
{
    [SerializeField]
    STATE state = STATE.Default;

    [SerializeField]
    Animator animController;

    [SerializeField]
    GameObject model;

    [SerializeField]
    GameObject cameraRotater;

    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public Vector2 turn = Vector2.zero;
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    void Update()
    {
        switch (state)
        {
            case STATE.Default:
                Crouch();
                Move();
                PushPull();
                break;
            case STATE.Crouch:
                Crouch();
                Move();
                break;
            case STATE.PushPull:
                Move();
                PushPull();
                break;
        }
        //crouch

        
    }

    [SerializeField] Collider interactHitbox;
    [SerializeField] GameObject dragging = null;

    private void PushPull()
    {
        //if(dragging != null)
        //{
        //    Vector3 force = controller.velocity;
        //    force.y = 0;
        //    force.Normalize();
        //    dragging.GetComponent<Rigidbody>().AddForce(force * 1.0f,  ForceMode.VelocityChange);
        //}

        if (!Input.GetKeyDown(KeyCode.Q)) return;

        if (dragging != null) {
            Debug.Log("lmao3");
            dragging.transform.parent = null;
            //dragging.GetComponent<Joint>().connectedBody = null;
            dragging = null;
            state = STATE.Default;
            return;
        }

        Collider[] colliders = new Collider[10] ;
        Physics.OverlapSphereNonAlloc(interactHitbox.transform.position, 0.1f, colliders);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("PushPull"))
            {
                dragging = collider.gameObject;
                dragging.transform.parent = transform;
                //dragging.GetComponent<Joint>().connectedBody = GetComponentInChildren<Rigidbody>();
                state = STATE.PushPull;
                return;
            }
        }
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        return;
        var rigidBody = hit.collider.attachedRigidbody;

        if (rigidBody != null)
        {
            var forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            rigidBody.AddForceAtPosition(forceDirection * 0.5f, transform.position, ForceMode.Impulse);


        }
    }

    private void Move()
    {
        bool anim = false;
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            if (moveDirection.magnitude > 0) { anim = true; }
            moveDirection = cameraRotater.transform.TransformDirection(moveDirection).normalized;
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        turn.x += Input.GetAxis("Mouse X");
        turn.x += Input.GetAxis("Mouse X 2");
        turn.x += Input.GetAxis("Mouse X 3");
        //turn.y += Input.GetAxis("Mouse Y");
        //cameraRotater.transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
        cameraRotater.transform.localRotation = Quaternion.Euler(0, Camera.main.transform.localRotation.eulerAngles.y, 0);
        if (input.magnitude != 0)
        {
            Vector3 newdir = model.transform.position + cameraRotater.transform.TransformDirection(input/* + Vector3.up * 1*/).normalized;
            newdir = Vector3.Lerp(model.transform.position + model.transform.forward, newdir, 10.0f * Time.deltaTime);
            model.transform.LookAt(newdir);
            //
            //model.transform.LookAt(model.transform.position + cameraRotater.transform.TransformDirection(input/* + Vector3.up * 1*/));
        }

        //animController.SetBool("moving", anim);
    }

    bool crouching = false;
    [SerializeField] CapsuleCollider playerHitbox;
    [SerializeField] GameObject standingModel;
    [SerializeField] GameObject crouchingModel;

    private void Crouch()
    {
        if (!Input.GetKeyDown(KeyCode.E)) return;

        crouching = !crouching;
        if(crouching)
        {
            standingModel.SetActive(false);
            crouchingModel.SetActive(true);
            controller.height = 1;
            controller.center = new Vector3(0, 0.5f, 0);
            state = STATE.Crouch;
        }
        else
        {
            standingModel.SetActive(true);
            crouchingModel.SetActive(false);
            controller.height = 2;
            controller.center = new Vector3(0, 1, 0);
            state = STATE.Default;
        }
    }
}
