using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pole : MonoBehaviour
{
    public int realValue;
    public Board board;
    public bool locked = false;
    bool clicked = false;

    public void Click()
    {
        Debug.Log(board.deska.IndexOf(this));
        foreach (Pole p in board.deska) p.clicked = false;
        clicked = true;
    }
    private void Update()
    {
        if (locked || !clicked) return;

        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(vKey))
            {
                int key = (int)vKey-256;
                if (key < 1 || key > 9)
                {
                    clicked = false;
                    return;
                }
                if (board.CheckForNumber(board.deska.IndexOf(this), key))
                {
                    realValue = key;
                    transform.GetComponentInChildren<Text>().text = key.ToString();
                    clicked = false;
                }
            }
        }
    }
}
