using UnityEngine;
using System.Collections;

public class ChibiZunko : MonoBehaviour {
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

    public float power{get;private set;}
    public bool selected{get;private set;}
    public bool leftFace{get;private set;}
    public bool alive{get;private set;}
    public FieldObject target{get;private set;}

    public float HP { get; set; }
    public float initHP{get;set;}

    FieldScene fieldScene;
    ZunkoController zunkoController;

    AnimatorStateInfo stateInfo;

    //private ChibiZunkoState state;
    //private ImageLoader ld;
	// Use this for initialization
	void Start () {
        power = 1f;
        selected = false;
        leftFace = true;
        //target = null;
        initHP = INITHP;
        HP = initHP;
        animator = GetComponent<Animator>();
        fieldScene = gameObject.transform.parent.gameObject.GetComponent<FieldScene>();
        zunkoController = gameObject.transform.parent.gameObject.GetComponent<ZunkoController>();
        //stateInfo = StateName.Awake;
        //setCollision(new RectF(0, 0, COL_WID, COL_HEI));

        //state = new ChibiZunkoStateSpawn(this);
	}
	
	// Update is called once per frame
	void Update () {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);

	}


    public void Chase(FieldObject target)
    {
        if (stateInfo.nameHash == ANI_CHASE) return;

        this.target = target;
        animator.SetTrigger("chase");
    }
    public void UnsetChasing()
    {
        if (target != null&&stateInfo.nameHash==ANI_CHASE)
        {
            target = null;
            //state = new ChibiZunkoStateWait(this);
            animator.SetTrigger("wait");
        }
    }

    public void StartAttack(FieldObject target)
    {
        if (stateInfo.nameHash == ANI_ATTACK
                || stateInfo.nameHash == ANI_REST)
            return;

        this.target = target;
        //state = StateName.Attack;
        animator.SetTrigger("Attack");
    }

    public void EndAttack()
    {
        if (target != null&&stateInfo.nameHash==ANI_ATTACK)
        {
            target = null;
            //state=StateName.Wait;
            animator.SetTrigger("wait");
        }
    }

    public void Walk()
    {
        if (stateInfo.nameHash == ANI_WALK)
        {

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
        zunkoController.removeZunkoList(gameObject);

    }

    public void setDirection(bool left)
    {
        leftFace = left;
    }
    public void Select() { selected = !selected; }
    public void Select(bool flag)
    {
        selected = flag;
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
