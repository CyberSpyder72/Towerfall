using UnityEngine;

public class SidePlatform : MonoBehaviour
{
    private float horizontalBound = 9;
    private int direction = 1;
    private int platformSpeed = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.position.x) > horizontalBound)
        {
            direction *= -1;
            transform.position = new Vector3(horizontalBound*-direction, transform.position.y, transform.position.z);

        }
        transform.Translate(Vector3.right * Time.deltaTime * platformSpeed * direction);
    }
}
