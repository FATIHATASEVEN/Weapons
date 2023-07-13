using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public int MinDamage, MaxDamage;
    public Camera playercamera;
    public float range = 300f;
    private EnemyManager enemyManager;
    public ParticleSystem flash;
    public GameObject damageeffect;

    void Start()
    {
        
    }
    void Update()
    {
        if( Input.GetButtonDown("Fire1"))
        {
            Fire();
            flash.Play();
        }
    }
    void Fire()
    {
        RaycastHit hit;
        if(Physics.Raycast(playercamera.transform.position,playercamera.transform.forward,out hit,range))
            {
            Debug.Log(hit.transform.name);
            enemyManager = hit.transform.GetComponent<EnemyManager>();
            Instantiate(damageeffect, hit.point, Quaternion.LookRotation(hit.normal));

            if(enemyManager!=null )
                {
                enemyManager.EnemyTakeDamage(Random.Range(MinDamage, MaxDamage));
            }  
        }
    }
}
