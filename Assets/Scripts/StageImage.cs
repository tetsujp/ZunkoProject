using UnityEngine;
using System.Collections;

public class StageImage : MonoBehaviour {

	// Use this for initialization
    Vector3 startPosition;
    Vector2 firstPosition;
    Vector2 startMousePosition { get; set; }
    bool startMove = false;
    float velocity = 10f;
	void Start () {
        firstPosition = transform.position;
        startMousePosition = new Vector2(0, 0);
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        Move();
        SetStartPosition();
        EndMove();
        Clamp();
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
    void Move()
    {
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
    void Clamp()
    {

        // カメラの座標を取得
        Vector2 pos = transform.position;
        // カメラの位置が画面内に収まるように制限をかける
        pos.x = Mathf.Clamp(pos.x, firstPosition.x - 100f, firstPosition.x + 100f);
        pos.y = Mathf.Clamp(pos.y, firstPosition.y - 810f, firstPosition.y + 320f);
        // 制限をかけた値をカメラの位置とする
        transform.position = pos;
    }
}
