using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EscapeManager : MonoBehaviour
{
    [SerializeField] string requiredItem;

    [Header("Reference")]
    public DialogueManager w_DialogueManager;
    public DialogueManager dialogueManager;
    public Dialogue escapeDialogue;
    public Dialogue dialogue;

    [Header("Windstate")]
    public GameObject winState;
    public GameObject player;

    void Start()
    {
        winState.SetActive(false);
    }

    public bool AttemptEscape(Inventory inventory)
    {
        if (string.IsNullOrEmpty(requiredItem) || inventory.Has(requiredItem))
        {
            Debug.Log("Escaped through " + gameObject.name);

            player.SetActive(false);
            winState.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            w_DialogueManager.StartDialogue(escapeDialogue);

            return true;
        }
        else
        {
            Debug.Log("You need " + requiredItem + " to escape!");
            dialogueManager.StartDialogue(dialogue);
            return false;
        }
    }
}
