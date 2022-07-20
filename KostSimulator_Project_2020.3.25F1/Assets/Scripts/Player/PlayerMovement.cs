using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[Serializable]
public struct UnitPoint
{
    public float CurrentPoint;
    public float MaximumPoint;

    public float CurrentStock
    {
        get
        {
            return CurrentPoint;
        }
        set
        {
            if (value > MaximumPoint)
            {
                value = MaximumPoint;
            }
            else if (value < 0)
            {
                value = 0;
            }
            CurrentPoint = value;
        }
    }

    public float MaximumStock
    {
        get
        {
            return MaximumPoint;
        }
        set
        {
            CurrentPoint = MaximumPoint = value;
        }
    }
}


public class PlayerMovement : MonoBehaviour, IDataPersistence
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private float jumpHorizontalSpeed;
    [SerializeField] private float jumpButtonGracePeriod;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private DialogueUI dialogueUI;

    [Header("Attributes SO")]
    [SerializeField] private AttributesScriptableObject playerAttributesSO;

    public IInteractable Interactable { get; set; }
    public DialogueUI DialogueUI => dialogueUI;
    public AudioClip Click;

    private Player playerInput;
    private CharacterController characterController;
    private AudioSource playerAudio;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private bool isJumping;
    private bool isGrounded;
    private bool exitPressed = false;

    private void Awake()
    {
        playerInput = new Player();
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput?.Disable();
    }

    private void Start()
    {
        originalStepOffset = characterController.stepOffset;
        cameraTransform = Camera.main.transform;
        playerAudio = GetComponent<AudioSource>();
        Physics.SyncTransforms();
    }

    public void LoadData (GameData data)
    {
        this.transform.position = data.playerPosition;
        //load the values from our game data into the scriptable object
        //playerAttributesSO.hunger = data.playerAttributesData.hunger;
        //playerAttributesSO.stamina = data.playerAttributesData.stamina;
        //playerAttributesSO.intellectScore = data.playerAttributesData.intellectScore;
        //playerAttributesSO.frustration = data.playerAttributesData.frustration;
    }

    public void SaveData(GameData data)
    {
        data.playerPosition = this.transform.position;
        //store the values from our scriptable object into the game data
        //data.playerAttributesData.hunger = playerAttributesSO.hunger;
        //data.playerAttributesData.stamina = playerAttributesSO.stamina;
        //data.playerAttributesData.intellectScore = playerAttributesSO.intellectScore;
        //data.playerAttributesData.frustration = playerAttributesSO.frustration;
    }

    void Update()
    {
        //below code just used to test exiting the scene
        if (exitPressed)
        {
            //save the game anytime before loading a new scene
            DataPersistenceManager.instance.SaveGame();

            //Load the main menu scene
            SceneManager.LoadSceneAsync("MainMenu");
        }

        if (playerInput.PlayerMain.Select.triggered)
        {
            //Interactable?.Interact(this);
            //playerAudio.PlayOneShot(Click);
        }

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    OnApplicationFocus(false);
        //}

        #region Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movementInput = playerInput.PlayerMain.Move.ReadValue<Vector2>();
        Vector3 movementDirection = new Vector3(movementInput.x, 0f, movementInput.y);
        //Vector3 movementDirection = (cameraTransform.forward * movementInput.y + cameraTransform.right * movementInput.x);
        //movementDirection.y = 0f;
        //Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            inputMagnitude /= 2;
        }

        animator.SetFloat("InputMagnitude", inputMagnitude, 0.05f, Time.deltaTime);

        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        #region Jump
        float gravity = Physics.gravity.y * gravityMultiplier;

        if (isJumping && ySpeed > 0 && playerInput.PlayerMain.Jump.triggered == false)
        {
            gravity *= 2;
        }

        ySpeed += gravity * Time.deltaTime;

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (playerInput.PlayerMain.Jump.triggered)
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;

            //GROUNDED CONDITION
            animator.SetBool("IsGrounded", true);
            isGrounded = true;
            animator.SetBool("IsJumping", false);
            isJumping = false;
            animator.SetBool("IsFalling", false);

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = Mathf.Sqrt(jumpHeight * -3 * gravity);
                //JUMPING CONDITION
                animator.SetBool("IsJumping", true);
                isJumping = true;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;
            animator.SetBool("IsGrounded", false);
            isGrounded = false;

            if ((isJumping && ySpeed < 0) || ySpeed < -2)
            {
                animator.SetBool("IsFalling", true);
            }
        }

        #endregion

        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
            //if (playerAudio != null)
            //{
            //    if (!playerAudio.isPlaying)
            //    {
            //        playerAudio.Play();
            //    }
            //}
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsMoving", false);
            //if (playerAudio.isPlaying)
            //{
            //    playerAudio.Stop();
            //}
        }

        if (isGrounded == false)
        {
            Vector3 velocity = movementDirection * inputMagnitude * jumpHorizontalSpeed;
            velocity.y = ySpeed;

            characterController.Move(velocity * Time.deltaTime); //jump
        }
    }
    #endregion

    private void OnAnimatorMove()
    {
        if (isGrounded)
        {
            Vector3 velocity = animator.deltaPosition;
            velocity.y = ySpeed * Time.deltaTime;

            characterController.Move(velocity);
        }
    }

    public void ExitPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            exitPressed = true;
        }
        else if (context.canceled)
        {
            exitPressed = false;
        }
    }

    //private void OnApplicationFocus(bool focus)
    //{
    //    if (focus)
    //    {
    //        Cursor.lockState = CursorLockMode.Locked;
    //    }
    //    else
    //    {
    //        Cursor.lockState = CursorLockMode.None;
    //    }
    //}
}
