using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Movement1 : MonoBehaviour
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
    [SerializeField]  public float speed = 6.0F;
    [SerializeField]  public float runMultiplier = 1.5F;
    [SerializeField]  public float pushSpeed = 6.0F; //Weight of object?
    [SerializeField]  public float crouchSpeedMultiplier = 0.6F;
    [SerializeField]  public float jumpSpeed = 8.0F;
    //[SerializeField] public float gravity = 20.0F;
    [SerializeField] public float gravity = 9.8F;
    private Vector3 moveDirection = Vector3.zero;

    void Update()
    {
        switch (state)
        {
            case STATE.Default:
                Crouch();
                Move();
                PushPull();
                Hold();
                break;
            case STATE.Crouch:
                Crouch();
                Move();
                break;
            case STATE.PushPull:
                //Move();
                PushPull();
                break;
            case STATE.Hold:
                Move();
                Hold();
                Crouch();
                break;
        }
        //crouch

        
    }

    [SerializeField] GameObject interact;
    [SerializeField] GameObject dragging = null;
    Vector3 distance = Vector3.zero;
    private void PushPull()
    {
        if (dragging != null)
        {
            //only forward/backwards
            Vector3 direction = dragging.transform.position - transform.position;
            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            moveDirection = cameraRotater.transform.TransformDirection(moveDirection).normalized;

            direction = Vector3.Project(moveDirection, direction);
            dragging.GetComponent<Rigidbody>().MovePosition(dragging.transform.position +
                direction * Time.deltaTime * pushSpeed);
            //if ((transform.position - dragging.transform.position).magnitude - distance.magnitude >= 1.01f )
            //{
            //    //dragging.GetComponent<Rigidbody>().velocity = new Vector3(controller.velocity.x, -9.81f, controller.velocity.z);
            //    Debug.Log("Disatnce disconnect");
            //    dragging.transform.parent = null;
            //    //dragging.GetComponent<Joint>().connectedBody = null;
            //    dragging = null;
            //    state = STATE.Default;
            //    return;
            //}
        }

        if (!Input.GetButtonDown("Drag")) return;

        if (dragging != null) {
            Debug.Log("lmao3");
            dragging.transform.parent = null;
            transform.parent = null;
            //dragging.GetComponent<Joint>().connectedBody = null;
            dragging = null;
            state = STATE.Default;
            controller.enabled = true;
            transform.rotation = Quaternion.identity;
            return;
        }

        Collider[] colliders = new Collider[10] ;
        Physics.OverlapSphereNonAlloc(interact.transform.position, 0.1f, colliders);
        foreach (Collider collider in colliders)
        {
            if (collider != null && collider.CompareTag("PushPull"))
            {
                controller.enabled = false;
                dragging = collider.attachedRigidbody.gameObject;
                //dragging.transform.parent = transform;
                //dragging.GetComponent<Joint>().connectedBody = GetComponentInChildren<Rigidbody>();
                transform.parent = dragging.transform;
                state = STATE.PushPull;
                distance = (dragging.transform.position - transform.position);
                return;
            }
        }
        
    }

    [SerializeField] GameObject holdPosition;
    [SerializeField] GameObject holding = null;
    private void Hold()
    {
        if (!Input.GetButtonDown("Drag")) return;

        if (holding != null)
        {
            Debug.Log("lmao4");
            holding.GetComponent<Rigidbody>().isKinematic = false;
            holding.transform.parent = null;
            holding = null;
            state = STATE.Default;
            
            return;
        }

        Collider[] colliders = new Collider[10];
        Physics.OverlapCapsuleNonAlloc(interact.transform.position + Vector3.up * 0.5f, interact.transform.position - Vector3.up * 0.5f, 0.25f, colliders);

        foreach (Collider collider in colliders)
        {
            if (collider != null && collider.CompareTag("Hold"))
            {
                holding = collider.attachedRigidbody.gameObject;
                holding.transform.parent = holdPosition.transform;
                holding.transform.position = holdPosition.transform.position;
                state = STATE.Hold;
                holding.GetComponent<Rigidbody>().isKinematic = true;
                return;
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.Log(hit.gameObject.name);
        if (hit.rigidbody != null && hit.rigidbody.gameObject.Equals(dragging))
        {
            Debug.Log("SUKIS");
            state = STATE.Default;
            dragging.transform.parent = null;
            dragging = null;
        }

        return;
        var rigidBody = hit.rigidbody;

        if (rigidBody/* != null && rigidBody.velocity.magnitude <= 0.1f*/)
        {
            var forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection = hit.moveDirection;
            forceDirection.y = 0;
            //forceDirection = rigidBody.transform.TransformDirection(forceDirection);
            forceDirection.Normalize();

            //rigidBody.AddForceAtPosition(forceDirection * 0.25f, transform.position, ForceMode.Impulse);
            rigidBody.AddForceAtPosition(forceDirection * 0.25f, transform.position, ForceMode.Impulse);
        }
    }

    Vector3 push = Vector3.zero;
    private void Move()
    {
        Debug.Log(Input.GetButton("Run"));
        bool anim = false;
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            if (moveDirection.magnitude > 0) { anim = true; }
            moveDirection = cameraRotater.transform.TransformDirection(moveDirection).normalized;
            push = moveDirection;
            moveDirection *= speed * (Input.GetButton("Run") ? runMultiplier : 1.0f) * (state.Equals(STATE.Crouch) ? crouchSpeedMultiplier : 1.0f);
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
    [SerializeField] GameObject standingModel;
    [SerializeField] GameObject crouchingModel;

    private void Crouch()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            standingModel.SetActive(false);
            crouchingModel.SetActive(true);
            controller.height = 1;
            controller.center = new Vector3(0, 0.5f, 0);
            state = STATE.Crouch;
            crouching = true;
        }

        if (Input.GetButtonUp("Crouch"))
        {
            standingModel.SetActive(true);
            crouchingModel.SetActive(false);
            controller.height = 2;
            controller.center = new Vector3(0, 1, 0);
            state = holding == null ? STATE.Default : STATE.Hold;
            crouching = false;
        }

        //if (!Input.GetButtonDown("Crouch")) return;

        //crouching = !crouching;
        //if(crouching)
        //{
        //    standingModel.SetActive(false);
        //    crouchingModel.SetActive(true);
        //    controller.height = 1;
        //    controller.center = new Vector3(0, 0.5f, 0);
        //    state = STATE.Crouch;
        //}
        //else
        //{
        //    standingModel.SetActive(true);
        //    crouchingModel.SetActive(false);
        //    controller.height = 2;
        //    controller.center = new Vector3(0, 1, 0);
        //    state = STATE.Default;
        //}
    }
}
