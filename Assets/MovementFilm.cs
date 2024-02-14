using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFilm : MonoBehaviour
{

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
        bool anim = false;
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
            if (moveDirection.magnitude > 0) { anim = true; }
            //moveDirection = cameraRotater.transform.TransformDirection(moveDirection);
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

}
