using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Chicken : MonoBehaviour
{
    [SerializeField] GameObject[] Waypoints;
    [SerializeField] float speed = 1f;
    [SerializeField] GameObject BubbleChat;
    int currentWaypointIndex = 0;
    Animator _animator;
    TypewritterEffect _dialog;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _dialog = BubbleChat.GetComponent<TypewritterEffect>();
    }

    void Update()
    {
        
        if (PlayerPrefs.GetString("GameState") == "Intro")
        {
            Debug.Log("Apaan seh?");
            Intro();
        } else {
            gameObject.SetActive(false);
        }

    }

    void Intro()
    {

        if (currentWaypointIndex >= Waypoints.Length)
        {
            _animator.SetBool("walk", false);
            _animator.SetBool("idle", true);

            if (_animator.GetBool("idle") == true)
            {
                Talk();
            }

        }
        else {
            // Rotate to face the next waypoint
            Vector3 direction = (transform.position - Waypoints[currentWaypointIndex].transform.position).normalized;
            if (direction != Vector3.zero && currentWaypointIndex < Waypoints.Length)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = lookRotation;
            }

            if (Vector3.Distance(transform.position, Waypoints[currentWaypointIndex].transform.position) < .1f)
            {

                currentWaypointIndex++;

            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, Waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
            }
        }  
    }

    void Talk()
    {
        // Show bubble chat
        BubbleChat.SetActive(true);
        _dialog.StartTyping();
        
    }
}