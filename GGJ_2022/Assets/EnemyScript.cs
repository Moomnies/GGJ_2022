using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.GetComponent<BulletScript>()) {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
