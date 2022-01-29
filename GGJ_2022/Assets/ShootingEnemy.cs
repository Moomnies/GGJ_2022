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

                Debug.Log("Pew First");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {

        if (collision.gameObject.GetComponent<PlayerScript>() != null) {

            shootCooldown.Tick();

            if (shootCooldown.CurrentValue <= 0) {

                Debug.Log("Shoot");
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
}
