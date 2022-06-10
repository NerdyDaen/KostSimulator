using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActive : MonoBehaviour
{
    public static AudioSoundManager instance;

    [Header("Movement Settings")]
    public CharacterController TargetPlayer;
    public Camera TargetCamera;
    public Animator TargetAnimator;
    public float mouseSensitivity = 2f;
    public float PlayerSpeed;
    public float VerticalSpeed;
    public float JumpSpeed;
    public float Gravity;
    public float upLimit = -50;
    public float downLimit = 50;
    Vector3 moveDirection;
    private AudioSource playerAudio;
    public AudioClip Click;
    public bool IsMoving;

    [Header("Animation Settings")]
    public string AnimationWalkingState;

    [Header("UI Settings")]
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }
    public bool IsSelecting;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None; Cursor.visible = true;
        playerAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        PlayerMovement();
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interactable?.Interact(this);
            playerAudio.PlayOneShot(Click);
        }
    }
    void PlayerMovement()
    {
        //Interacrive environment 
        Shader.SetGlobalVector("_PositionMoving", transform.position);

        //variabel untuk pergerakan dan rotasi
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");
        float horizontalRotation = Input.GetAxis("Mouse X");
        float verticalRotation = Input.GetAxis("Mouse Y");

        //perubahan rotasi kamera dan karakter berdasarkan mouse
        TargetPlayer.transform.Rotate(0, horizontalRotation * mouseSensitivity, 0);
        TargetCamera.transform.Rotate(-verticalRotation * mouseSensitivity, 0, 0);

        //clamp nilai rotasi 
        Vector3 currentRotation = TargetCamera.transform.localEulerAngles;
        if (currentRotation.x > 180) currentRotation.x -= 360;
        currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
        TargetCamera.transform.localRotation = Quaternion.Euler(currentRotation);

        //cek nilai untuk kecepatan loncat/gravitasi
        if (TargetPlayer.isGrounded) VerticalSpeed = 0;
        else VerticalSpeed -= Gravity * Time.deltaTime;
        Vector3 gravityMove = new Vector3(0, VerticalSpeed, 0);

        //buat variabel untuk maju ke depan
        Vector3 move = TargetPlayer.transform.forward * verticalMove + TargetPlayer.transform.right * horizontalMove;

        //if (Input.GetButton("Jump")) move.y = JumpSpeed;
        TargetPlayer.Move(PlayerSpeed * Time.deltaTime * move + gravityMove * Time.deltaTime);

        //jalankan animasi sesuai dengan parameter
        TargetAnimator.SetBool(AnimationWalkingState, verticalMove != 0 || horizontalMove != 0);

        //if (Input.GetKeyDown(KeyCode.Space) && TargetPlayer.isGrounded == false)
        //{
        //    TargetAnimator.SetTrigger("Jump");
        //}

        if (horizontalMove != 0 || verticalMove != 0)
        {
            IsMoving = true;
            if (!playerAudio.isPlaying)
            {
                playerAudio.Play();
            }
        }
        else
        {
            IsMoving = false;
            playerAudio.Stop();
        }
    }
}
