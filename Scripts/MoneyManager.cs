using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1000)]
public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    public int Total { get; private set; }

    public event System.Action<int> OnMoneyChanged;

    void Awake() { Instance = this; }

    public void Add(int amount)
    {
        Total += amount;
        OnMoneyChanged?.Invoke(Total);
    }
}
