using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
   public LevelConfig config;
    public void StartLevel(){
        GameManager.Instance.currentLevel = config;
        SceneManager.LoadScene("GameScene");
        Debug.Log("Starting level: " + config.levelName);
    }

}
