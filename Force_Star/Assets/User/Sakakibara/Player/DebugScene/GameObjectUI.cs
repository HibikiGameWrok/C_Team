using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//*|***|***|***|***|***|***|***|***|***|***|***|
// 言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using TexImageData = GameDataPublic.TexImageData;
using RenderUIData = GameDataPublic.RenderUIData;
using TexImageHidden = GameDataPublic.TexImageHidden;



public class GameObjectUI : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // スプライト表示エリア
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected GameObject m_imageObject;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 描画データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    protected TexImageData m_texImageData;
    [SerializeField]
    protected RenderUIData m_renderUIData;
    [SerializeField]
    private TexImageHidden m_texImageHidden;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 描画システム
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    protected Sprite m_sprite;
    protected Image m_image;
    protected Canvas m_canvas;
    protected RectTransform m_rectTransform;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        OriginAwake();
    }
    protected void OriginAwake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // スプライト表示エリア
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_imageObject = gameObject;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 描画システム キャンバス
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_imageObject.GetComponent<Canvas>() == null)
        {
            m_canvas = m_imageObject.AddComponent<Canvas>();
        }
        else
        {
            m_canvas = m_imageObject.GetComponent<Canvas>();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 描画システム
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_imageObject.GetComponent<Image>() == null)
        {
            m_image = m_imageObject.AddComponent<Image>();
        }
        else
        {
            m_image = m_imageObject.GetComponent<Image>();
        }
        m_rectTransform = m_imageObject.transform as RectTransform;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 描画モード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_rectTransform.anchoredPosition3D = Vector3.zero;
        
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 描画システム
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_texImageData = new TexImageData();
        m_renderUIData = new RenderUIData();
        m_texImageHidden = new TexImageHidden();
        m_texImageData.Reset();
        m_renderUIData.Reset();
        m_texImageHidden.Reset();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 開始時
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Start()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 自動設定に対抗呪文
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_canvas.overrideSorting = true;

        m_image.color = new Color(1, 1, 1, 1);
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Update()
    {

    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 自身の絵を決める
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetImage(TexImageData texImageData)
    {
        m_texImageData = texImageData;
    }
    public void SetImageUpdate(TexImageData texImageData)
    {
        m_texImageData = texImageData;
        MakeImage();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 自身の位置を決める
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetPosition(Vector2 pos)
    {
        m_rectTransform.anchoredPosition = pos;
    }
    public void SetPosition(Vector2 screen, Vector2 ratePos)
    {
        m_rectTransform.anchoredPosition = MyCalculator.EachTimes(screen, ratePos);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 自身の大きさを決める
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetScale(Vector2 scale)
    {
        m_rectTransform.sizeDelta = scale;
    }
    public void SetScaleScreen(Vector2 screen, Vector2 rateScale)
    {
        m_rectTransform.sizeDelta = MyCalculator.EachTimes(screen, rateScale);
    }
    public void SetScaleOrigin(Vector2 rateScale)
    {
        m_rectTransform.sizeDelta = MyCalculator.EachTimes(m_texImageHidden.spriteSize, rateScale);
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 自身の深度を決める
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetDepth(int depth)
    {
        m_renderUIData.depth = depth;
        m_canvas.overrideSorting = true;
        m_canvas.sortingOrder = m_renderUIData.depth;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 自身の中心点を決める
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetPibot(Vector2 pibot)
    {
        m_renderUIData.pibot = pibot;
        m_rectTransform.pivot = m_renderUIData.pibot;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 自身の透明度を決める
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetAlpha(float alpha)
    {
        m_renderUIData.alpha = alpha;
        Color imageLast = m_image.color;
        imageLast.a = m_renderUIData.alpha;
        m_image.color = imageLast;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 自身の中心点を決める
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetRect(Rect rect)
    {
        m_texImageData.rextParsent = rect;
        MakeImage();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 自身の絵を変化させる
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected void MakeImage()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Textureを作成する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Texture2D newTexture = m_texImageData.image;
        float newWidth = newTexture.width;
        float newHeight = newTexture.height;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Spriteを作成する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Rectを作成する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Rect rectImage = new Rect();
        rectImage.xMax = m_texImageData.rextParsent.xMax * newWidth;
        rectImage.xMin = m_texImageData.rextParsent.xMin * newWidth;
        rectImage.yMax = m_texImageData.rextParsent.yMax * newHeight;
        rectImage.yMin = m_texImageData.rextParsent.yMin * newHeight;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 大きさを作成する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 imageSize = Vector2.one;
        imageSize.x = rectImage.width;
        imageSize.y = rectImage.height;
        float pixelsPerUnit = 1;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // その他を作成する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 pibot = m_texImageData.pibot;
        uint extrude = 0;
        SpriteMeshType meshType = SpriteMeshType.FullRect;
        Vector4 border = new Vector4(0, 0, 0, 0);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // スプライトを作成する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Sprite makingData = m_sprite;
        if (rectImage.width != 0 && rectImage.height != 0 && newTexture != null) 
        {
            makingData = Sprite.Create(newTexture, rectImage, pibot, pixelsPerUnit, extrude, meshType, border);
        }
        m_sprite = makingData;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 描画データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_texImageHidden.Reset();
        m_texImageHidden.textureSize = imageSize;
        m_texImageHidden.spriteSize.x = MyCalculator.Division(imageSize.x, pixelsPerUnit);
        m_texImageHidden.spriteSize.y = MyCalculator.Division(imageSize.y, pixelsPerUnit);

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 表示データをいじる
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_image.sprite = m_sprite;

        ////*|***|***|***|***|***|***|***|***|***|***|***|
        //// 描画モード
        ////*|***|***|***|***|***|***|***|***|***|***|***|
        //Vector2 spriteSize = m_texImageData.size;
        //spriteSize.x = newWidth;
        //spriteSize.y = newHeight;
        //m_spriteRenderer.size = spriteSize;
        //m_spriteRenderer.size = new Vector2(1, 1);
    }


}
