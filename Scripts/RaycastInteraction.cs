using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaycastInteraction : MonoBehaviour
{
    [Header("Raycast")]
    public float interactDistance = 2f;
    public LayerMask interactLayer;

    [Header("Scripts")]
    public DialogueManager dialogueManager;
    public Inventory inventory;

    [Header("Others")]
    public TextMeshProUGUI targetNameText;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        targetNameText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueManager != null && dialogueManager.IsDialogueActive)
        {
            targetNameText.text = "";
            return;
        }

        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
        {
            ItemDescriptor descriptor = hit.collider.GetComponent<ItemDescriptor>();
            if (descriptor != null)
            {
                targetNameText.text = descriptor.getName();
            }
            else
            {
                targetNameText.text = "";
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                switch (hit.collider.tag)
                {
                    case "dialogue":
                        hit.collider.GetComponent<Interactable_Dialogue>()?.TriggerDialogue();
                        break;
                    case "interact":
                        hit.collider.GetComponent<Interact_Debug>()?.Interact();
                        break;
                    case "item":
                        var item = hit.collider.GetComponent<TimedPickup>();
                        if (item != null)
                        {
                            StartCoroutine(item.Pickup(() =>
                            {
                                Debug.Log("Finished pickup!");
                            }));
                        }
                        break;
                    case "escape":
                        var escape = hit.collider.GetComponent<EscapeManager>();
                        if (escape != null)
                        {
                            escape.AttemptEscape(inventory);
                        }
                        break;
                    case "teleporter":
                        hit.collider.GetComponent<Teleporter>()?.TeleportPlayer();
                        break;
                }
            }
        }
        else
        {
            targetNameText.text = "";
        }
    }
}
