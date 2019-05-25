using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*|***|***|***|***|***|***|***|***|***|***|***|
// 音楽
//*|***|***|***|***|***|***|***|***|***|***|***|
using SoundID = SEManager.SoundID;

public class PlaySceneDirectorIndex
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // シングルトン
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private static PlaySceneDirectorIndex director = null;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // シングルトン生成
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static PlaySceneDirectorIndex GetInstance()
    {
        if (director == null)
        {
            director = new PlaySceneDirectorIndex();
        }
        return director;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // シングルトン消滅
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static void Remove()
    {
        if (director != null)
        {
            director = null;
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // メインカメラ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    Camera m_mainCamera;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // メインカメラの技
    //*|***|***|***|***|***|***|***|***|***|***|***|
    CameraArts m_mainCameraArts;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星の運営者
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlayStarManeger m_starManeger;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // SEの運営者
    //*|***|***|***|***|***|***|***|***|***|***|***|
    SEManager m_seManeger;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データ初期化
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlaySceneDirectorIndex()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // メインカメラ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_mainCamera = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // メインカメラにターゲットされている
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_mainCameraArts = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星の運営者
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starManeger = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // SEの運営者
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_seManeger = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲームクリアフラグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        ClearReset();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲームオーバーフラグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        ClearGameOver();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データ終了
    //*|***|***|***|***|***|***|***|***|***|***|***|
    ~PlaySceneDirectorIndex()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // メインカメラ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_mainCamera = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // メインカメラにターゲットされている
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_mainCameraArts = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星の運営者
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starManeger = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // SEの運営者
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_seManeger = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲームクリアフラグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        ClearReset();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲームオーバーフラグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        ClearGameOver();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データ終わりか？再生か？
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void AllReset()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // メインカメラ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_mainCamera = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // メインカメラにターゲットされている
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_mainCameraArts = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星の運営者
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starManeger = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // SEの運営者
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_seManeger = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲームクリアフラグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        ClearReset();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲームオーバーフラグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        ClearGameOver();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ポインター受付
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetPointerMainCamera(Camera camera)
    {
        m_mainCamera = camera;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ポインター受付
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetPointerMainCameraArts(CameraArts arts)
    {
        m_mainCameraArts = arts;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ポインター受付
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetPointerStarManeger(PlayStarManeger maneger)
    {
        m_starManeger = maneger;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ポインター受付
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetPointerSEManeger(SEManager maneger)
    {
        m_seManeger = maneger;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ポインター受付
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //public void SetPointerTargetCamera(TargetFollow follow)
    //{
    //    m_targetCamera = follow;
    //}
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 狙われたい人を受け付けています
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetObjectTargetCamera(GameObject target)
    {
        if (m_mainCameraArts != null && target != null)
        {
            m_mainCameraArts.SetTarget(target);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // スクリーンのどこでしょう？
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public Vector3 GetScreenPos(Vector3 position)
    {
        if (m_mainCamera != null)
        {
            Vector3 ansPos = m_mainCamera.WorldToScreenPoint(position);
            return ansPos;
        }
        return Vector3.zero;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星の管理者に報告   
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星の管理者に報告
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void ApplyStarDiffusion(Vector3 position, int num)
    {
        m_starManeger.CreateStarDiffusionPisce(position, num);
    }
    public void ApplyStarDiffusion(Vector3 position, float angle, float angleSwing, float speedMax, float speedMin, int num)
    {
        m_starManeger.CreateStarDiffusionPisce(position, angle, angleSwing, speedMax, speedMin, num);
    }
    public void ApplyStarDiffusion(Vector3 position, float angle, float angleSwing, float speedMax, int num)
    {
        m_starManeger.CreateStarDiffusionPisce(position, angle, angleSwing, speedMax, 0, num);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 左右にぶちまける
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void ApplyStarDiffusionLeftSide(Vector3 position, float angleSwing, float speedMax, int num)
    {
        Vector2 vec = new Vector2(-1, 0);
        float angle = ChangeData.Vector2ToAngleDeg(vec);
        m_starManeger.CreateStarDiffusionPisce(position, angle, angleSwing, speedMax, 0, num);
    }
    public void ApplyStarDiffusionLeftSide(Vector3 position, float angleSwing, float speedMax, float speedMin, int num)
    {
        Vector2 vec = new Vector2(-1, 0);
        float angle = ChangeData.Vector2ToAngleDeg(vec);
        m_starManeger.CreateStarDiffusionPisce(position, angle, angleSwing, speedMax, speedMin, num);
    }
    public void ApplyStarDiffusionRightSide(Vector3 position, float angleSwing, float speedMax, int num)
    {
        Vector2 vec = new Vector2(1, 0);
        float angle = ChangeData.Vector2ToAngleDeg(vec);
        m_starManeger.CreateStarDiffusionPisce(position, angle, angleSwing, speedMax, 0, num);
    }
    public void ApplyStarDiffusionRightSide(Vector3 position, float angleSwing, float speedMax, float speedMin, int num)
    {
        Vector2 vec = new Vector2(1, 0);
        float angle = ChangeData.Vector2ToAngleDeg(vec);
        m_starManeger.CreateStarDiffusionPisce(position, angle, angleSwing, speedMax, speedMin, num);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 跳ねる星を出してください
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void ApplyStarBounce(Vector3 position, int num)
    {
        m_starManeger.CreateStarBouncePisce(position, num);
    }
    public void ApplyStarBounce(Vector3 position, float angle, float angleSwing, float speedMax, float speedMin, float timeMax, float timeLevel, int num)
    {
        m_starManeger.CreateStarBouncePisce(position, angle, angleSwing, speedMax, speedMin, timeMax, timeLevel, num);
    }
    public void ApplyStarBounce(Vector3 position, float angle, float angleSwing, float speedMax, float timeMax, float timeLevel, int num)
    {
        m_starManeger.CreateStarBouncePisce(position, angle, angleSwing, speedMax, 0, timeMax, timeLevel, num);
    }
    public void ApplyStarBounce(Vector3 position, float angle, float angleSwing, float speedMax, float speedMin, int num)
    {
        float timeMax = 300.0f;
        float timeLevel = 200.0f;
        m_starManeger.CreateStarBouncePisce(position, angle, angleSwing, speedMax, speedMin, timeMax, timeLevel, num);
    }
    public void ApplyStarBounce(Vector3 position, float angle, float angleSwing, float speedMax, int num)
    {
        float timeMax = 300.0f;
        float timeLevel = 200.0f;
        m_starManeger.CreateStarBouncePisce(position, angle, angleSwing, speedMax, 0, timeMax, timeLevel, num);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 左にぶちまける
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void ApplyStarBounceLeftSide(Vector3 position, float angleSwing, float speedMax, float speedMin, float timeMax, float timeLevel, int num)
    {
        Vector2 vec = new Vector2(-1, 0);
        float angle = ChangeData.Vector2ToAngleDeg(vec);
        m_starManeger.CreateStarBouncePisce(position, angle, angleSwing, speedMax, speedMin, timeMax, timeLevel, num);
    }
    public void ApplyStarBounceLeftSide(Vector3 position, float angleSwing, float speedMax, float timeMax, float timeLevel, int num)
    {
        Vector2 vec = new Vector2(-1, 0);
        float angle = ChangeData.Vector2ToAngleDeg(vec);
        m_starManeger.CreateStarBouncePisce(position, angle, angleSwing, speedMax, 0, timeMax, timeLevel, num);
    }
    public void ApplyStarBounceLeftSide(Vector3 position, float angleSwing, float speedMax, float speedMin, int num)
    {
        Vector2 vec = new Vector2(-1, 0);
        float angle = ChangeData.Vector2ToAngleDeg(vec);
        float timeMax = 300.0f;
        float timeLevel = 200.0f;
        m_starManeger.CreateStarBouncePisce(position, angle, angleSwing, speedMax, speedMin, timeMax, timeLevel, num);
    }
    public void ApplyStarBounceLeftSide(Vector3 position, float angleSwing, float speedMax, int num)
    {
        Vector2 vec = new Vector2(-1, 0);
        float angle = ChangeData.Vector2ToAngleDeg(vec);
        float timeMax = 300.0f;
        float timeLevel = 200.0f;
        m_starManeger.CreateStarBouncePisce(position, angle, angleSwing, speedMax, 0, timeMax, timeLevel, num);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 右にぶちまける
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void ApplyStarBounceRightSide(Vector3 position, float angleSwing, float speedMax, float speedMin, float timeMax, float timeLevel, int num)
    {
        Vector2 vec = new Vector2(1, 0);
        float angle = ChangeData.Vector2ToAngleDeg(vec);
        m_starManeger.CreateStarBouncePisce(position, angle, angleSwing, speedMax, speedMin, timeMax, timeLevel, num);
    }
    public void ApplyStarBounceRightSide(Vector3 position, float angleSwing, float speedMax, float timeMax, float timeLevel, int num)
    {
        Vector2 vec = new Vector2(1, 0);
        float angle = ChangeData.Vector2ToAngleDeg(vec);
        m_starManeger.CreateStarBouncePisce(position, angle, angleSwing, speedMax, 0, timeMax, timeLevel, num);
    }
    public void ApplyStarBounceRightSide(Vector3 position, float angleSwing, float speedMax, float speedMin, int num)
    {
        Vector2 vec = new Vector2(1, 0);
        float angle = ChangeData.Vector2ToAngleDeg(vec);
        float timeMax = 300.0f;
        float timeLevel = 200.0f;
        m_starManeger.CreateStarBouncePisce(position, angle, angleSwing, speedMax, speedMin, timeMax, timeLevel, num);
    }
    public void ApplyStarBounceRightSide(Vector3 position, float angleSwing, float speedMax, int num)
    {
        Vector2 vec = new Vector2(1, 0);
        float angle = ChangeData.Vector2ToAngleDeg(vec);
        float timeMax = 300.0f;
        float timeLevel = 200.0f;
        m_starManeger.CreateStarBouncePisce(position, angle, angleSwing, speedMax, 0, timeMax, timeLevel, num);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星を個別で作るようの関数
    // 外部から出現位置と星の取得数を入力し生成する関数(壁衝突,ジャンプ力を外部で操作する用)
    // 引数(星の位置,星の取得数,X軸の方向(flase:左　true:右),最初のジャンプ力)
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void CreateOneStar(Vector2 objectPos, Vector2 playerPos, int maxStar, bool flag, float jump, bool center = false)
    {
        if (!center)
        {
            if (playerPos.x < objectPos.x)
            {
                ApplyStarBounceRightSide(objectPos, 90.0f, 0.2f, maxStar);
            }
            else
            {
                ApplyStarBounceLeftSide(objectPos, 90.0f, 0.2f, maxStar);
            }
        }
        else
        {
            ApplyStarBounce(objectPos, maxStar);
        }
    }
    public void CreateOneStar(Vector2 pos, Vector2 playerPos, int maxStar, bool center = false)
    {

        if (!center)
        {
            if (playerPos.x < pos.x)
            {
                ApplyStarBounceRightSide(pos, 90.0f, 0.2f, 0.01f, maxStar);
            }
            else
            {
                ApplyStarBounceLeftSide(pos, 90.0f, 0.2f, 0.01f, maxStar);
            }
        }
        else
        {
            ApplyStarBounce(pos, maxStar);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 時間あり
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void CreateOneStarTime(Vector2 objectPos, Vector2 playerPos, int maxStar, bool flag, float jump, float timeMax, float timeLevel, bool center = false)
    {
        if (!center)
        {
            if (playerPos.x < objectPos.x)
            {
                ApplyStarBounceRightSide(objectPos, 90.0f, 0.2f, timeMax, timeLevel, maxStar);
            }
            else
            {
                ApplyStarBounceLeftSide(objectPos, 90.0f, 0.2f, timeMax, timeLevel, maxStar);
            }
        }
        else
        {
            ApplyStarBounce(objectPos, maxStar);
        }
    }
    public void CreateOneStarTime(Vector2 pos, Vector2 playerPos, int maxStar, float timeMax, float timeLevel, bool center = false)
    {

        if (!center)
        {
            if (playerPos.x < pos.x)
            {
                ApplyStarBounceRightSide(pos, 90.0f, 0.2f, 0.01f, timeMax, timeLevel, maxStar);
            }
            else
            {
                ApplyStarBounceLeftSide(pos, 90.0f, 0.2f, 0.01f, timeMax, timeLevel, maxStar);
            }
        }
        else
        {
            ApplyStarBounce(pos, maxStar);
        }
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // サウンドON!SE.GO!
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void PlaySoundEffect(SoundID id)
    {
        m_seManeger.PlaySoundEffect(id);
    }
    public void PlaySoundEffect(SoundID id, float volume)
    {
        m_seManeger.PlaySoundVolume(volume);
        m_seManeger.PlaySoundEffect(id);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // サウンドON!敵の爆発.GO!
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void PlaySoundEffectWowEnemy()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 画面揺れ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_seManeger.PlaySoundEffect(SoundID.DAMAGE_02);
        m_seManeger.PlaySoundEffect(SoundID.HYUN_01);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゲームクリア！
    //*|***|***|***|***|***|***|***|***|***|***|***|
    bool m_platinumAngel;
    bool m_gameClearFlag;
    public void ClearReset()
    {
        m_platinumAngel = false;
        m_gameClearFlag = false;
    }
    public void SetClearAnimation()
    {
        m_platinumAngel = true;
    }
    public void SetClearFlag()
    {
        m_platinumAngel = true;
        m_gameClearFlag = true;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // どっかーん
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void LetsShake()
    {
        float duration = 0.5f;
        float magnitude = 0.5f;
        duration = 30.0f;
        magnitude = 0.5f;
        m_mainCameraArts.Shake(duration, magnitude);
    }
    public void LetsShake(float duration, float magnitude)
    {
        m_mainCameraArts.Shake(duration, magnitude);
    }
    public void LetsShake(float duration, float magnitudeMax, float magnitudeMin)
    {
        m_mainCameraArts.Shake(duration, magnitudeMax, magnitudeMin);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // リアルバネ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void LetsHeightShake()
    {
        float duration = 0.5f;
        float magnitude = 0.5f;
        duration = 30.0f;
        magnitude = 0.5f;
        m_mainCameraArts.HeightShake(duration, magnitude);
    }
    public void LetsHeightShake(float duration, float magnitude)
    {
        m_mainCameraArts.HeightShake(duration, magnitude);
    }
    public void LetsHeightShake(float duration, float magnitudeMax, float magnitudeMin)
    {
        m_mainCameraArts.HeightShake(duration, magnitudeMax, magnitudeMin);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 地震
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void LetsWidthShake()
    {
        float duration = 0.5f;
        float magnitude = 0.5f;
        duration = 30.0f;
        magnitude = 0.5f;
        m_mainCameraArts.WidthShake(duration, magnitude);
    }
    public void LetsWidthShake(float duration, float magnitude)
    {
        m_mainCameraArts.WidthShake(duration, magnitude);
    }
    public void LetsWidthShake(float duration, float magnitudeMax, float magnitudeMin)
    {
        m_mainCameraArts.WidthShake(duration, magnitudeMax, magnitudeMin);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 敵の爆発は画面まで揺れる！
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void WowEnemy()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 画面揺れ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float duration = 3.0f;
        float magnitudeMax = 0.30f;
        float magnitudeMin = 0.15f;
        m_mainCameraArts.WidthShake(duration, magnitudeMax, magnitudeMin);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゲームクリア！取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool GetClearAnimation()
    {
        return m_platinumAngel;
    }
    public bool GetClearFlag()
    {
        return m_gameClearFlag;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゲームクリア！
    //*|***|***|***|***|***|***|***|***|***|***|***|
    bool m_gameOverAnime;
    bool m_gameOver;
    public void ClearGameOver()
    {
        m_gameOverAnime = false;
        m_gameOver = false;
    }
    public void SetGameOverAnimation()
    {
        m_gameOverAnime = true;
    }
    public void SetGameOverFlag()
    {
        m_gameOverAnime = true;
        m_gameOver = true;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゲームクリア！取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool GetGameOverAnimation()
    {
        return m_gameOverAnime;
    }
    public bool GetGameOver()
    {
        return m_gameOver;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゲームクリア！
    //*|***|***|***|***|***|***|***|***|***|***|***|
    bool m_aliveFlagPlayScene;
    public void SetAliveFlagPlayScene(bool setBool)
    {
        m_aliveFlagPlayScene = setBool;
    }
    public bool GetAliveFlagPlayScene()
    {
        return m_aliveFlagPlayScene;
    }
}
