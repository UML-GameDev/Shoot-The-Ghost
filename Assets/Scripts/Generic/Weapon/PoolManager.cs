using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    public GameObject objPrefab;
    public int objAmount = 20;

    private List<GameObject> objs;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;

        // Preload objs
        objs = new List<GameObject>(objAmount);

        for (int i = 0; i < objAmount; i++)
        {
            GameObject prefabInstance = Instantiate(objPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            objs.Add(prefabInstance);
        }
    }

    public GameObject GetBullet()
    {
        foreach (GameObject obj in objs)
        {
            if (!obj.activeInHierarchy)
            {
                // This finds a bullet that isn't active, and activates it
                obj.SetActive(true);
                return obj;
            }
        }
        // This brings the bullet into the gameworld
        GameObject prefabInstance = Instantiate(objPrefab);
        // I don't know what this does
        prefabInstance.transform.SetParent(transform);
        objs.Add(prefabInstance);

        return prefabInstance;
    }
}
