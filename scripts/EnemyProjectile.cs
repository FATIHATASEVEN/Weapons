using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damageAmount = 25;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag=="Player")
        {
            PlayerController player = collision.transform.GetComponent<PlayerController>();
            player.PlayerTakeDamage(damageAmount);
            Destroy(this.gameObject);
        }
        else 
        {
            Destroy(this.gameObject, 1f);
        }
    }
}
