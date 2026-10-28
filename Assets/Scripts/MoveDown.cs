using UnityEngine;

public class MoveDown : MonoBehaviour
{
    private SpawnSection spawnSection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int floor; //When do we want to remove this section
    void Start()
    {
        spawnSection = GameObject.Find("GameManager").GetComponent<SpawnSection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnSection.isGameActive)
        {
            transform.Translate(Vector3.back * Time.deltaTime * spawnSection.sectionSpeed);
        }
        if(transform.position.y <= floor)
        {
            Destroy(gameObject);
        }
    }
}
