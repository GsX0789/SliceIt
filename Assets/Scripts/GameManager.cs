using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField] List<ObjectsTarget> itemsToSpawn;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI lifeText;
    [SerializeField] TextMeshProUGUI gmOverText;
    [SerializeField] Button restartButton;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject pausedMenu;
    [SerializeField] AudioClip swordSwingSFX;
    [SerializeField] AudioClip bombExplosionSFX;
    [SerializeField] AudioSource gameMusicAudioSrc;
    [SerializeField] Slider audioSlider;
    private AudioSource sfxAudioSource;

    private float baseSpawnTime = 1.2f;
    private float totalSpawnTime;
    private int totalScoreMult = 1;
    private int playerScore = 0;
    private int totalLives = 3;
    private bool isPaused = false;
    private bool isPlayerAlive = false;
    public bool IsPlayerAlive { get => isPlayerAlive; }

    private void Start()
    {
        sfxAudioSource = GetComponent<AudioSource>();
        audioSlider.value = gameMusicAudioSrc.volume;
    }

    private void Update()
    {
        UpdateAudio();
        CheckForPause();
    }

    IEnumerator SpawnObjs()
    {
       while (isPlayerAlive)
        {
            yield return new WaitForSeconds(totalSpawnTime);
            InstantiateRandomObj();
        }
       
    }

    public void AddScore(int amountToAdd)
    {
        playerScore += amountToAdd * totalScoreMult;
        if(playerScore <= 0)
        {
            playerScore = 0;
        }
        scoreText.text = $"Score:\n{playerScore}";
    }

    public void Damage()
    {
        totalLives--;
        lifeText.text = $"Lives:\n{totalLives}";

        if (totalLives <= 0)
        {
            GameOverSequence();
            lifeText.text = $"Lives:\n{totalLives}";
        }
    }


    public void NewGame(float difficultMultiplier, int scoreMultiplier)
    {

        totalSpawnTime = baseSpawnTime * difficultMultiplier;
        totalScoreMult = scoreMultiplier;
        playerScore = 0;
        totalLives = 3;
        isPlayerAlive = true;
        Time.timeScale = 1;

        scoreText.text = $"Score:\n{playerScore}";
        lifeText.text = $"Lives:\n{totalLives}";

        restartButton.gameObject.SetActive(false);
        gmOverText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        lifeText.gameObject.SetActive(true);
        mainMenu.SetActive(false);
        
        StartCoroutine(SpawnObjs());
        Debug.Log(totalSpawnTime);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlaySwordSwing()
    {
        sfxAudioSource.clip = swordSwingSFX;
        sfxAudioSource.Play();
    }

    public void PlayBombExplosionSFX()
    {
        sfxAudioSource.clip = bombExplosionSFX;
        sfxAudioSource.Play();
    }

    void InstantiateRandomObj()
    {
        int randomIndex = Random.Range(0, itemsToSpawn.Count);
        Instantiate(itemsToSpawn[randomIndex]);
    }

    void GameOverSequence()
    {

        totalLives = 0;
        isPlayerAlive = false;
        gmOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        
        StopCoroutine(SpawnObjs());
    }

    void UpdateAudio()
    {
        gameMusicAudioSrc.volume = audioSlider.value;
    }

    void CheckForPause()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (isPlayerAlive)
            {
                if (!isPaused)
                {
                    pausedMenu.gameObject.SetActive(true);
                    Time.timeScale = 0;
                    isPaused = true;
                }
                else if(isPaused)
                {
                    pausedMenu.gameObject.SetActive(false);
                    Time.timeScale = 1;
                    isPaused = false;
                }
                
            }
        }
        
    }

}
