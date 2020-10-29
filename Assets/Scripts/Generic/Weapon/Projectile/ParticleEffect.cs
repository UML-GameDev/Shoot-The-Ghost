using UnityEngine;

public class ParticleEffect: MonoBehaviour
{
    public ThrowableData throwableData;
    ParticleSystem ps;
    void Start(){
        ps = GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule pShape = ps.shape;
        pShape.radius = throwableData.effectRadius;
    }

    void Update(){
        if(!ps.IsAlive()){
            Destroy(gameObject);
        }
    }
}