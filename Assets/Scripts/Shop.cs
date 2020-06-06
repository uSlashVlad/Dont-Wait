using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour {
    public int crystals;

    [SerializeField] TextMeshProUGUI crystalsInd;

    public float[] i1 = new float[] { 1, 0.5f };
    public float[] i2 = new float[] { 1, 1.5f };
    public float[] i3 = new float[] { 1.25f, 0.5f };
    public float[] i4 = new float[] { 1, 1, 1 };

    void Start() {
        crystals = PlayerPrefs.GetInt("Crystals");

        UpdateInd();
    }
    
    public void UpdateInd() {
        crystalsInd.text = crystals + "";
    }

    public bool ItemUpgrade(int cost, ShopItem item) {
        if (crystals < cost)
            return false;
        else {
            crystals -= cost;

            item.LvlUp();

            UpdateInd();

            PlayerPrefs.SetInt("Crystals", crystals);

            return true;
        }
    }
}
