using UnityEngine;

public class textureOffsetAnimation : MonoBehaviour
{
    private Material material;
    private float timer = 0f;
    private float offsetIncrement = 0.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            Vector2 offset = material.mainTextureOffset;
            offset.y += offsetIncrement;
            material.mainTextureOffset = offset;
            timer = 0f;
        }
    }
}
