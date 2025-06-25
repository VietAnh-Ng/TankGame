using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TankItem
{
    public GameObject Prefab;
    public string Name;
    public string Description;
    public int HP;
    public int ATK;
    public float Speed;
    public int Armor;
    public int Range;
    public int Price;
    public int Id;
    public bool IsBought;
}

[Serializable]
public class PlayerData
{
    public int[] BoughtIds;
}

public class TankItemView : MonoBehaviour
{
    [SerializeField] GameObject Tank;
    [SerializeField] TMP_Text txtName;
    [SerializeField] TMP_Text txtDes;
    [SerializeField] TMP_Text txtHP;
    [SerializeField] TMP_Text txtATK;
    [SerializeField] TMP_Text txtSpeed;
    [SerializeField] TMP_Text txtArmor;
    [SerializeField] TMP_Text txtRange;
    [SerializeField] TMP_Text txtPrice;
    [SerializeField] Text txtBtn;

    private string FullDescription;

    [SerializeField]
    private GameObject currentTank;

    private TankItem Data;

    const string ListBoughtIdsKey = "list_bought_ids";

    public void LoadData(TankItem item)
    {
        var playerData = GetPlayerData();
        Data = item;
        if (currentTank != null)
        {
            Destroy(currentTank);
        }
        currentTank = Instantiate(Data.Prefab, Tank.transform);
        currentTank.transform.Rotate(0, 0, -90);
        txtName.text = Data.Name;
        FullDescription = $"Mô tả: {Data.Description}";
        //txtDes.text = $"{FullDescription[..10]}...";
        txtHP.text = $"HP: {Data.HP}";
        txtATK.text = $"ATK: {Data.ATK}";
        txtSpeed.text = $"Speed: {Data.Speed} m/s";
        txtArmor.text = $"Armor: {Data.Armor}";
        txtRange.text = $"Range: {Data.Range} m";
        txtPrice.text = $"{Data.Price}$";
        Data.IsBought = playerData.BoughtIds.Contains(Data.Id);
        if (Data.IsBought)
        {
            txtBtn.text = "Sell";
        }
        else
        {
            txtBtn.text = "Buy";
        }
    }

    public void ButtonClick()
    {
        var playerData = GetPlayerData();
        if (Data.IsBought)
        {
            Debug.Log($"sell {Data.Id}");
            var ids = new List<int>(playerData.BoughtIds);
            ids.Remove(Data.Id);
            playerData.BoughtIds = ids.ToArray();
            SavePlayerData(playerData);
            txtBtn.text = "Buy";
            Data.IsBought = false;
        }
        else
        {
            Debug.Log($"buy {Data.Id}");
            var ids = new List<int>(playerData.BoughtIds);
            ids.Add(Data.Id);
            playerData.BoughtIds = ids.ToArray();
            SavePlayerData(playerData);
            txtBtn.text = "Sell";
            Data.IsBought = true;
        }
    }

    private static PlayerData GetPlayerData()
    {
        try
        {
            var str = PlayerPrefs.GetString(ListBoughtIdsKey);
            Debug.Log("GetPlayerData");
            Debug.Log(str);
            return JsonUtility.FromJson<PlayerData>(str);
        }
        catch
        {
            var pData = new PlayerData();
            pData.BoughtIds = new int[0];
            return pData;
        }
    }

    private static void SavePlayerData(PlayerData playerData)
    {
        var str = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(ListBoughtIdsKey, str);
        PlayerPrefs.Save();

    }
}
