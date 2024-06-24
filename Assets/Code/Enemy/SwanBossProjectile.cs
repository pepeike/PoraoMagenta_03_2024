using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanBossProjectile : MonoBehaviour
{

    private float timer = 0f;
    private const short timeoutTime = 6;
    private Rigidbody rb;
    [HideInInspector]
    public float projectileSpeed;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        timer += Time.deltaTime;
        if (timer >= timeoutTime) {
            Destroy(gameObject);
        }
        rb.velocity = transform.forward * projectileSpeed;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            GetComponent<Collider>().enabled = false;
            other.GetComponentInParent<PlayerControl>().TakeDamage(1);
        }
    }


}
