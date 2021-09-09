using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon;
using Photon.Pun;
using Photon.Realtime;

public class ChacterMovement : MonoBehaviourPunCallbacks
{
    public Rigidbody fizik;
    private float hýz = 15;
    public Animator animator;
    float x, z;
    void Start() 
    {
        fizik = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (photonView.IsMine)
            {
                animator.SetBool("IsMine", true);
            }
        }
    }
    void Update()
    {

    } 
    void FixedUpdate()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        avatarControl(x, z);
    }
    void ileri()
    {
        Debug.Log(photonView.IsMine);
    }
    void sol()
    {
        Debug.Log(photonView.IsMine);
    }
    void sag()
    {
        Debug.Log(photonView.IsMine);
    }
    void geri()
    {
        Debug.Log(photonView.IsMine);
    }

    void avatarControl(float x, float z)
    {
        if (photonView.IsMine)
        {
            Vector3 vec = new Vector3(x * Time.fixedDeltaTime * hýz, 0, z * Time.fixedDeltaTime * hýz);
            transform.position += vec;
            animator.SetFloat("Horizontal", x);
            animator.SetFloat("Vertical", z);
            //Debug.Log(animator.GetFloat("Vertical") + " " + animator.GetFloat("Horizontal"));
        }
    }
}
