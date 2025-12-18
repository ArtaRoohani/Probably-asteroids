using UnityEngine;

public class Asteroid : MonoBehaviour {
    public Transform objectToLookAt;
    [SerializeField] private ParticleSystem destroyedParticles;
    public int size = 3;
    public int clones = 2;

    [SerializeField] private float minAngularVelocity = -200f;
    [SerializeField] private float maxAngularVelocity = 200f;

    public GameManager gameManager;
    private void Start() {
        transform.localScale = 0.5f * size * Vector3.one;
    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    Vector2 direction = new Vector2(Random.value, Random.value).normalized;
    float spawnSpeed = Random.Range(4f - size, 5f - size);
    rb.AddForce(direction * spawnSpeed, ForceMode2D.Impulse);
    rb.angularVelocity = Random.Range(minAngularVelocity, maxAngularVelocity);

    gameManager.asteroidCount++;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Bullet")) {

            gameManager.asteroidCount--;
            
            Destroy(collision.gameObject);

            if (size > 1) {
              for (int i = 0; i < clones; i++) {
                    Asteroid newAsteroid = Instantiate(this, transform.position, Quaternion.identity);
                    newAsteroid.size = size - 1;
                    newAsteroid.gameManager = gameManager;                    
               }
            }


            Instantiate(destroyedParticles, transform.position, Quaternion.identity);
            FindAnyObjectByType<GameManager>().IncreaseScore();
            Destroy(gameObject);
        }
    }

}