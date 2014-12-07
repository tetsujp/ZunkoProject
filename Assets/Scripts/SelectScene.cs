using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SelectScene : MonoBehaviour {

    //FieldSceneプレハブ
    public GameObject[] preField;
    //fieldSceneのリスト
    //List<GameObject> fieldList = new List<GameObject>();
    Dictionary<string, GameObject> fieldList = new Dictionary<string, GameObject>();
    //public GameObject[] StageBuilding;

    //Stageのリスト
    Dictionary<string, GameObject> stageList = new Dictionary<string, GameObject>();

    
	// Use this for initialization
	void Start () {

        //ステージデータ取得
        foreach (var stage in GameObject.FindGameObjectsWithTag("StageBuilding"))
        {
            stageList.Add(stage.GetComponent<StageBuilding>().name,stage);
        }

	}
	
	// Update is called once per frame
	void Update () {
	}

    //ステージから呼ばれ、シーンの切り替えを行う
    //セレクトシーンを非アクティブ、フィールドシーンをアクティブにする
    public void ChangeToFieldScene(string stageName)
    {
        //自分のの非アクティブ化
        gameObject.SetActive(false);

        //なければ作成する
        if (!fieldList.ContainsKey(stageName))
        {
            foreach(var field in preField){
                if(field.GetComponent<FieldScene>().fieldName==stageName){
                    fieldList.Add(stageName,(GameObject)Instantiate(field));
                }
            }
        }

        //フィールドシーンのアクティブ化
        fieldList[stageName].SetActive(true);
    }
}
