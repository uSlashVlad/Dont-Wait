using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
 
public class Main : MonoBehaviour {
 
    public float critC = 5;
 
    [SerializeField] AudioSource soundSrc;
 
    public float timeClick = 1;
 
    public int clicks = 0;
 
    public float time = 10;
    float timer = 10;
 
    [SerializeField] Slider timeBar;
    [SerializeField] TextMeshProUGUI clicksInd;
 
    public int up1COST = 10;
    public int up2COST = 10;
 
    [SerializeField] TextMeshProUGUI up1Text;
    [SerializeField] TextMeshProUGUI up2Text;
   
    [SerializeField] string U1T = "clicks";
    [SerializeField] string U2T = "time";
 
    public float saveTime = 2.5f;
    public float saveTimer = 0;
 
    public float K = 1;
 
    public bool timeW = true;
 
    [SerializeField] Text inds;
 
    // Increasments
    public float maxTimeInc = 0;
    public float timeIncInc = 0;
   
    public int clickIncStart = 0;
    public float critCIncStart = 0;
 
    public float up1Inc = 0;
    public float up2Inc = 0;
   
    public int d1Inc = 0;
    public int d2Inc = 0;
    public int d3Inc = 0;
 
    void Start() {
        if (PlayerPrefs.GetFloat("MaxTime") != 0)
            Load();
        else
            Save();
 
        if (PlayerPrefs.GetInt("Death") == 1)
            GetComponent<Death>().DeathOpen();
 
        clicksInd.text = clicks + " clicks";
        up1Text.text = U1T + " - " + up1COST + "c";
        up2Text.text = U2T + " - " + up2COST + "c";
 
        timer = time;
 
        K = CalculateK();
 
        critC = 5 + critCIncStart;
    }
   
    float t;
 
    void Update() {
 
        if (timeW) {
            timer -= Time.deltaTime * K;
 
            if (timer <= 0) {
                GetComponent<Death>().DeathOpen();
                PlayerPrefs.SetInt("Death", 1);
            }
            else {
                t += Time.deltaTime;
                if (t >= 1) {
                    PlayerPrefs.SetInt("SecStat", PlayerPrefs.GetInt("SecStat") + 1);
                    t = 0;
                }
            }
        }
 
        timeBar.maxValue = time;
        timeBar.value = timer;
 
        inds.text = Math.Floor(timer) + " / " + Math.Floor(time);
 
        saveTimer += Time.deltaTime;
       
        if (saveTimer >= saveTime) {
            Save();
            saveTimer = 0;
        }
    }
 
    public void Click() {
        if (timeW) {
            timer += timeClick;
            //
            if (timer > time)
                timer = time;
 
            clicks += 1 + clickIncStart;
            clicksInd.text = clicks + " clicks";
 
            soundSrc.Play();
 
            PlayerPrefs.SetInt("ClicksStat", PlayerPrefs.GetInt("ClicksStat") + 1);
 
            K = CalculateK();
 
            if (CalcChance(critC))
                Click();
        }
    }
 
    public void Upgrade(int upInd) {
        if (upInd == 1) {
            if (clicks >= up1COST) {
                clicks -= up1COST;
                up1COST = (int)Mathf.Floor(up1COST * 1.5f);
                timeClick += 0.5f + up1Inc;
                up1Text.text = U1T + " - " + up1COST + "c";
            }
        }
        else if (upInd == 2) {
            if (clicks >= up2COST) {
                clicks -= up2COST;
                up2COST = (int)Mathf.Floor(up2COST * 1.5f);
                time += 1.75f + up2Inc;
                up2Text.text = U2T + " - " + up2COST + "c";
            }
        }
 
        critC += 0.5f;
 
        clicksInd.text = clicks + " clicks";
    }
 
    public void Save() {
        PlayerPrefs.SetFloat("TimeClick", timeClick);
        PlayerPrefs.SetInt("Clicks", clicks);
        PlayerPrefs.SetFloat("MaxTime", time);
        PlayerPrefs.SetFloat("Time", timer);
        PlayerPrefs.SetInt("Up1", up1COST);
        PlayerPrefs.SetInt("Up2", up2COST);
        PlayerPrefs.SetFloat("Crit", critC);
       
        Debug.Log("SAVE!");
    }
 
    public void Load() {
        timeClick = PlayerPrefs.GetFloat("TimeClick");
        clicks = PlayerPrefs.GetInt("Clicks");
        time = PlayerPrefs.GetFloat("MaxTime") + maxTimeInc;
        timer = PlayerPrefs.GetFloat("Time") + timeIncInc;
        up1COST = PlayerPrefs.GetInt("Up1");
        up2COST = PlayerPrefs.GetInt("Up2");
        critC = PlayerPrefs.GetFloat("Crit");
 
        if (critC == 0)
            critC = 5 + critCIncStart;
 
        clicksInd.text = clicks + " clicks";
        up1Text.text = U1T + " - " + up1COST + "c";
        up2Text.text = U2T + " - " + up2COST + "c";
 
        Debug.Log("LOAD!");
    }
 
    public float CalculateK () {
        int clicksS = PlayerPrefs.GetInt("ClicksStat") + 1;
        float k = (clicksS * clicksS) / (3500 * (float)Math.Sqrt(clicksS));
 
        if (k < 1)
            k = 1;
        else if (k > 1000)
            k = 15;
 
        //Debug.Log(k);
        return k;
    }
 
    public bool CalcChance (float chance) {
        float rand = UnityEngine.Random.Range(0, 100);
 
        if (rand <= chance)
            return true;
        else
            return false;
    }
}
