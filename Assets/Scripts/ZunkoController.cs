using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//ずん子を動かす
//アクティブになっているFieldのみに適応
public class ZunkoController : MonoBehaviour
{

    ZunkoManager zunkoManager;
    bool isNowChange = false;
    bool isSet = false;
    // Use this for initialization
    void Start()
    {
        zunkoManager = gameObject.GetComponent<ZunkoManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Select();
    }

    //マウス操作で動かす
    void Select()
    {
        //選択
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint2d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isNowChange = zunkoManager.SetSelect(worldPoint2d);
            if (isNowChange == true) isSet = true;
            else if (isNowChange == false && isSet == true)
            {
                Move();
            }
        }
        //全て選択から外す
        if (Input.GetMouseButtonDown(1) && isSet == true)
        {
            zunkoManager.CancelSelect();
            isSet = false;
        }
    }
    void Move()
    {
        //移動先を選択し移動
            Vector2 worldPoint2d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            //zunkoManager.Move(Input.mousePosition);
            zunkoManager.Move(worldPoint2d);    
        isSet = false;
    }

}
