using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    void Start()
    {
        if (MoneyManager.Instance != null)
        {
            MoneyManager.Instance.OnMoneyChanged += UpdateText;
            UpdateText(MoneyManager.Instance.Total);
        }
        else
        {
            Debug.LogWarning("MoneyDisplay: MoneyManager.Instance is null in Start.");
        }
    }

    void OnDestroy()
    {
        if (MoneyManager.Instance != null)
            MoneyManager.Instance.OnMoneyChanged -= UpdateText;
    }

    private void UpdateText(int newTotal)
    {
        if (moneyText != null)
            moneyText.text = $"${newTotal}.00";
        else
            Debug.LogWarning("MoneyDisplay: moneyText (TMP) is not assigned.");
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (moneyText == null)
            moneyText = GetComponentInChildren<TextMeshProUGUI>();
    }
#endif
}
