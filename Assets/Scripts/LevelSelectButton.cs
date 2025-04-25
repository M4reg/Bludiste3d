using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelSelectButton : MonoBehaviour
{
   public LevelConfig config;
   private Button button;
   
   void Start()
    {
        // Získání komponenty Button
        button = GetComponent<Button>();
        if (button == null)
        {
            return;
        }
    // Enable button only if the level is unlocked
        button.interactable = GameManager.Instance.IsLevelUnlocked(config);
    }
    // Spuštění úrovně po kliknutí na tlačítko
    public void StartLevel(){
        GameManager.Instance.currentLevel = config;
        SceneManager.LoadScene("GameScene");
    }
    
}