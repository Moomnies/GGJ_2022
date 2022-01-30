using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentScript : MonoBehaviour
{
    [SerializeField]
    float shootCooldown;
    [SerializeField]
    GameObject bullet;

    Cooldown shootCooldownTimer;

    private void Start() {

        shootCooldownTimer = new Cooldown(shootCooldown, true);
    }

    private void Update() {

        shootCooldownTimer.Tick();

    }

    private void OnTriggerStay2D(Collider2D collision) {
            
        if(collision.gameObject.GetComponent<PlayerScript>() != null && shootCooldownTimer.CurrentValue <= 0) {

            Shoot(collision.gameObject.transform.position);
            Debug.Log("Shoot");
            shootCooldownTimer.ResetTimer();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.GetComponent<BulletScript>()) {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }

    public void Shoot(Vector3 position) {

        GameObject bulletSpawn = Instantiate(bullet, this.transform.position, transform.rotation);
        TurrentBullet turrentScript = bulletSpawn.GetComponent<TurrentBullet>();

        turrentScript.Destination = position;
    }
}
