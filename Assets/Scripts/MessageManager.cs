using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class MessageManager : MonoBehaviour
{

    // Use this for initialization
    static Text message;
    static int line = 5;
    static List<string> nowText = new List<string>();
    private static bool startFlag = false;
    float createInterval = 10f;
    List<string> textPool = new List<string>();
    //改行数
    void Start()
    {
        message = GameObject.FindGameObjectWithTag("Voice").GetComponent<Text>();
        textPool = SetInitText();
        StartCoroutine("Voice");
        MessageManager.AddMessage("どこにずんだを広めますか？");
    }

    // Update is called once per frame
    void Update()
    {

    }
    //追加
    public static void AddMessage(string m)
    {
        if (nowText.Count == line)
        {
            //先頭削除
            nowText.RemoveAt(0);
        }
        nowText.Add(m);
        message.text = string.Empty;
        foreach (var str in nowText)
        {
            message.text += str + "\n";
        }
    }

    //全消し
    public static void ClearMessage()
    {
        message.text = "";
        nowText.Clear();
    }
    public static void StartVoice()
    {
        startFlag = true;
    }
    public static void StopVoice()
    {
        startFlag = false;
    }
    //自動で話す
    IEnumerator Voice()
    {
        while (true)
        {
            if (startFlag == true)
            {
                AddMessage(textPool[Random.Range(0, textPool.Count - 1)]);
            }
            yield return new WaitForSeconds(createInterval);
        }
    }
    List<string> SetInitText()
    {
        List<string> temp = new List<string>();
        temp.Add("ずんだ餅にしちゃいます");
        temp.Add("17歳の高校2年生です");
        temp.Add("６月５日ボカロ発売！");
        temp.Add("姉のイタコと妹のきりたんもいます");
        temp.Add("私を使ってください！無料です");
        temp.Add("ずんだアロー！！！！（ずんだテロ）");
        temp.Add("夢は秋葉原にずんだカフェを作ること");
        temp.Add("東北ずん子です。応援してください");
        temp.Add("(」・ω・)」ずん!(/・ω・)/だー");
        temp.Add( "＼（○ず・ω・だ○）／");
        temp.Add("姉妹のUTAU音源も出ます");
        temp.Add("3Dモデルも無料なんです");
        temp.Add("アプリ募集中です");
        temp.Add("UNITY-CHAN kawaii");
        temp.Add("ずんだ餅をどうぞ（強制）");
        temp.Add("(今日もずんだ弁当デース…☆)");
        temp.Add("我はずんだの不可視なる暗黒の使者");
        temp.Add("そういうのはこっそりやるのものです");
        temp.Add("そういうのはこっそりやるのものです");
        temp.Add("CV:佐藤聡美……、CV:佐藤聡美!!!111");
        temp.Add("ごく普通の高校生です(ﾆｺｯ)");
        return temp;



    }
}
