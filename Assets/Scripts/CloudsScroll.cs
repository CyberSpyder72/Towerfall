using UnityEngine;

public class CloudsScroll : MonoBehaviour
{
    public Vector2 flowSpeed = new Vector2(0.02f, 0.015f);
    Renderer rend;
    public Vector2 offset;

    void Start() => rend = GetComponent<Renderer>();

    void Update()
    {
        offset += flowSpeed * Time.deltaTime;
        rend.material.mainTextureOffset = offset;
    }
}