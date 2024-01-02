using Unity.CodeEditor;
using UnityEditor;

namespace NeovimEditor.Editor {
    [InitializeOnLoad]
    public static class EditorRegisterer {
        static EditorRegisterer() {
            CodeEditor.Register(new NeovimCodeEditor());
        }
    }
}