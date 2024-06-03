using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public Camera cam;
    public Transform LookAt;

    

    private PlayerControl playerControl;

    private GameObject playerBody;

    private void Awake() {
        playerControl = GetComponent<PlayerControl>();
        playerBody = playerControl.playerBody;
    }

    private void LateUpdate() {
        Vector2 dir = playerControl.movement.ReadValue<Vector2>();
        LookAt.localPosition = new Vector3(dir.x, 0, dir.y);
        playerBody.transform.LookAt(LookAt, Vector3.up);
        
    }

}
