using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Stuff you can fuck with")]
    [SerializeField]
    float drag;
    [SerializeField]
    float shootCooldown;

    [Header("Vera & Jonas Stuff")]
    [SerializeField]
    GameObject shootPosition;

    Cooldown shootTimer;

    private void Start() {

        shootTimer = new Cooldown(shootCooldown, true);        
    }

    private void FixedUpdate() {

        Vector3 _origPos = gameObject.transform.position;

        if (Input.GetKey(KeyCode.W)) {
            transform.position += Vector3.up * drag;
        } else if (Input.GetKey(KeyCode.D)) {
            transform.position += Vector3.right * drag;
        } else if (Input.GetKey(KeyCode.A)) {
            transform.position += Vector3.left * drag;
        } else if (Input.GetKey(KeyCode.S)) {
            transform.position += Vector3.down * drag;
        }

        Vector3 moveDirection = gameObject.transform.position - _origPos;

        if (moveDirection != Vector3.zero) {
            float angle = Mathf.Atan2(moveDirection.x, moveDirection.y) * -Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (Input.GetKey(KeyCode.Mouse0) && shootTimer.CurrentValue <= 0) {

            
            shootTimer.ResetTimer();
        }

        shootTimer.Tick();
    }
}
