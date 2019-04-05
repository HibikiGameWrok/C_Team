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
//public class TexEditorWindow : EditorWindow
//{
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    // 起動スイッチ
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    [MenuItem("Tools/SomeToolWindow")]
//    void Open()
//    {
//        GetWindow<TexEditorWindow>("タブに表示したいタイトル");
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
#endif

