using System.Linq;

namespace EmreBeratKR.LazyCoroutines.Editor
{
    [UnityEditor.CustomEditor(typeof(LazyCoroutines.Runner))]
    public class LazyCoroutineRunnerEditor : UnityEditor.Editor
    {
        private bool m_TaggedFoldout;
        private bool m_UntaggedFoldout;
        
        
        public override void OnInspectorGUI()
        {
            var routines = GetRoutines();
            var taggedRoutines = routines.Where(routine => routine.Value.IsTagged);
            var untaggedRoutines = routines.Where(routine => !routine.Value.IsTagged);

            UnityEditor.EditorGUILayout.LabelField($"Running Routines Count : {routines.Count}");
            UnityEditor.EditorGUILayout.Space();

            m_TaggedFoldout = UnityEditor.EditorGUILayout.Foldout(m_TaggedFoldout, "Tagged Routines");

            if (m_TaggedFoldout)
            {
                UnityEditor.EditorGUI.indentLevel += 1;
                
                foreach (var (id, routine) in taggedRoutines)
                {
                    var label = $"[{routine.tag}][{id}] : {routine.name}";
                    UnityEditor.EditorGUILayout.LabelField(label);
                }
                
                UnityEditor.EditorGUI.indentLevel -= 1;
            }
            
            UnityEditor.EditorGUILayout.Space();
            m_UntaggedFoldout = UnityEditor.EditorGUILayout.Foldout(m_UntaggedFoldout, "Untagged Routines");

            if (m_UntaggedFoldout)
            {
                UnityEditor.EditorGUI.indentLevel += 1;
                
                foreach (var (id, routine) in untaggedRoutines)
                {
                    var label = $"[{id}] : {routine.name}";
                    UnityEditor.EditorGUILayout.LabelField(label);
                }
                
                UnityEditor.EditorGUI.indentLevel -= 1;
            }
        }
        
        public override bool RequiresConstantRepaint()
        {
            return true;
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