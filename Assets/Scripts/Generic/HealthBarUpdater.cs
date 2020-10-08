using UnityEngine;

public class HealthBarUpdater : MonoBehaviour
{
    float initHealthScaleX;
    
    public GameObject owner;
    public Transform healthBarTrans;

    void Start()
    {
        owner.GetComponent<HealthUpdatable>().OnHealthUpdated.AddListener(updateHealth);
        initHealthScaleX = healthBarTrans.localScale.x;
    }
    void updateHealth(float currHealth)
    {
        float newScaleX = (currHealth / 100f) * initHealthScaleX;
        float scaleDelta = newScaleX - healthBarTrans.localScale.x;
        
        healthBarTrans.localScale = new Vector3(newScaleX, healthBarTrans.localScale.y, 0);
        healthBarTrans.position += new Vector3(scaleDelta/2, 0, 0);
    }

    void Update()
    {
        transform.LookAt(transform.position + new Vector3(0, 0, 4));
    }
}
