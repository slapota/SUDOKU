using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public GameObject skupina;
    public List<Pole> deska = new List<Pole>();
    public List<Transform> groups = new List<Transform>();

    private void Start()
    {
        /*foreach (Transform t in groups)
        {
            Transform[] temp = t.GetComponentsInChildren<Transform>();

            foreach (Transform t2 in temp)
            {
                deska.Add(t2.GetComponent<Pole>());
            }
        }*/

        SetNumbers();
    }
    void SetNumbers()
    {
        foreach(Pole p in deska)
        {
            p.board = this;
            p.realValue = 0;
            //p.transform.GetComponentInChildren<Text>().text = deska.IndexOf(p).ToString();
        }

        int end = Random.Range(40, 60);
        for(int i = 0; i < end; i++)
        {
            int t = Random.Range(0, deska.Count);
            int g = Random.Range(1, 10);
            if(CheckForNumber(t, g))
            {
                deska[t].realValue = g;
                deska[t].transform.GetComponentInChildren<Text>().text = g.ToString();
            }
        }
    }
    IEnumerator RandNum(List<int> temp, int index)
    {
        yield return new WaitForSeconds(0.01f);

        int rnd = temp[Random.Range(0, temp.Count)];
        if (CheckForNumber(index, rnd))
        {
            deska[index].realValue = rnd;
            deska[index].GetComponentInChildren<Text>().text = rnd.ToString();
            temp.Remove(rnd);
        }
        else
        {
            StartCoroutine(RandNum(temp, index));
        }
    }
    public bool CheckForNumber(int target, int guess)
    {
        Pole[] temp = deska[target].transform.parent.GetComponentsInChildren<Pole>();
        for(int i = 0; i <temp.Length; i++)
        {
            if (target % 9 == i) continue;

            if (temp[i].realValue == guess) return false;
        }
        for(int i = Mathf.FloorToInt(target/9)*9; i < Mathf.FloorToInt(target / 9) * 9 + 9; i++)
        {
            if (i == target) continue;
            if (deska[i].realValue == guess) return false;
        }
        for(int i = target-Mathf.FloorToInt(target/9)*9; i < deska.Count; i+=9)
        {
            if(i == target) continue;
            if (deska[i].realValue == guess) return false;
        }

        return true;
    }
}
