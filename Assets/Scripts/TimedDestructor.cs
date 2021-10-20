using UnityEngine;

public class TimedDestructor : MonoBehaviour
{
    public float lifeTime = 2.0f;

    void Awake()
    {
        Invoke("Destroy", lifeTime);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
