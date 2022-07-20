using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerActive : MonoBehaviour, IDataPersistence
{
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;
    public float jumpButtonGracePeriod;
    public IInteractable Interactable { get; set; }
    public DialogueUI DialogueUI => dialogueUI;
    public AudioClip Click;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private DialogueUI dialogueUI;

    private Animator animator;
    private Player playerInput;
    private CharacterController characterController;
    private AudioSource playerAudio;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
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

    void Start()
    {
        animator = GetComponent<Animator>();
        originalStepOffset = characterController.stepOffset;
        cameraTransform = Camera.main.transform;
        playerAudio = GetComponent<AudioSource>();
        Physics.SyncTransforms();
    }

    public void LoadData(GameData data)
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
            Interactable?.Interact(this);
            //playerAudio.PlayOneShot(Click);
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movementInput = playerInput.PlayerMain.Move.ReadValue<Vector2>();

        Vector3 movementDirection = new Vector3(movementInput.x, 0f, movementInput.y);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;

        //animator.SetFloat("InputMagnitude", inputMagnitude, 0.05f, Time.deltaTime);

        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        #region Jump
        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        //if (Input.GetButtonDown("Jump"))
        //{
        //    jumpButtonPressedTime = Time.time;
        //}

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;
        }
        #endregion

        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }
}
