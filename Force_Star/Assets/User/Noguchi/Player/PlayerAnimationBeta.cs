using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationBeta : MonoBehaviour
{

    Animator anim;                  // アニメーター
    private bool beforeGroundFlag;  // 直前フレームの接地判断


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        beforeGroundFlag = this.transform.parent.GetComponent<PlayerControllerHattri>().GetJumpFlag();
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rig2d = this.transform.parent.GetComponent<Rigidbody2D>();                          // これのRigid2D
        bool groundFlag = this.transform.parent.GetComponent<PlayerControllerHattri>().GetJumpFlag();         // 接地判断
        bool punchFlag = GameObject.Find("AttackBoal").GetComponent<PunchController>().GetPunchFlag();  // パンチ判断

        anim.SetInteger("Speed", (int)rig2d.velocity.x);    // 移動速度のデータを送る

        if(!groundFlag && beforeGroundFlag)
            anim.SetTrigger("Jump");                        // ジャンプ瞬間のトリガーを送る

        if (groundFlag && !beforeGroundFlag)
            anim.SetTrigger("Ground");                      // 着地の瞬間のトリガーを送る

        anim.SetBool("OnGround", groundFlag);               // 接地判定のデータを送る

        anim.SetBool("Punch", punchFlag);                   // パンチ判定のデータを送る
    }
}
