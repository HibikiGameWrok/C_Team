using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*|***|***|***|***|***|***|***|***|***|***|***|
// 言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using TexImageData = GameDataPublic.TexImageData;
using RenderImageData = GameDataPublic.RenderImageData;
using TexImageHidden = GameDataPublic.TexImageHidden;

using WarehouseData;
using CommonImageNum = WarehouseData.WarehouseStaticData.Object2D_Numbers_Common;


public abstract class DebugCanvas : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 私有地のキャンバス
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected WarehouseObject m_warehouseObject;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 私有地のキャンバス
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected GameObject m_canvasObject;
    protected Canvas m_haveCanvas;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 共通変数
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected Vector2 m_screenSize;
    protected Vector2 m_mousePos;

    protected class ImageUIData
    {
        public Vector2 imageScale;
        public Vector2 imagePos;
        public void Init()
        {
            imageScale = Vector2.one;
            imagePos = Vector2.zero;
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // UIをするための基本セット
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected class OriginUIGroup
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの背景
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public GameObjectUI gameObjectUI;
        public ImageUIData imageUIData;
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        m_warehouseObject = WarehouseObject.GetInstance();
        AwakeImage();
        AwakeUI();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 起動のその先を
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected abstract void AwakeImage();
    protected abstract void AwakeUI();



    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Update()
    {
        MousePosSet();
        UpdateWork();
        UpdateImage();
        UpdateUI();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 更新のその先を
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected abstract void UpdateWork();
    protected abstract void UpdateImage();
    protected abstract void UpdateUI();
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // メンバー作成
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected GameObjectUI CreateMenber(TexImageData texData, string name = "imageTenplete", bool preserveAspect = true)
    {
        GameObject createObject = new GameObject();
        GameObjectUI create = createObject.AddComponent<GameObjectUI>();
        createObject.name = name;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // TexImageDataを創る
        //*|***|***|***|***|***|***|***|***|***|***|***|
        create.SetImageUpdate(texData);
        return create;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // screen  枠サイズ
    // pos  枠の中の場所
    // scale  大きさ倍率
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected void AssetSet(ref GameObjectUI menber, Vector2 screen, ImageUIData uiData, bool screenScale = false)
    {
        menber.SetPosition(screen, uiData.imagePos);
        if (screenScale)
        {
            menber.SetScaleScreen(screen, uiData.imageScale);
        }
        else
        {
            menber.SetScaleOrigin(uiData.imageScale);
        }
    }
    protected void AssetSet(ref OriginUIGroup menber, Vector2 screen, bool screenScale = false)
    {
        AssetSet(ref menber.gameObjectUI, screen, menber.imageUIData, screenScale);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 画面サイズ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected void ScreenPosSet()
    {
        m_screenSize.x = Screen.width;
        m_screenSize.y = Screen.height;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 画面サイズのうちのマウス
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected void MousePosSet()
    {
        Vector2 MouseScreenPersent;
        Vector2 TargetPersent = Input.mousePosition;
        ScreenPosSet();
        MouseScreenPersent.x = TargetPersent.x / m_screenSize.x;
        MouseScreenPersent.y = TargetPersent.y / m_screenSize.y;
        m_mousePos = MouseScreenPersent;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // マウスと画面の連動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected Vector2 MouseToScreenPos(Vector2 mousePos)
    {
        Vector2 screenPos;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 枠の色変化
        //*|***|***|***|***|***|***|***|***|***|***|***|
        screenPos.x = mousePos.x - 0.5f;
        screenPos.y = mousePos.y - 0.5f;
        return screenPos;
    }
    protected Vector2 ScreenPosToMouse(Vector2 screenPos)
    {
        Vector2 mousePos;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 枠の色変化
        //*|***|***|***|***|***|***|***|***|***|***|***|
        mousePos.x = screenPos.x + 0.5f;
        mousePos.y = screenPos.y + 0.5f;
        return mousePos;
    }
}
