using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Enemy_IA_Ground : MonoBehaviour
{
    public Vector2 jumpForce_front;
    public float jump_height = 4f;
    public float speed = 5f;
    public LayerMask collisions;

    public Transform[] waypoints;
    public int waypoint_target_index;
    public bool facing_Right = true;

    public float min_distance_waypoint = 2f;

    public Transform gfx_parent;
    public bool canMove = true;
    public bool onGround = false;

    [Header("Sensores"), Space(2)]
    public float sensor_floor_front_range = 2;
    public float sensor_floor_below_range = 2;

    [Header("Componentes"), Space(2)]
    public CapsuleCollider2D capsule_collider;
    private Rigidbody2D rigibody;

    private void Start()
    {
        waypoint_target_index = 0;
        rigibody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        canMove = true;
        onGround = false;

        //Validamos coordenada con relacion en su distancia actual.
        float delta_distance = waypoints[waypoint_target_index].position.x - transform.position.x;

        float distance_Waypoint = Mathf.Abs(delta_distance);
        if (distance_Waypoint < min_distance_waypoint)
        {
            waypoint_target_index = Update_WaypointTarget(waypoint_target_index);
        }

        //Sentido hacia donde debe de caminar
        float direction = Mathf.Sign(delta_distance);
        gfx_parent.localScale = new Vector3(direction, 1, 1);



        //SENSOR SUELO FRENTE

        //Posicion original del enemigo - Tamano original del collider / 2
        Vector3 origin_floor = transform.position + Vector3.down * capsule_collider.bounds.size.y * 0.5f;
        // Detectamos cualquier colision dentro del espacio minimo de colision
        RaycastHit2D[] hits_floor_front = Physics2D.RaycastAll(origin_floor + Vector3.up * 0.2f, Vector2.right * direction, sensor_floor_front_range, collisions);
        Debug.DrawLine(origin_floor + Vector3.up * 0.2f, origin_floor + Vector3.up * 0.2f + Vector3.right * direction * sensor_floor_front_range, Color.red);

        float collision_max_height = 0;
        if (hits_floor_front.Length > 0)
        {
            //Detectamos algo
            for (int i = 0; i < hits_floor_front.Length; i++)
            {
                //Checamos cada objeto con lo que colisionaremos
                if (hits_floor_front[i].collider.gameObject != null)
                {
                    //Verificamos que existe el codigo
                    canMove = false;
                }


                float collision_top_coord = hits_floor_front[i].collider.transform.position.y + hits_floor_front[i].collider.bounds.size.y * 0.5f;
                if (collision_max_height < collision_top_coord)
                {
                    collision_max_height = collision_top_coord;
                }
            }
        }

        //SENSOR SUELO DEBAJO
        RaycastHit2D[] hits_floor_below = Physics2D.RaycastAll(origin_floor, Vector2.down, sensor_floor_below_range, collisions);
        Debug.DrawLine(origin_floor, origin_floor + Vector3.down * sensor_floor_below_range, Color.black);
        if (hits_floor_below.Length > 0)
        {
            //Detectamos algo
            for (int i = 0; i < hits_floor_below.Length; i++)
            {
                //Checamos cada objeto con lo que colisionaremos
                if (hits_floor_below[i].collider.gameObject != null)
                {
                    //Verificamos que existe el codigo
                    onGround = true;
                }
            }
        }

        Debug.Log(collision_max_height);

        if (canMove)
        {
            //Movimiento hacia al frente
            Vector3 targetMovement_X = Vector3.right * direction * speed * Time.deltaTime;
            transform.Translate(targetMovement_X, Space.World);
        }
        else
        {
            if (onGround)
            {
                Debug.DrawLine(transform.position + Vector3.up * jump_height, transform.position + Vector3.up * jump_height + Vector3.right * direction * sensor_floor_front_range, Color.green);
                if (transform.position.y + jump_height > collision_max_height)
                {
                    //Tomamos la altura del enemigo y le agregamos lo alto que puede saltar. Luego comparamos si es posible hacer el salto
                    Debug.Log("Brinca");
                    rigibody.AddForce(new Vector2(jumpForce_front.x * direction, jumpForce_front.y));
                }
                else
                {
                    Debug.Log("Imposible");
                }
            }
        }
    }

    int Update_WaypointTarget(int _counter)
    {
        _counter++;
        if (_counter >= waypoints.Length)
        {
            _counter = 0;
        }
        return _counter;
    }

}
