using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;

public class PostProcessBuild
{
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget target, string pathToBuildProject)
    {
        if (target == BuildTarget.iOS)
        {
            string plistPath = Path.Combine(pathToBuildProject, "Info.plist");

            var plist = new UnityEditor.iOS.Xcode.PlistDocument();
            plist.ReadFromFile(plistPath);

            // "UIFileSharingEnabled" (iTunes File Sharing) を追加
            plist.root.SetBoolean("UIFileSharingEnabled", true);

            // "LSSupportsOpeningDocumentsInPlace" を追加
            plist.root.SetBoolean("LSSupportsOpeningDocumentsInPlace", true);

            // "NSMotionUsageDescription" (Privacy - Motion Usage Description) を追加
            plist.root.SetString("NSMotionUsageDescription", "Trying the HeadphoneAPI");

            // 保存
            plist.WriteToFile(plistPath);
        }
    }
}
