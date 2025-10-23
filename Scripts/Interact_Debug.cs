using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_Debug : MonoBehaviour
{
    public void Interact()
    {
        Debug.Log("I interacted with " + gameObject.name);
    }
}
