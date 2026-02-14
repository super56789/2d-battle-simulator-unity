using UnityEngine;

public class Norm : Boy
{
    private float attackForce = 500f;
    void Awake() {
        Initialize(50, 2, 2, true);
    }

    public override void Attack(Vector2 attackVector, GameObject targetedEnemy)
    {
        rb.AddForce(attackVector.normalized * attackForce);
    }
}
