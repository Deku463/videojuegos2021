using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreBHVR : MonoBehaviour
{
    public int coreValue = 1;
    public CoreManager coremanager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            coremanager.PlayerScorePoint(coreValue);
            Destroy(gameObject);

        }
    }

}
