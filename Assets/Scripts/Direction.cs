using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    public Transform player;

    private Rigidbody rigid;

    private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = transform.position - player.position;

        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        rigid
            .MovePosition(transform.position +
            (new Vector3(movement.x, movement.y, -(movement.z)) * 0.02f));
    }

    void movePlane(Vector3 direction)
    {
        rigid
            .MovePosition((Vector3) transform.position +
            (direction * 0.5f * Time.deltaTime));
    }
}
