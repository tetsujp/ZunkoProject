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

    static float INITHP = 10f;

    public float power { get; private set; }
    public bool selected { get; private set; }
    public bool leftFace { get; private set; }
    public bool alive { get; private set; }
    public Vector2 target { get; private set; }

    public float HP { get; set; }
    public float initHP;
    float velocity = 0.1f;


    ZunkoManager zunkoManager;
    ZunkoController zunkoController;

    AnimatorStateInfo stateInfo;

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
        animator = GetComponent<Animator>();
        zunkoManager = gameObject.transform.parent.gameObject.GetComponent<ZunkoManager>();
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
        if (stateInfo.nameHash == ANI_CHASE)
        {
            //移動
            Vector3 toVect = new Vector3(target.x - transform.position.x, target.y - transform.position.y, 0).normalized;
            transform.position += toVect * velocity;
            //十分近づいたら止まる
            //targetが当たり判定に存在
            if (GetComponent<CircleCollider2D>().OverlapPoint(target))
            {
                UnsetChasing();
            }
        }
    }



    //ZunkoManagerが行う内容
    public void Chase(Vector3 targetPosition)
    {
        //if (stateInfo.nameHash == ANI_CHASE) return;

        this.target = targetPosition;
        SetSelect(false);
        animator.SetTrigger("chase");
    }

    public void UnsetChasing()
    {
        if (stateInfo.nameHash == ANI_CHASE)
        {
            target = gameObject.transform.position;
            animator.SetTrigger("wait");
        }
    }

    public void StartAttack()
    {
        //if (stateInfo.nameHash == ANI_ATTACK
        //        || stateInfo.nameHash == ANI_REST)
        //    return;

        this.target = targetPosition;
        //state = StateName.Attack;
        animator.SetTrigger("Attack");
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

    public void Walk()
    {

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

    }

    public void setDirection(bool left)
    {
        leftFace = left;
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
    public void Damage(float val)
    {
        HP -= val;
    }



    //public boolean isAttacking()
    //{
    //    return state.getStateName() == ChibiZunkoState.StateName.ATTACK &&
    //            state.counter == 45;
    //}
    //public boolean isSpawning()
    //{
    //    return state.getStateName() == ChibiZunkoState.StateName.SPAWN &&
    //            state.counter == 1;
    //}

    //private static RectF dr = new RectF();
    //public void draw(Canvas canvas, float baseX, float baseY)
    //{
    //    Bitmap image = state.getImage(ld);

    //    dr.left = baseX + pos.x;
    //    dr.top = baseY + pos.y;
    //    dr.right = dr.left + image.getWidth() * power;
    //    dr.bottom = dr.top + image.getHeight() * power;
    //    canvas.drawBitmap(image, null, dr, null);
    //}
}
