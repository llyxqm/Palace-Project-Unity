using UnityEngine;
using Mono.Data.Sqlite; 
using System.IO;        
using System;

public class Sqlite_mg: MonoBehaviour
{
    private string dbPath;

    private void Awake()
    {

        dbPath = "URI=file:" + Application.persistentDataPath + "/palace.db";


        CopyDatabaseIfNeeded();

    }

    private void CopyDatabaseIfNeeded()
    {
        string targetPath = Path.Combine(Application.persistentDataPath, "palace.db");
        //if (!File.Exists(targetPath))
        //{
            string sourcePath = Path.Combine(Application.streamingAssetsPath, "palace.db");

            File.Copy(sourcePath, targetPath);
            Debug.Log("数据库初始化成功！");
        //}
    }
}