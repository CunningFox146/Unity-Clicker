using System;
using UnityEngine;
using Zenject;

namespace FloppaApp.InputSource
{
    public class PcInput : IInputSource, ITickable
    {
        public event Action<Vector2> Click;

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Click?.Invoke(Input.mousePosition);
            }
        }
    }
}
