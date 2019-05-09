using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildStar : MonoBehaviour
{

    public GameObject star; // 親
    StarMove starMove;      // /親の動きのスクリプト用


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星のレイヤーに変更
        //*|***|***|***|***|***|***|***|***|***|***|***|
        gameObject.layer = 12;
    }


    // Use this for initialization
    void Start () {
        //star = GameObject.Find("Star");
        starMove = star.GetComponent<StarMove>();
      
    }
	
	// Update is called once per frame
	void Update () {
		if(starMove.hitFlag)
        {
            Collider2D m_ObjectCollider = GetComponent<Collider2D>();
            m_ObjectCollider.isTrigger = true;              //  プレイヤーに当たった時に当たり判定を消す
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Floor"|| other.gameObject.tag == "")   // 床のタグと空いているほうは壁用
        {
            starMove.hitName = transform.name;  // 親に自分の名前を渡す
        }
    }
}
