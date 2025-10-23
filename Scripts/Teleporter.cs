using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform teleportPoint;

    public AudioSource audioSource;
    public GameObject owner;

    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = player.GetComponent<CharacterController>();
        owner.SetActive(false);

    }

    public void TeleportPlayer()
    {
        if (controller != null)
        {
            controller.enabled = false;
            player.position = teleportPoint.position;
            controller.enabled = true;
            audioSource.Stop();
            owner.SetActive(true);
        }
        else
        {
            player.position = teleportPoint.position;
        }
    }
}
