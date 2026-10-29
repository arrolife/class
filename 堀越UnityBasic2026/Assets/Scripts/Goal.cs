using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //  Playerが接触したら、GameClearに移行
        if(other.gameObject.CompareTag("Player"))
        {
            gameManager.GameClear();
        }
    }
}
