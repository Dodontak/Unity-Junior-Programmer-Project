using System;
using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject mainCamera;
    [SerializeField] private float BackgroundDist;
    private float cameraTrackingRate;
    private int page;
    private Vector3 nextPos;
    void Awake()
    {
        mainCamera = GameObject.Find("Main Camera");
        page = 0;
        cameraTrackingRate = BackgroundDist / 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        if (mainCamera != null)
        {
            float diff = mainCamera.transform.position.x - transform.position.x;
            if (diff > 16f)
            {
                nextPos = new Vector3(transform.position.x + 16f, transform.position.y, transform.position.z);
                ++page;
            }
            else if (diff < -16f)
            {
                nextPos = new Vector3(transform.position.x - 16f, transform.position.y, transform.position.z);
                --page;
            }
            else
            {
                nextPos = new Vector3((mainCamera.transform.position.x * cameraTrackingRate) + page * 16f, transform.position.y, transform.position.z);
            }
            transform.position = nextPos;
        }
    }
}
