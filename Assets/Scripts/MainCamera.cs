using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{

    Vector3 startPosition = new Vector3(0, 0, 0);
    Vector2 startMousePosition { get; set; }
    bool startMove = false;
    float velocity=0.1f;
    Vector2[] fieldVector = new Vector2[2];
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        SetStartPosition();
        EndMove();
        Scroll();
        Clamp();
    }
    //有効になった時
    void OnEnable()
    {
        //初期位置
        transform.position = new Vector2(0, 0);
        startMousePosition = new Vector2(0, 0);
        startPosition = new Vector2(0, 0);
        Transform field = gameObject.transform.parent.transform.FindChild("Field");
        fieldVector[0].x = field.GetComponent<MeshFilter>().mesh.vertices[3].x * field.localScale.x;
        fieldVector[0].y = field.GetComponent<MeshFilter>().mesh.vertices[3].y * field.localScale.y;
        fieldVector[1].x = field.GetComponent<MeshFilter>().mesh.vertices[2].x * field.localScale.x;
        fieldVector[1].y = field.GetComponent<MeshFilter>().mesh.vertices[2].y * field.localScale.y;
    }
    void SetStartPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //入力位置をスタートにする
            Vector2 worldPoint2d = Input.mousePosition;
            startMousePosition = worldPoint2d;
            startPosition = transform.position;
            startMove = true;
        }
    }
    void MoveCamera()
    {
        //
        if (Input.GetMouseButton(0) && startMove == true)
        {
            Vector2 moveMousePosition = Input.mousePosition;
            Vector3 vectMouse = moveMousePosition - startMousePosition;
            //微小な動きはカット
            if (vectMouse.magnitude < 5) return;

            transform.position += vectMouse.normalized * velocity;

        }
    }
    void EndMove()
    {
        if (Input.GetMouseButtonUp(0))
        {
            startMove = false;
        }

    }
    void Scroll()
    {
        float vect=Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize -= vect;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 3f, 10f);
    }
    void Clamp()
    {

        // カメラの座標を取得
        Vector2 pos = transform.position;
        // カメラの位置が画面内に収まるように制限をかける
        pos.x = Mathf.Clamp(pos.x, fieldVector[0].x, fieldVector[1].x);
        pos.y = Mathf.Clamp(pos.y, fieldVector[1].y, fieldVector[0].y);
        // 制限をかけた値をカメラの位置とする
        transform.position = pos;
    }
}
