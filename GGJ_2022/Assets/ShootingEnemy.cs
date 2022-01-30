using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ShootingEnemy : MonoBehaviour
{
    [Header("Stuff you can Fuck with")]
    [SerializeField]
    float cooldownTimer;

    [Header("Jonas & Vera Stuff")]
    [SerializeField]
    AIPath aiPath;
    [SerializeField]
    GameObject bullet;

    Vector3 playerPosition;    

    Cooldown seePlayerShootCooldown;
    Cooldown shootCooldown;

    private void Start() {

        seePlayerShootCooldown = new Cooldown(cooldownTimer, true);
        shootCooldown = new Cooldown(cooldownTimer, false);
    }

    private void Update() {
        seePlayerShootCooldown.Tick();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if(collision.gameObject.GetComponent<PlayerScript>() != null) {

            aiPath.canMove = false;
            shootCooldown.ToggleTimer();

            if(seePlayerShootCooldown.CurrentValue <= 0) {

                Shoot(collision.gameObject.transform.position);
            }
        }        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<BulletScript>()) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {

        if (collision.gameObject.GetComponent<PlayerScript>() != null) {

            shootCooldown.Tick();

            if (shootCooldown.CurrentValue <= 0) {

                Shoot(collision.gameObject.transform.position);
                shootCooldown.ResetTimer();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.gameObject.GetComponent<PlayerScript>() != null) {

            aiPath.canMove = true;
            seePlayerShootCooldown.ResetTimer();
            shootCooldown.ResetTimer();
            shootCooldown.ToggleTimer();
        }
    }

    public void Shoot(Vector3 position) {

        GameObject bulletSpawn = Instantiate(bullet, this.transform.position, transform.rotation);
        TurrentBullet turrentScript = bulletSpawn.GetComponent<TurrentBullet>();

        turrentScript.Destination = position;
    }
}
