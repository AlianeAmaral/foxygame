using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TMP_Text fruitTexts;
    public int fruits;
    public GameObject DiePanel;
    public TMP_Text dieFruitsText;
    public GameObject WinPanel;

    public static GameManager Instance = null;

    void Awake()
    {
        if (Instance != null) Destroy(Instance);
        else Instance = this;
    }
    void Start()
    {
        fruitTexts.text = $"X {fruits}";
    }


    public void AddFruit(int quantity)
    {
        fruits += quantity;

        fruitTexts.text = $"X {fruits}";
    }
    public void Die()
    {
        DiePanel.SetActive(true);
        dieFruitsText.text = $"{fruits} Frutas Coletadas!";
    }
    public void Win()
    {
        WinPanel.SetActive(true);
    }
    public int CheckFruits()
    {
        return GameObject.FindGameObjectsWithTag("Fruit").Count();
    }
}
