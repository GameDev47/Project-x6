using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class PlayerMovementScript : MonoBehaviourPunCallbacks
{
    [SerializeField]CinemachineFreeLook vCam;
    [SerializeField] Transform lookAt;
    private CinemachineTransposer transposer;
    Camera m_cam;


    private void Start()
    {
        m_cam = Camera.main;

       

        
        if (photonView.IsMine)
        {
            transposer = vCam.GetComponent<CinemachineTransposer>();

            vCam.gameObject.SetActive(true);
            enabled = true;
            
            

           // vCam.Follow = lookAt;
           // vCam.LookAt = lookAt;
        }


    }





    void Update()
        {


            if (photonView.IsMine)
            {
                {
                    if (Input.GetKey("w"))
                    {
                        transform.Translate(Vector3.forward * 10 * Time.deltaTime, Camera.main.transform);
                    }
                    if (Input.GetKey("a"))
                    {
                        transform.Translate(Vector3.left * 10 * Time.deltaTime, Camera.main.transform);
                    }
                    if (Input.GetKey("d"))
                    {
                        transform.Translate(Vector3.right * 10 * Time.deltaTime, Camera.main.transform);
                    }
                    if (Input.GetKey("s"))
                    {
                        transform.Translate(Vector3.back * 10 * Time.deltaTime, Camera.main.transform);
                    }

                    /* float D = Input.GetAxis("Horizontal") * 2f *Time.deltaTime;
                     float W = Input.GetAxis("Vertical") * 2f * Time.deltaTime;

                     transform.Translate(D, 0, W);*/
                }

            }
        }


    }

