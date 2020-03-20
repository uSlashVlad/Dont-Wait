using UnityEngine;
using UnityEngine.Advertisements;
using TMPro;

public class Death : MonoBehaviour {

    Options opt;

    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI stat;
    [SerializeField] TextMeshProUGUI statD;

    Main main;

    public bool showAd = true;

	void Start () {

        main = GetComponent<Main>();
        opt = GetComponent<Options>();
	}

    int d1;
    int d2;
    int d3;

    public void DeathOpen()
    {
        if (Advertisement.IsReady("video") & showAd)
        {
            Advertisement.Show("video");
            showAd = false;
        }
        panel.SetActive(true);
        stat.text = PlayerPrefs.GetInt("ClicksStat") + " clicks\n" + PlayerPrefs.GetInt("SecStat") + " secs";

        d1 = (int)Mathf.Floor(PlayerPrefs.GetInt("ClicksStat") / 1000) + main.d1Inc;
        d2 = (int)Mathf.Floor(PlayerPrefs.GetInt("SecStat") / 60) + main.d2Inc;
        d3 = main.d3Inc;
        //
        statD.text = d1 + "+" + d2 + "+" + d3 + " = " + (d1 + d2 + d3) + " d";
    }

    public void DeathReset()
    {
        PlayerPrefs.SetInt("Death", 0);
        panel.SetActive(false);
        opt.ResetSave();
        showAd = true;

        GetComponent<Shop>().crystals += d1 + d2 + d3;

        GetComponent<Shop>().UpdateInd();
    }
}
