using UnityEngine;
using System.Collections;

//セレクト時に表示される
public class StageBuilding : MonoBehaviour {

    //ステージ名
    public string stageName;
    //ステージ画像名
    //public Sprite imageSprite;


    SelectScene selectScene;
	// Use this for initialization
	void Start () {
        selectScene = GameObject.FindGameObjectWithTag("SelectScene").GetComponent<SelectScene>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void ChangeScene()
    {
        //フィールドシーンへ
        selectScene.ChangeToFieldScene(stageName);
    }

}
