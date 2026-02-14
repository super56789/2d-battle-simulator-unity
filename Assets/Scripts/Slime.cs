using UnityEngine;

public class Slime : Boy
{
    private float attackForce = 200f;
    private float bounceForce = 15f;
    void Awake() {
        Initialize(20, 1, 3, true);
    }

    public override void Attack(Vector2 attackVector, GameObject targetedEnemy)
    {
        rb.AddForce(attackVector.normalized * attackForce);
    }

    protected override void OnCollisionEnter2D(Collision2D col) {
        base.OnCollisionEnter2D(col);
        if (col.gameObject.tag == "Untagged") {
            return;
        }
        Vector2 direction = col.transform.position - transform.position;
        direction = direction.normalized;
        col.rigidbody.AddForce(direction * bounceForce, ForceMode2D.Impulse);
    }
}