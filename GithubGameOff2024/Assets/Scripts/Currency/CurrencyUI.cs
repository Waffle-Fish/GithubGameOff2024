using UnityEngine;
using TMPro;
public class CurrencyUI : MonoBehaviour
{
    private TextMeshProUGUI currencyText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currencyText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        currencyText.text = CurrencyManager.Instance.GetCurrency().ToString();
    }
}
