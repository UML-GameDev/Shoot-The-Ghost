using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolManager : MonoBehaviour
{
    public int currentCount { get; private set; }
    public int maxCount { get; private set; }
    public bool outofAmmo { get; private set; }

    public GameObject objectPrefab;
    public GunData data;

    private List<GameObject> objects;

    [HideInInspector] public UnityEvent OnCountUpdate;

    // Start is called before the first frame update
    void Awake()
    {
        maxCount = data.maxAmmo;
        currentCount = maxCount;
        // Preload objects
        objects = new List<GameObject>(maxCount);
        OnCountUpdate.AddListener(CheckAmmoCount);

        for (int i = 0; i < maxCount; i++)
        {
            GameObject prefabInstance = Instantiate(objectPrefab);
            //prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            objects.Add(prefabInstance);
        }
    }

    public void Refill()
    {
        currentCount = maxCount;
        outofAmmo = false;
        OnCountUpdate.Invoke();
    }

    public GameObject GetObject()
    {
        foreach (GameObject obj in objects)
        {
            if (!obj.activeInHierarchy)
            {
                // This finds a object that isn't active, and activates it
                obj.SetActive(true);
                --currentCount;
                OnCountUpdate.Invoke();
                return obj;
            }
        }
        //We reach this part IF currentCount is less than 0 (means exceed the amount of object in the list)
        //AND all the objects are active
        //But exists for safe guard
        // This brings the object into the gameworld
        GameObject prefabInstance = Instantiate(objectPrefab);
        //Sets the parent of the transformation to the parents(poolmanager)
        //prefabInstance.transform.SetParent(transform);
        objects.Add(prefabInstance);

        return prefabInstance;
    }
    
    void CheckAmmoCount()
    {
        outofAmmo = (!outofAmmo && currentCount <= 0);
    }
}
