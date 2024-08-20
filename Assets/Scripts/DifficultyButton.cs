using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;
    [SerializeField] float difficultMultiplier = 1.0f;
    [SerializeField] int scoreMultiplier = 1;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficult);
        gameManager = FindObjectOfType<GameManager>();
    }

    void SetDifficult()
    {
        Debug.Log($"the btn that was clicked is: {gameObject.name} + difficultMult is {difficultMultiplier}");
        gameManager.NewGame(difficultMultiplier, scoreMultiplier);
    }

}
