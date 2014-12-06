using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SelectManager : MonoBehaviour {


    List<GameObject> fieldList = new List<GameObject>();
    //public GameObject[] StageBuilding;
    
    //シーンデータ
    public GameObject[] fieldScene;
    public GameObject selectScene;
	// Use this for initialization
	void Start () {

        ////ステージデータ作成
        //foreach (var s in StageBuilding)
        //{
        //    fieldList.Add((GameObject)Instantiate(s));
        //}
	
        
	}
	
	// Update is called once per frame
	void Update () {
	}

    //ステージから呼ばれ、シーンの切り替えを行う
    //セレクトシーンを非アクティブ、フィールドシーンをアクティブにする
    public void ChangeToFieldScene(string stageName)
    {
        //セレクトシーンの非アクティブ化
        selectScene.SetActive(false);

        //フィールドシーンのアクティブ化
        foreach (var f in fieldScene)
        {
            if (f.GetComponent<FieldScene>().fieldName == stageName)
            {
                f.SetActive(true);
                break;
            }
        }



    }
}
