using UnityEngine;
using System.Collections;

public class ChibiZunko : MonoBehaviour
{
    //enum StateName { Awake, Wait, Attack, Walk,Chase,Rest }

    //Animation
    public readonly static int ANI_AWAKE = Animator.StringToHash("Base Layer.awake");
    public readonly static int ANI_WAIT = Animator.StringToHash("Base Layer.wait");
    public readonly static int ANI_ATTACK = Animator.StringToHash("Base Layer.attack");
    public readonly static int ANI_WALK = Animator.StringToHash("Base Layer.walk");
    public readonly static int ANI_CHASE = Animator.StringToHash("Base Layer.chase");
    public readonly static int ANI_REST = Animator.StringToHash("Base Layer.rest");

    Animator animator;

    static float INITHP = 20;
    static float DAMAGE_VALUE = 1f;

    public float power { get; private set; }
    public bool selected { get; private set; }
    public bool leftFace { get; private set; }
    public bool alive { get; private set; }
    public Vector2 target { get; private set; }
    public GameObject targetBuilding{get;private set;}


    public float HP { get; set; }
    public float initHP;
    float[] VELOCITY ={ 1f, 2f };
    float velocity;
    //ランダムで歩く確率
    int walkTiming = 60 * 5;
    //ランダムで歩く最大サイズ
    float walkRange = 3f;

    ZunkoManager zunkoManager;

    //画面端座標
    Vector2[] fieldVector=new Vector2[2];
    //ZunkoController zunkoController;

    [System.NonSerialized]
    public AnimatorStateInfo stateInfo;

    //private ChibiZunkoState state;
    //private ImageLoader ld;
    // Use this for initialization
    void Start()
    {
        power = 1f;
        selected = false;
        leftFace = true;
        //target = null;
        initHP = INITHP;
        HP = initHP;
        target = transform.position;
        targetBuilding = null;
        velocity= VELOCITY[0];
        animator = GetComponent<Animator>();
        zunkoManager = gameObject.transform.parent.gameObject.GetComponent<ZunkoManager>();
        Transform field = gameObject.transform.parent.transform.FindChild("Field");
        fieldVector[0].x = field.GetComponent<MeshFilter>().mesh.vertices[3].x*field.localScale.x;
        fieldVector[0].y = field.GetComponent<MeshFilter>().mesh.vertices[3].y * field.localScale.y;
        fieldVector[1].x = field.GetComponent<MeshFilter>().mesh.vertices[2].x * field.localScale.x;
        fieldVector[1].y = field.GetComponent<MeshFilter>().mesh.vertices[2].y * field.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        Move();
        
    }

    //ずん子が自分で変更する内容
    private void Move()
    {
        Walk();
        if (stateInfo.nameHash == ANI_CHASE || stateInfo.nameHash == ANI_WALK)
        {
            //移動
            Vector3 toVect = new Vector3(target.x - transform.position.x, target.y - transform.position.y, 0).normalized;
            transform.position += toVect * velocity * Time.deltaTime ;
            //移動制限
            //clampがかかったか
            bool isClamp = false;
            // プレイヤーの座標を取得
            Vector2 pos = transform.position;
            // プレイヤーの位置が画面内に収まるように制限をかける
            pos.x = Mathf.Clamp(pos.x, fieldVector[0].x, fieldVector[1].x);
            pos.y = Mathf.Clamp(pos.y, fieldVector[1].y, fieldVector[0].y);
            if (pos != (Vector2)transform.position) isClamp = true;
            // 制限をかけた値をプレイヤーの位置とする
            transform.position = pos;

            //右向き設定
            if (transform.position.x < target.x)
            {
                SetDirection(false);
            }
            else
            {
                SetDirection(true);
            }
            //十分近づいたら止まる
            //targetが当たり判定に存在
            //移動制限がかかる場合も停止
            if (GetComponent<CircleCollider2D>().OverlapPoint(target)||isClamp)
            {
                Stop();
            }
        }
    }



    //ZunkoManagerが行う内容
    public void Chase(Vector3 targetPosition)
    {
        //if (stateInfo.nameHash == ANI_CHASE) return;

        this.target = targetPosition;
        velocity = VELOCITY[1];
        SetSelect(false);
        animator.SetTrigger("chase");
    }

    public void Stop()
    {
        if (stateInfo.nameHash == ANI_CHASE || stateInfo.nameHash == ANI_WALK)
        {
            target = gameObject.transform.position;

            animator.SetTrigger("wait");
        }
    }

    void StartAttack(GameObject target)
    {
        this.target = gameObject.transform.position;
        targetBuilding = target;
        animator.SetTrigger("attack");
    }

    public void EndAttack()
    {
        if (stateInfo.nameHash == ANI_ATTACK)
        {
            target = gameObject.transform.position;
            //state=StateName.Wait;
            animator.SetTrigger("wait");
        }
    }
    public bool IsNowAttack()
    {
        return stateInfo.nameHash == ANI_ATTACK;
    }

    void Walk()
    {
        //ランダムで歩く
        if (stateInfo.nameHash == ANI_WAIT)
        {
            if (Random.Range(0, walkTiming) < 1)
            {
                Vector3 targePosition = gameObject.transform.position;
                targePosition.x = target.x + Random.Range(-walkRange, walkRange);
                targePosition.y = target.y + Random.Range(-walkRange, walkRange);
                this.target = targePosition;
                velocity = VELOCITY[0];
                animator.SetTrigger("walk");
            }
        }

    }

    public void Rest()
    {
        if (stateInfo.nameHash == ANI_ATTACK)
        {
            animator.SetTrigger("rest");
        }
    }
    public void DeleteThis()
    {
        zunkoManager.RemoveZunkoList(gameObject);
        Destroy(gameObject);

    }

    void SetDirection(bool left)
    {
        leftFace = left;
        Vector3 scale = gameObject.transform.localScale;
        if (leftFace)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else
        {
            scale.x = -Mathf.Abs(scale.x);
        }
        gameObject.transform.localScale = scale;
    }
    public void ReverseSelect()
    {
        SetSelect(!selected);
    }
    public void SetSelect(bool flag)
    {
        selected = flag;
        //画像の色変更
        ChangeColor();
    }
    //select時画像色変更
    void ChangeColor()
    {
        //標準
        if (!selected)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0.3f, 0.1f, 1f);
        }
    }
    //attackアニメの最後に呼ばれる
    //1秒に1回
    public void Damage()
    {

        if (stateInfo.nameHash == ANI_ATTACK)
        {
            //Buildingにダメージ
            targetBuilding.GetComponent<FieldBuilding>().Damage(power);
            //Creatorになったら攻撃ストップ
            if (targetBuilding.GetComponent<FieldBuilding>().isCreator())
            {
                EndAttack();
            }
            else
            {

                //自分にダメージ
                HP -= DAMAGE_VALUE;
                if (HP <= 0)
                {
                    Rest();
                }
            }
        }
    }

    bool IsAttackable(){
        return stateInfo.nameHash==ANI_WAIT||stateInfo.nameHash==ANI_WALK||stateInfo.nameHash==ANI_CHASE;
    }

    //当たり検知でBuilding,画面外の当たりを比べる
    void OnTriggerEnter2D(Collider2D collider)
    {

        //当たりが自分のフィールド内
        if (collider.gameObject.transform.parent == gameObject.transform.parent)
        {
                //当たりがBuildingの時
                if (collider.tag == "FieldBuilding")
                {
                    //攻撃可能
                    if (IsAttackable())
                    {
                    //Creatorではない
                    if (!collider.gameObject.GetComponent<FieldBuilding>().isCreator())
                    {
                        //攻撃設定
                        StartAttack(collider.gameObject);
                    }
                }
            }
        }
    }
}
