using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    

    [SerializeField]
    private float platerMovementSpeed = 3.0f;

    [SerializeField]
    private float mouseSensitivity = 5.0f;
    [SerializeField]
    private float cameraSmoothValue = 2.0f;

    [SerializeField]
    private float jumpForwardForce = 100.0f;
    [SerializeField]
    private float jumpUpForce = 2.0f;
    [SerializeField]
    private Transform cameraCenter;
    [SerializeField]
    private Animator playerAnimator;

    private float playerHeight;
    private Rigidbody playerRigidbody;

    public Transform empty;
    public Transform lookFor;

    [SerializeField]
    public float sightRotationSpeed;
    [SerializeField]
    private RectTransform weaponSight;
    [SerializeField]

    private bool isGrounded;
    private bool isMoving;

    public Vector2 mouseLook;
    public Vector2 smoothV;
    private Quaternion lookRotation;
    private Vector3 lookPoint;
    private Vector2 middleScreenPosition;
    private Vector3 lookDirection;
    private Camera playerCamera;
    private Vector2 _move;
    private Vector2 _look;
    private Vector3 movement;

    private void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        playerHeight = GetComponent<CapsuleCollider>().radius;
        playerRigidbody = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
        middleScreenPosition = new Vector2(Screen.width / 2, Screen.height / 2);
    }
    public void Update()
    {
        UpdateMovement();
        StartCoroutine(Jump());
    }
    private void FixedUpdate()
    {
        moveCharacter(movement);
        KeyboardMovement();
        MouseMovement();
    }
    private void UpdateMovement()
    {
        movement = new Vector3(_move.x, 0, _move.y);
    }
    private void moveCharacter(Vector3 direction)
    {
        direction = playerRigidbody.rotation * direction;
        playerRigidbody.MovePosition(playerRigidbody.position + direction * platerMovementSpeed * Time.fixedDeltaTime);
        isMoving = true;
        

    }

    IEnumerator Jump()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, playerHeight, LayerMask.GetMask("Ground")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        if (isGrounded)
        {

            if (Keyboard.current.spaceKey.wasReleasedThisFrame)
            {
                playerAnimator.SetBool("isJumping", true);
                yield return new WaitForSeconds(0.5f);
                GetComponent<Rigidbody>().AddForce(((transform.up * jumpUpForce) + transform.forward) * jumpForwardForce);
            }
            
        }
        else
        {
             playerAnimator.SetBool("isJumping", false);
        }
    }
    public void KeyboardMovement()
    {

        if (Keyboard.current.shiftKey.isPressed && isMoving == true ) //run
        {
            platerMovementSpeed = 5;
        }
        else
        {
            platerMovementSpeed = 3f;
        }
        Vector3 translation = new Vector3(_move.x, 0, -_move.y);
        if (translation.z < 0)
        {
            playerAnimator.SetBool("isWalking", true);
        }
        else
        {
            
            playerAnimator.SetBool("isWalking", false);
        }
        if (translation.z < 0)
        {
            playerAnimator.SetBool("isWalking", true);
        }
        else
        {

            playerAnimator.SetBool("isWalking", false);
        }
    }

    public void MouseMovement()
    {
        var axisMovementVector = new Vector2(_look.x, _look.y);
        axisMovementVector *= mouseSensitivity * cameraSmoothValue;
        smoothV = Vector2.Lerp(smoothV, axisMovementVector, 1f / cameraSmoothValue);
        mouseLook += smoothV;
        transform.localRotation = Quaternion.AngleAxis(mouseLook.x, transform.up);
        cameraCenter.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);

        if (cameraCenter.rotation.eulerAngles.x > 45f && cameraCenter.rotation.eulerAngles.x < 180f)
        {
            cameraCenter.rotation = Quaternion.Euler(45f, cameraCenter.rotation.eulerAngles.y, 0);
            mouseLook.y = -45;
        }

        if (cameraCenter.rotation.eulerAngles.x > 180f && cameraCenter.rotation.eulerAngles.x < 315f)
        {
            cameraCenter.rotation = Quaternion.Euler(315f, cameraCenter.rotation.eulerAngles.y, 0);
            mouseLook.y = 45;
        }
    }
    private void UpdateSight()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(middleScreenPosition);
        if (Physics.Raycast(ray, out hit))
        {
            lookPoint = hit.point;

            lookRotation = Quaternion.LookRotation(lookDirection);

            lookFor.position = lookPoint;

        }
        else
        {
            lookFor.position = empty.position;
        }

    }
    private void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
    }
    private void OnLook(InputValue value)
    {
        _look = value.Get<Vector2>();
    }
    ////private void OnCollisionEnter(Collision collision)
    ////{
    ////    if (collision.gameObject.CompareTag("box1"))
    ////    {

    ////        if (GetComponent<GameController>().player.transform.position.magnitude < 0.5f)
    ////        {
    ////            subject.Notify();
    ////        }
    ////    }
    ////}
    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("box1"))
    //    {

    //            subject.Notify();
            
    //    }
    //}
}
