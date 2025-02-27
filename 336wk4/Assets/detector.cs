using UnityEngine;

public class detector : MonoBehaviour
{
    public GameManager GameManager;
    void OnTriggerEnter(Collider other)
    {
        GameManager.OnDetect(this);
    }
}
