using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    StarterCamera starterCamera;
    public bool isStartMoving = false;

    [SerializeField]
    GameObject menuPanel, inGamePanel, finishPanel;
    public int level;

    private void Awake()
    {
        level = FileManager.LoadScore();
        starterCamera = FindObjectOfType<StarterCamera>();
    }
    private void Update()
    {
        if (StackSystem.instance.gameState == StackSystem.GameState.finished)
        {
            isStartMoving = false;
            Invoke("finishGame", 5f);
        }
    }
    public void finishGame()
    {
        menuPanel.SetActive(false);
        inGamePanel.SetActive(false);
        finishPanel.SetActive(true);
    }
    public void StartGame()
    {
        StackSystem.instance.totalMoney = FileManager.LoadScore();
        menuPanel.SetActive(false);
        inGamePanel.SetActive(true);
        finishPanel.SetActive(false);
        StartCoroutine(IEStartGame());
    }

    IEnumerator IEStartGame()
    {
        starterCamera.Play();
        yield return new WaitForSeconds(1.25f);
        isStartMoving = true;
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        StackSystem.instance.totalMoney += StackSystem.instance.newMoneyValue;
        FileManager.SaveScore(StackSystem.instance.totalMoney);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        level = SceneManager.GetActiveScene().buildIndex;
        FileManager.SaveScore(level);
    }
}
