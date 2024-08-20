using UnityEngine;

//This make sure that the gameObject that will receive this script need to have the conditions below!!
[RequireComponent (typeof(TrailRenderer), typeof(BoxCollider))]

public class ClickAndSwipe : MonoBehaviour
{
    private GameManager gameManager;
    private Camera cam;
    private Vector3 mousePos;
    private TrailRenderer trail;
    private BoxCollider boxCol;

    private bool swiping = false;

    //TODO, try to adjust it, so we use raycast to detect the hit!!

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        cam = Camera.main;
        trail = GetComponent<TrailRenderer>();
        boxCol = GetComponent<BoxCollider>();
        trail.enabled = false;
        boxCol.enabled = false;
    }

    private void Update()
    {
        if (gameManager.IsPlayerAlive)
        {
            if (Input.GetMouseButton(0))
            {
                swiping = true;
                UpdateComponents();
            }
            else
            {
                swiping = false;
                UpdateComponents();
            }

            if(swiping)
            {
                UpdateMousePos();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<ObjectsTarget>() != null)
        {
            collision.gameObject.GetComponent<ObjectsTarget>().DestroyTarget();
        }
    }

    void UpdateMousePos()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        transform.position = mousePos;
    }

    void UpdateComponents()
    {
        if(trail != null && boxCol != null)
        {
            trail.enabled = swiping;
            boxCol.enabled = swiping;
        }
        
    }
}
