using UnityEngine;

public class Violin : MonoBehaviour
{
    public InputManager im;

    CircleCollider2D aoe;

    bool isHolding;
    public float maxRadius = 10f;
    public float minRadius = 2f;
    public float increaseRate = 0.5f;
    public float decreaseRate = 1.1f;
    public float currentRadius = 0f;

    void Awake()
    {
        aoe = GetComponent<CircleCollider2D>();
        if (!aoe) aoe = gameObject.AddComponent<CircleCollider2D>();
    }

    void OnEnable()
    {
        im.attackEvent += SetHolding;
        currentRadius = minRadius;
        aoe.isTrigger = true;
        aoe.radius = currentRadius;
        aoe.enabled = false;
        gameObject.SetActive(true);
    }

    void OnDisable()
    {
        im.attackEvent -= SetHolding;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (isHolding)
        {
            if (currentRadius <= maxRadius)
            {
                currentRadius += increaseRate * Time.deltaTime;
            }
        }
        else
        {
            if (currentRadius > minRadius)
            {
                currentRadius -= decreaseRate * Time.deltaTime;
            }
        }
        aoe.enabled = currentRadius > minRadius;
        aoe.radius = currentRadius;
    }
    public void SetHolding(bool isHolding)
    {
        this.isHolding = isHolding;
    }
}
