using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventControl : MonoBehaviour {

    public int maxMineCount, badBound, normalBound, goodBound;
    public float timerStart;

    public static Element[,] mineElements;
    public Text timerText;
    public Text bugCountText;

    private Element[,] elements;
    private int mineCount = 0;
    private int bugCount = 0;
    private float timer;
    private bool isGameEnd = false;
    private int resultParameter = 0;
    private bool isGameEndOver = false;

    private GameObject endObject;

	// Use this for initialization
	void Start () {
        timer = timerStart;
        endObject = GameObject.Find("GameEnd");
        endObject.SetActive(false);
        elements = GridControl.elements;
        if(mineCount != maxMineCount)
        {
            ModifyMineCount();
        }
	}

    private void ModifyMineCount()
    {
        Element elem;
        if (mineCount > maxMineCount)
        {
            elem = elements[Random.Range(0, GridControl.w), Random.Range(0, GridControl.h)];
            while (!elem.mine && mineCount > 0)
            {
                elem = elements[Random.Range(0, GridControl.w), Random.Range(0, GridControl.h)];
            }
            elem.mine = false;
            MineCountDown();
            ModifyMineCount();
        }
        else if (mineCount < maxMineCount)
        {
            elem = elements[Random.Range(0, GridControl.w), Random.Range(0, GridControl.h)];
            elem.mine = true;
            MineCountUp();
            ModifyMineCount();
        }
    }

    public void MineCountDown()
    {
        mineCount--;
    }

    public void MineCountUp()
    {
        mineCount++;
    }

    // Update is called once per frame
    void Update () {
        timerText.text = timer.ToString("N2") + "초";
        bugCountText.text = "BUGS: " + bugCount.ToString() + "/" + maxMineCount.ToString();
        if (!isGameEnd)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            if(bugCount == maxMineCount || timer < 0)
            {
                isGameEnd = true;
            }
        }
        else
        {
            if (!isGameEndOver)
            {
                OnGameEnd();
            }
        }
	}

    private void OnGameEnd()
    {
        timer = 0;
        isGameEnd = true;
        GridControl.UncoverMines();
        SetResultParameter();
        endObject.SetActive(true);
        ModifyProgrammerParameter(resultParameter);
        isGameEndOver = true;
    }

    private void SetResultParameter(){
        if(bugCount > 0){
            if(bugCount <= badBound){
                SetResult("BAD", +3);
            }
            else if(bugCount <= normalBound){
                SetResult("NORMAL", 0);
            }
            else if(bugCount <= goodBound){
                SetResult("GOOD", -3);
            }
            else if(bugCount <= maxMineCount){
                SetResult("OVER", -5);
            }
        }
    }

    private void ModifyProgrammerParameter(int param){
        PlayerPrefs.SetInt("Programmer", PlayerPrefs.GetInt("Programmer") + param);
    }

    private void SetResult(string rsltTxt, int rsltPrm){
        Text resultText = endObject.transform.Find("Result").gameObject.GetComponentInChildren<Text>();
        resultText.text = rsltTxt;
        resultParameter = rsltPrm;
    }

    public void TimerModify(float time)
    {
        timer += time;
    }

    public float GetTimer()
    {
        return timer;
    }

    public void BugCountUp()
    {
        bugCount++;
    }

    public void BugCountDown()
    {
        bugCount--;
    } 

    public int GetBugCount()
    {
        return bugCount;
    }

    public void SetGameEnd(bool b)
    {
        isGameEnd = b;
    }
}
