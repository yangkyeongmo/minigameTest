using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickerEventControl : MonoBehaviour {

    public float timerStart;
    public GameObject ctrl;
    public GameObject endObject;
    public float badBound, normalBound, goodBound;

    private bool gameEnd = false;
    private float timer;
    private int count = 0;
    private int resultParameter = 0;
    private GameObject result;

    private Text timerText;
    private Text countText;


	// Use this for initialization
	void Start () {
        timer = timerStart;
        timerText = GameObject.Find("TimerText").GetComponent<Text>();
        countText = GameObject.Find("CountText").GetComponent<Text>();
        result = endObject.transform.Find("Result").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        timerText.text = timer.ToString("N2") + "초";
        countText.text = "HITS: " + count.ToString();

		if(timer > 0 && !gameEnd)
        {
            timer -= Time.deltaTime;
        }
        else if(timer <= 0)
        {
            timer = 0;
            gameEnd = true;
        }
        if (gameEnd)
        {
            OnGameEnd();
        }
	}

    private void OnGameEnd()
    {
        //TODO: Do something on game end
        Button btn = GameObject.Find("CtrlButton").GetComponent<Button>();
        btn.enabled = false;
        SetResultParameter();
        endObject.SetActive(true);
        DecreaseArtParameter(resultParameter);
    }

    private void SetResultParameter(){
        if(count > 0){
            if(count <= badBound){
                SetResult("BAD", +3);
            }
            else if(count <= normalBound){
                SetResult("NORMAL", 0);
            }
            else if(count <= goodBound){
                SetResult("GOOD", -3);
            }
            else {
                SetResult("OVER", -5);
            }
        }
    } 
    
    private void SetResult(string rsltTxt, int rsltPrm){
        Text resultText = result.transform.Find("Text").GetComponent<Text>();
        resultText.text = rsltTxt;
        resultParameter = rsltPrm;
    }

    private void DecreaseArtParameter(int param){
        PlayerPrefs.SetInt("Art", PlayerPrefs.GetInt("Art") + param);
    }

    public void ModifyCount(int mdfy)
    {
        count += mdfy;
    }
}
