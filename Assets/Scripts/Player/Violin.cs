using UnityEngine;

public class Violin : MonoBehaviour
{
    CircleCollider2D aoe;

    public bool isHolding;
    const float maxRadius = 10f;
    const float minRadius = 5f;
    const float increaseRate = 0.5f;
    const float decreaseRate = 1.1f;
    public float currentRadius = 0f;

    void Awake()
    {
        aoe = GetComponent<CircleCollider2D>();
        if (!aoe) aoe = gameObject.AddComponent<CircleCollider2D>();
    }

    void OnEnable()
    {
        currentRadius = minRadius;
        aoe.isTrigger = true;
        aoe.radius = currentRadius;
    }

    void OnDisable()
    {
        aoe.isTrigger = false;
        aoe.radius = 0f;
        currentRadius = 0f;
    }

    void Update()
    {
        if (isHolding)
        {
            if(currentRadius <= maxRadius)
            {
                currentRadius += increaseRate * Time.deltaTime;
            }
        }
        else
        {
            if(currentRadius >= minRadius)
            {
                currentRadius -= decreaseRate * Time.deltaTime;
            }
        }
        aoe.radius = currentRadius;
    }
    public void SetHolding(bool isHolding)
    {
        this.isHolding = isHolding; 
    }
}
