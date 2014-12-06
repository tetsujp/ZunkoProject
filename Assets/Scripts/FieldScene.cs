using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FieldScene : MonoBehaviour {

    public string fieldName;

    //フィールド名

    //フィールド上の建物のリスト
    List<GameObject> buildingList = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        //buildingの検索
        foreach (Transform child in transform)
        {
            if (child.tag == "Building")
            {
                //
                buildingList.Add(child.gameObject);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }


}
