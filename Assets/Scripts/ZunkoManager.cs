using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
//ずん子管理用
//マウス操作も管理
public class ZunkoManager : MonoBehaviour
{

    //ずん子の最大数
    readonly static int MAX_ZUNKO_COUNT = 150;

    //現在Activeなフィールドか
    public bool isActive=true;
    List<GameObject> zunkoList = new List<GameObject>();
    BuildingManager buildingManager;

    //アクティブ時の操作用の変数
    bool isNowChange = false;
    bool isSet = false;

    Text zunkoNumberText;
    public bool isCreatable()
    {
        return MAX_ZUNKO_COUNT > zunkoList.Count;
    }

    public void AddZunkoList(GameObject zunko)
    {
        zunkoList.Add(zunko);
    }

    public void RemoveZunkoList(GameObject zunko)
    {
        zunkoList.Remove(zunko);
    }
    // Use this for initialization
    void Start()
    {
        buildingManager = gameObject.GetComponent<BuildingManager>();
        zunkoNumberText = transform.Find("Canvas/ZunkoNumber").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isActive)
        {
            UpdateControl();
            SetZunkoNumberText();
        }
    }

    void UpdateControl()
    {
        UpdateSelect();
        CancelSelect();
        Move();

    }


    void UpdateSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isNowChange = false;
            //    //マウスが乗っかったもののみ選択を反転
            //    //同じ位置にいる場合全てが選択
            Vector2 worldPoint2d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var query = zunkoList.Where(z => z.collider2D.OverlapPoint(worldPoint2d) == true&&z.GetComponent<ChibiZunko>().IsNowAttack()==false)
                .ToList();
            query.ForEach(z => z.GetComponent<ChibiZunko>().ReverseSelect());
            if (query.Count != 0) isNowChange = true;
        }
    }
    //全ての選択を解除
    void CancelSelect()
    {
        if (Input.GetMouseButtonDown(1))
        {
            zunkoList.ToList().ForEach(p => p.GetComponent<ChibiZunko>().SetSelect(false));
            isSet = false;
        }
    }
    //指定した位置に移動
    void Move()
    {
        if (Input.GetMouseButtonDown(0)&&isNowChange==false)
        {
            Vector2 worldPoint2d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            zunkoList.Where(p => p.GetComponent<ChibiZunko>().selected == true)
                .ToList()
                .ForEach(z => z.GetComponent<ChibiZunko>().Chase(worldPoint2d));
        }
    }
    public int GetZunkoCount()
    {
        return zunkoList.Count;
    }
    void SetZunkoNumberText()
    {
        zunkoNumberText.text = "地域のずんこ: " + GetZunkoCount() + "ずん";
    }
}
