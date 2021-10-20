using UnityEngine;

public class Health : MonoBehaviour
{
    public int healthPoints = 4;

    public GameObject particalPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<OnMonsterClick>().ClickSum >= healthPoints)
        {
            if (particalPrefab != null)
            {
                Instantiate(particalPrefab, transform.position, Quaternion.identity);
            }

            if (GameManager.gm != null)
            {
                GameManager.gm.CountScore(1);
            }

            Destroy(gameObject);
        }
    }
}
