using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.WSA;

public class Bomb : MonoBehaviour
{
    private float outOfBoundsX = 40;
    private float outOfBoundsY = -15;
    private float zCenter = -2;
    private float bombForce = 5;
    public float direction = 1;
    private Rigidbody bombRb;

    public GameObject explosion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bombRb = GetComponent<Rigidbody>();
        //Vector3 launch = new Vector3(1, 2, 0);
        //bombRb.AddForce(launch * bombForce, ForceMode.Impulse);
        bombRb.AddForce(Vector3.right * direction/4 * bombForce, ForceMode.Impulse);
        bombRb.AddForce(Vector3.up * bombForce * 3, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z != zCenter)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zCenter);
        }

        if (Mathf.Abs(transform.position.x) > outOfBoundsX || transform.position.y<outOfBoundsY)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
        //{
        Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        Destroy(gameObject);
        // }

    }
}
