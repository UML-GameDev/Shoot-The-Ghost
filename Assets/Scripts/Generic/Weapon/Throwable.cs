using UnityEngine;

public class Throwable : MonoBehaviour
{
    public ThrowableData throwableData;
    public GameObject blastEffect = null;
    CircleCollider2D blastCollider;
    float lifeTimer = 0;
    bool hasExploded = false;


    void OnEnable()
    {
        lifeTimer = throwableData.lifeDuration;
        hasExploded = false;

        //Add Collider2D based on blast radius
        blastCollider = gameObject.AddComponent<CircleCollider2D>() as CircleCollider2D;
        blastCollider.isTrigger = true;
        blastCollider.radius = throwableData.effectRadius;
        blastCollider.enabled = false;
    }

    void Update()
    {
        //decrease timer
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0){
            if(!hasExploded){
                lifeTimer = 0.1f; //add extratime for the collision checking 
                hasExploded =true;
                blastCollider.enabled = true;
            }else{
                //instantiate blast effect
                if(blastEffect) Instantiate(blastEffect,transform.position,new Quaternion());
                gameObject.SetActive(false);
            }
        } 
    }
}
