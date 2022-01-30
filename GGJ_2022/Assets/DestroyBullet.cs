using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<TurrentScript>() || collision.gameObject.GetComponent<BulletScript>()) {
            Destroy(collision.gameObject);
        }
    }
}
