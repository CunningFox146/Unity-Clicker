using System;
using UnityEngine;
using Zenject;

namespace FloppaApp.InputSource
{
    public class MobileInput : IInputSource, ITickable
    {
        public event Action<Vector2> Click;

        public void Tick()
        {
            if (Input.touchCount == 0) return;

            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Click?.Invoke(touch.position);
            }
        }
    }
}
