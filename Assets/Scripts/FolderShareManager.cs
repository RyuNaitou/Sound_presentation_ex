using System.IO;
using System.IO.Compression;
using UnityEngine;

public class FolderShareManager : MonoBehaviour
{
    /// <summary>
    /// フォルダをコピーし、ZIPに圧縮して共有
    /// </summary>
    public void ShareFolderAsZip()
    {
        // 元フォルダのパス
        string sourcePath = Path.Combine(Application.persistentDataPath, CSVInfo.foldername);

        // 複製フォルダのパス
        string duplicateFolderPath = Path.Combine(Application.temporaryCachePath, "DuplicatedFolder");

        // ZIPファイルの保存先
        string zipFilePath = Path.Combine(Application.temporaryCachePath, $"{CSVInfo.foldername}.zip");

        try
        {
            // 1. フォルダを複製
            CopyDirectory(sourcePath, duplicateFolderPath);
            Debug.Log($"Folder duplicated to: {duplicateFolderPath}");

            // 2. ZIPに圧縮
            if (File.Exists(zipFilePath))
            {
                File.Delete(zipFilePath); // 既存のZIPファイルを削除
            }
            ZipFile.CreateFromDirectory(duplicateFolderPath, zipFilePath);
            Debug.Log($"Folder compressed to ZIP: {zipFilePath}");

            // 3. NativeShareで共有
            new NativeShare()
                .AddFile(zipFilePath)
                .SetSubject("Shared Folder")
                .Share();

            Debug.Log("Sharing completed.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error during folder sharing: {ex.Message}");
        }
        finally
        {
            // 複製フォルダをクリーンアップ（不要な一時フォルダの削除）
            if (Directory.Exists(duplicateFolderPath))
            {
                Directory.Delete(duplicateFolderPath, true);
                Debug.Log("Temporary folder cleaned up.");
            }
        }
    }

    /// <summary>
    /// サブフォルダを含めてフォルダ全体をコピー
    /// </summary>
    /// <param name="sourceDir">元フォルダのパス</param>
    /// <param name="destinationDir">コピー先フォルダのパス</param>
    private void CopyDirectory(string sourceDir, string destinationDir)
    {
        // コピー先フォルダを作成
        Directory.CreateDirectory(destinationDir);

        // ファイルをコピー
        foreach (var file in Directory.GetFiles(sourceDir))
        {
            string destFile = Path.Combine(destinationDir, Path.GetFileName(file));
            File.Copy(file, destFile);
        }

        // サブフォルダを再帰的にコピー
        foreach (var directory in Directory.GetDirectories(sourceDir))
        {
            string destDir = Path.Combine(destinationDir, Path.GetFileName(directory));
            CopyDirectory(directory, destDir);
        }
    }
}