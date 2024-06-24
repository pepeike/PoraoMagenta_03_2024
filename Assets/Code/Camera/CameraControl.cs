using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public Camera cam;
    public Transform LookAt;

    

    private PlayerControl playerControl;

    private GameObject playerBody;

    private Vector3 dirVec3 = Vector3.right;

    private void Awake() {
        playerControl = GetComponent<PlayerControl>();
        playerBody = playerControl.playerBody;
    }

    private void LateUpdate() {
        Vector2 dir = playerControl.movement.ReadValue<Vector2>();
        
        if (dir != Vector2.zero ) {
            dirVec3 = new Vector3(dir.x, 0f, dir.y);
        }
        
        playerBody.transform.forward = dirVec3;
        
        
        
    }

}
