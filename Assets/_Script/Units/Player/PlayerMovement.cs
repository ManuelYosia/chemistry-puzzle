using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Player player;
    bool isGrounded = true;
    float speed;
    string playerState;
    Vector3 newVelocity;
   

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString("GameState") == "Playing")
        {
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 2f);
            newVelocity = Vector3.up * player.rb.velocity.y;

            if (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Horizontal") > 0 ||
               Input.GetAxis("Vertical") < 0 || Input.GetAxis("Vertical") > 0)
            {
                playerState = Input.GetKey(KeyCode.LeftShift) ? "run" : "walk";

                newVelocity.x = Input.GetAxis("Horizontal") * speed;
                newVelocity.z = Input.GetAxis("Vertical") * speed;

            }
            else
            {
                player.playerAnimator.SetBool("isIdle", true);
                player.playerAnimator.SetBool("isWalking", false);
                player.playerAnimator.SetBool("isRunning", false);
                playerState = "idle";
            }

            if (!(playerState == "run"))
            {
                player.stamina += 10 * Time.deltaTime;
                player.stamina = Mathf.Clamp(player.stamina, 0, 100);
            }

            if (playerState == "run" && player.stamina > 0)
            {
                player.stamina -= 20*Time.deltaTime;
                speed = player.runSpeed;
                player.playerAnimator.SetBool("isRunning", true);
                player.playerAnimator.SetBool("isIdle", false);

            } else if (playerState == "walk")
            {
                speed = player.walkSpeed;

                player.playerAnimator.SetBool("isWalking", true);
                player.playerAnimator.SetBool("isIdle", false);
            }


            // Jump
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                player.rb.AddForce(new Vector3(0, player.jumpSpeed, 0), ForceMode.Impulse);
                newVelocity.y = player.jumpSpeed;
                isGrounded = false;
                player.jumpSFX.Play();
            }
        }
        


    }

    // Update is called every 0.02s (default)
    void FixedUpdate()
    {   
        player.rb.velocity = transform.TransformDirection(newVelocity);
    }

    // Update is called after update method run
    void LateUpdate()
    {
        if(PlayerPrefs.GetString("GameState") == "Playing")
        {
            Vector3 e = player.playerCamera.eulerAngles;
            e.x -= Input.GetAxis("Mouse Y");
            player.playerCamera.eulerAngles = e;
        }
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor")) {
            isGrounded = false;
        }
    }
}
