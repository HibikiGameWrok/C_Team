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

public class RocketPartsPointMake : EditorWindow
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // Playシーンのディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlaySceneDirector m_playDirector;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ロケット候補地点
    //*|***|***|***|***|***|***|***|***|***|***|***|
    List<Vector3> m_rocketPartsCandidatePosition;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 書いたデータ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    List<List<string>> m_rocketPartsStringPosition;
    enum Vector3ID
    {
        ID_X,
        ID_Y,
        ID_Z,
        NUM,
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // スクロール
    //*|***|***|***|***|***|***|***|***|***|***|***|
    Vector2 m_scroll;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 起動スイッチ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [MenuItem("Tools/RocketPartsPointMake")]
    static void Init()
    {
        RocketPartsPointMake window = RocketPartsPointMake.GetWindow<RocketPartsPointMake>();
        window.Show();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 最初
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private void OnEnable()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Playシーンのディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playDirector = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ロケット候補地点
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_rocketPartsCandidatePosition = new List<Vector3>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 書いたデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_rocketPartsStringPosition = new List<List<string>>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // スクロール
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_scroll = new Vector2(0, 0);
    }
    void OnGUI()
    {
        if (m_playDirector == null)
        {
            EditorGUILayout.HelpBox("GameObject入れてください", MessageType.Info);
        }
        EditorGUI.BeginChangeCheck();
        m_playDirector = EditorGUILayout.ObjectField("PlaySceneDirector", m_playDirector, typeof(PlaySceneDirector), true) as PlaySceneDirector;
        if (EditorGUI.EndChangeCheck())
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // これで一緒だね。
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_rocketPartsCandidatePosition = m_playDirector.GetPointCandidatePosition();
        }
        Vector2 max = maxSize;
        Vector2 min = minSize;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // スクロールバーあり描画
        //*|***|***|***|***|***|***|***|***|***|***|***|
        using (EditorGUILayout.ScrollViewScope scrollview = new EditorGUILayout.ScrollViewScope(m_scroll))
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // スクロール更新
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_scroll = scrollview.scrollPosition;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 情報があるなら
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_playDirector)
            {


                //*|***|***|***|***|***|***|***|***|***|***|***|
                // ファイル読み込み
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (GUILayout.Button("ファイル読み込み！"))
                {
                    ReadRocketPartsData();
                }
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // ファイル書き出し
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (GUILayout.Button("ファイル書き出し"))
                {
                    MakeRocketPartsData();
                }
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 情報更新後
                //*|***|***|***|***|***|***|***|***|***|***|***|
                UpdateRocketPartsDataBefore();
                EditorGUILayout.HelpBox("ここから下のデータは候補地点のデータ", MessageType.Info);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 候補地点追加
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (GUILayout.Button("候補地点の追加"))
                {
                    AddPosiitonRocketPartsData();
                }

                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 編集情報
                //*|***|***|***|***|***|***|***|***|***|***|***|
                for (int index = 0; index < m_rocketPartsStringPosition.Count; index++) 
                {
                    EditorGUILayout.LabelField("Position_" + index);
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 編集できるよ。
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_rocketPartsStringPosition[index][(int)Vector3ID.ID_X] = EditorGUILayout.TextField("Position_" + index + "_x", m_rocketPartsStringPosition[index][(int)Vector3ID.ID_X]);
                    m_rocketPartsStringPosition[index][(int)Vector3ID.ID_Y] = EditorGUILayout.TextField("Position_" + index + "_y", m_rocketPartsStringPosition[index][(int)Vector3ID.ID_Y]);
                    m_rocketPartsStringPosition[index][(int)Vector3ID.ID_Z] = EditorGUILayout.TextField("Position_" + index + "_z", m_rocketPartsStringPosition[index][(int)Vector3ID.ID_Z]);
                }
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 情報更新後
                //*|***|***|***|***|***|***|***|***|***|***|***|
                UpdateRocketPartsDataAfter();
            }




        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 情報更新前
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateRocketPartsDataBefore()
    {
        Vector3 pointData;
        List<string> listData;
        m_rocketPartsStringPosition.Clear();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データ生存確認
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_rocketPartsCandidatePosition == null)
        {
            return;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 編集情報
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_rocketPartsCandidatePosition.Count; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            listData = new List<string>();
            pointData = m_rocketPartsCandidatePosition[index];
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 追加
            //*|***|***|***|***|***|***|***|***|***|***|***|
            listData.Add(pointData.x.ToString());
            listData.Add(pointData.y.ToString());
            listData.Add(pointData.z.ToString());
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 適用
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_rocketPartsStringPosition.Add(listData);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 情報更新後
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateRocketPartsDataAfter()
    {
        Vector3 pointData;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データ生存確認
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_rocketPartsCandidatePosition == null)
        {
            return;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 編集情報
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_rocketPartsCandidatePosition.Count; index++)
        {
            pointData = m_rocketPartsCandidatePosition[index];
            pointData.x = float.Parse(m_rocketPartsStringPosition[index][(int)Vector3ID.ID_X]);
            pointData.y = float.Parse(m_rocketPartsStringPosition[index][(int)Vector3ID.ID_Y]);
            pointData.z = float.Parse(m_rocketPartsStringPosition[index][(int)Vector3ID.ID_Z]);
            m_rocketPartsCandidatePosition[index] = pointData;
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 候補地点追加
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AddPosiitonRocketPartsData()
    {
        Vector3 pointData;
        List<string> listData;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データ生存確認
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_rocketPartsCandidatePosition == null)
        {
            return;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 編集情報
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 作成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        listData = new List<string>();
        pointData = Vector3.zero;
        m_rocketPartsCandidatePosition.Add(pointData);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 追加
        //*|***|***|***|***|***|***|***|***|***|***|***|
        listData.Add(pointData.x.ToString());
        listData.Add(pointData.y.ToString());
        listData.Add(pointData.z.ToString());
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 適用
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_rocketPartsStringPosition.Add(listData);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ファイル読み込み
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void ReadRocketPartsData()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ファイル読み込み
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playDirector.UpdateReadFile();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ファイル書き出し
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void MakeRocketPartsData()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ファイル書き出し
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playDirector.UpdateMakeFile();
    }
}




#endif

