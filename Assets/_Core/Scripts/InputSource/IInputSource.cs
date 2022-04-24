using System;
using UnityEngine;

namespace FloppaApp.InputSource
{
    public interface IInputSource
    {
        public event Action<Vector2> Click;
    }
}
