using UnityEngine;
using System.Collections;
using System.Linq;

public class BigZunko : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void ChangeToSelectScene()
    {
        GameObject.FindGameObjectsWithTag("FieldScene")
            .ToList()
            .ForEach(f => f.GetComponent<FieldScene>().ChangeToSelectScene());
    } 

}
