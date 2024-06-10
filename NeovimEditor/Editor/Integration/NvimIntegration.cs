namespace NeovimEditor.Editor.Integration {
    public static class NvimIntegration {
        public static bool GetNvimExecutablePath(out string nvimExecutablePath) {
            UnityEngine.Debug.Log("<color=\"grey\">NvimIntegration.GetNvimExecutablePath</color>");
            var res = CmdRunner.Run("which", "nvim", ".");
            if (res.ExitCode != 0) {
                UnityEngine.Debug.Log("<color=\"red\">Error in GetNvimExecutablePath: " + res.StandardOutput + "</color>");
                nvimExecutablePath = "";
                return false;
            } else {
                nvimExecutablePath = res.StandardOutput;
                return true;
            }
        }

        public static void OpenFile(string nvimExecutable, string slnFile, string path, int line, int column) {
            UnityEngine.Debug.Log("<color=\"grey\">NvimIntegration.OpenFile</color>");
            var res = CmdRunner.Run("nvim", $"--server /tmp/nvimsocket --remote-send \"<C-\\><C-n>:n {path}<CR>:{line}<CR>\"", ".");
            if (res.ExitCode != 0) {
                UnityEngine.Debug.Log("<color=\"red\">Error in OpenFile: " + res.StandardError + "</color>");
            }

        }

        public static void SyncProject(string nvimExecutable) {
            /*UnityEngine.Debug.Log("<color=\"grey\">NvimIntegration.RestartOmnisharp</color>");*/
            /*var res = CmdRunner.Run("nvim", $"--server /tmp/nvimsocket --remote-send \"<C-\\><C-n>:LspRestart<CR>\"", ".");*/
            /*if (res.ExitCode != 0) {*/
            /*    UnityEngine.Debug.Log("<color=\"red\">Error in RestartOmnisharp: " + res.StandardError + "</color>");*/
            /*}*/
        }
    }

}