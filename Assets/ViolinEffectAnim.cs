using UnityEngine;

public class ViolinEffectAnim : MonoBehaviour
{
    Violin violin;

    ParticleSystem effectPs;
    ParticleSystem.ShapeModule shapePs;
    
    public float rotRate = 0.25f;
    float radiTime = 0.5f;
    float currTime = 0f;

    float currRadi = 5f;

    void Awake()
    {
        violin = GetComponent<Violin>();
        effectPs = GetComponent<ParticleSystem>();

        shapePs = effectPs.shape;
        shapePs.radius = currRadi;
    }

    void Update()
    {
        Quaternion rot = transform.rotation;
        rot.eulerAngles += new Vector3(0, 0, rotRate);
        transform.rotation = rot;

        //check if we hit the time to increase
        if (currTime < radiTime)
        {
            //check if we can increase or decrease 
            currRadi = violin.currentRadius;
            shapePs.radius = currRadi;
            effectPs.Clear();
            effectPs.Emit(20);
            currTime = 0;
        }
        else
        {
            currTime += Time.deltaTime;
        }
    }
}
