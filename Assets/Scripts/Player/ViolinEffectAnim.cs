using UnityEngine;

public class ViolinEffectAnim : MonoBehaviour
{
    Violin violin;

    ParticleSystem effectPs;
    ParticleSystem.ShapeModule shapePs;
    
    public float rotRate = 0.25f;


    void Awake()
    {
        violin = GetComponent<Violin>();
        effectPs = GetComponent<ParticleSystem>();

        shapePs = effectPs.shape;
        shapePs.radius = violin.minRadius;
        effectPs.Clear();
    }

    void Update()
    {
        Quaternion rot = transform.rotation;
        rot.eulerAngles += new Vector3(0, 0, rotRate);
        transform.rotation = rot;

        //check if we can increase or decrease 
        shapePs.radius = violin.currentRadius;
        effectPs.Clear();
        if(violin.currentRadius > violin.minRadius) effectPs.Emit(20);
    }
}
