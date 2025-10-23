using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoneyAmount : MonoBehaviour
{
    [SerializeField] private int amount = 0;

    public void Collect()
    {
        MoneyManager.Instance.Add(amount);
    }
}
