using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
{
     private Vector3 gravity = new Vector3(0, 0.1f, 0);//0.15f
    public GameObject micVolume;
    private float Speed = 10f;
    private float moveSpeed;
    PhotonView view;
    private void Start()
    {
       view = GetComponent<PhotonView>();
    }
    void FixedUpdate()
    {
        if(!view.IsMine)
        {
            
            return;
            // float movespeed = Input.GetAxis("Vertical") * Time.deltaTime * Speed;
            // transform.Translate(0,movespeed,0);
        }
        Move();
       
   
    }
    void Move() {
            moveSpeed = micVolume.GetComponent<MicrophoneInput>().loudness * 0.01f;
            transform.position -= gravity;
            transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed, 0);
    }

}
