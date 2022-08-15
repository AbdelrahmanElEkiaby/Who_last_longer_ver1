using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Player : MonoBehaviour
{
    private Vector3 gravity = new Vector3(0, 0.02f, 0);
    public GameObject micVolume;
    private float moveSpeed;


    void FixedUpdate()
    {
    /* Move Cow upwards based on Mic volume */
    moveSpeed = micVolume.GetComponent<MicrophoneInput>().loudness * 0.01f;
    transform.position = new Vector3(-2, transform.position.y + moveSpeed, 0);
    }
  
}
