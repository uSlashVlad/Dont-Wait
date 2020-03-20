using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour {

    Main main;

    [SerializeField] GameObject panel;
    public bool show = false;

    [SerializeField] GameObject panelHelp;
    public bool showHelp = false;

    [SerializeField] GameObject panelShop;
    public bool showShop = false;

    [SerializeField] Toggle muteTog;
    [SerializeField] AudioSource soundSrc;

    [SerializeField] Toggle indsTog;
    [SerializeField] GameObject inds;

    [SerializeField] Button clickButton;
    [SerializeField] GameObject freeze;

//    [SerializeField] string vkURL = "https://vk.com/debils_tech";

	void Start () {
        main = GetComponent<Main>();

        muteTog.isOn = (PlayerPrefs.GetInt("Mute") == 1) ? true : false;
        soundSrc.mute = !muteTog.isOn;

        indsTog.isOn = (PlayerPrefs.GetInt("Inds") == 1) ? true : false;
        inds.SetActive(indsTog.isOn);

        // * * * Для установки паузы при запуске игры * * *
        TogglePanel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePanel();
        else if (Input.GetKeyDown(KeyCode.H))
            ToggleHelp();
    }

    public void TogglePanel()
    {
        if (show)
        {
            panel.SetActive(false);
            show = false;
            //
            main.timeW = true;
            clickButton.interactable = true;
        }
        else
        {
            panel.SetActive(true);
            show = true;
            //
            main.timeW = false;
            clickButton.interactable = false;
        }

        if ((showHelp || showShop) & !show)
        {
            if (showHelp)
                ToggleHelp();
            else if (showShop)
                ToggleShop();
        }
    }

    public void ToggleHelp()
    {
        if (showHelp)
        {
            panelHelp.SetActive(false);
            showHelp = false;
        }
        else
        {
            panelHelp.SetActive(true);
            showHelp = true;
        }

        if (showHelp & !show)
            TogglePanel();
        else if (!showHelp & show)
            TogglePanel();
    }

    public void ToggleShop()
    {
        if (showShop)
        {
            panelShop.SetActive(false);
            showShop = false;
        }
        else
        {
            panelShop.SetActive(true);
            showShop = true;
        }

        if (showShop & !show)
            TogglePanel();
        else if (!showShop & show)
            TogglePanel();
    }

    public void ResetSave()
    {
        PlayerPrefs.SetFloat("TimeClick", 1 + main.timeIncInc);
        PlayerPrefs.SetInt("Clicks", 0);
        PlayerPrefs.SetFloat("MaxTime", 10);
        PlayerPrefs.SetFloat("Time", 10);
        PlayerPrefs.SetInt("Up1", 10);
        PlayerPrefs.SetInt("Up2", 10);
        PlayerPrefs.SetFloat("Crit", 5);
        //
        PlayerPrefs.SetInt("ClicksStat", 0);
        PlayerPrefs.SetInt("SecStat", 0);

        main.Load();
        main.K = main.CalculateK();
    }

    public void ChangeVolume()
    {
        soundSrc.mute = !muteTog.isOn;

        PlayerPrefs.SetInt("Mute", (soundSrc.mute) ? 0 : 1);
    }

    public void ToggleInds()
    {
        inds.SetActive(indsTog.isOn);

        PlayerPrefs.SetInt("Inds", (inds.active) ? 1 : 0);
    }
}
