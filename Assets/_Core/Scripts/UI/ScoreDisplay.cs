using FloppaApp.Service;
using TMPro;
using UnityEngine;

namespace FloppaApp.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class ScoreDisplay : MonoBehaviour
    {
        private TMP_Text _text;
        private ScoreService _scoreService;

        [Zenject.Inject]
        private void Constructor(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            OnScoreChangedHandler(_scoreService.Score);
        }

        private void OnEnable()
        {
            RegisterEventHandlers();
        }

        private void OnDisable()
        {
            UnregisterEventHandlers();
        }

        private void RegisterEventHandlers()
        {
            _scoreService.ScoreChanged += OnScoreChangedHandler;
        }

        private void UnregisterEventHandlers()
        {
            _scoreService.ScoreChanged -= OnScoreChangedHandler;
        }

        private void OnScoreChangedHandler(int score)
        {
            _text.text = score.ToString();
        }
    }
}
