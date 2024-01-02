using System.Diagnostics;
using System.IO;

namespace NeovimEditor.Editor.Integration {
    public static class CmdRunner {
        public class Result {
            public string StandardOutput;
            public string StandardError;
            public int ExitCode;
            public string Message;
        }

        public static Result Run(string toolPath, string arguments, string workingDirectory) {
            var stdoutFileName = Path.GetTempFileName();
            var stderrFileName = Path.GetTempFileName();

            var process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.RedirectStandardInput = false;
            process.StartInfo.RedirectStandardOutput = false;
            process.StartInfo.RedirectStandardError = false;

            process.StartInfo.WorkingDirectory = workingDirectory;
            process.StartInfo.FileName = "bash";
            process.StartInfo.Arguments = string.Format("-l -c '\"{0}\" {1} 1> {2} 2> {3}'", toolPath, arguments, stdoutFileName, stderrFileName);
            process.Start();

            process.WaitForExit();

            var stdout = File.ReadAllText(stdoutFileName);
            var stderr = File.ReadAllText(stderrFileName);

            File.Delete(stdoutFileName);
            File.Delete(stderrFileName);

            var result = new Result();
            result.StandardOutput = stdout;
            result.StandardError = stderr;
            result.ExitCode = process.ExitCode;

            var messagePrefix = result.ExitCode == 0 ? "Command executed successfully" : "Failed to run command";
            result.Message = string.Format("{0}: '{1} {2}'\nstdout: {3}\nstderr: {4}\nExit code: {5}", messagePrefix, toolPath, arguments, stdout, stderr, process.ExitCode);

            return result;
        }
    }
}