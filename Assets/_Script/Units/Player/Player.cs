using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Singleton<Player>
{
    [Header("References")]
    public Transform head;
    public Rigidbody rb;
    public AudioSource jumpSFX;
    public Transform playerCamera;
    public PlayerUI playerUI;
    public Animator playerAnimator;
    public Slider staminaBar;

    [Header("Properties")]
    public float walkSpeed;
    public float runSpeed;
    public float jumpSpeed;
    public float stamina;
    

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        staminaBar.value = stamina / 100f;
    }
}