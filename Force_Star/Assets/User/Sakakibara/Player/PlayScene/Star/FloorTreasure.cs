using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*|***|***|***|***|***|***|***|***|***|***|***|
// プレイヤー倉庫言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;

//*|***|***|***|***|***|***|***|***|***|***|***|
// 音楽
//*|***|***|***|***|***|***|***|***|***|***|***|
using SoundID = SEManager.SoundID;


public class FloorTreasure : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゲーム共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlaySceneDirectorIndex m_directorIndex;
    PlayerDirectorIndex m_playerIndex;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 持っている財宝長
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    public AreaTreasure m_ereaTreasure;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 財宝崩
    //*|***|***|***|***|***|***|***|***|***|***|***|
    bool m_appStar;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たったフラグ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private bool m_armHit;
    [SerializeField]
    private bool m_bodyHit;
    [SerializeField]
    private bool m_headHit;
    [SerializeField]
    private bool m_legHit;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たったフラグEX
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private bool m_armHitFlag;
    private bool m_bodyHitFlag;
    private bool m_headHitFlag;
    private bool m_legHitFlag;
    private Vector3 m_armHitPoint;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たったフラグトリガー
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private bool m_armHitT;
    private bool m_bodyHitT;
    private bool m_headHitT;
    private bool m_legHitT;

    private bool m_armHitR;
    private bool m_bodyHitR;
    private bool m_headHitR;
    private bool m_legHitR;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 時間
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private float m_timeMax;
    private float m_timeLevel;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲーム共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndex = PlaySceneDirectorIndex.GetInstance();
        m_playerIndex = PlayerDirectorIndex.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 財宝崩
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_appStar = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たったフラグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armHit = false;
        m_bodyHit = false;
        m_headHit = false;
        m_legHit = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たったフラグEX
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armHitFlag = false;
        m_bodyHitFlag = false;
        m_headHitFlag = false;
        m_legHitFlag = false;
        m_armHitPoint = Vector3.zero;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たったフラグトリガー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armHitT = false;
        m_bodyHitT = false;
        m_headHitT = false;
        m_legHitT = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 時間
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_timeMax = 75.0f;
        m_timeLevel = 50.0f;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 情報！
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerIndex.SetPointerLastPanel(this);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 情報！
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armHitT = false;
        m_legHit = false;
        m_bodyHit = m_armHitT;
        m_headHit = m_legHit;
        m_legHit = m_bodyHit;
        m_armHitT = m_headHit;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 定期更新データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void FixedUpdate()
    {

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // トリガー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if(m_appStar)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーの腕に当たったか？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (!m_armHitR && m_armHitFlag)
            {
                m_armHitT = true;
            }
            else
            {
                m_armHitT = false;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーの体に当たったか？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (!m_bodyHitR && m_bodyHitFlag)
            {
                m_bodyHitT = true;
            }
            else
            {
                m_bodyHitT = false;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーの頭に当たったか？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (!m_headHitR && m_headHitFlag)
            {
                m_headHitT = true;
            }
            else
            {
                m_headHitT = false;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーの脚に当たったか？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (!m_legHitR && m_legHitFlag)
            {
                m_legHitT = true;
            }
            else
            {
                m_legHitT = false;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーの脚に当たったか？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_legHitT)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 踏み鳴らされる地
                //*|***|***|***|***|***|***|***|***|***|***|***|
                StompingGround();
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーの頭に当たったか？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_headHitT)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // このリンゴを頭に乗せてくれ。
                //*|***|***|***|***|***|***|***|***|***|***|***|
                HeadThrust();
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーの体と腕に当たったか？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_armHit && m_bodyHitT)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 壁に突入する
                //*|***|***|***|***|***|***|***|***|***|***|***|
                DiveInto();
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たったフラグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armHit = false;
        m_bodyHit = false;
        m_headHit = false;
        m_legHit = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たったフラグEX記憶
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armHitR = m_armHitFlag;
        m_bodyHitR = m_bodyHitFlag;
        m_headHitR = m_headHitFlag;
        m_legHitR = m_legHitFlag;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 最後の床は同じ？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (!m_playerIndex.GetPointerLastPanel(this))
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 前の情報取得
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_armHitFlag = m_playerIndex.GetLastPanel_Arm();
            m_bodyHitFlag = m_playerIndex.GetLastPanel_Body();
            m_headHitFlag = m_playerIndex.GetLastPanel_Head();
            m_legHitFlag = m_playerIndex.GetLastPanel_Leg();
            m_armHitPoint = Vector3.zero;
        }
        else
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 当たったフラグE
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_armHitFlag = false;
            m_bodyHitFlag = false;
            m_headHitFlag = false;
            m_legHitFlag = false;
            m_armHitPoint = Vector3.zero;
        }

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 財宝復活
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_appStar = false;


    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤーに当たったら。
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void OnCollisionStay2D(Collision2D other)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーに当たったか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (WarehousePlayer.BoolTagIsPlayer(other.gameObject.tag))
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 当たり判定更新
            //*|***|***|***|***|***|***|***|***|***|***|***|
            UpdateHit(other.gameObject);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 情報！
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_playerIndex.SetPointerLastPanel(this);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤーに当たったら。
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void OnCollisionEnter2D(Collision2D other)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーに当たったか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (WarehousePlayer.BoolTagIsPlayer(other.gameObject.tag))
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 当たり判定更新
            //*|***|***|***|***|***|***|***|***|***|***|***|
            UpdateHit(other.gameObject);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 情報！
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_playerIndex.SetPointerLastPanel(this);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤーに当たったら。
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void OnTriggerStay2D(Collider2D other)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーに当たったか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (WarehousePlayer.BoolTagIsPlayer(other.gameObject.tag))
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 当たり判定更新
            //*|***|***|***|***|***|***|***|***|***|***|***|
            UpdateHit(other.gameObject);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 情報！
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_playerIndex.SetPointerLastPanel(this);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤーに当たったら。
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void OnTriggerEnter2D(Collider2D other)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーに当たったか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (WarehousePlayer.BoolTagIsPlayer(other.gameObject.tag))
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 当たり判定更新
            //*|***|***|***|***|***|***|***|***|***|***|***|
            UpdateHit(other.gameObject);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 情報！
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_playerIndex.SetPointerLastPanel(this);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateHit(GameObject hitObject)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 財宝崩
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_appStar = true;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの脚に当たったか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (hitObject.tag == WarehousePlayer.GetTag_PlayerHitLegParts())
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 地に足のついた
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_playerIndex.GetGroundFlag())
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 当たり判定起動
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_legHit = true;
                m_legHitFlag = true;
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの頭に当たったか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (hitObject.tag == WarehousePlayer.GetTag_PlayerHitHeadParts())
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 当たり判定起動
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_headHit = true;
            m_headHitFlag = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの体に当たったか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (hitObject.tag == WarehousePlayer.GetTag_PlayerHitBodyParts())
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 当たり判定起動
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_bodyHit = true;
            m_bodyHitFlag = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの腕に当たったか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (hitObject.tag == WarehousePlayer.GetTag_PlayerHitArmParts())
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 当たり判定起動
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_armHit = true;
            m_armHitFlag = true;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 当たり判定場所起動
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_armHitPoint = hitObject.transform.position;
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 踏み鳴らされる地
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void StompingGround()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 財宝出現
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //Vector3 point = m_playerIndex.GetPlayerPosition();
        //float scale = m_playerIndex.GetScale();
        Vector3 pointLeft = m_playerIndex.GetPlayerLegLeftPositon();
        Vector3 pointRight = m_playerIndex.GetPlayerLegRightPositon();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 角度設定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 leftAngleV = new Vector2(-1, 1);
        Vector2 rightAngleV = new Vector2(1, 1);
        float leftAngle = ChangeData.Vector2ToAngleDeg(leftAngleV);
        float rightAngle = ChangeData.Vector2ToAngleDeg(rightAngleV);
        float swing = 30.0f;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 位置移動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //pointLeft.x = pointLeft.x - MyCalculator.Division(scale, 2.0f);
        //pointRight.x = pointRight.x + MyCalculator.Division(scale, 2.0f);
        //pointLeft.x = pointLeft.x;
        //pointRight.x = pointRight.x;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 位置転換
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (pointLeft.x > pointRight.x)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 左右が正しいか？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            ChangeData.Change2Data(ref pointLeft.x, ref pointRight.x);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 画面横揺れ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float duration = 3.0f;
        float magnitudeMax = 0.2f;
        float magnitudeMin = 0.05f;
        m_directorIndex.LetsHeightShake(duration, magnitudeMax, magnitudeMin);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndex.PlaySoundEffect(GetFloorSE());
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 財宝崩
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int starNum = AppStarNum();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 財宝あらわる
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (starNum > 0)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星が出る * starNum
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_directorIndex.ApplyStarBounce(pointLeft, leftAngle, swing, 0.3f, 0.1f, m_timeMax, m_timeLevel, starNum);
            m_directorIndex.ApplyStarBounce(pointRight, rightAngle, swing, 0.3f, 0.1f, m_timeMax, m_timeLevel, starNum);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // このリンゴを頭に乗せてくれ。
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void HeadThrust()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 財宝出現
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector3 point = m_playerIndex.GetPlayerHeadPositon();
        float scale = m_playerIndex.GetScale();
        Vector3 pointLeft = point;
        Vector3 pointRight = point;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 角度設定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 leftAngleV = new Vector2(-1, -1);
        Vector2 rightAngleV = new Vector2(1, -1);
        float leftAngle = ChangeData.Vector2ToAngleDeg(leftAngleV);
        float rightAngle = ChangeData.Vector2ToAngleDeg(rightAngleV);
        float swing = 30.0f;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 位置移動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        pointLeft.x = pointLeft.x - scale;
        pointRight.x = pointRight.x + scale;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 画面横揺れ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float duration = 3.0f;
        float magnitudeMax = 0.2f;
        float magnitudeMin = 0.05f;
        m_directorIndex.LetsHeightShake(duration, magnitudeMax, magnitudeMin);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndex.PlaySoundEffect(GetHeadSE());
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 財宝崩
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int starNum = AppStarNum();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 財宝あらわる
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (starNum > 0)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星が出る * starNum
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_directorIndex.ApplyStarBounce(pointLeft, leftAngle, swing, 0.3f, 0.1f, m_timeMax, m_timeLevel, starNum);
            m_directorIndex.ApplyStarBounce(pointRight, rightAngle, swing, 0.3f, 0.1f, m_timeMax, m_timeLevel, starNum);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 壁に突入する
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void DiveInto()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 財宝出現
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector3 pointBody = m_playerIndex.GetPlayerBodyPositon();
        float scale = m_playerIndex.GetScale();
        Vector3 pointUp = pointBody;
        Vector3 pointDown = pointBody;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 角度設定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 upAngleV = new Vector2(0, 1);
        Vector2 downAngleV = new Vector2(0, -1);
        float upAngle = ChangeData.Vector2ToAngleDeg(upAngleV);
        float downAngle = ChangeData.Vector2ToAngleDeg(downAngleV);
        float swing = 30.0f;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 位置移動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        pointUp.y = pointUp.y + scale;
        pointDown.y = pointDown.y - scale;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 位置転換
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector3 thisPoint = m_armHitPoint;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // どちらから接触したか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 左から接触したか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (pointBody.x > thisPoint.x)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 右に放出
            //*|***|***|***|***|***|***|***|***|***|***|***|
            upAngleV = new Vector2(1, 1);
            downAngleV = new Vector2(1, -1);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 右から接触したか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        else if (pointBody.x < thisPoint.x)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 左に放出
            //*|***|***|***|***|***|***|***|***|***|***|***|
            upAngleV = new Vector2(-1, 1);
            downAngleV = new Vector2(-1, -1);
        }
        upAngle = ChangeData.Vector2ToAngleDeg(upAngleV);
        downAngle = ChangeData.Vector2ToAngleDeg(downAngleV);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 画面横揺れ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float duration = 3.0f;
        float magnitudeMax = 0.2f;
        float magnitudeMin = 0.05f;
        m_directorIndex.LetsWidthShake(duration, magnitudeMax, magnitudeMin);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndex.PlaySoundEffect(GetWallSE());
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 財宝崩
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int starNum = AppStarNum();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 財宝あらわる
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (starNum > 0)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星が出る * starNum
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float m_timeMax = 300.0f;
            float m_timeLevel = 200.0f;
            m_directorIndex.ApplyStarBounce(pointUp, upAngle, swing, 0.3f, 0.1f, m_timeMax, m_timeLevel, starNum);
            m_directorIndex.ApplyStarBounce(pointDown, downAngle, swing, 0.3f, 0.1f, m_timeMax, m_timeLevel, starNum);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星の出現量
    //*|***|***|***|***|***|***|***|***|***|***|***|
    int AppStarNum()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 財宝崩
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int starNum = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 財宝崩
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_ereaTreasure)
        {
            starNum = m_ereaTreasure.AppStarNum();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 財宝崩
        //*|***|***|***|***|***|***|***|***|***|***|***|
        return starNum;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // SEの番号
    //*|***|***|***|***|***|***|***|***|***|***|***|
    SoundID GetWallSE()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 番号
        //*|***|***|***|***|***|***|***|***|***|***|***|
        SoundID soundID = SoundID.HITWALL;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 番号取得
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_ereaTreasure)
        {
            soundID = m_ereaTreasure.GetWallSE();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 番号
        //*|***|***|***|***|***|***|***|***|***|***|***|
        return soundID;
    }
    SoundID GetHeadSE()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 番号
        //*|***|***|***|***|***|***|***|***|***|***|***|
        SoundID soundID = SoundID.FOOTSTEP_01;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 番号取得
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_ereaTreasure)
        {
            soundID = m_ereaTreasure.GetHeadSE();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 番号
        //*|***|***|***|***|***|***|***|***|***|***|***|
        return soundID;
    }
    SoundID GetFloorSE()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 番号
        //*|***|***|***|***|***|***|***|***|***|***|***|
        SoundID soundID = SoundID.FOOTSTEP_01;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 番号取得
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_ereaTreasure)
        {
            soundID = m_ereaTreasure.GetFloorSE();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 番号
        //*|***|***|***|***|***|***|***|***|***|***|***|
        return soundID;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool GetArm()
    {
        bool flag = false;
        if (m_armHitR)
        {
            flag = true;
        }
        return flag;
    }
    public bool GetBody()
    {
        bool flag = false;
        if (m_bodyHitR)
        {
            flag = true;
        }
        return flag;
    }
    public bool GetHead()
    {
        bool flag = false;
        if (m_headHitR)
        {
            flag = true;
        }
        return flag;
    }
    public bool GetLeg()
    {
        bool flag = false;
        if (m_legHitR)
        {
            flag = true;
        }
        return flag;
    }

}


////*|***|***|***|***|***|***|***|***|***|***|***|
//// プレイヤーの一部に当たったか？
////*|***|***|***|***|***|***|***|***|***|***|***|
//if (WarehousePlayer.BoolTagIsPlayer(other.gameObject.tag))
//{
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    // 財宝崩
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    float starNumF;
//    int starNum;
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    // 財宝崩1
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    starNumF = m_haveTreasure * m_outTreasureParsent;
//    starNum = Mathf.CeilToInt(starNumF);
//    starNum = ChangeData.Among(starNum, 0, m_haveTreasure);
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    // 財宝崩2
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    starNumF = m_outTreasure;
//    m_outTreasure -= m_outTreasureAttenuation;
//    starNum = Mathf.CeilToInt(starNumF);
//    starNum = ChangeData.Among(starNum, 0, m_haveTreasure);
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    // 財宝あらわる
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    if (starNum > 0)
//    {
//        Vector3 point = other.gameObject.transform.position;
//        float scale = m_playerIndex.GetScale();
//        Vector3 pointLeft = other.gameObject.transform.position;
//        Vector3 pointRight = other.gameObject.transform.position;
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        // 位置移動
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        pointLeft.x = pointLeft.x - scale;
//        pointRight.x = pointRight.x + scale;
//        Vector2 leftAngleV = new Vector2(-1, 1);
//        Vector2 rightAngleV = new Vector2(1, 1);
//        float leftAngle = ChangeData.Vector2ToAngleDeg(leftAngleV);
//        float rightAngle = ChangeData.Vector2ToAngleDeg(rightAngleV);
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        // 星が出る * starNum
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        m_directorIndex.ApplyStarBounce(pointLeft, leftAngle, 30, 0.3f, 0.1f, starNum);
//        m_directorIndex.ApplyStarBounce(pointRight, rightAngle, 30, 0.3f, 0.1f, starNum);
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        // 資源の減少
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        m_haveTreasure -= starNum;
//    }
//}
