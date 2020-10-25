using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PoolManager : MonoBehaviour
{
    public int currentCount { get; private set; }
    public int objectAmount { get; private set; }

    public GameObject objectPrefab;
    public GunData data;

    private List<GameObject> objects;

    [HideInInspector] public UnityEvent<int> OnCountUpdate;

    // Start is called before the first frame update
    void Awake()
    {
        objectAmount = data.maxAmmo;
        currentCount = objectAmount;
        // Preload objects
        objects = new List<GameObject>(objectAmount);

        for (int i = 0; i < objectAmount; i++)
        {
            GameObject prefabInstance = Instantiate(objectPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            objects.Add(prefabInstance);
        }
    }

    public GameObject GetObject()
    {
        foreach (GameObject obj in objects)
        {
            if (!obj.activeInHierarchy)
            {
                // This finds a object that isn't active, and activates it
                obj.SetActive(true);
                OnCountUpdate.Invoke(--currentCount);
                return obj;
            }
        }
        // This brings the object into the gameworld
        GameObject prefabInstance = Instantiate(objectPrefab);
        // I don't know what this does
        prefabInstance.transform.SetParent(transform);
        objects.Add(prefabInstance);

        return prefabInstance;
    }
}
