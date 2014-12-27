using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class FieldScene : MonoBehaviour {

    public string fieldName;

    Dictionary<string, string> filedDic = new Dictionary<string, string>(){
        {"Sendai","仙台"},{"Matsushima","松島"},{"Naruko","鳴子"},
        {"Hukushima","福島"},
        {"Akita","秋田"},
        {"Iwate","岩手"},
        {"Yamagata","山形"},
        {"Aomori","青森"},
    };
    //フィールド名

    //フィールド上の建物のリスト
    ZunkoManager zunkoManager;
    BuildingManager buildingManager;
    GameObject UI;
    // Use this for initialization
    void Start()
    {
        zunkoManager = GetComponent<ZunkoManager>();
        buildingManager = GetComponent<BuildingManager>();
        UI = transform.Find("Canvas").gameObject;
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Init()
    {
        MessageManager.ClearMessage();
        MessageManager.AddMessage(filedDic[fieldName] + "に入りました");
        MessageManager.AddMessage("ずんだを広めていきましょう");
        MessageManager.StartVoice();
    }

    //再開
    public void Resume()
    {
        zunkoManager.isActive=true;
        //メッシュ再開
        GetComponentsInChildren<Renderer>().ToList().ForEach(z => z.gameObject.GetComponent<Renderer>().enabled = true);

        //カメラアクティブ
        gameObject.transform.FindChild("Main Camera").gameObject.SetActive(true);

        UI.SetActive(true);

        Init();
    }

    //セレクト画面へ
    public void ChangeToSelectScene()
    {
        //zunkoManagerのInput停止
        zunkoManager.isActive=false;
        //全てのメッシュ停止
        GetComponentsInChildren<Renderer>().ToList().ForEach(z => z.gameObject.GetComponent<Renderer>().enabled = false);

        //カメラ非アクティブ
        gameObject.transform.FindChild("Main Camera").gameObject.SetActive(false);
        
        //UI非アクティブ
        UI.SetActive(false);

        //メッセージ削除
        MessageManager.ClearMessage();
        MessageManager.StopVoice();
        MessageManager.AddMessage("どこにずんだを広めますか？");

    }

    



}
