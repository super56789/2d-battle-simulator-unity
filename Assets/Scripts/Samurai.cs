using UnityEngine;

public class Samurai : Boy
{
    private float slashDamage = 20;
    private float attackForce = 500f;
    void Awake() {
        Initialize(35, 1, 4, false);
    }

    public override void Attack(Vector2 attackVector, GameObject targetedEnemy)
    {
        Vector3 positionVector = targetedEnemy.transform.position - transform.position;
        Vector3 newPosition = targetedEnemy.transform.position + positionVector;
        newPosition.x += 0.2f;
        if (!Physics2D.OverlapPoint(newPosition) && !inBounds(newPosition)) {
            targetedEnemy.GetComponent<Boy>().TakeDamage(Mathf.Max(slashDamage * getAttackScaling(targetedEnemy.transform.position), slashDamage));
            transform.position = newPosition;
        } else {
            rb.AddForce(attackVector.normalized * attackForce);
        }

    }

    private bool inBounds(Vector3 position) {
        if (position.x < -7.5 || position.x > 7.5) {
            return false;
        } else if (position.y > 3.5 || position.y < -3.5) {
            return false;
        }

        return true;
    }
}
