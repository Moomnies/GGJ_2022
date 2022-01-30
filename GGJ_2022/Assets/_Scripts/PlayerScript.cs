using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerScript : MonoBehaviourPunCallbacks
{
    [Header("Stuff you can fuck with")]
    [SerializeField]
    float drag;
    [SerializeField]
    float shootCooldown;

    [Header("Vera & Jonas Stuff")]
    [SerializeField]
    GameObject shootPosition;
    [SerializeField]
    GameObject bullet;

    Cooldown shootTimer;

    PhotonView view;

    public static Transform playerPosition;

    private void Start()
    {
        playerPosition = this.transform;
        shootTimer = new Cooldown(shootCooldown, true);
        view = gameObject.GetComponent<PhotonView>();
    }

    private void FixedUpdate()
    {            
            Vector3 _origPos = gameObject.transform.position;

            if (Input.GetKey(KeyCode.W))
            {
                transform.position += Vector3.up * drag;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.position += Vector3.right * drag;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.position += Vector3.left * drag;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.position += Vector3.down * drag;
            }

            Vector3 moveDirection = gameObject.transform.position - _origPos;

            if (moveDirection != Vector3.zero)
            {
                float angle = Mathf.Atan2(moveDirection.x, moveDirection.y) * -Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }

            if (Input.GetKey(KeyCode.Mouse0) && shootTimer.CurrentValue <= 0)
            {
                Shoot();
                photonView.RPC("RPC_Shoot", RpcTarget.Others);
            }
            shootTimer.Tick();
        
    }

    public void Shoot()
    {
        Instantiate(bullet, shootPosition.transform.position, transform.rotation);
        shootTimer.ResetTimer();
    }

    [PunRPC]
    void RPC_Shoot()
    {
        Shoot();
    }
}
