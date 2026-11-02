using Unity.VisualScripting;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private float speed = 5;
    private float zCenter = -2;
    private float outOfBounds = 40; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
        transform.Translate(Vector3.down * Time.deltaTime);

        if (transform.position.z != zCenter)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zCenter);
        }

        if(Mathf.Abs(transform.position.x) > outOfBounds)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
        //{
            Destroy(gameObject);
       // }
        
    }
}
