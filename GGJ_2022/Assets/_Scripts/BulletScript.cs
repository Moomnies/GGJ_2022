using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D body;

    [SerializeField]
    float speed;

    private void FixedUpdate() {

        body.velocity = transform.up * speed;
    }
}
