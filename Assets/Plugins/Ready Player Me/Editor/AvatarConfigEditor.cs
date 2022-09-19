using UnityEditor;
using UnityEngine;

namespace ReadyPlayerMe
{
    [CustomEditor(typeof(AvatarConfig))]
    public class AvatarConfigEditor : Editor
    {
        private AvatarConfig avatarConfigTarget;

        public override void OnInspectorGUI()
        {
            avatarConfigTarget = (AvatarConfig) target;
            DrawDefaultInspector();
            DrawMorphTargets();
        }

        private void DrawMorphTargets()
        {
            GUILayout.Space(5);
            GUILayout.Label("Morph Targets", EditorStyles.boldLabel);
            GUILayout.Space(3);
            for (var i = 0; i < avatarConfigTarget.MorphTargets.Count; i++)
            {
                DrawMorphTarget(i);
            }
            DrawAddMorphTargetButton();
        }

        private void DrawMorphTarget(int index)
        {
            GUILayout.BeginHorizontal();
            {
                EditorGUI.BeginChangeCheck();
                MorphTarget morph = (MorphTarget) EditorGUILayout.EnumPopup(avatarConfigTarget.MorphTargets[index]);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(avatarConfigTarget, "Modify Morph Target");
                    avatarConfigTarget.MorphTargets[index] = morph;
                    EditorUtility.SetDirty(avatarConfigTarget);
                }

                if (GUILayout.Button("Remove", GUILayout.Width(100)))
                {
                    Undo.RecordObject(avatarConfigTarget, "Delete Morph Target");
                    avatarConfigTarget.MorphTargets.RemoveAt(index);
                    EditorUtility.SetDirty(avatarConfigTarget);
                }
            }
            GUILayout.EndHorizontal();
        }

        private void DrawAddMorphTargetButton()
        {
            GUILayout.Space(3);
            if (GUILayout.Button("Add", GUILayout.Height(30)))
            {
                Undo.RecordObject(avatarConfigTarget, "Add Morph Target");
                avatarConfigTarget.MorphTargets.Add(MorphTarget.None);
                EditorUtility.SetDirty(avatarConfigTarget);
            }
        }
    }
}
