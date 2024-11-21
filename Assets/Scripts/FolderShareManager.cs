using System.IO;
using System.IO.Compression;
using UnityEngine;

public class FolderShareManager : MonoBehaviour
{
    /// <summary>
    /// �t�H���_���R�s�[���AZIP�Ɉ��k���ċ��L
    /// </summary>
    public void ShareFolderAsZip()
    {
        // ���t�H���_�̃p�X
        string sourcePath = Path.Combine(Application.persistentDataPath, CSVInfo.foldername);

        // �����t�H���_�̃p�X
        string duplicateFolderPath = Path.Combine(Application.temporaryCachePath, "DuplicatedFolder");

        // ZIP�t�@�C���̕ۑ���
        string zipFilePath = Path.Combine(Application.temporaryCachePath, "SharedData.zip");

        try
        {
            // 1. �t�H���_�𕡐�
            CopyDirectory(sourcePath, duplicateFolderPath);
            Debug.Log($"Folder duplicated to: {duplicateFolderPath}");

            // 2. ZIP�Ɉ��k
            if (File.Exists(zipFilePath))
            {
                File.Delete(zipFilePath); // ������ZIP�t�@�C�����폜
            }
            ZipFile.CreateFromDirectory(duplicateFolderPath, zipFilePath);
            Debug.Log($"Folder compressed to ZIP: {zipFilePath}");

            // 3. NativeShare�ŋ��L
            new NativeShare()
                .AddFile(zipFilePath)
                .SetSubject("Shared Folder")
                .SetText("Here is the shared folder data.")
                .Share();

            Debug.Log("Sharing completed.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error during folder sharing: {ex.Message}");
        }
        finally
        {
            // �����t�H���_���N���[���A�b�v�i�s�v�Ȉꎞ�t�H���_�̍폜�j
            if (Directory.Exists(duplicateFolderPath))
            {
                Directory.Delete(duplicateFolderPath, true);
                Debug.Log("Temporary folder cleaned up.");
            }
        }
    }

    /// <summary>
    /// �T�u�t�H���_���܂߂ăt�H���_�S�̂��R�s�[
    /// </summary>
    /// <param name="sourceDir">���t�H���_�̃p�X</param>
    /// <param name="destinationDir">�R�s�[��t�H���_�̃p�X</param>
    private void CopyDirectory(string sourceDir, string destinationDir)
    {
        // �R�s�[��t�H���_���쐬
        Directory.CreateDirectory(destinationDir);

        // �t�@�C�����R�s�[
        foreach (var file in Directory.GetFiles(sourceDir))
        {
            string destFile = Path.Combine(destinationDir, Path.GetFileName(file));
            File.Copy(file, destFile);
        }

        // �T�u�t�H���_���ċA�I�ɃR�s�[
        foreach (var directory in Directory.GetDirectories(sourceDir))
        {
            string destDir = Path.Combine(destinationDir, Path.GetFileName(directory));
            CopyDirectory(directory, destDir);
        }
    }
}
