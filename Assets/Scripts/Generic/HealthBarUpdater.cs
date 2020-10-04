using UnityEngine;

public class HealthBarUpdater : MonoBehaviour
{
    float initHealthScaleX;
    
    public GameObject owner;

    void Start()
    {
        owner.GetComponent<HealthUpdatable>().OnHealthUpdated.AddListener(updateHealth);
        initHealthScaleX = transform.localScale.x;
    }
    void updateHealth(float currHealth)
    {
        transform.localScale = new Vector3((currHealth / 100f) * initHealthScaleX, transform.localScale.y, 0);
    }
}
