using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//ずん子を動かす
public class ZunkoController : MonoBehaviour {

    readonly static int MAX_ZUNKO_COUNT = 100;
    //フィールド上のズン子のリスト
    List<GameObject> zunkoList = new List<GameObject>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

	}
    public bool isCreatable()
    {
        return MAX_ZUNKO_COUNT > zunkoList.Count;
    }

    public void AddZunkoList(GameObject zunko)
    {
        zunkoList.Add(zunko);
    }

    public void removeZunkoList(GameObject zunko)
    {
        zunkoList.Remove(zunko);
    }

    //マウス操作で動かす
    void Mover()
    {
        //ボタン入力
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint2d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            foreach (var zunko in zunkoList)
            {
                //すでに選択したずん子を選択で解除
                if (zunko.collider2D.OverlapPoint(worldPoint2d))
                {
                    zunko.GetComponent<ChibiZunko>().Select();
                    //1体だけ選択
                    break;
                }
                
            }
        }
    }
}
