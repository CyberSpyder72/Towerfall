using UnityEngine;

public class BounceBall : MonoBehaviour
{
    private float outOfBoundsX = 40;
    private float outOfBoundsY = -15;
    private float zCenter = -2;
    private float ballForce = 3;
    public float direction = 1;
    private Rigidbody ballRb;

    public GameObject pop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballRb = GetComponent<Rigidbody>();
        //Vector3 launch = new Vector3(1, 2, 0);
        //bombRb.AddForce(launch * bombForce, ForceMode.Impulse);
        ballRb.AddForce(Vector3.right * direction * ballForce, ForceMode.Impulse);
        ballRb.AddForce(Vector3.up * ballForce/2, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z != zCenter)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zCenter);
        }

        if (Mathf.Abs(transform.position.x) > outOfBoundsX || transform.position.y < outOfBoundsY)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Rigidbody platformRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 bounce = (transform.position - collision.gameObject.transform.position).normalized;

            //ballRb.AddForce(bounce*ballForce, ForceMode.Impulse);
        }

        else if (collision.gameObject.CompareTag("Wall"))
        {
            Rigidbody platformRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 bounce = (collision.gameObject.transform.position - transform.position).normalized;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Instantiate(pop, transform.position, transform.rotation);
    }
}
