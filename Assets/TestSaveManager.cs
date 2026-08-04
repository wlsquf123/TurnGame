using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class ObjectSaveData
{
    public int prefabIndex;
    public Vector3 position;
}

[System.Serializable]
public class SaveData
{
    public List<ObjectSaveData> objects = new List<ObjectSaveData>();
}

public class TestSaveManager : MonoBehaviour
{
    public GameObject[] prefabs;

    private string savePath;

    private void Start()
    {
        savePath =
            Application.persistentDataPath +
            "/ObjectSave.json";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            Load();
        }
    }

    private void Save()
    {
        SaveData saveData = new SaveData();

        SaveTarget[] targets =
            FindObjectsByType<SaveTarget>(
                FindObjectsSortMode.None
            );

        foreach (SaveTarget target in targets)
        {
            ObjectSaveData objectData =
                new ObjectSaveData();

            objectData.prefabIndex =
                target.prefabIndex;

            objectData.position =
                target.transform.position;

            saveData.objects.Add(objectData);
        }

        string json =
            JsonUtility.ToJson(saveData, true);

        File.WriteAllText(savePath, json);

        Debug.Log(
            saveData.objects.Count +
            "개 오브젝트 저장 완료"
        );
    }

    private void Load()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("저장 파일이 없습니다.");
            return;
        }

        string json =
            File.ReadAllText(savePath);

        SaveData saveData =
            JsonUtility.FromJson<SaveData>(json);

        // 현재 오브젝트 전부 제거
        SaveTarget[] currentTargets =
            FindObjectsByType<SaveTarget>(
                FindObjectsSortMode.None
            );

        foreach (SaveTarget target in currentTargets)
        {
            Destroy(target.gameObject);
        }

        // 저장된 종류와 위치대로 다시 생성
        foreach (ObjectSaveData objectData
                 in saveData.objects)
        {
            if (objectData.prefabIndex < 0 ||
                objectData.prefabIndex >= prefabs.Length)
            {
                continue;
            }

            Instantiate(
                prefabs[objectData.prefabIndex],
                objectData.position,
                Quaternion.identity
            );
        }

        Debug.Log(
            saveData.objects.Count +
            "개 오브젝트 불러오기 완료"
        );
    }
}