using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject gameStartMenu;
    [SerializeField] GameObject gameRestartMenu;
    [SerializeField] TextMeshProUGUI endScore;
    [SerializeField] TextMeshProUGUI gameScore;

    private void Start()
    {
        gameStartMenu.SetActive(true);
        gameRestartMenu.SetActive(false);
    }

    private void Update()
    {
        gameScore.text = "Score: " + playerController.score;
        
        if (playerController.isDead)
        {
            gameRestartMenu.SetActive(true);
            endScore.text = "Score: " + playerController.score;
        }
    }

    public void StartGame()
    {
        playerController.isStart = true;
        playerController.anim.SetBool("Run", true);
        gameStartMenu.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
