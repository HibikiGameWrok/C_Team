using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerControllerStatic
{
    public struct PlayerControllerStruct
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コンパス情報
        // 方向の量
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public Vector2 m_compassPower;
        public float m_compassAngle;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コンパス情報
        // 動いたか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public bool m_compassMove;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // スティック情報
        // 動きの量
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public Vector2 m_stickPower;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // スティック情報
        // 動いたか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public bool m_stickMove;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // スティック情報
        // 動きの方向
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public float m_stickAngle;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃コード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public bool m_flagAttackKeyTrigger;
        public bool m_flagAttackKeyPushing;
        public bool m_flagAttackKey;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ジャンプコード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public bool m_flagJumpKeyTrigger;
        public bool m_flagJumpKeyPushing;
        public bool m_flagJumpKey;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 踏みつけコード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public bool m_flagTrampleKeyTrigger;
        public bool m_flagTrampleKeyPushing;
        public bool m_flagTrampleKey;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // スタートコード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public bool m_flagStartKeyTrigger;
        public bool m_flagStartKeyPushing;
        public bool m_flagStartKey;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 回復コード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public bool m_flagRecoveryKeyTrigger;
        public bool m_flagRecoveryKeyPushing;
        public bool m_flagRecoveryKey;


        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドリセット
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public void ResetCommond()
        {
            m_stickPower = Vector2.zero;
            m_stickAngle = 0.0f;
            m_stickMove = false;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 攻撃コード
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_flagAttackKeyTrigger = false;
            m_flagAttackKeyPushing = false;
            m_flagAttackKey = false;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ジャンプコード
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_flagJumpKeyTrigger = false;
            m_flagJumpKeyPushing = false;
            m_flagJumpKey = false;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 踏みつけコード
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_flagTrampleKeyTrigger = false;
            m_flagTrampleKeyPushing = false;
            m_flagTrampleKey = false;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // スタートコード
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_flagStartKeyTrigger = false;
            m_flagStartKeyPushing = false;
            m_flagStartKey = false;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 回復コード
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_flagRecoveryKeyTrigger = false;
            m_flagRecoveryKeyPushing = false;
            m_flagRecoveryKey = false;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 取得するためリセット
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public void UpdateCommondReStart()
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // コンパス情報
            // 方向の量
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_compassPower = Vector2.zero;
            m_compassAngle = 0.0f;
            m_compassMove = false;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // スティック情報
            // 動きの量
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_stickPower = Vector2.zero;
            m_stickAngle = 0.0f;
            m_stickMove = false;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 攻撃コード
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_flagAttackKey = false;
            m_flagAttackKeyTrigger = false;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ジャンプコード
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_flagJumpKey = false;
            m_flagJumpKeyTrigger = false;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 踏みつけコード
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_flagTrampleKey = false;
            m_flagTrampleKeyTrigger = false;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // スタートコード
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_flagStartKey = false;
            m_flagStartKeyTrigger = false;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 回復コード
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_flagRecoveryKey = false;
            m_flagRecoveryKeyTrigger = false;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドリセット
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public void UpdateCommondTrigger()
        {

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // スティック情報登録
            //*|***|***|***|***|***|***|***|***|***|***|***|
            MakeStick();

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 攻撃コード
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_flagAttackKey)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 押している
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (!m_flagAttackKeyPushing)
                {
                    m_flagAttackKeyTrigger = true;
                }
                m_flagAttackKeyPushing = true;
            }
            else
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 押していない
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_flagAttackKeyPushing = false;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ジャンプコード
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_flagJumpKey)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 押している
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (!m_flagJumpKeyPushing)
                {
                    m_flagJumpKeyTrigger = true;
                }
                m_flagJumpKeyPushing = true;
            }
            else
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 押していない
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_flagJumpKeyPushing = false;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 踏みつけコード
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_flagTrampleKey)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 押している
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (!m_flagTrampleKeyPushing)
                {
                    m_flagTrampleKeyTrigger = true;
                }
                m_flagTrampleKeyPushing = true;
            }
            else
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 押していない
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_flagTrampleKeyPushing = false;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // スタートコード
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_flagStartKey)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 押している
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (!m_flagStartKeyPushing)
                {
                    m_flagStartKeyTrigger = true;
                }
                m_flagStartKeyPushing = true;
            }
            else
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 押していない
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_flagStartKeyPushing = false;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 回復コード
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_flagRecoveryKey)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 押している
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (!m_flagRecoveryKeyPushing)
                {
                    m_flagRecoveryKeyTrigger = true;
                }
                m_flagRecoveryKeyPushing = true;
            }
            else
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 押していない
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_flagRecoveryKeyPushing = false;
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // スティック情報登録
        //*|***|***|***|***|***|***|***|***|***|***|***|
        private void MakeStick()
        {
            m_stickPower.x = ChangeData.Among(m_stickPower.x, -1.0f, 1.0f);
            m_stickPower.y = ChangeData.Among(m_stickPower.y, -1.0f, 1.0f);
            if (MyCalculator.LongVector2(m_stickPower) != 0.0f)
            {
                m_stickMove = true;
                m_stickAngle = ChangeData.Vector2ToAngleDeg(m_stickPower);
            }
        } 
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // スティック情報登録
        //*|***|***|***|***|***|***|***|***|***|***|***|
        private void MakeCompass()
        {
            m_compassPower.x = ChangeData.Among(m_compassPower.x, -1.0f, 1.0f);
            m_compassPower.y = ChangeData.Among(m_compassPower.y, -1.0f, 1.0f);
            if (MyCalculator.LongVector2(m_compassPower) != 0.0f)
            {
                m_compassMove = true;
                m_compassAngle = ChangeData.Vector2ToAngleDeg(m_compassPower);
            }
        }
    }
}
