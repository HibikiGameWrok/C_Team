using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*|***|***|***|***|***|***|***|***|***|***|***|
// ギミック用セット
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehouseUnity = WarehouseData.WarehouseUnity;
using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;
//*|***|***|***|***|***|***|***|***|***|***|***|
// 番号データギミック用
//*|***|***|***|***|***|***|***|***|***|***|***|
using UnityTitleNum = WarehouseData.WarehouseStaticData.Object2D_UnityNumbers_Title;
using UnityPlayNum = WarehouseData.WarehouseStaticData.Object2D_UnityNumbers_Play;
using UnityResultNum = WarehouseData.WarehouseStaticData.Object2D_UnityNumbers_Result;
//*|***|***|***|***|***|***|***|***|***|***|***|
// 画像データ言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using TexImageData = GameDataPublic.TexImageData;
using RenderImageData = GameDataPublic.RenderImageData;
using PartsData = GameDataPublic.PartsData;


//*|***|***|***|***|***|***|***|***|***|***|***|
// シンプルなギミック
//*|***|***|***|***|***|***|***|***|***|***|***|
public class GimmickObject : GimmickIObject
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    GimmickBodyColliderBox m_collider;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 画像
    //*|***|***|***|***|***|***|***|***|***|***|***|
    GameObjectSprite m_imageGimmick;
    int m_animateNumber = 0;
    int m_animateCutX = 0;
    int m_animateCutY = 0;
    float m_animateTime = 0;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 以下、サメから継承
    //*|***|***|***|***|***|***|***|***|***|***|***|

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // Y方向にかかる力
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    float m_jumpForce = 300.0f;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // X方向にかかる力
    //*|***|***|***|***|***|***|***|***|***|***|***|
    float m_walkForce = 30.0f;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // X方向に力がかかり過ぎないように抑制する値
    //*|***|***|***|***|***|***|***|***|***|***|***|
    float m_maxWalkSpeed = 2.0f;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ギミックの方向
    //*|***|***|***|***|***|***|***|***|***|***|***|
    bool m_key = false;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 消すためのフラグ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private bool m_deathFlag = false;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 地面に当たっている時だけジャンプする為のフラグ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private bool m_groundFlag = false;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 自動でジャンプする為のフラグ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private bool m_jumpFlag = false;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 自動でジャンプする為のタイマー
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private int m_jumpTimer = 0;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 消えるまでのタイマー
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private float m_deathTimer;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 消すためのカウント
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private float m_deathCount = 0.0f;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected override void AwakeGimmick()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_collider = new GimmickBodyColliderBox(this.gameObject);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 画像のデータを作成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_imageGimmick = MakeGameObjectSprite();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 画像データを作りましょう
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TexImageData texData = new TexImageData();
        texData.Reset();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 画像データの材料を倉庫から取り出します。
        // UnityPlayNumの番号を変えると画像が変わります
        //*|***|***|***|***|***|***|***|***|***|***|***|
        MakeTexImageDataImage(ref texData, UnityPlayNum.ENEMY_STARFISH);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 画像データの材料を細かく刻みます。
        // 3番目の引数 X
        // 4番目の引数 Y
        // 2番目の引数 Num
        // 画像を X * Y 等分し、右上から横優先でNum番目の
        // 画像だけにできます。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        MakeTexImageDataRect(ref texData, 0, 2, 2);
        m_animateNumber = 0;
        m_animateCutX = 2;
        m_animateCutY = 2;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 画像データの大きさを決めます。
        // 2番目の引数 Size
        // 画像がUNITYの大きさSizeになります
        //*|***|***|***|***|***|***|***|***|***|***|***|
        MakeTexImageDataSize(ref texData, new Vector2(10.0f, 10.0f));
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 仕上げにGameObjectSpriteにTexImageDataを
        // 入れ込んで出来上がりです。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        MakeSprite(ref m_imageGimmick, texData);

        //MakeSpriteImage(ref m_imageGimmick, 0, 1, 1);

    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 初めてのフレーム
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected override void StartGimmick()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定大きさ！
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 size = new Vector2(10.0f, 10.0f);
        Vector2 pos = new Vector2(0.0f, 0.0f);
        m_collider.SetPointSize(pos, size);
        m_collider.SetPlayHit();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected override void UpdateGimmick()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 動き
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_deathFlag != true)
        {
            //時間経過の管理をするif文
            if (m_jumpTimer < 10)
            {
                m_jumpTimer++;
            }
            //m_jumpFlagがfalseでありタイマーが最大ならm_jumpFlagをtrueにする
            if (m_jumpFlag != true && m_jumpTimer == 10)
            {
                m_jumpFlag = true;
            }

            //X方向のspeedを設定する
            float speedx = Mathf.Abs(this.m_rigid2D.velocity.x);

            //m_jumpFlagがtrueでありspeedが最大値を超えていないなら
            if (m_jumpFlag && speedx < this.m_maxWalkSpeed)
            {
                //X方向に力を加える
                this.m_rigid2D.AddForce(KeyPower(transform.right * this.m_walkForce, m_key));
            }

            //m_jumpFlagがtrueでありgroundFlagがtrueでありY方向の加えられる力が0.0fなら
            if (m_jumpFlag && m_groundFlag)
            {
                //ジャンプする
                this.m_rigid2D.AddForce(transform.up * this.m_jumpForce);

                //m_jumpFlagをfalseにしてm_jumpTimerを0に戻す
                m_jumpFlag = false;
                m_jumpTimer = 0;
            }
        }

        //死んでしまったら
        if (m_deathFlag == true)
        {
            //死んで消えてしまうまでの猶予はここで決まっているのだ
            m_deathCount++;
            //だからそれまで余生を過ごし時が来たら
            if (m_deathTimer < m_deathCount)
            {
                //跡形もなく消えてゆけ
                Destroy(this.gameObject);
            }
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 描画用
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected override void RenderGimmick()
    {
        m_animateTime += 0.1f;
        if (m_animateTime >= 5.0f)
        {
            m_animateNumber += 1;
            m_animateTime -= 5.0f;
            int maxAnimeNum = m_animateCutX * m_animateCutY;
            m_animateNumber = ChangeData.AntiOverflow(m_animateNumber, maxAnimeNum);
        }
        AnimateSprite(ref m_imageGimmick, m_animateNumber, m_animateCutX, m_animateCutY);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ダメージをもらう
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected override void AeceiveAttack()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 絵の大きさ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 imageSize = this.m_imageGimmick.GetIamgeSize();
        float posX1;
        float posX2;
        float posY;
        posX1 = this.transform.position.x + imageSize.x / 2 + 3;
        posX2 = this.transform.position.x - imageSize.x / 2 - 3;
        posY = this.transform.position.y - imageSize.y / 2;

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星が出る *100
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playIndex.ApplyStar(new Vector2(posX1, posY), 100);

        float gravityForce = 0.0f;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //好きな大きさの重力を指定する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_rigid2D.gravityScale = gravityForce;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //「死にました」とフラグで伝える
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_deathFlag = true;
    }
}
