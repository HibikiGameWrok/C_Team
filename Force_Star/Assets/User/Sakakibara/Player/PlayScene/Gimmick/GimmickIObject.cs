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
// ギミックはこれを継承する
//*|***|***|***|***|***|***|***|***|***|***|***|
abstract public class GimmickIObject : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ギミック倉庫
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected WarehouseUnity m_warehouseUnity;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイシーン共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected PlaySceneDirectorIndex m_playIndex;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected PlayerDirectorIndex m_playerIndex;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected Rigidbody2D m_rigid2D;


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ギミック共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //GimmickDirectorIndex m_enemyIndex;




    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ギミック倉庫
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_warehouseUnity = WarehouseUnity.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイシーン共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playIndex = PlaySceneDirectorIndex.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerIndex = PlayerDirectorIndex.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ギミック共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //m_enemyIndex = GimmickDirectorIndex.GetInstance();

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // タグ・レイヤー変更
        //*|***|***|***|***|***|***|***|***|***|***|***|
        gameObject.tag = "Gimmick";
        //gameObject.layer = 13;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_rigid2D = gameObject.AddComponent<Rigidbody2D>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 回ることはなく動き続ける・・・
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_rigid2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        m_rigid2D.sleepMode = RigidbodySleepMode2D.NeverSleep;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 起動抽象クラス
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeGimmick();


    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 起動抽象クラス
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected abstract void AwakeGimmick();
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 開始時
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Start()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 初めてのフレーム抽象クラス
        //*|***|***|***|***|***|***|***|***|***|***|***|
        StartGimmick();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 初めてのフレーム抽象クラス
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected abstract void StartGimmick();
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Update()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 更新抽象クラス
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateGimmick();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 描画用抽象クラス
        //*|***|***|***|***|***|***|***|***|***|***|***|
        RenderGimmick();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 更新抽象クラス
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected abstract void UpdateGimmick();
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 描画用抽象クラス
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected abstract void RenderGimmick();
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定用
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected bool GetHitAttackBoal(string tag)
    {
        bool returnFlag = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃が当たった？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (tag == WarehousePlayer.GetTag_AttackBoal())
        {
            returnFlag = true;
        }
        return returnFlag;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // KeyでTRUEなら反転
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected float KeyPower(float power, bool key)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // KeyでTRUEなら反転
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (key)
        {
            power *= -1;
        }
        return power;
    }
    protected Vector2 KeyPower(Vector2 power, bool key)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // KeyでTRUEなら反転
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (key)
        {
            power *= -1;
        }
        return power;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 作る
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected GameObjectSprite MakeGameObjectSprite()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 子どもに作成。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        GameObjectSprite spriteData = gameObject.AddComponent<GameObjectSprite>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー親子
        //*|***|***|***|***|***|***|***|***|***|***|***|
        return spriteData;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 作成TexImageData画像切り取り
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected void MakeTexImageDataRect(ref TexImageData texImageData, int imageNum, int imagePartsX, int imagePartsY)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // イメージのRECTを作る
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Rect newRect = new Rect();
        newRect = MyCalculator.RectSizeReverse_Y(imageNum, imagePartsX, imagePartsY);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // イメージのRECTを適用
        //*|***|***|***|***|***|***|***|***|***|***|***|
        texImageData.rextParsent = newRect;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 作成TexImageData画像データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected void MakeTexImageDataImage(ref TexImageData texImageData, UnityPlayNum imageNumber)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // イメージのImageを作る
        //*|***|***|***|***|***|***|***|***|***|***|***|
        texImageData.image = m_warehouseUnity.GetTexture2DPlay(imageNumber);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 作成TexImageData大きさ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected void MakeTexImageDataSize(ref TexImageData texImageData, Vector2 imageSize)
    {
        texImageData.size = imageSize;
    }
    protected void MakeTexImageDataSize(ref TexImageData texImageData, Vector2 imageSize, Vector2 imagePibot)
    {
        texImageData.size = imageSize;
        texImageData.pibot = imagePibot;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 作成スプライト
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected void MakeSprite(ref GameObjectSprite sprite, TexImageData texImageData)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // イメージのRECTを適用
        //*|***|***|***|***|***|***|***|***|***|***|***|
        sprite.SetImageUpdate(texImageData);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 作成スプライト
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected void AnimateSprite(ref GameObjectSprite sprite, int imageNum, int imagePartsX, int imagePartsY)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // イメージのRECTを作る
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Rect newRect = new Rect();
        newRect = MyCalculator.RectSizeReverse_Y(imageNum, imagePartsX, imagePartsY);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // イメージのRECTを適用
        //*|***|***|***|***|***|***|***|***|***|***|***|
        sprite.SetRect(newRect);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コリジョン発生装置
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void OnTriggerEnter2D(Collider2D col)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃されたらTRUE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (GetHitAttackBoal(col.gameObject.tag))
        {
            AeceiveAttack();
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ダメージをもらう
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected abstract void AeceiveAttack();
}