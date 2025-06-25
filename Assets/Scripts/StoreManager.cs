using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [SerializeField] List<TankItem> Tanks;
    [SerializeField] List<TankItemView> TankViews;
    [SerializeField] TMP_Text txtPageNumber;

    private int currentPage = 0;
    private List<int> BoughtIds = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        LoadPage(1);
    }

    public void LoadPage(int page)
    {
        if (currentPage == page) return;

        currentPage = page;
        txtPageNumber.text = page.ToString();
        for (int i = 0; i < TankViews.Count; i++)
        {
            TankViews[i].LoadData(Tanks[(currentPage - 1) * TankViews.Count + i]);
        }
    }

    public void NextPage()
    {
        if (currentPage >= Mathf.Floor(1.0f * Tanks.Count / TankViews.Count)) return;

        LoadPage(currentPage + 1);
    }

    public void PreviusPage()
    {
        if (currentPage <= 1) return;

        LoadPage(currentPage - 1);
    }
}
