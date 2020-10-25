using UnityEngine;

/*
 * HealthBarUpdater 
 *      This class handles UI of Entities' health Bar
 *      
 *      This class inherits Monobehaviour so it can attach to gameObject
 *      
 *      This class should be attach to a gameObject that has HealthBar
 */ 

public class HealthBarUpdater : MonoBehaviour
{
    float initHealthScaleX;
    float globalHealthScaleX;
    public GameObject owner;
    public Transform healthBarTrans;

    void Start()
    { 
        //Update will called everytime owner's health is changed
        owner.GetComponent<HealthUpdatable>().OnHealthUpdated.AddListener(UpdateHealth);
        initHealthScaleX = healthBarTrans.localScale.x;
        
        healthBarTrans.parent = null;
        globalHealthScaleX = healthBarTrans.localScale.x;
        healthBarTrans.parent = transform;
    }
    
    void UpdateHealth(float currHealth)
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
