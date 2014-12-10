using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class FieldScene : MonoBehaviour {

    public string fieldName;

    
    //フィールド名

    //フィールド上の建物のリスト
    ZunkoManager zunkoManager;
    BuildingManager buildingManager;
    // Use this for initialization
    void Start()
    {
        zunkoManager = GetComponent<ZunkoManager>();
        buildingManager = GetComponent<BuildingManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //再開
    public void Resume()
    {
        zunkoManager.isActive=true;
        //メッシュ再開
        GetComponentsInChildren<Renderer>().ToList().ForEach(z => z.gameObject.renderer.enabled = true);
        //カメラアクティブ
        gameObject.transform.FindChild("Main Camera").gameObject.SetActive(true);
    }

    //セレクト画面へ
    public void ChangeToSelectScene()
    {
        //zunkoManagerのInput停止
        zunkoManager.isActive=false;
        //全てのメッシュ停止
        GetComponentsInChildren<Renderer>().ToList().ForEach(z=>z.gameObject.renderer.enabled = false);

        //カメラ非アクティブ
        gameObject.transform.FindChild("Main Camera").gameObject.SetActive(false);

    }

    



}
