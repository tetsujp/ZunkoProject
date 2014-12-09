using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BuildingManager : MonoBehaviour {

    //Buildingリスト
    List<GameObject> buildingList = new List<GameObject>();
    //ずん子生成用
    ZunkoManager zunkoManager;
    // Use this for initialization
	void Start () {

        zunkoManager = gameObject.GetComponent<ZunkoManager>();
        foreach(Transform child in transform){
            if (child.tag == "FieldBuilding")
            {
                buildingList.Add(child.gameObject);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        SetCreateState();

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

}
