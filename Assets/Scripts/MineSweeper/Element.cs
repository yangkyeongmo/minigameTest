using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Element : MonoBehaviour, IPointerDownHandler {

    public bool mine;

    public Sprite[] emptyTextures;
    public Sprite mineTexture;
    public Sprite markTexture;
    public Sprite mistakeTexture;
    public Sprite defaultTexture;

    private SpriteRenderer sr;
    private EventControl ec;
    private bool marked = false;
    private int x;
    private int y;
    private Vector3 mousePosition;

    // Use this for initialization
    void Awake () {
        sr = GetComponent<SpriteRenderer>();
        ec = GameObject.Find("Grid").GetComponent<EventControl>();
        //Random gerneration of mine
        mine = Random.value < 0.15;
        x = (int)transform.position.x;
        y = (int)transform.position.y;
        GridControl.elements[x, y] = this;
        if (mine)
        {
            ec.MineCountUp();
        }
	}

    public void OnPointerDown(PointerEventData peData)
    {
        if (ec.GetTimer() > 0 && ec.GetBugCount() < ec.maxMineCount)
        {
            if (peData.pointerId == -1)
            {
                OnLeftMouseButtonClicked();
            }
            else if (peData.pointerId == -2)
            {
                OnRightMouseButtonClicked();
            }
            if (GridControl.IsFinished())
            {
                EventControl.isGameEnd = true;
            }
        }
    }

    private void OnLeftMouseButtonClicked()
    {
        if (mine && !marked)
        {
            if(sr.sprite != mistakeTexture)
            {
                if (ec.GetTimer() > 0)
                {
                    ec.TimerModify(-2.0f);
                }
                sr.sprite = mistakeTexture;
                ec.ClickedBugCountUp();
            }
        }
        else if(!marked)
        {
            LoadTexture(GridControl.AdjacentMines(x, y));

            GridControl.FFuncover(x, y, new bool[GridControl.w, GridControl.h]);

            if (GridControl.IsFinished())
            {
                print("you win");
            }
        }
    }

    private void OnRightMouseButtonClicked()
    {
        if(sr.sprite != mistakeTexture)
        {
            if (!marked)
            {
                if (mine)
                {
                    ec.BugCountUp();
                    ec.ClickedBugCountUp();
                }
                sr.sprite = markTexture;
                marked = true;
            }
            else
            {
                if (mine)
                {
                    ec.BugCountDown();
                    ec.ClickedBugCountDown();
                }
                sr.sprite = defaultTexture;
                marked = false;
            }
        }
    }

    public void LoadTexture(int adjacentCount)
    {
        if (mine)
        {
            sr.sprite = mineTexture;
        }
        else
        {
            sr.sprite = emptyTextures[adjacentCount];
        }
    }

    public bool IsCovered()
    {
        return GetComponent<SpriteRenderer>().sprite.name == "mine_button";
    }
}
