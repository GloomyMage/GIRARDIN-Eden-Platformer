using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRPT_Camera2 : MonoBehaviour
{
    public GameController count;
    public SCRPT_Camera_Control control;
    public Transform target1;
    public Transform target2;
    public Transform target3;

    Vector3 velocity = Vector3.zero;

    [Range(0, 1)]
    public float smoothTime;

    public Vector3 positionOffset;

    private void Awake()
    {
        Vector3 targetPosition = target1.position + positionOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        this.GetComponent<SCRPT_Camera_Control>().enabled = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && count.count >= 0)
        {
            this.GetComponent<SCRPT_Camera2>().enabled = false;
            this.GetComponent<SCRPT_Camera_Control>().enabled = true;
        }

        if (count.count < 19)
        {
            smoothTime = 0;
            Vector3 targetPosition = target1.position + positionOffset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            smoothTime = 1;

        }

        else if (count.count < 25)
        {
            Vector3 targetPosition = target2.position + positionOffset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            smoothTime = 0.25f;
        }
        else if (count.count < 26)
        {
            smoothTime = 1;
            Vector3 targetPosition = target3.position + positionOffset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        else if (count.count < 27)
        {
            Vector3 targetPosition = target1.position + positionOffset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }

        else if (count.count < 28)
        {
            this.GetComponent<SCRPT_Camera2>().enabled = false;
            this.GetComponent<SCRPT_Camera_Control>().enabled = true;
        }
        }
    }
