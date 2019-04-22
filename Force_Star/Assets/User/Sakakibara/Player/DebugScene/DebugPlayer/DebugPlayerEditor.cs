using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;
using PlayerDataNum = WarehouseData.PlayerData.WarehousePlayer.PlayerData_Number;
using PlayerDataNumberList = WarehouseData.PlayerData.WarehousePlayer.PlayerData_Number_List;


using TexImageData = GameDataPublic.TexImageData;
using RenderImageData = GameDataPublic.RenderImageData;
using PartsData = GameDataPublic.PartsData;

using ListPartsData = DebugPlayer.ListPartsData;
//*|***|***|***|***|***|***|***|***|***|***|***|
// 拡張している時だけ
//*|***|***|***|***|***|***|***|***|***|***|***|
#if UNITY_EDITOR
using UnityEditor;
[CustomPropertyDrawer(typeof(ListPartsData))]
public class ListPartsDataEditer : PropertyDrawer
{

    private class PropertyData
    {
        public SerializedProperty listProperty;
    }

    private Dictionary<string, PropertyData> _propertyDataPerPropertyPath = new Dictionary<string, PropertyData>();
    private PropertyData m_property;
    private ListPartsData m_myData;
    int m_index = 0;

    float m_baseHeight = EditorGUIUtility.singleLineHeight;
    float m_indexHeight = EditorGUIUtility.singleLineHeight * 15.0f;

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


        m_property.listProperty = property.FindPropertyRelative("listData");
        m_index = m_property.listProperty.arraySize;

        if(m_index > 0)
        {
            //SerializedProperty listData = m_property.listProperty.GetFixedBufferElementAtIndex(0);
            SerializedProperty listData = m_property.listProperty.GetArrayElementAtIndex(0);
            bool getBool = listData.isExpanded;
        }



        //m_property.listProperty = property.GetFixedBufferElementAtIndex("listData");

        _propertyDataPerPropertyPath.Add(property.propertyPath, m_property);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Init(property);


        //m_myData = this.attribute as ListPartsData;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 使用可能領域
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Rect UseField = position;
        Rect fieldRect = UseField;
        float height = m_baseHeight;
        float heightListData = m_indexHeight;
        //int moveXNum = 0;
        int moveYNum = 0;
        //int moveYListNum = 0;
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
            // Indent = 0
            //*|***|***|***|***|***|***|***|***|***|***|***|
            EditorGUI.indentLevel = 0;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ラベルを表示
            // 残りの使用可能な大きさを取る
            //*|***|***|***|***|***|***|***|***|***|***|***|
            label.text = "イラストデータ";
            fieldRect = EditorGUI.PrefixLabel(fieldRect, GUIUtility.GetControlID(FocusType.Passive), label);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 表示された数
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int displayCount = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 要素リスト
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_index; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // Indent = 1
            //*|***|***|***|***|***|***|***|***|***|***|***|
            EditorGUI.indentLevel = 1;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // Prefab化前に大きさを決める
            //*|***|***|***|***|***|***|***|***|***|***|***|
            fieldRect = indentedUseField;
            fieldRect.height = heightListData;
            fieldRect.height = m_baseHeight;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // フィールド取得
            //*|***|***|***|***|***|***|***|***|***|***|***|
            SerializedProperty listData = m_property.listProperty.GetArrayElementAtIndex(index);
            bool getBool = listData.isExpanded;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // Prefab化前に位置を決める
            //*|***|***|***|***|***|***|***|***|***|***|***|
            moveYNum = 1 + index;
            //moveYListNum = index;
            for (int count = 0; count < moveYNum; count++)
            {
                fieldRect.yMin += height;
                fieldRect.yMax += height;
            }
            for (int count = 0; count < displayCount; count++)
            {
                fieldRect.yMin += heightListData - height;
                fieldRect.yMax += heightListData - height;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // フィールド描画
            //*|***|***|***|***|***|***|***|***|***|***|***|
            PlayerDataNumberList listNum = (PlayerDataNumberList)index;
            string name = listNum.ToString();
            GUIContent newLabel = new GUIContent();
            newLabel.text = name;
            listData.isExpanded = EditorGUI.Foldout(fieldRect, listData.isExpanded, newLabel);
            getBool = listData.isExpanded;



            //EditorGUI.LabelField(fieldRect, name);
            //listData
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // Prefab化前に位置を決める
            //*|***|***|***|***|***|***|***|***|***|***|***|

            //fieldRect.yMin += height;
            //fieldRect.yMax += height;

            fieldRect.height = heightListData - height;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 描画するなら
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (getBool)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // Prefab化した後変更加えるためPropertyScopeを使う
                //*|***|***|***|***|***|***|***|***|***|***|***|
                using (new EditorGUI.PropertyScope(fieldRect, label, property))
                {
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // Indent = 2
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    EditorGUI.indentLevel = 2;
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // ラベルを表示しない
                    //*|***|***|***|***|***|***|***|***|***|***|***|

                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // フィールド描画サイズ設定
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    var firstRect = fieldRect;
                    firstRect.width = firstRect.width;
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // フィールド描画
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    EditorGUI.PropertyField(firstRect, m_property.listProperty.GetArrayElementAtIndex(index), GUIContent.none, true);
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 表示された加算
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    displayCount++;
                }
            }
        }
    }





    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        Init(property);

        float baseHeight = m_baseHeight;

        float indexHeight = m_indexHeight * m_index;

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 要素リスト
        //*|***|***|***|***|***|***|***|***|***|***|***|
        indexHeight = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 要素リスト
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_index; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // フィールド取得
            //*|***|***|***|***|***|***|***|***|***|***|***|
            SerializedProperty listData = m_property.listProperty.GetArrayElementAtIndex(index);
            bool getBool = listData.isExpanded;

            if(getBool)
            {
                indexHeight += m_indexHeight;
            }
            else
            {
                indexHeight += m_baseHeight;
            }
        }

        return baseHeight + indexHeight;
    }
}

[CustomEditor(typeof(DebugPlayer))]
public class DebugPlayerEditer : Editor
{

    List<string> m_GUIDDataList;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゲームが動いているならTRUE
    //*|***|***|***|***|***|***|***|***|***|***|***|
    bool m_moveGameFlag;
    private void OnEnable()
    {
        m_GUIDDataList = new List<string>();
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DebugPlayer mytarget = (DebugPlayer)target;

        if (GUILayout.Button("ファイルに登録"))
        {
            mytarget.UpdateMakeFile();
        }

        if (GUILayout.Button("ファイルから呼び出し"))
        {
            mytarget.UpdateReadFile();
        }

        if (GUILayout.Button("ファイル更新"))
        {
            MakePartsData();
        }

        bool gameMove = EditorApplication.isPlaying;
        m_moveGameFlag = gameMove;

        if (GUILayout.Button("XXX") && m_moveGameFlag)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // GUIDを伝達する
            //*|***|***|***|***|***|***|***|***|***|***|***|
            mytarget.AnimeStudyGUID(m_GUIDDataList);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツデータ作成
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void MakePartsData()
    {
        MakePartsFile();
        MakePartsGUID();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ファイルデータ作成
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void MakePartsFile()
    {
        DebugPlayer mytarget = (DebugPlayer)target;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーだ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        string groupName = "Player";
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーのプレハブに群生する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーのプレハブを探す
        //*|***|***|***|***|***|***|***|***|***|***|***|
        string data = AssetDatabase.AssetPathToGUID("Assets/DebugScene/DebugPlayer/DebugPlayer.prefab");
        string path = AssetDatabase.GUIDToAssetPath(data);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーのプレハブの近くに作成する。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        string fileParentPath = System.IO.Path.GetDirectoryName(path);
        string fileName = groupName + "PartsFile";
        string filePathName = fileParentPath + "/" + fileName;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ファイルがもうあるか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (!AssetDatabase.IsValidFolder(filePathName))
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 無いから作る
            //*|***|***|***|***|***|***|***|***|***|***|***|
            AssetDatabase.CreateFolder(fileParentPath, fileName);
        }
        else
        {
            string[] files = System.IO.Directory.GetFiles(filePathName, "*", System.IO.SearchOption.AllDirectories);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ファイルを空にしよう。
            //*|***|***|***|***|***|***|***|***|***|***|***|
            for (int index = 0; index < files.Length; index++)
            {
                AssetDatabase.DeleteAsset(files[index]);
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // お手本を作成する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        string nameX = "prefab" + groupName + "Parts";
        string filePathX = filePathName + "/" + nameX + ".prefab";

        GameObject prefabParts = new GameObject(nameX);
        AnimePlayerSprite partsAnime = prefabParts.AddComponent<AnimePlayerSprite>();

        bool sucsess = false;
        Object prefab = PrefabUtility.SaveAsPrefabAsset(prefabParts, filePathX, out sucsess);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // PlayerDataNumberList一つに一つずつ作成する。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        PlayerDataNumberList listNum;
        string listName;
        string GUIDData;

        GameObject partsObject = null;
        m_GUIDDataList.Clear();
        for (int index = 0; index < (int)PlayerDataNumberList.NUM; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // PlayerDataNumberListの名前
            //*|***|***|***|***|***|***|***|***|***|***|***|
            listNum = (PlayerDataNumberList)index;
            listName = listNum.ToString();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // データ作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            partsObject = Instantiate(prefab) as GameObject;
            partsObject.name = listName;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ファイル作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            filePathX = filePathName + "/" + listName + ".prefab";
            PrefabUtility.SaveAsPrefabAsset(prefabParts, filePathX, out sucsess);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 後始末
            //*|***|***|***|***|***|***|***|***|***|***|***|
            DestroyImmediate(partsObject);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // GUIDを取得する
            //*|***|***|***|***|***|***|***|***|***|***|***|
            GUIDData = AssetDatabase.AssetPathToGUID(filePathX);
            m_GUIDDataList.Add(GUIDData);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 後始末
        //*|***|***|***|***|***|***|***|***|***|***|***|
        DestroyImmediate(prefabParts);

    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // IDデータ作成
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void MakePartsGUID()
    {
        DebugPlayer mytarget = (DebugPlayer)target;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーだ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        string groupName = "Player";
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーのプレハブに群生する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーのプレハブを探す
        //*|***|***|***|***|***|***|***|***|***|***|***|
        string data = AssetDatabase.AssetPathToGUID("Assets/DebugScene/DebugPlayer/DebugPlayer.prefab");
        string path = AssetDatabase.GUIDToAssetPath(data);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーのプレハブの近くに作成する。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        string fileParentPath = System.IO.Path.GetDirectoryName(path);
        string fileName = groupName + "PartsFile";
        string filePathName = fileParentPath + "/" + fileName;


        //AssetDatabase.




    }

}
#endif
//PrefabUtility.ReplacePrefab(prefabParts, prefab, ReplacePrefabOptions.ConnectToPrefab);
//PrefabUtility.CreateNew
//prefabParts.name = nameX;
//PrefabUtility.ApplyAddedGameObject(prefabParts, System.IO.Path.Combine(filePath, prefabParts.name), new InteractionMode());
//PrefabUtility.CreatePrefab(System.IO.Path.Combine(filePath, prefabParts.name), prefabParts);
//AssetDatabase.AddObjectToAsset(prefabParts, System.IO.Path.Combine(filePath, prefabParts.name));
//AssetDatabase.CreateAsset(prefabParts, System.IO.Path.Combine(filePath, nameX));
////*|***|***|***|***|***|***|***|***|***|***|***|
//// ゲームが動いているか知る
////*|***|***|***|***|***|***|***|***|***|***|***|
//private static void OnChangedPlayMode(PlayModeStateChange state)
//{
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    // ゲームが動いているならTRUE
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    if (state == PlayModeStateChange.ExitingEditMode)
//    {
//        m_moveGameFlag = true;
//    }
//    else if (state == PlayModeStateChange.EnteredPlayMode)
//    {
//        m_moveGameFlag = true;
//    }
//    else if (state == PlayModeStateChange.ExitingPlayMode)
//    {
//        m_moveGameFlag = true;
//    }
//    else if (state == PlayModeStateChange.EnteredEditMode)
//    {
//        m_moveGameFlag = true;
//    }

//}