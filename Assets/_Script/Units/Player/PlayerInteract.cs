using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] float rayDistance;
    [SerializeField] LayerMask layerMask;

    Ray ray;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(player.playerCamera.position, player.playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, rayDistance, layerMask)){
            player.playerUI.ShowInstructionText();
            player.playerUI.ShowPromptText(hitInfo.collider.GetComponent<Interactable>().promptMessage);

            Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
            if (Input.GetKeyDown(KeyCode.E))
            {
                hitInfo.collider.GetComponent<Interactable>().BaseInteract();
            }
        }
        else
        {
            player.playerUI.HideInstructionText();
            player.playerUI.HidePromptText();
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.green);
        }
    }
}
