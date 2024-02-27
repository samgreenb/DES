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
    Hold,
    Headbutt,
    Sniff
}

public enum SNIFFSTATE
{
    NotSniff,
    None,
    Far,
    Medium,
    Close,
    Found
}

public class DogController : MonoBehaviour
{
    [SerializeField]
    STATE state = STATE.Default;
    SNIFFSTATE sniffState = SNIFFSTATE.NotSniff;

    [SerializeField]
    Animator animController;

    [SerializeField]
    GameObject model;

    [SerializeField]
    GameObject cameraRotater;

    CharacterController controller;

    [SerializeField]
    List<GameObject> sniffAreas;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        sniffAreas = new List<GameObject>();
    }

    public void AddSniffArea(GameObject hiddenItem)
    {
        sniffAreas.Add(hiddenItem);
    }

    public void RemoveFromSniffArea(GameObject hiddenItem)
    {
        sniffAreas.Remove(hiddenItem);
    }

    public Vector2 turn = Vector2.zero;
    [SerializeField]  public float speed = 6.0F;
    [SerializeField]  public float runMultiplier = 1.5F;
    [SerializeField]  public float pushSpeed = 6.0F; //Weight of object?
    [SerializeField]  public float crouchSpeedMultiplier = 0.6F;
    [SerializeField]  public float sniffSpeedMultiplier = 0.6F;
    [SerializeField]  public float jumpSpeed = 8.0F;
    [SerializeField] public float gravity = 9.8F;
    private Vector3 moveDirection = Vector3.zero;

    void Update()
    {
        switch (state)
        {
            case STATE.Default:
                Move();
                Hold();
                Crouch();
                PushPull();
                Headbutt();
                Sniff();
                break;
            case STATE.Crouch:
                Crouch();
                Move();
                Hold();
                break;
            case STATE.PushPull:
                //Move();
                PushPull();
                break;
            //case STATE.Hold:
            //    Move();
            //    Hold();
            //    Crouch();
                break;
            case STATE.Headbutt:
                break;
            case STATE.Sniff:
                Move();
                Sniff();
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case STATE.PushPull:
                //only forward/backwards
                Vector3 direction = dragging.transform.position - transform.position;
                moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
                moveDirection = cameraRotater.transform.TransformDirection(moveDirection).normalized;
                direction = Vector3.Project(moveDirection, direction);

                direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
                direction = cameraRotater.transform.TransformDirection(direction).normalized;

                dragging.GetComponent<Rigidbody>().MovePosition(dragging.transform.position +
                    direction * pushSpeed);
                break;
        }
    }

    [SerializeField]
    float closeMediumCutoff = 10.0f;
    [SerializeField]
    float MediumFarCutoff = 20.0f;
    private void Sniff()
    {

        if (!controller.isGrounded) return;

        if (Input.GetButtonDown("Smell"))
        {
            state = STATE.Sniff;
            sniffState = SNIFFSTATE.None;
        }

        if(state == STATE.Sniff)
        {
            float smallestDistance = 9999.0f;
            foreach(GameObject sniffItem in sniffAreas)
            {
                float objectDistance = (gameObject.transform.position - sniffItem.transform.position).magnitude;
                if (objectDistance >= smallestDistance) continue;
                if (objectDistance < closeMediumCutoff) sniffState = SNIFFSTATE.Close;
                if (objectDistance >= closeMediumCutoff && objectDistance < MediumFarCutoff) sniffState = SNIFFSTATE.Medium;
                if (objectDistance >= MediumFarCutoff) sniffState = SNIFFSTATE.Far;
                smallestDistance = objectDistance;
            }
        }

        if (Input.GetButtonUp("Smell"))
        {
            state = STATE.Default;
            sniffState = SNIFFSTATE.NotSniff;
        }
    }

    [SerializeField] GameObject interact;
    [SerializeField] GameObject dragging = null;
    Vector3 distance = Vector3.zero;
    private void PushPull()
    {
        if (!controller.isGrounded) return;

        if (dragging != null)
        {
            ////only forward/backwards
            //Vector3 direction = dragging.transform.position - transform.position;
            //moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            //moveDirection = cameraRotater.transform.TransformDirection(moveDirection).normalized;
            //direction = Vector3.Project(moveDirection, direction);

            //direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            //direction = cameraRotater.transform.TransformDirection(direction).normalized;

            //dragging.GetComponent<Rigidbody>().MovePosition(dragging.transform.position +
            //    direction * Time.deltaTime * pushSpeed);


            Vector3 currentDistance = dragging.transform.position - transform.position;
            currentDistance.y = 0;
            if (currentDistance.magnitude > distance.magnitude + 0.4f)
            {
                currentDistance.Normalize();
                currentDistance *= 3.0f;
                currentDistance.y = -gravity;
                controller.Move(currentDistance * Time.deltaTime);

            } else if (currentDistance.magnitude < distance.magnitude + 0.2f)
            {
                currentDistance.Normalize();
                currentDistance *= 3.0f;
                currentDistance.y = gravity;
                controller.Move(-currentDistance * Time.deltaTime);
            }
            currentDistance.y = 0;
            model.transform.LookAt(model.transform.position + currentDistance);

        }

        if (Input.GetButtonDown("Drag"))
        {
            Collider[] colliders = new Collider[10];
            Physics.OverlapSphereNonAlloc(interact.transform.position, 0.1f, colliders);
            foreach (Collider collider in colliders)
            {
                if (collider != null && collider.CompareTag("PushPull"))
                {
                    //controller.enabled = false;
                    dragging = collider.attachedRigidbody.gameObject;
                    //transform.parent = dragging.transform;
                    state = STATE.PushPull;
                    distance = (dragging.transform.position - transform.position);
                    return;
                }
            }
        }

        if (Input.GetButtonUp("Drag"))
        {
            if (dragging != null)
            {
                Debug.Log("lmao3");
                //dragging.transform.parent = null;
                //transform.parent = null;
                dragging = null;
                state = STATE.Default;
                //controller.enabled = true;
                //transform.rotation = Quaternion.identity;
                return;
            }

            
        }
        
    }

    [SerializeField]
    float HeadbuttTime = 1.0f;
    private void Headbutt()
    {
        if (!controller.isGrounded) return;

        if (Input.GetButtonDown("Drag"))
        {
            Collider[] colliders = new Collider[10];
            Physics.OverlapCapsuleNonAlloc(interact.transform.position + Vector3.up * 0.5f, interact.transform.position - Vector3.up * 0.5f, 0.25f, colliders);

            foreach (Collider collider in colliders)
            {
                if(collider == null) continue;
                HeadbuttTarget target = collider.gameObject.GetComponent<HeadbuttTarget>();
                if (target != null)
                {
                    state = STATE.Headbutt;
                    Invoke(nameof(FinishHeadbutt), HeadbuttTime);
                    target.OnHeadbutted(HeadbuttTime);
                }
            }
        }
    }

    private void FinishHeadbutt()
    {
        state = STATE.Default;
    }

    [SerializeField] GameObject holdPosition;
    [SerializeField] GameObject holding = null;
    private void Hold()
    {
        if (Input.GetButtonDown("Drag"))
        {
            Collider[] colliders = new Collider[10];
            Physics.OverlapCapsuleNonAlloc(interact.transform.position + Vector3.up * 0.5f, interact.transform.position - Vector3.up * 0.5f, 0.25f, colliders);

            foreach (Collider collider in colliders)
            {
                if (collider != null && collider.CompareTag("Hold"))
                {
                    holding = collider.attachedRigidbody.gameObject;
                    holding.transform.parent = holdPosition.transform;
                    holding.transform.position = holdPosition.transform.position;
                    //state = STATE.Hold;
                    holding.GetComponent<Rigidbody>().isKinematic = true;
                    holding.GetComponentInChildren<BoxCollider>().enabled = false;
                    return;
                }
            }
        }


        if (Input.GetButtonUp("Drag"))
        {
            if (holding != null)
            {
                Debug.Log("lmao4");
                holding.GetComponent<Rigidbody>().isKinematic = false;
                holding.GetComponentInChildren<BoxCollider>().enabled = true;
                holding.transform.parent = null;
                holding = null;
                //state = STATE.Default;

                return;
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        return;
        //This is to try to improve dragging later on

        //Debug.Log(hit.gameObject.name);
        //if (hit.rigidbody != null && hit.rigidbody.gameObject.Equals(dragging))
        //{
        //    Debug.Log("SUKIS");
        //    state = STATE.Default;
        //    dragging.transform.parent = null;
        //    dragging = null;
        //}

        //return;
    }

    Vector3 push = Vector3.zero;
    private void Move()
    {
        
        //bool anim = false;
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        CharacterController controller = GetComponent<CharacterController>();
        Debug.Log("Is grounded? : " + controller.isGrounded);

            //moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //if (moveDirection.magnitude > 0) { anim = true; }
        input = cameraRotater.transform.TransformDirection(input).normalized;
        push = moveDirection;
        switch (state)
        {
            case STATE.Default:
            case STATE.Hold:
                input *= speed * (Input.GetButton("Run") ? runMultiplier : 1.0f);

                break;
            case STATE.Crouch:
                input *= speed * crouchSpeedMultiplier;

                break;
            case STATE.Sniff:
                input *= speed * sniffSpeedMultiplier;

                break;
        }
        moveDirection = new Vector3(input.x, moveDirection.y, input.z);
        if(controller.isGrounded)
        {
            moveDirection.y = -9.8f;
        }
        //moveDirection *= speed * (Input.GetButton("Run") ? runMultiplier : 1.0f) * (state.Equals(STATE.Crouch) ? crouchSpeedMultiplier : 1.0f);
        if (controller.isGrounded && Input.GetButtonDown("Jump") && state != STATE.Crouch)
        {
            moveDirection.y = jumpSpeed;
        }
        
        if(!controller.isGrounded) {
            moveDirection.y -= gravity * Time.deltaTime;
        }   
            
        
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

    [SerializeField] GameObject standingModel;
    [SerializeField] GameObject crouchingModel;

    private void Crouch()
    {
        if (!controller.isGrounded) return;

        if (Input.GetButtonDown("Crouch"))
        {
            standingModel.SetActive(false);
            crouchingModel.SetActive(true);
            controller.height = 1;
            controller.center = new Vector3(0, 0.5f, 0);
            state = STATE.Crouch;
        }

        if (state == STATE.Crouch && !Input.GetButton("Crouch"))
        {
            // Bit shift the index of the layer (8) to get a bit mask
            int layerMask = 1 << 3;

            // This would cast rays only against colliders in layer 8.
            // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
            layerMask = ~layerMask;

            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 2.0f, layerMask) && !hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Did Hit : " + hit.collider.gameObject.name);
                return;
            }
            standingModel.SetActive(true);
            crouchingModel.SetActive(false);
            controller.height = 2;
            controller.center = new Vector3(0, 1, 0);
            state = STATE.Default;
        }
    }

    public SNIFFSTATE GetSniffState()
    {
        return sniffState;
    }

    public STATE GetState()
    {
        return state;
    }
}
