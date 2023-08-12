using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

namespace EmreBeratKR.LazyCoroutines.Editor
{
    [CustomEditor(typeof(LazyCoroutines.Runner))]
    public class LazyCoroutineRunnerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var routines = GetRoutines();

            EditorGUILayout.LabelField($"Running Routines Count : {routines.Count}");
            EditorGUILayout.Space();
            
            foreach (var (id, routine) in routines)
            {
                EditorGUILayout.LabelField($"[{id}] : {routine.name}");
            }
        }


        private Dictionary<uint, LazyCoroutines.Routine> GetRoutines()
        {
            return (Dictionary<uint, LazyCoroutines.Routine>) 
                typeof(LazyCoroutines)
                .GetField("Routines", BindingFlags.NonPublic | BindingFlags.Static)
                ?.GetValue(target);
        }
    }
}