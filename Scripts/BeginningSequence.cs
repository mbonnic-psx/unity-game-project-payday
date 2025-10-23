using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginningSequence : MonoBehaviour
{

    [Header("Reference")]
    public Animator animator;
    public Billboard billboard;
    public DialogueManager dialogueManager;

    [Header("Objects")]
    public GameObject itself;
    public GameObject enemyAI;
    public GameObject lights;
    public GameObject itemsToEscape;
    public GameObject escapes;
    public GameObject valuables;

    [Header("Audio")]
    public AudioSource powerOff;
    public AudioSource whisper;
    public AudioSource ambi;

    private string currentAnim = "";
    private bool isInConversation;

    // Start is called before the first frame update
    void Start()
    {
        billboard.EnableLookAt();
        ChangeAnimation("Idle");
        isInConversation = false;
        enemyAI.SetActive(false);
        itemsToEscape.SetActive(false);
        escapes.SetActive(false);
        valuables.SetActive(false);
    }

    void Update()
    {
        if (dialogueManager != null && dialogueManager.IsDialogueActive)
        {
            isInConversation = true;
        }

        if (dialogueManager != null && isInConversation == true && !dialogueManager.IsDialogueActive)
        {
            itself.SetActive(false);
            lights.SetActive(false);
            enemyAI.SetActive(true);
            itemsToEscape.SetActive(true);
            escapes.SetActive(true);
            valuables.SetActive(true);
            powerOff.Play();
            whisper.Play();
            ambi.Play();
        }
    }

    private void ChangeAnimation(string animation, float crossfade = 0.2f)
    {
        if (currentAnim != animation)
        {
            currentAnim = animation;
            animator.CrossFade(animation, crossfade); // Transitions the current animation to the next animation
        }
    }
}
