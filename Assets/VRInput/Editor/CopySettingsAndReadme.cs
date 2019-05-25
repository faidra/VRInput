using UnityEditor.Callbacks;
using System.IO;
using UnityEditor;

static class CopySettingsAndReadme
{
    // ルート以外の動作確認してないです
    [PostProcessBuild(1)]
    static void OnPostProcessBuild(BuildTarget target, string pathToBuiltProject)
    {
        var targetDirectory = Path.GetDirectoryName(pathToBuiltProject);
        CopyFiles(targetDirectory, "./", "*.yml");
        CopyFile(targetDirectory, "Readme.txt");
    }

    static void CopyFiles(string targetDir, string sourceDirectory, string pattern)
    {
        foreach (var f in Directory.GetFiles(sourceDirectory, pattern)) CopyFile(targetDir, f);
    }

    static void CopyFile(string targetDir, string fileName)
    {
        File.Copy(fileName, Path.Combine(targetDir, Path.GetFileName(fileName)), true);
    }
}