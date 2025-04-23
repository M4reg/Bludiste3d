using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelSelectButton : MonoBehaviour
{
   public LevelConfig config;
   private Button button;
   
   void Start()
    {
        button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("Button component not found on LevelSelectButton!");
            return;
        }
    // Enable button only if the level is unlocked
        button.interactable = GameManager.Instance.IsLevelUnlocked(config);
    }
    public void StartLevel(){
        GameManager.Instance.currentLevel = config;
        SceneManager.LoadScene("GameScene");
        Debug.Log("Starting level: " + config.levelName);
    }
    
}