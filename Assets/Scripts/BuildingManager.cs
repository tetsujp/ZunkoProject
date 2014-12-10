using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour {

    //Buildingリスト
    List<GameObject> buildingList = new List<GameObject>();
    //ずん子生成用
    ZunkoManager zunkoManager;
    GameObject clearPanel;
    // Use this for initialization
	void Start () {

        zunkoManager = gameObject.GetComponent<ZunkoManager>();
        foreach(Transform child in transform){
            if (child.tag == "FieldBuilding")
            {
                buildingList.Add(child.gameObject);
            }
        }
        clearPanel = transform.Find("Canvas/Zundaka").gameObject;
        
	}
	
	// Update is called once per frame
	void Update () {
        SetCreateState();
        SetClear();
	}
    //最大数に達した
    public void SetCreateState()
    {
        bool state=zunkoManager.isCreatable();
        buildingList.ToList().ForEach(p => p.GetComponent<FieldBuilding>().SetCreatable(state));
        //foreach (var build in buildingList)
        //{
        //    build.GetComponent<FieldBuilding>().SetCreatable(state);
        //}
    }

    public bool IsAllCreator()
    {
        return buildingList.Where(z => z.GetComponent<FieldBuilding>().isCreator() == true).ToList().Count == buildingList.Count;
    }
    void SetClear()
    {
        if (clearPanel.activeSelf == false)
        {
            if (IsAllCreator())
            {
                clearPanel.SetActive(true);
            }
        }
    }


}
