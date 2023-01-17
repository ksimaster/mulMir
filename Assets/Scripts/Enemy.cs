using UnityEngine;
using Mirror;

public class Enemy : NetworkBehaviour
{
    public int health;
    public int damage;
    public float radius;

    //private Player player;

    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("Player");
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Damage()
    {
        health -= damage;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
