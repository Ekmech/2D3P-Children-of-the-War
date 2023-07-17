using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    // Týrmanma mekaniði için týrmanýlacak objeye 2 tane collider koymak gerekmekte birinin istrigger seçeneðini true yapýp x,y,z deðerlerinin 1.2 olmasý gerekiyor.(tag = Climbable)
    [Header("Control Settings")]
    [SerializeField] InputAction newInputAction;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float interactSpeed;
    [SerializeField] float jumpPow;
    [SerializeField] float grav;
    [SerializeField] float climbSpeed;
    [SerializeField] float hungerSpeed;
    [SerializeField] float thirstSpeed;
    [SerializeField] KeyCode climbKey = KeyCode.F;
    [Header("Mouse Settings")]
    [SerializeField] float mouseSens = 1f;
    [SerializeField] float maxViewAngle = 60f;
    [SerializeField] bool invertX;
    [SerializeField] bool invertY;

    private CharacterController characterController;
    private InteractionController interactionController;
    private HungerAndThirst hungerAndThirst;
    private float currentSpeed;
    private float horizontalInput;
    private float verticalInput;
    private Transform mainCamera;


    private Vector3 heightMovement;
    private bool jump = false;
    private bool isClimbing = false;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        hungerAndThirst = GetComponent<HungerAndThirst>();
        interactionController = GetComponent<InteractionController>();
        characterController = GetComponent<CharacterController>();
        if (!Camera.main.GetComponent<CameraController>())
        {
            Camera.main.gameObject.AddComponent<CameraController>();
        }
        mainCamera = GameObject.FindWithTag("CameraPoint").transform;
    }
    private void OnEnable()
    {
        newInputAction.Enable();
    }
    private void OnDisable()
    {
        newInputAction.Disable();
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Press 'F' to climb");
        if (other.CompareTag("Climbable"))
        {
            if (Input.GetKey(climbKey))
            {
                StartClimbing();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Climbable"))
        {
            StopClimbing();
        }
    }
    void Update()
    {
        KeyboardInput();
        AnimationChanger();
    }
    private void FixedUpdate()
    {
        Move();
        Climbing();
    }
    private void LateUpdate()
    {
        Rotate();
    }
    private void AnimationChanger()
    {
        if (newInputAction.ReadValue<Vector2>().magnitude > 0f && characterController.isGrounded)
        {
            if (currentSpeed == walkSpeed)
            {
                anim.SetBool("Walk", true);
                anim.SetBool("Run", false);
                anim.SetBool("Thirst", false);
                anim.SetBool("Climb", false);
                anim.SetBool("Interaction", false);
            }
            else if (currentSpeed == runSpeed)
            {
                anim.SetBool("Walk", false);
                anim.SetBool("Run", true);
                anim.SetBool("Thirst", false);
                anim.SetBool("Interaction", false);
                anim.SetBool("Climb", false);
            }
            else if (currentSpeed == interactSpeed)
            {
                anim.SetBool("Walk", false);
                anim.SetBool("Run", false);
                anim.SetBool("Thirst", false);
                anim.SetBool("Interaction", true);
                anim.SetBool("Climb", false);
            }
            else if (isClimbing)
            {
                //anim.SetBool("Climb", true);
                anim.SetBool("Walk", false);
                anim.SetBool("Run", false);
                anim.SetBool("Thirst", false);
                anim.SetBool("Interaction", false);
            }
            else if (currentSpeed == thirstSpeed)
            {
                anim.SetBool("Thirst", true);
                anim.SetBool("Walk", false);
                anim.SetBool("Run", false);
                anim.SetBool("Interaction", false);
                anim.SetBool("Climb", false);
            }
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
            anim.SetBool("Interaction", false);
           // anim.SetBool("Climb", false);
        }
    }
    private void Move()
    {
        if (!isClimbing)
        {
            if (jump)
            {
                heightMovement.y = jumpPow;
                jump = false;
            }
            heightMovement.y -= grav * Time.deltaTime;
            Vector3 localVerticalVector = transform.forward * verticalInput;
            Vector3 localHorizontalVector = transform.right * horizontalInput;

            Vector3 movementVector = localVerticalVector + localHorizontalVector;
            movementVector.Normalize();
            movementVector *= currentSpeed * Time.deltaTime;
            characterController.Move(movementVector + heightMovement);
            if (characterController.isGrounded)
            {
                heightMovement.y = 0f;
            }
        }
    }
    private void Rotate()
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + MouseInput().x, transform.eulerAngles.z);
        if (mainCamera != null)
        {
            if (mainCamera.eulerAngles.x > maxViewAngle && mainCamera.eulerAngles.x < 180f)
            {
                mainCamera.rotation = Quaternion.Euler(maxViewAngle, mainCamera.eulerAngles.y, mainCamera.eulerAngles.z);
            }
            else if (mainCamera.eulerAngles.x > 180f && mainCamera.eulerAngles.x < 360f - maxViewAngle)
            {
                mainCamera.rotation = Quaternion.Euler(360f - maxViewAngle, mainCamera.eulerAngles.y, mainCamera.eulerAngles.z);
            }
            else
            {
                mainCamera.rotation = Quaternion.Euler(mainCamera.rotation.eulerAngles + new Vector3(-MouseInput().y, 0f, 0f));
            }
        }
    }
    private void StopClimbing()
    {
        isClimbing = false;
    }
    private void StartClimbing()
    {
        isClimbing = true;
        anim.SetBool("Climb", true);
    }
    private void Climbing()
    {
        if (isClimbing)
        {
            float veticalInput = Input.GetAxis("Vertical");
            Vector3 moveDirection = transform.up * verticalInput;
            moveDirection *= climbSpeed * Time.deltaTime;
            characterController.Move(moveDirection);
        }
    }
    private void KeyboardInput()
    {
        horizontalInput = newInputAction.ReadValue<Vector2>().x;
        verticalInput = newInputAction.ReadValue<Vector2>().y;
        if (Keyboard.current.spaceKey.wasPressedThisFrame && characterController.isGrounded)
        {
            jump = true;
        }
        if ((hungerAndThirst.isHunger == false && hungerAndThirst.isThirst == false) && Keyboard.current.leftShiftKey.isPressed)
        {
            currentSpeed = runSpeed;
        }
        else if (hungerAndThirst.isThirst)
        {
            currentSpeed = thirstSpeed;
        }
        else if (hungerAndThirst.isHunger)
        {
            currentSpeed = hungerSpeed;
        }
        else if (interactionController.isInteracting)
        {
            currentSpeed = interactSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }
    }
    private Vector2 MouseInput()
    {
        return new Vector2(invertX ? -Mouse.current.delta.x.ReadValue() : Mouse.current.delta.x.ReadValue(),
            invertY ? -Mouse.current.delta.y.ReadValue() : Mouse.current.delta.y.ReadValue()) * mouseSens;
    }
}