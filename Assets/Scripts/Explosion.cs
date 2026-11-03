using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    private float delay = .5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Despawn());

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime);
    }
    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);

    }
}
