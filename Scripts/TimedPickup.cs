using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimedPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public float pickupTime = 3f;
    public GameObject valuable;

    [Header("UI")]
    public Slider pickupProgressUI;

    [Header("Reference")]
    public PlayerMovement playerMovement;
    public Inventory inventory;

    private bool isBeingPickup = false;

    void Start()
    {
        pickupProgressUI.gameObject.SetActive(false);
    }

    public IEnumerator Pickup(System.Action onComplete)
    {
        if (isBeingPickup) yield break;
        isBeingPickup = true;
        playerMovement.canMove = false;

        if (pickupProgressUI != null)
        {
            pickupProgressUI.gameObject.SetActive(true);
            pickupProgressUI.value = 0;
        }

        float elapsed = 0f;
        while (elapsed < pickupTime)
        {
            elapsed += Time.deltaTime;
            if (pickupProgressUI != null)
                pickupProgressUI.value = Mathf.Clamp01(elapsed / pickupTime);

            yield return null;
        }

        onComplete?.Invoke();

        var item = GetComponent<ItemIdentification>();
        if (item != null && inventory != null && !string.IsNullOrEmpty(item.itemID))
        {
            inventory.Add(item.itemID);
            Debug.Log($"Added {item.itemID} to inventory");
        }

        var money = GetComponent<MoneyAmount>();
        if (money != null)
        {
            money.Collect();
        }

        Destroy(gameObject);

        if (valuable != null)
        {
            valuable.SetActive(true);
        }

        if (pickupProgressUI != null)
            pickupProgressUI.gameObject.SetActive(false);

        isBeingPickup = false;
        playerMovement.canMove = true;
    }
}
