using FloppaApp.InputSource;
using FloppaApp.Service;
using System;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace FloppaApp.World
{
    public class FloppaSpawner : MonoBehaviour
    {
        [SerializeField] private Floppa _floppaPrefab;
        [SerializeField] private float _spawnRange;
        [SerializeField] private float _spawnHeight;

        private ObjectPool<Floppa> _pool;
        private ScoreService _scoreService;
        private IInputSource _input;

        [Zenject.Inject]
        private void Constructor(IInputSource input, ScoreService scoreService)
        {
            _scoreService = scoreService;
            _input = input;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            UnityEditor.Handles.DrawWireDisc(transform.position + Vector3.up * _spawnHeight, Vector3.up, _spawnRange);
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, _spawnRange);
        }
#endif

        private void Awake()
        {
            _pool = new(
                () => Instantiate(_floppaPrefab, transform),
                (item) => item.gameObject.SetActive(true),
                (item) => item.gameObject.SetActive(false)
            );
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
            _input.Click += OnClickHandler;
        }

        private void UnregisterEventHandlers()
        {
            _input.Click -= OnClickHandler;
        }

        private async void OnClickHandler(Vector2 pos)
        {
            _scoreService.Score++;

            var floppa = _pool.Get();
            floppa.transform.rotation = Quaternion.identity;
            floppa.transform.position = GetSpawnPos();

            await floppa.Appear();

            _pool.Release(floppa);
        }

        private Vector3 GetSpawnPos()
        {
            float angle = (float)Math.PI * 2f * Random.Range(0f, 1f);
            float range = Random.Range(0f, _spawnRange);
            return transform.position + new Vector3(Mathf.Cos(angle) * range, _spawnHeight, Mathf.Sin(angle) * range);
        }
    }
}
