using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFire : MonoBehaviour
{

    public Transform firepoint;
    public ParticleSystem bullet_ps;

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Debug.Log("AAAA");
            bullet_ps.Emit(1);
        }
    }


}
