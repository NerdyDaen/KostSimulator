using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private float jumpHorizontalSpeed;
    [SerializeField] private float jumpButtonGracePeriod;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private DialogueUI dialogueUI;

    public IInteractable Interactable { get; set; }
    public DialogueUI DialogueUI => dialogueUI;
    public AudioClip Click;

    private CharacterController characterController;
    private AudioSource playerAudio;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private bool isJumping;
    private bool isGrounded;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interactable?.Interact(this);
            //playerAudio.PlayOneShot(Click);
        }

        #region Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
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

        if (isJumping && ySpeed > 0 && Input.GetButton("Jump") == false)
        {
            gravity *= 2;
        }

        ySpeed += gravity * Time.deltaTime;

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
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
            //if (!playerAudio.isPlaying)
            //{
            //    //playerAudio.Play();
            //}
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsMoving", false);
            //if (playerAudio.isPlaying)
            //{
            //    //playerAudio.Stop();
            //}
        }

        if (isGrounded == false)
        {
            Vector3 velocity = movementDirection * inputMagnitude * jumpHorizontalSpeed;
            velocity.y = ySpeed;

            characterController.Move(velocity * Time.deltaTime);
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