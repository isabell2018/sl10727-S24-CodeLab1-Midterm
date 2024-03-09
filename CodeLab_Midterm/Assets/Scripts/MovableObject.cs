using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    private const string FILE_DIR = "/SAVEDATA/";

    private string FILE_NAME = "<name>.json";

    private string FILE_PATH;
    
    // Start is called before the first frame update
    void Start()
    {
        FILE_NAME = FILE_NAME.Replace("<name>", name);
        FILE_PATH = Application.dataPath + FILE_DIR + FILE_NAME;
        if (File.Exists(FILE_PATH))
        {
            string jsonString = File.ReadAllText(FILE_PATH);
            transform.position = JsonUtility.FromJson<Vector3>(jsonString);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition();
        
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 result = Input.mousePosition;
        result.z = Camera.main.WorldToScreenPoint(transform.position).z;
        result = Camera.main.ScreenToWorldPoint(result);
        return result;
    }

    private void OnApplicationQuit()
    {
        string fileContent = JsonUtility.ToJson(transform.position, true);
        Debug.Log(fileContent);
        File.WriteAllText(FILE_PATH,fileContent);
    }
}
