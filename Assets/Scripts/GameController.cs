using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SlingShooter slingShooter;
    public TrailController trailController;
    public List<Bird> birds;
    public List<Enemy> enemies;
    private Bird _shotBird;
    public BoxCollider2D tapCollider;
    public GameObject gameOverScreen;

    private bool _isGameEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < birds.Count; i++)
        {
            birds[i].onBirdDestroyed += ChangeBird;
            birds[i].onBirdShot += AssignTrail;
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].onEnemyDestroyed += CheckGameEnd;
        }

        tapCollider.enabled = false;
        slingShooter.InitiateBird(birds[0]);
        _shotBird = birds[0];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void ChangeBird()
    {
        tapCollider.enabled = false;

        if (_isGameEnded)
        {
            return;
        }

        birds.RemoveAt(0);
        
        if(birds.Count > 0)
        {
            slingShooter.InitiateBird(birds[0]);
            _shotBird = birds[0];
        }
    }

    public void CheckGameEnd(GameObject destroyEnemy)
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i].gameObject == destroyEnemy)
            {
                enemies.RemoveAt(i);
                break;
            }
        }

        if(enemies.Count == 0)
        {
            _isGameEnded = true;
            GameOver();
        }
    }

    public void AssignTrail(Bird bird)
    {
        trailController.SetBird(bird);
        StartCoroutine(trailController.SpawnTrail());
        tapCollider.enabled = true;
    }

    private void OnMouseUp()
    {
        if(_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        gameOverScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
