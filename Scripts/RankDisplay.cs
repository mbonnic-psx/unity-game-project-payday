using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rankText;

    // Start is called before the first frame update
    void Start()
    {
        if (MoneyManager.Instance != null)
        {
            MoneyManager.Instance.OnMoneyChanged += UpdateRank;
            UpdateRank(MoneyManager.Instance.Total);
        }
    }

    void OnDestroy()
    {
        if (MoneyManager.Instance != null)
        {
            MoneyManager.Instance.OnMoneyChanged -= UpdateRank;
        }
    }

    void UpdateRank(int total)
    {
        string rank = GetRank(total);
        if (rankText) rankText.text = rank;
    }

    string GetRank(int money)
    {
        if (money >= 1000)
        {
            return "S";
        }
        else if (money >= 750)
        {
            return "A";
        }
        else if (money >= 500)
        {
            return "B";
        }
        else if (money >= 250)
        {
            return "C";
        }
        else if (money >= 100)
        {
            return "D";
        }
        else
        {
            return "F";
        }
    }
}
