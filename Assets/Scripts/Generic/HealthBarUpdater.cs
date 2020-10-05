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
        float newScaleX = (currHealth / 100f) * initHealthScaleX;
        float scaleDelta = newScaleX - transform.localScale.x;

        transform.localScale = new Vector3(newScaleX, transform.localScale.y, 0);
        transform.position += new Vector3(scaleDelta/2, 0, 0);
    }
}
