using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] // for health bar
    private HUD m_HUD;

    #region player_variables
    Rigidbody2D rb;                     // possibly unneccessary
    SpriteRenderer[] indicators;        // 0 is player, 1-4 are facing indicators, 5-8 are attack indicators
    PolygonCollider2D[] attackBoxes;
    Hitbox[] hitboxes;
    #endregion

    #region movement_variables
    public float moveSpeed;             // How fast the player moves
    private float xAxis;                // X-input for movement
    private float yAxis;                // Y-input for movement
    private Vector2 movementVector;     // tool to calculate movement
    #endregion

    #region attack_variables
    public float attackSpeed;           // How many seconds required between attacks; defined by weapon eventually
    private float attackTimer;          // resets to attackSpeed after attack; may only attack when timer < 0
    public float damage;                // power of player's attacks; defined by weapon eventually
    private Vector3 mousePosition;
    private float mouseX;               // possibly unneccessary
    private float mouseY;               // possibly unneccessary
    private bool[] facing;              // [R, U, L, D] with one value true and others false at all times
    #endregion

    #region health_variables
    public float health;                // current health of player
    public float maxhealth;
    private bool mercyInvuln;           // tells if player currently has mercy invulnerability
    public float mercyInvulnLength;     // length of mercy invuln 
    private float mercyInvulnTimer;     // resets to mercyInvulnLength whenever invuln ends
    private bool dying;                 // is the player currently dying?
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        indicators = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 1; i < indicators.Length; i++)
        {
            indicators[i].enabled = false;
        }
        attackBoxes = GetComponentsInChildren<PolygonCollider2D>();
        for (int i = 0; i < attackBoxes.Length; i++)
        {
            attackBoxes[i].enabled = false;
        }
        hitboxes = GetComponentsInChildren<Hitbox>();
        xAxis = 0;
        yAxis = 0;
        movementVector = new Vector2(0, 0);

        facing = new bool[4];

        mercyInvuln = false;
        mercyInvulnTimer = 0;
        dying = false;

        maxhealth = health;

        m_HUD.UpdateText(1.0f * health, maxhealth);
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
        movementVector = new Vector2(xAxis, yAxis);
        movementVector *= Time.deltaTime * moveSpeed;
        if (!dying)
        {
            rb.MovePosition(rb.position + movementVector);
        }

        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));

        DetermineFaceChange();
        HandleFaceChange();

        mercyInvulnTimer -= Time.deltaTime;
        if (mercyInvulnTimer < 0)
        {
            mercyInvuln = false;
        }

        attackTimer -= Time.deltaTime;
        if (Input.GetButton("Fire1") && attackTimer <= 0 && !dying)
        {
            Attack();
        }
    }

    #region inventory_functions
    public void equip(Item item)
    {
        item.equip(gameObject);
        m_HUD.UpdateHealth(1.0f * health / maxhealth);
        m_HUD.UpdateText(1.0f * health, maxhealth);
    }

    public void unequip(Item item)
    {
        item.unequip(gameObject);
        m_HUD.UpdateHealth(1.0f * health / maxhealth);
        m_HUD.UpdateText(1.0f * health, maxhealth);
    }
    #endregion

    private void DetermineFaceChange()
    {
        for (int i = 0; i < facing.Length; i++)
        {
            if (facing[i])
            {
                facing[i] = false;
                break;
            }
        }
        float angle = Mathf.Rad2Deg * Mathf.Atan((mousePosition.y - transform.position.y) / (mousePosition.x - transform.position.x));
        if (mousePosition.x > transform.position.x)
        {
            if (angle <= 45 && angle > -45)
            {
                facing[0] = true;
            }
            else if (angle > 45)
            {
                facing[1] = true;
            }
            else
            {
                facing[3] = true;
            }
        }
        else
        {
            if (angle <= 45 && angle > -45)
            {
                facing[2] = true;
            }
            else if (angle > 45)
            {
                facing[3] = true;
            }
            else
            {
                facing[1] = true;
            }
        }
    }

    private void HandleFaceChange()
    {
        for (int i = 1; i < facing.Length + 1; i++)
        {
            if (indicators[i].enabled)
            {
                indicators[i].enabled = false;
                break;
            }
        }
        for (int i = 0; i < facing.Length; i++)
        {
            if (facing[i])
            {
                indicators[i+1].enabled = true;
                break;
            }
        }
    }

    private void Attack()
    {
        StartCoroutine(AttackRoutine(System.Array.IndexOf(facing, true)));
    }

    IEnumerator AttackRoutine(int direction)
    {
        hitboxes[direction].damageValue = damage;
        indicators[5 + direction].enabled = true;
        attackBoxes[direction].enabled = true;
        attackTimer = attackSpeed;
        yield return new WaitForSeconds(0.1f);
        indicators[5 + direction].enabled = false;
        attackBoxes[direction].enabled = false;
        hitboxes[direction].damageApplied = false;
    }

    public void TakeDamage(float damage)
    {
        if (!mercyInvuln)
        {
            mercyInvulnTimer = mercyInvulnLength;
            mercyInvuln = true;
            health -= damage;
            m_HUD.UpdateHealth(1.0f * health / maxhealth);
            m_HUD.UpdateText(1.0f * health, maxhealth);
            Debug.Log("Took " + damage + " damage! Health is now " + health);
            if (health <= 0)
            {
                dying = true;
                Die();
            }
        }
    }

    private void Die()
    {
        Debug.Log("Ran out of health! Game over...");
        Time.timeScale = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(collision.gameObject.GetComponent<Enemy>().AttackDamage);
        }
    }

}
