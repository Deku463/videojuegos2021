using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_check : MonoBehaviour
{
    public int hp_max = 100;
    public int hp_current;

    private void Start()
    {
        hp_current = hp_max;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {

            hp_current -= other.GetComponent<bullet>().damage;

            if (hp_current <= 0)
            {
                Destroy(gameObject);
            }

            Debug.Log("Vida actual: " + hp_current.ToString());

        }
    }

}
