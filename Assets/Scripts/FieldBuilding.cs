using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FieldBuilding: MonoBehaviour {

    public float createSpace=1f;//Creatorからずれる最大値

    public GameObject preZunko;
    ZunkoManager zunkoManager;
    //public bool isBuildingState=false; 
    bool isCreatable;//ずん子が最大数に達したか
    [System.NonSerialized]public float createInterval=1f;//生成間隔
    [System.NonSerialized]
    public float nowHP;
    public float initHP;
    public Sprite creatorSprite;

    //攻撃しているずん子リスト
    //List<GameObject> targeted=new List<GameObject>();
    // Use this for initialization
	void Start () {
        //Creator画像のロード
        //creatorSprite = Resources.Load<Sprite>("cz_zunda");
        //preZunko = Resources.Load<GameObject>("ChibiZunko");
        zunkoManager = transform.parent.gameObject.GetComponent<ZunkoManager>();
        //preZunko = (GameObject)Resources.Load("ChibiZunko");
        nowHP = initHP;
        isCreatable = true;
        if (isCreator())
        {
            SetBuildingState();
        }
    }
	
	// Update is called once per frame
	void Update () {
	

	}
    
    //zunkoから呼ばれる
    public void Damage(float val)
    {
        //既にCreatorならダメージ処理なし
        if (isCreator()) return;
        nowHP -= val;
        //HPが0の時ずん子を作るようになる
        if (nowHP <= 0)
        {
            SetBuildingState();
        }
    }
    void SetBuildingState()
    {
            //スプライト変更
            gameObject.GetComponent<SpriteRenderer>().sprite = creatorSprite;
            //ずん子生成開始
            StartCoroutine("Create");
            //当たり判定を消す
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
}

    public bool isCreator() { return nowHP <= 0; }
    public void SetCreatable(bool f) { isCreatable = f; }

    IEnumerator Create()
    {
        while (true)
        {
            if (isCreatable)
            {
                //Creatorから少しずれた位置で生成
                Vector2 startPosition = new Vector2(transform.position.x + Random.value * createSpace * 2 - createSpace, transform.position.y + Random.value * createSpace * 2 - createSpace);
                GameObject newZunko=(GameObject)Instantiate(preZunko, startPosition, transform.rotation);
                newZunko.transform.parent = gameObject.transform.parent;
                zunkoManager.AddZunkoList(newZunko);
            }
                yield return new WaitForSeconds(createInterval);
        }
    }

}
