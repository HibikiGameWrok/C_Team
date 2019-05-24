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

public class TheAnimateWindow : EditorWindow
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 起動中のアニメーター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    Animator m_animator;
    GameObject m_target;
    List<string> m_animeName;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パスデータ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    List<List<string>> m_pathData;
    List<List<string>> m_pathWrite;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // スクロール
    //*|***|***|***|***|***|***|***|***|***|***|***|
    Vector2 m_scroll;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 起動スイッチ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [MenuItem("Tools/TheAnimateWindow")]
    static void Init()
    {
        TheAnimateWindow window = TheAnimateWindow.GetWindow<TheAnimateWindow>();
        window.Show();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 最初
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private void OnEnable()
    {
        m_animeName = new List<string>();
        m_pathData = new List<List<string>>();
        m_pathWrite = new List<List<string>>();
        m_scroll = new Vector2(0, 0);
    }
    void OnGUI()
    {
        if (m_animator == null)
        {
            EditorGUILayout.HelpBox("GameObject入れてください", MessageType.Info);
        }
        EditorGUI.BeginChangeCheck();
        m_animator = EditorGUILayout.ObjectField("animator", m_animator, typeof(Animator), true) as Animator;
        if (EditorGUI.EndChangeCheck())
        {
            FindPath();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // m_pathDataの空白コピー作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_pathWrite.Clear();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // AnimationClipの一つごとに
            //*|***|***|***|***|***|***|***|***|***|***|***|
            for (int animeNum = 0; animeNum < m_pathData.Count; animeNum++)
            {
                List<string> makeWriteDataClip = new List<string>();
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // EditorCurveBindingの一つごとに
                //*|***|***|***|***|***|***|***|***|***|***|***|
                for (int bindingNum = 0; bindingNum < m_pathData[animeNum].Count; bindingNum++)
                {
                    makeWriteDataClip.Add("");
                }
                m_pathWrite.Add(makeWriteDataClip);
            }
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
            // データがあるか？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_pathData.Count != 0 && m_pathWrite.Count != 0)
            {
                EditorGUILayout.HelpBox("左が古いPATH 右に新しいPATH", MessageType.Info);
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // AnimationClipの一つごとに
            //*|***|***|***|***|***|***|***|***|***|***|***|
            for (int animeNum = 0; animeNum < m_pathData.Count; animeNum++)
            {
                EditorGUILayout.HelpBox(m_animeName[animeNum] + "の変更だ", MessageType.Info);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // EditorCurveBindingの一つごとに
                //*|***|***|***|***|***|***|***|***|***|***|***|
                for (int bindingNum = 0; bindingNum < m_pathData[animeNum].Count; bindingNum++)
                {
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // テキストボックス一つ
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_pathWrite[animeNum][bindingNum] = EditorGUILayout.TextField(m_pathData[animeNum][bindingNum], m_pathWrite[animeNum][bindingNum]);
                }
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // データがあるか？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_pathData.Count != 0 && m_pathWrite.Count != 0)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 転換装置起動！
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (GUILayout.Button("作動開始！"))
                {
                    ReplacePath();
                    FindPath();
                }
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // おそらく装置起動！
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (GUILayout.Button("おそらくこれではないだろうか？"))
                {
                    NearPath();
                }
            }
        }




    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パスに探し到達せよ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void FindPath()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // アニメーションパスたち
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_pathData.Clear();
        m_animeName.Clear();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // アニメーターに変換
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UnityEditor.Animations.AnimatorController controller = m_animator.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // AnimationClipのリスト作成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        List<AnimationClip> clips = controller.animationClips.Distinct().ToList();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // AnimationClipの一つごとに
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int animeNum = 0; animeNum < clips.Count; animeNum++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // animeNum番目のアニメーション
            //*|***|***|***|***|***|***|***|***|***|***|***|
            AnimationClip clip = clips[animeNum];
            m_animeName.Add(clip.name);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // animeNum番目のアニメーションパスたち
            //*|***|***|***|***|***|***|***|***|***|***|***|
            List<string> getPathDataClip = new List<string>();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // animeNum番目のアニメーションをさらに
            // EditorCurveBindingごとに分割。
            // Add Propertyの一つだと思えばよい。
            //*|***|***|***|***|***|***|***|***|***|***|***|
            EditorCurveBinding[] bindings = AnimationUtility.GetCurveBindings(clip);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // EditorCurveBindingの一つごとに
            //*|***|***|***|***|***|***|***|***|***|***|***|
            for (int bindingNum = 0; bindingNum < bindings.Length; bindingNum++)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // bindingNum番目
                //*|***|***|***|***|***|***|***|***|***|***|***|
                EditorCurveBinding binding = bindings[bindingNum];
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // bindingNum番目へのパス
                //*|***|***|***|***|***|***|***|***|***|***|***|
                string getPathData = "";
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // パス取得、反映
                //*|***|***|***|***|***|***|***|***|***|***|***|
                getPathData = binding.path;
                getPathDataClip.Add(getPathData);
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 被りを消滅させる
            // パス取得、反映
            //*|***|***|***|***|***|***|***|***|***|***|***|
            getPathDataClip = getPathDataClip.Distinct().ToList();
            m_pathData.Add(getPathDataClip);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これではないだろうか？
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void NearPath()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ターゲットを頭につけるだけ。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        string headPath = m_animator.transform.name;
        string headMargePath = headPath + "/";
        string headMargePathX = headMargePath;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // AnimationClipの一つごとに
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int animeNum = 0; animeNum < m_pathData.Count; animeNum++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // EditorCurveBindingの一つごとに
            //*|***|***|***|***|***|***|***|***|***|***|***|
            for (int bindingNum = 0; bindingNum < m_pathData[animeNum].Count; bindingNum++)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // つなぎ合わせる
                //*|***|***|***|***|***|***|***|***|***|***|***|
                headMargePathX = headMargePath + m_pathData[animeNum][bindingNum];
                m_pathWrite[animeNum][bindingNum] = headMargePathX;
            }
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 変換開始！
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void ReplacePath()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // アニメーターに変換
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UnityEditor.Animations.AnimatorController controller = m_animator.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // AnimationClipのリスト作成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        List<AnimationClip> clips = controller.animationClips.Distinct().ToList();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // AnimationClipの一つごとに
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int animeNum = 0; animeNum < clips.Count; animeNum++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // animeNum番目のアニメーション
            //*|***|***|***|***|***|***|***|***|***|***|***|
            AnimationClip clip = clips[animeNum];
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // animeNum番目のアニメーションをさらに
            // EditorCurveBindingごとに分割。
            // Add Propertyの一つだと思えばよい。
            //*|***|***|***|***|***|***|***|***|***|***|***|
            EditorCurveBinding[] bindings = AnimationUtility.GetCurveBindings(clip);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // EditorCurveBindingの一つごとに
            //*|***|***|***|***|***|***|***|***|***|***|***|
            for (int bindingNum = 0; bindingNum < bindings.Length; bindingNum++)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // bindingNum番目
                //*|***|***|***|***|***|***|***|***|***|***|***|
                EditorCurveBinding binding = bindings[bindingNum];

                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 新しいEditorCurveBindingよ！
                //*|***|***|***|***|***|***|***|***|***|***|***|
                EditorCurveBinding newBinding = binding;


                //*|***|***|***|***|***|***|***|***|***|***|***|
                // それ以外は引き継ごう。
                // だったら古いEditorCurveBindingは消さないと！
                //*|***|***|***|***|***|***|***|***|***|***|***|
                AnimationCurve curve = AnimationUtility.GetEditorCurve(clip, binding);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // どの文字データが対応しているかな？
                //*|***|***|***|***|***|***|***|***|***|***|***|
                for (int charNum = 0; charNum < m_pathData[animeNum].Count; charNum++)
                {
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 文字データがあるなら変換
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    if (newBinding.path.Contains(m_pathData[animeNum][charNum]))
                    {
                        newBinding.path = newBinding.path.Replace(m_pathData[animeNum][charNum], m_pathWrite[animeNum][charNum]);
                    }
                }
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // EditorCurveBindingを破壊する
                // 最後のNULLが消すサイン
                //*|***|***|***|***|***|***|***|***|***|***|***|
                AnimationUtility.SetEditorCurve(clip, binding, null);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 新しくEditorCurveBinding再生する
                //*|***|***|***|***|***|***|***|***|***|***|***|
                AnimationUtility.SetEditorCurve(clip, newBinding, curve);
                //bindingNum = bindingNum;
            }
        }
    }
}




#endif

