using UnityEngine;

public class Sensor : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.GetComponent<ObjectsTarget>() != null)
       {
            ObjectsTarget targetRef = collision.gameObject.GetComponent<ObjectsTarget>();
            if(targetRef != null)
            {
                if(!targetRef.IsBadObj) 
                {
                    gameManager.Damage();
                }
                Destroy(collision.gameObject);
            } 
            
       }
    }
}
