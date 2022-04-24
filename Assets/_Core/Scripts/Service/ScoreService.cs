using System;

namespace FloppaApp.Service
{
    public class ScoreService
    {
        public event Action<int> ScoreChanged;

        private SaveService _saveService;

        private int _score;

        public int Score
        {
            get => _score;
            set
            {
                if (_score == value) return;
                _score = value;
                ScoreChanged?.Invoke(_score);
                _saveService?.SaveScore(_score);
            }
        }

        [Zenject.Inject]
        public ScoreService(SaveService saveService)
        {
            _saveService = saveService;

            _score = _saveService.LoadScore();
        }
    }
}
