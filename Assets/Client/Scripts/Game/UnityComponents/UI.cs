using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class UI : MonoBehaviour
    {
        public FinalCutScene FinalCutScene;
        public Slider AudioSlider;

        public float EndGameScreenFadeDuration;
        public CanvasGroup EndGameScreen;
        public Image OutGameAreaWarning;

        public TMP_Text Developers;

        public string[] DevelopersVariations;
    }
}