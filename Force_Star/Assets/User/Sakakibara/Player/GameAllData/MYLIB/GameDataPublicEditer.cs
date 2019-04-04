using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TexImageData = GameDataPublic.TexImageData;
using RenderImageData = GameDataPublic.RenderImageData;
using PartsData = GameDataPublic.PartsData;
//*|***|***|***|***|***|***|***|***|***|***|***|
// 拡張している時だけ
//*|***|***|***|***|***|***|***|***|***|***|***|
#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(RenderImageData))]
public class RenderImageDataEditer : PropertyDrawer
{

    private class PropertyData
    {
        public SerializedProperty depthProperty;

    }

    private Dictionary<string, PropertyData> _propertyDataPerPropertyPath = new Dictionary<string, PropertyData>();
    private PropertyData m_property;
    private RenderImageData m_myData;

    private void OnEnable()
    {

    }

    private void Init(SerializedProperty property)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // もう作ってあるならやらない
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (_propertyDataPerPropertyPath.TryGetValue(property.propertyPath, out m_property))
        {
            return;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 新規作成する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_property = new PropertyData();
        m_property.depthProperty = property.FindPropertyRelative("depth");
        _propertyDataPerPropertyPath.Add(property.propertyPath, m_property);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Init(property);

        //m_myData = this.attribute as RenderImageData;


        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 使用可能領域
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Rect UseField = position;
        Rect fieldRect = UseField;
        float height = EditorGUIUtility.singleLineHeight;
        //int moveXNum = 0;
        int moveYNum = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // インデントされた位置のRect
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Rect indentedUseField = EditorGUI.IndentedRect(fieldRect);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Prefab化前に大きさを決める
        //*|***|***|***|***|***|***|***|***|***|***|***|
        fieldRect = indentedUseField;
        fieldRect.height = height;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Prefab化前に位置を決める
        //*|***|***|***|***|***|***|***|***|***|***|***|
        moveYNum = 0;
        for (int count = 0; count < moveYNum; count++)
        {
            fieldRect.yMin += height;
            fieldRect.yMax += height;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Prefab化した後変更加えるためPropertyScopeを使う
        //*|***|***|***|***|***|***|***|***|***|***|***| 
        using (new EditorGUI.PropertyScope(fieldRect, label, property))
        {

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ラベルを表示
            // 残りの使用可能な大きさを取る
            //*|***|***|***|***|***|***|***|***|***|***|***|
            label.text = "深さ";
            label.text = "画像の深さ";
            fieldRect = EditorGUI.PrefixLabel(fieldRect, GUIUtility.GetControlID(FocusType.Passive), label);

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // Indent = 0
            //*|***|***|***|***|***|***|***|***|***|***|***|
            var preIndent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // フィールド描画サイズ設定
            //*|***|***|***|***|***|***|***|***|***|***|***|
            var firstRect = fieldRect;
            firstRect.width /= 2;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // フィールド描画
            //*|***|***|***|***|***|***|***|***|***|***|***|
            EditorGUI.PropertyField(firstRect, m_property.depthProperty, GUIContent.none);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        Init(property);

        return EditorGUIUtility.singleLineHeight;
    }

    //public override void OnInspectorGUI()
    //{

    //    //　シリアライズオブジェクトの更新
    //    serializedObject.Update();

    //    //　ここにシリアライズデータの表示をする

    //    //　シリアライズオブジェクトのプロパティの変更を更新
    //    serializedObject.ApplyModifiedProperties();

    //}


    //public override void OnInspectorGUI()
    //{
    //    RenderImageData targetPlayer = new target;
    //    EditorGUILayout.LabelField("Some help", "Some other text");

    //    targetPlayer.speed = EditorGUILayout.Slider("Speed", targetPlayer.speed, 0, 100);

    //    // Show default inspector property editor
    //    DrawDefaultInspector();
    //}


    //}
}


[CustomPropertyDrawer(typeof(TexImageData))]
public class TexImageDataEditer : PropertyDrawer
{

    private class PropertyData
    {
        public SerializedProperty imageProperty;
        public SerializedProperty sizeProperty;
        public SerializedProperty rextParsentProperty;

    }

    private Dictionary<string, PropertyData> _propertyDataPerPropertyPath = new Dictionary<string, PropertyData>();
    private PropertyData m_property;
    private TexImageData m_myData;

    private void OnEnable()
    {

    }

    private void Init(SerializedProperty property)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // もう作ってあるならやらない
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (_propertyDataPerPropertyPath.TryGetValue(property.propertyPath, out m_property))
        {
            return;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 新規作成する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_property = new PropertyData();
        m_property.imageProperty = property.FindPropertyRelative("image");
        m_property.sizeProperty = property.FindPropertyRelative("size");
        m_property.rextParsentProperty = property.FindPropertyRelative("rextParsent");
        _propertyDataPerPropertyPath.Add(property.propertyPath, m_property);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Init(property);

        //m_myData = this.attribute as TexImageData;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 使用可能領域
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Rect UseField = position;
        Rect fieldRect = UseField;
        float height = EditorGUIUtility.singleLineHeight;
        //int moveXNum = 0;
        int moveYNum = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // インデントされた位置のRect
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Rect indentedUseField = EditorGUI.IndentedRect(fieldRect);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Prefab化前に大きさを決める
        //*|***|***|***|***|***|***|***|***|***|***|***|
        fieldRect = indentedUseField;
        fieldRect.height = height;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Prefab化前に位置を決める
        //*|***|***|***|***|***|***|***|***|***|***|***|
        moveYNum = 0;
        for (int count = 0; count < moveYNum; count++)
        {
            fieldRect.yMin += height;
            fieldRect.yMax += height;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Prefab化した後変更加えるためPropertyScopeを使う
        //*|***|***|***|***|***|***|***|***|***|***|***|
        using (new EditorGUI.PropertyScope(fieldRect, label, property))
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ラベルを表示
            // 残りの使用可能な大きさを取る
            //*|***|***|***|***|***|***|***|***|***|***|***|
            label.text = "画像";
            fieldRect = EditorGUI.PrefixLabel(fieldRect, GUIUtility.GetControlID(FocusType.Passive), label);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // Indent = 0
            //*|***|***|***|***|***|***|***|***|***|***|***|
            var preIndent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // フィールド描画サイズ設定
            //*|***|***|***|***|***|***|***|***|***|***|***|
            var firstRect = fieldRect;
            //firstRect.width /= 2;
            firstRect.width = firstRect.width;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // フィールド描画
            //*|***|***|***|***|***|***|***|***|***|***|***|
            EditorGUI.PropertyField(firstRect, m_property.imageProperty, GUIContent.none);

        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Prefab化前に大きさを決める
        //*|***|***|***|***|***|***|***|***|***|***|***|
        fieldRect = indentedUseField;
        fieldRect.height = height;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Prefab化前に位置を決める
        //*|***|***|***|***|***|***|***|***|***|***|***|
        moveYNum = 1;
        for (int count = 0; count < moveYNum; count++)
        {
            fieldRect.yMin += height;
            fieldRect.yMax += height;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Prefab化した後変更加えるためPropertyScopeを使う
        //*|***|***|***|***|***|***|***|***|***|***|***|
        using (new EditorGUI.PropertyScope(fieldRect, label, property))
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ラベルを表示
            // 残りの使用可能な大きさを取る
            //*|***|***|***|***|***|***|***|***|***|***|***|
            label.text = "画像の大きさ";
            fieldRect = EditorGUI.PrefixLabel(fieldRect, GUIUtility.GetControlID(FocusType.Passive), label);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // Indent = 0
            //*|***|***|***|***|***|***|***|***|***|***|***|
            var preIndent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // フィールド描画サイズ設定
            //*|***|***|***|***|***|***|***|***|***|***|***|
            var firstRect = fieldRect;
            firstRect.width = firstRect.width;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // フィールド描画
            //*|***|***|***|***|***|***|***|***|***|***|***|
            EditorGUI.PropertyField(firstRect, m_property.sizeProperty, GUIContent.none);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Prefab化前に大きさを決める
        //*|***|***|***|***|***|***|***|***|***|***|***|
        fieldRect = indentedUseField;
        fieldRect.height = height * 2.0f;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Prefab化前に位置を決める
        //*|***|***|***|***|***|***|***|***|***|***|***|
        moveYNum = 2;
        for (int count = 0; count < moveYNum; count++)
        {
            fieldRect.yMin += height;
            fieldRect.yMax += height;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Prefab化した後変更加えるためPropertyScopeを使う
        //*|***|***|***|***|***|***|***|***|***|***|***|
        using (new EditorGUI.PropertyScope(fieldRect, label, property))
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ラベルを表示
            // 残りの使用可能な大きさを取る
            //*|***|***|***|***|***|***|***|***|***|***|***|
            label.text = "画像の切り取り割合";
            fieldRect = EditorGUI.PrefixLabel(fieldRect, GUIUtility.GetControlID(FocusType.Passive), label);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // Indent = 0
            //*|***|***|***|***|***|***|***|***|***|***|***|
            var preIndent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // フィールド描画サイズ設定
            //*|***|***|***|***|***|***|***|***|***|***|***|
            var firstRect = fieldRect;
            firstRect.width = firstRect.width;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // フィールド描画
            //*|***|***|***|***|***|***|***|***|***|***|***|
            EditorGUI.PropertyField(firstRect, m_property.rextParsentProperty, GUIContent.none);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        Init(property);

        return EditorGUIUtility.singleLineHeight * 4.0f;
    }
}
#endif
//using (new EditorGUI.PropertyScope(fieldRect, label, property))
//{
////*|***|***|***|***|***|***|***|***|***|***|***|
//// ラベルを表示
////*|***|***|***|***|***|***|***|***|***|***|***|
//// ラベルを表示し、ラベルの右側のプロパティを描画すべき領域のpositionを得る
//fieldRect = EditorGUI.PrefixLabel(fieldRect, GUIUtility.GetControlID(FocusType.Passive), label);
//// ここでIndentを0に
//var preIndent = EditorGUI.indentLevel;
//EditorGUI.indentLevel = 0;
//// プロパティを描画
//var firstRect = fieldRect;
//firstRect.width /= 2;
//GUIContent l_labelData = new GUIContent("DD");
////EditorGUI.LabelField(firstRect, new GUIContent( "DD"));
////EditorGUI.PropertyField(firstRect, m_property.depthProperty, GUIContent.none);
//EditorGUI.PropertyField(firstRect, m_property.depthProperty, l_labelData);
////EditorGUI.PropertyField("深さ", m_property.depthProperty);
////EditorGUI.LabelField("深さ", m_property.depthProperty);
////EditorGUILayout.IntField("深さ", m_property.depthProperty);
//EditorGUI.indentLevel = preIndent;
//}