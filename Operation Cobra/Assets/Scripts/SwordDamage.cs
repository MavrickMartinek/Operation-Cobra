using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour {

    public float damage;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "sword")
        {
            Health target = col.gameObject.GetComponent<Health>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
