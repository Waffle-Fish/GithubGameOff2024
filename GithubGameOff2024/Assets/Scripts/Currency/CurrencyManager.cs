using UnityEngine;
using TMPro;
using System.Collections;
public class CurrencyManager : MonoBehaviour
{
    private static CurrencyManager _instance;

    public int amount = 0;
    public float saveInterval = 1f;
    public static CurrencyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                if (FindFirstObjectByType<CurrencyManager>())
                {
                    GameObject g = GameObject.Find("CurrencyManager");
                    if (g.GetComponent<CurrencyManager>())
                    {
                        _instance = g.GetComponent<CurrencyManager>();
                    }
                    else
                    {
                        _instance = g.AddComponent<CurrencyManager>();
                    }
                }
                else
                {
                    GameObject g = new GameObject("CurrencyManager");
                    _instance = g.AddComponent<CurrencyManager>();
                }
            }

            return _instance;
        }
        set
        {
            _instance = value;
        }

    }
    void Start()
    {
        _instance = this;
        PlayerPrefs.SetInt("Currency", amount);
        StartCoroutine(SaveCurrency());
    }

    public IEnumerator SaveCurrency()
    {
        PlayerPrefs.SetInt("Currency", amount);
        yield return new WaitForSeconds(saveInterval);
    }
    public void AddCurrency(int amount)
    {
        Instance.amount += amount;
    }
    public bool RemoveCurrency(int amount)
    {
        if (Instance.amount >= amount)
        {
            Instance.amount -= amount;
            return true;
        }
        return false;
    }

    public int GetCurrency()
    {
        return Instance.amount;
    }


}
