using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentBullet : MonoBehaviour
{
    [SerializeField]
    float speed;

    Vector3 destination;

    public Vector3 Destination { get => destination; set => destination = value; }

    private void Update() {

        transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);

        if(transform.position == destination) {
            Destroy(this.gameObject);
        }
    }
}
