//*|***|***|***|***|***|***|***|***|***|***|***|
// 拡張している時だけ
//*|***|***|***|***|***|***|***|***|***|***|***|
#if UNITY_EDITOR

using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using System.Linq;
using System.Collections.Generic;


using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;
using PlayerDataNum = WarehouseData.PlayerData.WarehousePlayer.PlayerData_Number;
using PlayerDataNumberList = WarehouseData.PlayerData.WarehousePlayer.PlayerData_Number_List;


//*|***|***|***|***|***|***|***|***|***|***|***|
// 倉庫データ
//*|***|***|***|***|***|***|***|***|***|***|***|
namespace WarehouseData
{
    using PlayerData;

    public class WarehouseEditor : EditorWindow
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 倉庫へのアクセスデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        private string m_warehousePath;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // player
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //WarehousePlayer m_playerWarehouse;
        GameObject m_selectPlayerObject;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // オブジェクトのデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        private List<string> m_GUIDData;
        private List<Object> m_ObjectData;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // スクロール
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //Vector2 m_scroll;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 起動スイッチ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        [MenuItem("Tools/WarehouseEditor")]
        static void Init()
        {
            WarehouseEditor window = TheAnimateWindow.GetWindow<WarehouseEditor>();
            window.Show();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 最初
        //*|***|***|***|***|***|***|***|***|***|***|***|
        private void OnEnable()
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // player
            //*|***|***|***|***|***|***|***|***|***|***|***|
            //m_playerWarehouse = WarehousePlayer.GetInstance();
            m_GUIDData = new List<string>();
            m_ObjectData = new List<Object>();
            //m_scroll = new Vector2(0, 0);
        }
        void OnGUI()
        {


            //*|***|***|***|***|***|***|***|***|***|***|***|
            // このスクリプトの場所を探す。
            //*|***|***|***|***|***|***|***|***|***|***|***|
            MonoScript mono = MonoScript.FromScriptableObject(this);
            string path = AssetDatabase.GetAssetPath(mono);
            string editorFilePath = System.IO.Path.GetDirectoryName(path);
            string werehouseFilePath = path.Substring(0, editorFilePath.LastIndexOf(@"/"));
            m_warehousePath = werehouseFilePath;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // player
            //*|***|***|***|***|***|***|***|***|***|***|***|
            EditorGUILayout.HelpBox("Player情報", MessageType.Info);

            EditorGUI.BeginChangeCheck();
            m_selectPlayerObject = EditorGUILayout.ObjectField("Player", m_selectPlayerObject, typeof(GameObject), true) as GameObject;
            if (EditorGUI.EndChangeCheck())
            {

            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 転換装置起動！
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (GUILayout.Button("作動開始！"))
            {
                MakePartsFile_Player();
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // プレイヤーが無いと動かないから確認しよう。
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (m_selectPlayerObject != null)
                {
                    EquipPartsFile_Player();
                }
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 転換装置起動！
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (GUILayout.Button("作動停止！"))
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // プレイヤーが無いと動かないから確認しよう。
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (m_selectPlayerObject != null)
                {
                    RemovePartsFile_Player();
                }
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ファイルデータ作成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        void MakePartsFile_Player()
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーだ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            string groupName = "Player";
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // このスクリプトの近くに作成する。
            //*|***|***|***|***|***|***|***|***|***|***|***|
            string fileParentPath = m_warehousePath;
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
            prefabParts.AddComponent<AnimePlayerSprite>();

            bool sucsess = false;
            Object prefab = PrefabUtility.SaveAsPrefabAsset(prefabParts, filePathX, out sucsess);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // PlayerDataNumberList一つに一つずつ作成する。
            //*|***|***|***|***|***|***|***|***|***|***|***|
            PlayerDataNumberList listNum;
            string listName;
            string GUIDData;
            string pathData;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // オブジェクトのデータ初期
            //*|***|***|***|***|***|***|***|***|***|***|***|
            GameObject partsObject = null;
            m_GUIDData.Clear();
            m_ObjectData.Clear();
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
                //m_ObjectData
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // GUIDを取得する
                //*|***|***|***|***|***|***|***|***|***|***|***|
                GUIDData = AssetDatabase.AssetPathToGUID(filePathX);
                m_GUIDData.Add(GUIDData);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // GUID->Path
                //*|***|***|***|***|***|***|***|***|***|***|***|
                pathData = AssetDatabase.GUIDToAssetPath(GUIDData);
                Object getObject = AssetDatabase.LoadAssetAtPath<Object>(pathData);
                m_ObjectData.Add(getObject);
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 後始末
            //*|***|***|***|***|***|***|***|***|***|***|***|
            DestroyImmediate(prefabParts);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ファイルデータ装着
        //*|***|***|***|***|***|***|***|***|***|***|***|
        void EquipPartsFile_Player()
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーが無いと動かないから確認しよう。
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_selectPlayerObject == null)
            {
                return;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // PlayerDataNumberList一つに一つずつ作成する。
            //*|***|***|***|***|***|***|***|***|***|***|***|
            PlayerDataNumberList listNum;
            string listName;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // オブジェクトのデータ初期
            //*|***|***|***|***|***|***|***|***|***|***|***|
            GameObject partsObject = null;
            Object prefab = null;
            for (int index = 0; index < m_ObjectData.Count; index++)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // PlayerDataNumberListの名前
                //*|***|***|***|***|***|***|***|***|***|***|***|
                listNum = (PlayerDataNumberList)index;
                listName = listNum.ToString();
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // プレハブ取得
                //*|***|***|***|***|***|***|***|***|***|***|***|
                prefab = m_ObjectData[index];
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // プレハブからGAMEOBJECT作成
                //*|***|***|***|***|***|***|***|***|***|***|***|
                partsObject = Instantiate(prefab) as GameObject;
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // (Clone)いらない、ぽいー
                //*|***|***|***|***|***|***|***|***|***|***|***|
                partsObject.name = listName;
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 選択中のオブジェクトを親子にする
                //*|***|***|***|***|***|***|***|***|***|***|***|
                partsObject.transform.parent = m_selectPlayerObject.transform;
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ファイルデータ装着解除
        //*|***|***|***|***|***|***|***|***|***|***|***|
        void RemovePartsFile_Player()
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーが無いと動かないから確認しよう。
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_selectPlayerObject == null)
            {
                return;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 子どもを探して
            //*|***|***|***|***|***|***|***|***|***|***|***|
            List<GameObject> getData = new List<GameObject>();
            for (int number = 0; number < m_selectPlayerObject.transform.childCount; number++)
            {
                getData.Add(m_selectPlayerObject.transform.GetChild(number).gameObject);
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 全ての親子関係を消す
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_selectPlayerObject.transform.DetachChildren();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 子どもを消す
            //*|***|***|***|***|***|***|***|***|***|***|***|
            for (int number = 0; number < getData.Count; number++)
            {
                DestroyImmediate(getData[number]);
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パスに探し到達せよ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        void FindPath()
        {
            ////*|***|***|***|***|***|***|***|***|***|***|***|
            //// アニメーションパスたち
            ////*|***|***|***|***|***|***|***|***|***|***|***|
            //m_pathData.Clear();
            //m_animeName.Clear();
            ////*|***|***|***|***|***|***|***|***|***|***|***|
            //// アニメーターに変換
            ////*|***|***|***|***|***|***|***|***|***|***|***|
            //UnityEditor.Animations.AnimatorController controller = m_animator.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
            ////*|***|***|***|***|***|***|***|***|***|***|***|
            //// AnimationClipのリスト作成
            ////*|***|***|***|***|***|***|***|***|***|***|***|
            //List<AnimationClip> clips = controller.animationClips.Distinct().ToList();
            ////*|***|***|***|***|***|***|***|***|***|***|***|
            //// AnimationClipの一つごとに
            ////*|***|***|***|***|***|***|***|***|***|***|***|
            //for (int animeNum = 0; animeNum < clips.Count; animeNum++)
            //{
            //    //*|***|***|***|***|***|***|***|***|***|***|***|
            //    // animeNum番目のアニメーション
            //    //*|***|***|***|***|***|***|***|***|***|***|***|
            //    AnimationClip clip = clips[animeNum];
            //    m_animeName.Add(clip.name);
            //    //*|***|***|***|***|***|***|***|***|***|***|***|
            //    // animeNum番目のアニメーションパスたち
            //    //*|***|***|***|***|***|***|***|***|***|***|***|
            //    List<string> getPathDataClip = new List<string>();
            //    //*|***|***|***|***|***|***|***|***|***|***|***|
            //    // animeNum番目のアニメーションをさらに
            //    // EditorCurveBindingごとに分割。
            //    // Add Propertyの一つだと思えばよい。
            //    //*|***|***|***|***|***|***|***|***|***|***|***|
            //    EditorCurveBinding[] bindings = AnimationUtility.GetCurveBindings(clip);
            //    //*|***|***|***|***|***|***|***|***|***|***|***|
            //    // EditorCurveBindingの一つごとに
            //    //*|***|***|***|***|***|***|***|***|***|***|***|
            //    for (int bindingNum = 0; bindingNum < bindings.Length; bindingNum++)
            //    {
            //        //*|***|***|***|***|***|***|***|***|***|***|***|
            //        // bindingNum番目
            //        //*|***|***|***|***|***|***|***|***|***|***|***|
            //        EditorCurveBinding binding = bindings[bindingNum];
            //        //*|***|***|***|***|***|***|***|***|***|***|***|
            //        // bindingNum番目へのパス
            //        //*|***|***|***|***|***|***|***|***|***|***|***|
            //        string getPathData = "";
            //        //*|***|***|***|***|***|***|***|***|***|***|***|
            //        // パス取得、反映
            //        //*|***|***|***|***|***|***|***|***|***|***|***|
            //        getPathData = binding.path;
            //        getPathDataClip.Add(getPathData);
            //    }
            //    //*|***|***|***|***|***|***|***|***|***|***|***|
            //    // 被りを消滅させる
            //    // パス取得、反映
            //    //*|***|***|***|***|***|***|***|***|***|***|***|
            //    getPathDataClip = getPathDataClip.Distinct().ToList();
            //    m_pathData.Add(getPathDataClip);
            //}
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // これではないだろうか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        void NearPath()
        {
            ////*|***|***|***|***|***|***|***|***|***|***|***|
            //// ターゲットを頭につけるだけ。
            ////*|***|***|***|***|***|***|***|***|***|***|***|
            //string headPath = m_animator.transform.name;
            //string headMargePath = headPath + "/";
            //string headMargePathX = headMargePath;
            ////*|***|***|***|***|***|***|***|***|***|***|***|
            //// AnimationClipの一つごとに
            ////*|***|***|***|***|***|***|***|***|***|***|***|
            //for (int animeNum = 0; animeNum < m_pathData.Count; animeNum++)
            //{
            //    //*|***|***|***|***|***|***|***|***|***|***|***|
            //    // EditorCurveBindingの一つごとに
            //    //*|***|***|***|***|***|***|***|***|***|***|***|
            //    for (int bindingNum = 0; bindingNum < m_pathData[animeNum].Count; bindingNum++)
            //    {
            //        //*|***|***|***|***|***|***|***|***|***|***|***|
            //        // つなぎ合わせる
            //        //*|***|***|***|***|***|***|***|***|***|***|***|
            //        headMargePathX = headMargePath + m_pathData[animeNum][bindingNum];
            //        m_pathWrite[animeNum][bindingNum] = headMargePathX;
            //    }
            //}
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 変換開始！
        //*|***|***|***|***|***|***|***|***|***|***|***|
        void ReplacePath()
        {
            //    //*|***|***|***|***|***|***|***|***|***|***|***|
            //    // アニメーターに変換
            //    //*|***|***|***|***|***|***|***|***|***|***|***|
            //    UnityEditor.Animations.AnimatorController controller = m_animator.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
            //    //*|***|***|***|***|***|***|***|***|***|***|***|
            //    // AnimationClipのリスト作成
            //    //*|***|***|***|***|***|***|***|***|***|***|***|
            //    List<AnimationClip> clips = controller.animationClips.Distinct().ToList();
            //    //*|***|***|***|***|***|***|***|***|***|***|***|
            //    // AnimationClipの一つごとに
            //    //*|***|***|***|***|***|***|***|***|***|***|***|
            //    for (int animeNum = 0; animeNum < clips.Count; animeNum++)
            //    {
            //        //*|***|***|***|***|***|***|***|***|***|***|***|
            //        // animeNum番目のアニメーション
            //        //*|***|***|***|***|***|***|***|***|***|***|***|
            //        AnimationClip clip = clips[animeNum];
            //        //*|***|***|***|***|***|***|***|***|***|***|***|
            //        // animeNum番目のアニメーションをさらに
            //        // EditorCurveBindingごとに分割。
            //        // Add Propertyの一つだと思えばよい。
            //        //*|***|***|***|***|***|***|***|***|***|***|***|
            //        EditorCurveBinding[] bindings = AnimationUtility.GetCurveBindings(clip);
            //        //*|***|***|***|***|***|***|***|***|***|***|***|
            //        // EditorCurveBindingの一つごとに
            //        //*|***|***|***|***|***|***|***|***|***|***|***|
            //        for (int bindingNum = 0; bindingNum < bindings.Length; bindingNum++)
            //        {
            //            //*|***|***|***|***|***|***|***|***|***|***|***|
            //            // bindingNum番目
            //            //*|***|***|***|***|***|***|***|***|***|***|***|
            //            EditorCurveBinding binding = bindings[bindingNum];

            //            //*|***|***|***|***|***|***|***|***|***|***|***|
            //            // 新しいEditorCurveBindingよ！
            //            //*|***|***|***|***|***|***|***|***|***|***|***|
            //            EditorCurveBinding newBinding = binding;


            //            //*|***|***|***|***|***|***|***|***|***|***|***|
            //            // それ以外は引き継ごう。
            //            // だったら古いEditorCurveBindingは消さないと！
            //            //*|***|***|***|***|***|***|***|***|***|***|***|
            //            AnimationCurve curve = AnimationUtility.GetEditorCurve(clip, binding);
            //            //*|***|***|***|***|***|***|***|***|***|***|***|
            //            // どの文字データが対応しているかな？
            //            //*|***|***|***|***|***|***|***|***|***|***|***|
            //            for (int charNum = 0; charNum < m_pathData[animeNum].Count; charNum++)
            //            {
            //                //*|***|***|***|***|***|***|***|***|***|***|***|
            //                // 文字データがあるなら変換
            //                //*|***|***|***|***|***|***|***|***|***|***|***|
            //                if (newBinding.path.Contains(m_pathData[animeNum][charNum]))
            //                {
            //                    newBinding.path = newBinding.path.Replace(m_pathData[animeNum][charNum], m_pathWrite[animeNum][charNum]);
            //                }
            //            }
            //            //*|***|***|***|***|***|***|***|***|***|***|***|
            //            // EditorCurveBindingを破壊する
            //            // 最後のNULLが消すサイン
            //            //*|***|***|***|***|***|***|***|***|***|***|***|
            //            AnimationUtility.SetEditorCurve(clip, binding, null);
            //            //*|***|***|***|***|***|***|***|***|***|***|***|
            //            // 新しくEditorCurveBinding再生する
            //            //*|***|***|***|***|***|***|***|***|***|***|***|
            //            AnimationUtility.SetEditorCurve(clip, newBinding, curve);
            //            //bindingNum = bindingNum;
            //        }
            //    }
        }
    }
}
#endif
