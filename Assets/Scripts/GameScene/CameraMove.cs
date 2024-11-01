using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z );
    }
}
