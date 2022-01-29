using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    float drag;

    private void FixedUpdate() {

        if (Input.GetKey(KeyCode.W)) {
            transform.position += Vector3.up * drag;
        } else if (Input.GetKey(KeyCode.D)) {
            transform.position += Vector3.right * drag;
        } else if (Input.GetKey(KeyCode.A)) {
            transform.position += Vector3.left * drag;
        } else if (Input.GetKey(KeyCode.S)) {
            transform.position += Vector3.down * drag;
        }
    }
}
