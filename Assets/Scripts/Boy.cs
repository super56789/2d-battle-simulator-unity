using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.VFX;


public abstract class Boy : MonoBehaviour
{
    private float health;
    private float attack;
    private float wallDamage = 1.8f;
    private string type;
    public float movementSpeed = 2f;
    private float xTarget;
    private GameObject targetedEnemy;
    private bool attackActivated;
    private bool isRotating = false;
    private bool onFloor;
    private float bonusHeight = 0.2f;
    protected Vector3 lastVelocity;
    protected Rigidbody2D rb;
    [SerializeField] private GameObject bloodVFX;
  
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0.1f, 0.2f);
        InvokeRepeating("ActivateAttack", 1.5f, 2.5f);
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        lastVelocity = rb.linearVelocity;
    }

    void UpdateTarget()
    {
        xTarget = NearestEnemyXPos();
        if (xTarget > transform.position.x) {
            transform.localScale = new Vector3(1, 1, 1);
        } else {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Mathf.Abs(transform.position.x - xTarget) > 1.5f && onFloor && !isRotating)
        {
            MoveToTarget();
        }
        if (!isRotating) {
            Invoke("AttackTarget", Random.Range(0f, 1f));
        }
    }

    void ActivateAttack()
    {
        attackActivated = true;
    }

    void MoveToTarget()
    {
        Vector2 targetVector = new Vector2(xTarget, 0);
        Vector2 currentVector = new Vector2(transform.position.x, 0);
        Vector2 direction = targetVector - currentVector;
        rb.linearVelocity = direction.normalized * movementSpeed;
    }

    public void Initialize(float health, float attack, float movementSpeed, bool hasBonus) {
        this.health = health;
        this.attack = attack;
        xTarget = 0;
        type = gameObject.tag;
        if (!hasBonus) {
            bonusHeight = 0;
        }
    }
    
    public void TakeDamage(float damage) {
        health -= damage;
        Instantiate(bloodVFX, transform.position, Quaternion.identity);
        if (health <= 0) {
            Destroy(gameObject);
            BoysManager.Instance.removeBoy(gameObject, type);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D col) {
        rb.freezeRotation = false;
        string enemyType = col.gameObject.tag;
        if (col.gameObject.name == "Bottom Wall") {
            onFloor = true;
        } else if (col.gameObject.tag == "Untagged") {
            TakeDamage(wallDamage * rb.linearVelocity.magnitude);
        }
        if (enemyType != "Blue" && enemyType != "Red") {
            return;
        }
        if (type != enemyType && rb.linearVelocity.magnitude > col.rigidbody.linearVelocity.magnitude) {
            col.gameObject.GetComponent<Boy>().TakeDamage(attack * getAttackScaling(col.gameObject.transform.position));
        }
    }

    public float getAttackScaling(Vector3 oppositionPos) {
            return Mathf.Max(0, Vector3.Project(lastVelocity, oppositionPos - transform.position).magnitude);
    }

    void OnCollisionStay2D(Collision2D col) {
        if (transform.rotation.eulerAngles.z >= 85 && transform.rotation.eulerAngles.z <= 270) {
            rb.freezeRotation = true;
            isRotating = true;
        }
        
        if (isRotating && transform.rotation.eulerAngles.z > 5 && transform.rotation.eulerAngles.z < 355) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.forward), 3.5f);
        } else {
            isRotating = false;
        }
    }

    void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.name == "Bottom Wall") {
            onFloor = false;
        } 
    }

    private float NearestEnemyXPos() {
        float xPos = transform.position.x;
        List<GameObject> enemies = BoysManager.Instance.getBoysEnemies(type);
        GameObject closestEnemy = enemies.OrderBy(enemy => Mathf.Abs(xPos - enemy.transform.position.x)).First();
        targetedEnemy = closestEnemy;
        return closestEnemy.transform.position.x;
    }

    private void AttackTarget()
    {
        if (attackActivated && Mathf.Abs(transform.position.x - xTarget) < 5)
        {
            Vector2 attackVector = new Vector2(xTarget - transform.position.x, bonusHeight);
            Attack(attackVector, targetedEnemy);
            attackActivated = false;
        }
    }

    public abstract void Attack(Vector2 attackVector, GameObject targetedEnemy);

    }
