using LeopotamGroup.Globals;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class UI : MonoBehaviour
    {
        public Slider AudioSlider;

        public float EndGameScreenFadeDuration;
        public CanvasGroup EndGameScreen;
        public Image OutGameAreaWarning;
        public Image ChangeSceneFade;

        public TMP_Text Developers;
        public Button RestartButton;

        public string[] DevelopersVariations;
        public float ChangeSceneFadeDuration;

        public string MainSceneName;

        private void Start()
        {
            RestartButton.onClick.AddListener(Restart);
        }
        
        public void Restart()
        {
            var runtimeData = Service<RuntimeData>.Get();
            runtimeData.IsNewGame = true;
            SceneManager.LoadScene(MainSceneName);
        }
    }
}