using UnityEngine;
using System.Collections;

public class Building: MonoBehaviour {

    static float createSpace=1f;//Creatorからずれる最大値

    public GameObject zunko;
    public float createInterval=3f;//生成間隔   
    FieldScene fieldScene;
    ZunkoController zunkoController;

    static float INITHP = 100f;
    public float HP { get; set; }
    public float initHP { get; set; }
    public Sprite creatorSprite;
    // Use this for initialization
	void Start () {
        //fieldScene = GameObject.FindGameObjectWithTag("FieldManager").GetComponent<FieldManager>();
        //親から検索
        fieldScene = gameObject.transform.parent.gameObject.GetComponent<FieldScene>();
        zunkoController = gameObject.transform.parent.gameObject.GetComponent<ZunkoController>();
        //コルーチン処理
        //StartCoroutine("Create");
	
	}
	
	// Update is called once per frame
	void Update () {
	

	}

    void Damage(float val)
    {
        HP -= val;
        //HPが0の時ずん子を作るようになる
        if (HP <= 0)
        {
            //スプライト変更
            gameObject.GetComponent<SpriteRenderer>().sprite = creatorSprite;
            //ずん子生成開始
            StartCoroutine("Create");
            //当たり判定を消す
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    public bool isCreator() { return HP <= 0; }

    IEnumerator Create()
    {
        while (true)
        {
            if (zunkoController.isCreatable())
            {
                //Creatorから少しずれた位置で生成
                Vector2 startPosition = new Vector2(transform.position.x + Random.value * createSpace * 2 - createSpace, transform.position.y + Random.value * createSpace * 2 - createSpace);
                zunkoController.AddZunkoList((GameObject)Instantiate(zunko, startPosition, transform.rotation));
            }
                yield return new WaitForSeconds(createInterval);
        }
    }



}
