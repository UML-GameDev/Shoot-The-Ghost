using UnityEngine;

public class HealthBarUpdater : MonoBehaviour
{
    float initHealthScaleX;
    float globalHealthScaleX;
    public GameObject owner;
    public Transform healthBarTrans;

    void Start()
    {
        owner.GetComponent<HealthUpdatable>().OnHealthUpdated.AddListener(updateHealth);
        initHealthScaleX = healthBarTrans.localScale.x;
        
        healthBarTrans.parent = null;
        globalHealthScaleX = healthBarTrans.localScale.x;
        healthBarTrans.parent = transform;
    }
    
    void updateHealth(float currHealth)
    {
        float newGlobalScaleX = (currHealth / 100f) * globalHealthScaleX;
        float scaleDelta = newGlobalScaleX - globalHealthScaleX;
        
        healthBarTrans.localScale = new Vector3((currHealth / 100f) * initHealthScaleX, healthBarTrans.localScale.y, 1);
        healthBarTrans.position = transform.position + new Vector3(scaleDelta/2, 0, 0);        
    }

    void Update()
    {
        transform.LookAt(transform.position - Vector3.forward);
    }
}
