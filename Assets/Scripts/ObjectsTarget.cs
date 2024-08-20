using UnityEngine;

public class ObjectsTarget : MonoBehaviour
{

    //Reference variables
    [SerializeField] bool isBadObj;
    [SerializeField] int pointsAmount = 50;
    [SerializeField] ParticleSystem explosionParticle;
    private Rigidbody objRB;
    private GameManager gameManager;
    public bool IsBadObj { get => isBadObj; }

    //Logical variables
    private float ySpawnPos = -5;
    private float xSpawnPos = 4;
    private float maxThrowForce = 14;
    private float minThrowForce = 10;
    private float minTorqueForce = 10;
    private float maxTorqueForce = 15;
    private Vector2 spawnPos;

    private void Awake()
    {
        objRB = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetObjectPos();
        ThrowObjectUpwards();
    }

    /// <summary>
    /// Destroy the target, if it's a good target, add the points if it's a bad one, remove the points and lives from the player!!
    /// </summary>
    public void DestroyTarget()
    {
        if(gameManager.IsPlayerAlive)
        {
            if (!isBadObj)
            {
                gameManager.AddScore(pointsAmount);
                gameManager.PlaySwordSwing();
                Destroy(gameObject);
            }
            else
            {
                gameManager.AddScore(-pointsAmount);
                gameManager.PlayBombExplosionSFX();
                gameManager.Damage();
                Destroy(gameObject);

            }

            Instantiate(explosionParticle, transform.position, Quaternion.identity);
        }

    }

    /// <summary>
    /// Add Force and Torque to the object, allowing it to go up and rotate
    /// </summary>
    void ThrowObjectUpwards()
    {
        objRB.AddForce(Vector2.up * ReturnRandomForce(), ForceMode.Impulse);

        objRB.AddTorque(ReturnRandomTorque(), ReturnRandomTorque(), 
            ReturnRandomTorque(), ForceMode.Impulse);
    }
    /// <summary>
    /// Set the object spawn pos, the method uses a random Pos in the x
    /// </summary>
    void SetObjectPos()
    {
        spawnPos = new Vector3(ReturnRandomXPos(), ySpawnPos);
        transform.position = spawnPos;
    }

    float ReturnRandomForce() => Random.Range(minThrowForce, maxThrowForce);

    float ReturnRandomTorque() => Random.Range(minTorqueForce , maxTorqueForce);

    float ReturnRandomXPos() => Random.Range(-xSpawnPos, xSpawnPos);
}
