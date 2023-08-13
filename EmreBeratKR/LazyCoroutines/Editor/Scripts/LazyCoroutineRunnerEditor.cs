using System.Linq;

namespace EmreBeratKR.LazyCoroutines.Editor
{
    [UnityEditor.CustomEditor(typeof(LazyCoroutines.Runner))]
    public class LazyCoroutineRunnerEditor : UnityEditor.Editor
    {
        private static readonly UnityEngine.GUIStyle SearchBarStyle = new("ToolbarSeachTextField");


        private string m_SearchBarText = "";
        private bool m_TaggedFoldout;
        private bool m_UntaggedFoldout;
        
        
        public override void OnInspectorGUI()
        {
            var routines = GetRoutines();
            var taggedRoutines = routines
                .Where(routine => routine.Value.IsTagged)
                .ToArray();
            var untaggedRoutines = routines
                .Where(routine => !routine.Value.IsTagged)
                .ToArray();

            OnRunningRoutineCountGUI(routines.Count);
            UnityEditor.EditorGUILayout.Space();
            OnSearchbarGUI();
            UnityEditor.EditorGUILayout.Space();
            OnTaggedRoutineFoldoutGUI(taggedRoutines);
            UnityEditor.EditorGUILayout.Space();
            OnUntaggedRoutineFoldoutGUI(untaggedRoutines);
            
        }
        
        public override bool RequiresConstantRepaint()
        {
            return true;
        }


        private void OnSearchbarGUI()
        {
            m_SearchBarText = UnityEditor.EditorGUILayout.TextField(m_SearchBarText, SearchBarStyle);
        }
        
        private void OnRunningRoutineCountGUI(int count)
        {
            UnityEditor.EditorGUILayout.LabelField($"Running Routines Count : {count}");
        }

        private void OnTaggedRoutineFoldoutGUI(System.Collections.Generic.KeyValuePair<uint, LazyCoroutines.Routine>[] routines)
        {
            m_TaggedFoldout = UnityEditor.EditorGUILayout.Foldout(m_TaggedFoldout, $"Tagged Routines ({routines.Length})");

            if (m_TaggedFoldout)
            {
                var isEmpty = true;
                UnityEditor.EditorGUI.indentLevel += 1;
                
                foreach (var (id, routine) in routines)
                {
                    var label = $"[{routine.tag}][{id}] : {routine.name}";
                    
                    if (!IsMatchWithSearch(label)) continue;

                    UnityEditor.EditorGUILayout.LabelField(label);
                    isEmpty = false;
                }

                if (isEmpty)
                {
                    UnityEditor.EditorGUILayout.LabelField(IsSearching() ? "No Match" : "Empty");
                }
                
                UnityEditor.EditorGUI.indentLevel -= 1;
            }
        }

        private void OnUntaggedRoutineFoldoutGUI(System.Collections.Generic.KeyValuePair<uint, LazyCoroutines.Routine>[] routines)
        {
            m_UntaggedFoldout = UnityEditor.EditorGUILayout.Foldout(m_UntaggedFoldout, $"Untagged Routines ({routines.Length})");

            if (m_UntaggedFoldout)
            {
                var isEmpty = true;
                UnityEditor.EditorGUI.indentLevel += 1;
                
                foreach (var (id, routine) in routines)
                {
                    var label = $"[{id}] : {routine.name}";
                    
                    if (!IsMatchWithSearch(label)) continue;
                    
                    UnityEditor.EditorGUILayout.LabelField(label);
                    isEmpty = false;
                }

                if (isEmpty)
                {
                    UnityEditor.EditorGUILayout.LabelField(IsSearching() ? "No Match" : "Empty");
                }
                
                UnityEditor.EditorGUI.indentLevel -= 1;
            }
        }

        private bool IsMatchWithSearch(string text)
        {
            return text.ToLower().Contains(m_SearchBarText.ToLower());
        }
        
        private bool IsSearching()
        {
            return !string.IsNullOrEmpty(m_SearchBarText);
        }
        
        private System.Collections.Generic.Dictionary<uint, LazyCoroutines.Routine> GetRoutines()
        {
            return (System.Collections.Generic.Dictionary<uint, LazyCoroutines.Routine>) 
                typeof(LazyCoroutines)
                .GetField("Routines", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                ?.GetValue(target);
        }
    }
}