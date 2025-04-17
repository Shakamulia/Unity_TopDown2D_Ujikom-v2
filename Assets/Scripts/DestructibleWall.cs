using UnityEngine;

public class DestructibleWall : MonoBehaviour {

    [SerializeField] private GameObject destroyVFX;

    // jika masuk kedalam suatu collider maka dia akan ke trigger dan menghancurkan blokade
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            EnemyDash enemy = collision.gameObject.GetComponent<EnemyDash>();
            if (enemy != null && enemy.isDashing) {
                Instantiate(destroyVFX, transform.position, Quaternion.identity);
                Destroy(gameObject); // Hancurkan diri sendiri
            }
        }
    }
}