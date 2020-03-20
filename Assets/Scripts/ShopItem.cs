using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [SerializeField] string name;

    public int lvl = 1;
    //---------
    public int cost;
    [SerializeField] int costInc;
    //---------
    public enum TypeOfItem {Time, Click, Upgrades, Diamonds};
    public TypeOfItem type;

    [SerializeField] TextMeshProUGUI nameInd;
    [SerializeField] Button buyButton;
    [SerializeField] TextMeshProUGUI costInd;

    public string ID;

    Shop shop;
    Main main;

    void Start()
    {
        shop = GameObject.Find("EventSystem").GetComponent<Shop>();
        main = shop.GetComponent<Main>();

        ID = type.ToString();
        if (PlayerPrefs.GetInt(ID+"Lvl") == 0)
        {
            Save();
        }
        else
        {
            Load();
        }

        nameInd.text = name + " (" + lvl + ")";
        costInd.text = cost + "";


        UpdateInc();
    }
    
    void Update()
    {
        if (shop.crystals < cost)
            buyButton.interactable = false;
        else
            buyButton.interactable = true;
    }

    public void Upgrade()
    {
        if (shop.ItemUpgrade(cost, this))
        {
            nameInd.text = name + " (" + lvl + ")";
            costInd.text = cost + "";
        }
    }

    public void LvlUp()
    {
        lvl++;
        Save();

        cost = lvl * costInc;

        UpdateInc();
    }

    void UpdateInc()
    {
        Debug.Log("Increasment updated in " + ID);

        switch(type)
        {
            case TypeOfItem.Time:
                main.maxTimeInc = shop.i1[0] * lvl;
                main.timeIncInc = shop.i1[1] * lvl;
                break;

            case TypeOfItem.Click:
                main.clickIncStart = (int)shop.i2[0] * lvl;
                main.critCIncStart = shop.i2[1] * lvl;
                break;

            case TypeOfItem.Upgrades:
                main.up1Inc = shop.i3[0] * lvl;
                main.up2Inc = shop.i3[1] * lvl;
                break;

            case TypeOfItem.Diamonds:
                main.d1Inc = (int)shop.i4[0] * lvl;
                main.d2Inc = (int)shop.i4[1] * lvl;
                main.d3Inc = (int)shop.i4[2] * lvl;
                break;

            default:
                return;
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt(ID + "Lvl", lvl);
    }

    public void Load()
    {
        lvl = PlayerPrefs.GetInt(ID + "Lvl");
    }
}
