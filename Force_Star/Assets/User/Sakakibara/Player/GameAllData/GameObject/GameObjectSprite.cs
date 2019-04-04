using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*|***|***|***|***|***|***|***|***|***|***|***|
// 言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using TexImageData = GameDataPublic.TexImageData;
using RenderImageData = GameDataPublic.RenderImageData;

//*|***|***|***|***|***|***|***|***|***|***|***|
// GameObjectSpriteは眠らない
//*|***|***|***|***|***|***|***|***|***|***|***|
//[ExecuteInEditMode]
public class GameObjectSprite : MonoBehaviour
{

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 隠された描画システム
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected class TexImageHidden
    {
        public Vector2 textureSize;
        public Vector2 spriteSize;
        public void Reset()
        {
            textureSize = Vector2.one;
            spriteSize = Vector2.one;
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // スプライト表示エリア
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected GameObject m_spriteObject;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 描画データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    protected TexImageData m_texImageData;
    [SerializeField]
    protected RenderImageData m_renderImageData;
    [SerializeField]
    private TexImageHidden m_texImageHidden;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 描画システム
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    protected Sprite m_sprite;
    protected SpriteRenderer m_spriteRenderer;

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
        m_spriteObject = new GameObject("Sprite");
        m_spriteObject.transform.parent = gameObject.transform;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 描画システム
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_spriteObject.GetComponent<SpriteRenderer>() == null)
        {
            m_spriteRenderer = m_spriteObject.AddComponent<SpriteRenderer>();
        }
        else
        {
            m_spriteRenderer = m_spriteObject.GetComponent<SpriteRenderer>();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 描画モード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_spriteRenderer.tileMode = SpriteTileMode.Continuous;
        m_spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        //m_spriteRenderer.sortingLayerName = "";
        m_spriteRenderer.size = new Vector2(1, 1);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 描画システム
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_texImageData = new TexImageData();
        m_renderImageData = new RenderImageData();
        m_texImageHidden = new TexImageHidden();
        m_texImageData.Reset();
        m_texImageHidden.Reset();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 開始時
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Start ()
    {
		
	}

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Update ()
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
    // 自身の描画を決める
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetRender(RenderImageData renderImageData)
    {
        m_renderImageData = renderImageData;
    }
    public void SetRenderUpdate(RenderImageData renderImageData)
    {
        m_renderImageData = renderImageData;
        MakeRender();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 自身の位置を決める
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetPosition(Vector3 position)
    {
        gameObject.transform.position = position;
    }
    public void SetPositionLocal(Vector3 position)
    {
        gameObject.transform.localPosition = position;
    }
    public void SetImagePosition(Vector3 position)
    {
        m_spriteObject.transform.position = position;
    }
    public void SetImagePositionLocal(Vector3 position)
    {
        m_spriteObject.transform.localPosition = position;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 自身の回転を決める
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetRotation(Quaternion rotation)
    {
        gameObject.transform.rotation = rotation;
    }
    public void SetRotationLocal(Quaternion rotation)
    {
        gameObject.transform.localRotation = rotation;
    }
    public void SetImageRotation(Quaternion rotation)
    {
        m_spriteObject.transform.rotation = rotation;
    }
    public void SetImageRotationLocal(Quaternion rotation)
    {
        m_spriteObject.transform.localRotation = rotation;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 自身の大きさを決める！？
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetScaleLocal(Vector3 scale)
    {
        gameObject.transform.localScale = scale;
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
        float pixelsPerUnit = imageSize.x;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // その他を作成する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 pibot = new Vector2(0.5f, 0.5f);
        uint extrude = 0;
        SpriteMeshType meshType = SpriteMeshType.FullRect;
        Vector4 border = new Vector4(0, 0, 0, 0);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // スプライトを作成する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_sprite = Sprite.Create(newTexture, rectImage, pibot, pixelsPerUnit, extrude, meshType, border);

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
        m_spriteRenderer.sprite = m_sprite;

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 描画モード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 spriteSize = m_texImageData.size;
        m_spriteRenderer.size = spriteSize;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 自身の描画を変化させる
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected void MakeRender()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 描画モード深度
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_spriteRenderer.sortingOrder = m_renderImageData.depth;
    }
}
