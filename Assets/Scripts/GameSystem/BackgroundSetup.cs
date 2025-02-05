using UnityEngine;

public class BackgroundSetup : MonoBehaviour
{
    void Start()
    {
        Camera cam = Camera.main;
        if (cam != null)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, 0);

            float camHeight = cam.orthographicSize * 2;
            float camWidth = camHeight * cam.aspect;
            transform.localScale = new Vector3(camWidth, camHeight, 1);
        }
    }
}
