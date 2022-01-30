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
    float shootCooldown, pingCooldown;

    [Header("Vera & Jonas Stuff")]
    [SerializeField]
    GameObject shootPosition;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    GameObject dangerPing, goHerePing;

    Cooldown shootTimer, pingTimer;

    PhotonView view;



    public static Transform playerPosition;

    private void Start()
    {
        playerPosition = this.transform;
        shootTimer = new Cooldown(shootCooldown, true);
        pingTimer = new Cooldown(pingCooldown, true);
        view = gameObject.GetComponent<PhotonView>();
    }

    private void FixedUpdate()
    {
        if (view.IsMine)
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

            if (Input.GetKey(KeyCode.Mouse1) && pingTimer.CurrentValue <= 0)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Ping(mousePos, dangerPing);
                photonView.RPC("RPC_Ping", RpcTarget.Others, mousePos, dangerPing);
            }
            else if (Input.GetKey(KeyCode.Q) && pingTimer.CurrentValue <= 0)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Ping(mousePos, goHerePing);
                photonView.RPC("RPC_Ping", RpcTarget.Others, mousePos, goHerePing);
            }


            shootTimer.Tick();
            pingTimer.Tick();
        }
    }

    public void Shoot()
    {
        Instantiate(bullet, shootPosition.transform.position, transform.rotation);
        shootTimer.ResetTimer();
    }

    public void Ping(Vector2 pingPos, GameObject prefab)
    {

    }

    [PunRPC]
    void RPC_Shoot()
    {
        Shoot();
    }

    [PunRPC]
    void RPC_Ping(Vector2 pingPos, GameObject prefab)
    {
        Ping(pingPos, prefab);
    }
}
