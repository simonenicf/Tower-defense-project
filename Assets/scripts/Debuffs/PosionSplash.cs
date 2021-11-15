using System;
using UnityEngine;


public class PosionSplash : MonoBehaviour
{
    public int Damage { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(Damage, Element.TOXIC);
            Destroy(gameObject);
        }
    }
}
