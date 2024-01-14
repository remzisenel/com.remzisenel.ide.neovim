using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace NeovimEditor.Editor {
    internal class ScriptEditorData : ScriptableSingleton<ScriptEditorData> {
        // activeBuildTargetChanged has changed
        // making it true by default would cause multiple Sync projects on the startup
        [SerializeField] internal bool hasChanges;
        [SerializeField] internal string[] activeScriptCompilationDefines;

        public void InvalidateSavedCompilationDefines() {
            activeScriptCompilationDefines = EditorUserBuildSettings.activeScriptCompilationDefines;
        }

        public bool HasChangesInCompilationDefines() {
            return activeScriptCompilationDefines != null && !EditorUserBuildSettings.activeScriptCompilationDefines.SequenceEqual(activeScriptCompilationDefines);
        }
    }
}