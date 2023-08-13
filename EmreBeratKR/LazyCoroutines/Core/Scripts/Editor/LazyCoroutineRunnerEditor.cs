namespace EmreBeratKR.LazyCoroutines.Editor
{
    [UnityEditor.CustomEditor(typeof(LazyCoroutines.Runner))]
    public class LazyCoroutineRunnerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var routines = GetRoutines();

            UnityEditor.EditorGUILayout.LabelField($"Running Routines Count : {routines.Count}");
            UnityEditor.EditorGUILayout.Space();
            
            foreach (var (id, routine) in routines)
            {
                UnityEditor.EditorGUILayout.LabelField($"[{id}] : {routine.name}");
            }
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