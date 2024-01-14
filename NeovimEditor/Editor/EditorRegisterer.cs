using Unity.CodeEditor;
using UnityEditor;

namespace NeovimEditor.Editor {
    [InitializeOnLoad]
    public static class EditorRegisterer {
        private static readonly NeovimCodeEditor _editor = new();
        static EditorRegisterer() {
            CodeEditor.Register(_editor);
        }
    }
}