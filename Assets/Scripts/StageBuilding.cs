using UnityEngine;
using System.Collections;

//セレクト時に表示される
public class StageBuilding : MonoBehaviour {

    //ステージ名
    public string stageName;
    //ステージ画像名
    //public Sprite imageSprite;


    SelectManager selectManager;
	// Use this for initialization
	void Start () {
        selectManager = GameObject.FindGameObjectWithTag("SelectManager").GetComponent<SelectManager>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void ChangeScene()
    {
        //フィールドシーンへ
        selectManager.ChangeToFieldScene(stageName);
    }

}
