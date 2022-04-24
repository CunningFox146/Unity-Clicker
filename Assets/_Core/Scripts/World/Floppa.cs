using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace FloppaApp.World
{
    [RequireComponent(typeof(Rigidbody))]
    public class Floppa : MonoBehaviour
    {
        [SerializeField] private int _destroyDelay;
        [SerializeField] private int _angularVel;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public async UniTask Appear()
        {
            var ct = this.GetCancellationTokenOnDestroy();

            _rigidbody.angularVelocity = Vector3.up * _angularVel;
            transform.localScale = Vector3.zero;
            await transform.DOScale(Vector3.one, 0.5f).ToUniTask(cancellationToken: ct);
            await UniTask.Delay(_destroyDelay);
            await transform.DOScale(Vector3.zero, 0.5f).ToUniTask(cancellationToken: ct);
        }
    }
}