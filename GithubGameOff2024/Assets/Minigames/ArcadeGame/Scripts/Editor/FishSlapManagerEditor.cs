using UnityEngine;
using UnityEditor;

namespace ArcadePlatformer
{
    [CustomEditor(typeof(FishSlapManager))]
    public class FishSlapManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            FishSlapManager script = (FishSlapManager)target;

            if (GUILayout.Button("Get Collectables"))
            {
                var toReg = script.FindCollectables();
                for (int i = 0; i < toReg.Length; i++)
                {
                    EditorUtility.SetDirty(toReg[i]);
                }
                
            }

            if (GUILayout.Button("Get Checkpoints"))
            {
                var toReg = script.FindCheckPoints();
                for (int i = 0; i < toReg.Length; i++)
                {
                    EditorUtility.SetDirty(toReg[i]);
                }
            }
        }
    }
}