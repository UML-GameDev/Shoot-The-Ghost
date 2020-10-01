using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager instance;
    public static PoolManager Instance { get { return instance; } }

    public GameObject bulletPrefab;
    public int bulletAmount = 20;

    private List<GameObject> bullets;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        // Preload bullets
        bullets = new List<GameObject>(bulletAmount);

        for (int i = 0; i < bulletAmount; i++)
        {
            GameObject prefabInstance = Instantiate(bulletPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            bullets.Add(prefabInstance);
        }
    }

    public GameObject GetBullet()
    {
        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                // This finds a bullet that isn't active, and activates it
                bullet.SetActive(true);
                return bullet;
            }
        }
        // This brings the bullet into the gameworld
        GameObject prefabInstance = Instantiate(bulletPrefab);
        // I don't know what this does
        prefabInstance.transform.SetParent(transform);
        bullets.Add(prefabInstance);

        return prefabInstance;
    }
}
