public class ThrowController : InputUser
{
    Throw thrower;
    void Awake()
    {
        thrower = GetComponent<Throw>();
    }
    void OnEnable()
    {
        input.attackEvent += thrower.AttackState;//similiar to setHolding in IShooter class
        input.lookEvent += thrower.aimThrow;
    }

    void OnDisable()
    {
        input.attackEvent -= thrower.AttackState;//similiar to setHolding in IShooter class
        input.lookEvent -= thrower.aimThrow;
    }
}
