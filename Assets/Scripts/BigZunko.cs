using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class BigZunko : MonoBehaviour {

	// Use this for initialization
	//
    //Text voiceText;
    Text numberText;
    int zunkoCount;
    int stageCount;
    GameObject clearPanel;
    void Start () {
        //voiceText = gameObject.transform.FindChild("Voice").GetComponent<Text>();
        numberText = gameObject.transform.FindChild("NumberText").GetComponent<Text>();
        clearPanel=gameObject.transform.FindChild("Clear").gameObject;
        stageCount = GameObject.FindGameObjectsWithTag("StageBuilding").ToList().Count;
	}
	
	// Update is called once per frame
	void Update () {
        zunkoCount = GameObject.FindGameObjectsWithTag("FieldScene").ToList().Sum(z => z.GetComponent<ZunkoManager>().GetZunkoCount());
        SetText();
        SetClear();
	}
    public void ChangeToSelectScene()
    {
        GameObject.FindGameObjectsWithTag("FieldScene")
            .ToList()
            .ForEach(f => f.GetComponent<FieldScene>().ChangeToSelectScene());
    }
    void SetText()
    {
        numberText.text = "東北のずんこ: " + zunkoCount + "ずん";
    }
    //全てクリア
    void SetClear()
    {
        if (clearPanel.activeSelf == false)
        {
            if (GameObject.FindGameObjectsWithTag("FieldScene").Where(z => z.GetComponent<BuildingManager>().IsAllCreator() == true).ToList().Count == stageCount)
            {
                clearPanel.SetActive(true);

            }
        }
    }



}
