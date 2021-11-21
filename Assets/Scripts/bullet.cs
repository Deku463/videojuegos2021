using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]

public class bullet : MonoBehaviour
{
    public float size = 5f;
    public float speed = 50f;
    public int bulletFireRate = 10;
    public int damage = 2;
    public Sprite bullet_sprite;

    private ParticleSystem bullet_ps;

    private void Awake()
    {
        bullet_ps = GetComponent<ParticleSystem>();
        bullet_ps.textureSheetAnimation.SetSprite(0, bullet_sprite);

        var main_ps = bullet_ps.main;
        var emission_ps = bullet_ps.emission;

        main_ps.startSize = size;
        main_ps.startSpeed = speed;
        emission_ps.rateOverDistance = bulletFireRate;

    }

}
