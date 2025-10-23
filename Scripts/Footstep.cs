using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    public AudioSource walkingSound, sprintSound;
    public bool canSound = true;

    void Update()
    {
        if (canSound)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    walkingSound.enabled = false;
                    sprintSound.enabled = true;
                }
                else
                {
                    walkingSound.enabled = true;
                    sprintSound.enabled = false;
                }
            }
            else
            {
                walkingSound.enabled = false;
                sprintSound.enabled = false;
            }
        }

    }

    public void EnableSound(bool enable)
    {
        canSound = enable;
    }
}
