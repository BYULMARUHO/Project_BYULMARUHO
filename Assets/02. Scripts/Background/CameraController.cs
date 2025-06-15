using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform playerTransform;

    private Vector3 cameraPosition = new Vector3(0, 0, -10.0f);
    private float cameraMoveSpeed = 10.0f;

    public Vector2 center;
    public Vector2 mapSize;
    private float height;
    private float width;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    private void FixedUpdate()
    {
        LimitCameraArea();
    }

    public void LimitCameraArea()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position + cameraPosition, cameraMoveSpeed * Time.deltaTime);

        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, center.x - lx, center.x + lx);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, center.y - ly, center.y + ly);

        transform.position = new Vector3(clampX, clampY, -10.0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
