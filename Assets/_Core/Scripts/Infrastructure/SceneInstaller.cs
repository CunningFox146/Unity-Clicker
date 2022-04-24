using FloppaApp.InputSource;
using FloppaApp.Service;
using UnityEngine;
using Zenject;

namespace FloppaApp.Infrastructure
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Application.targetFrameRate = 300;

            BindSave();
            BindScore();

#if UNITY_EDITOR
            BindInput<PcInput>();
#elif UNITY_ANDROID
            BindInput<MobileInput>();
#endif
        }

        private void BindSave()
        {
            Container.BindInterfacesAndSelfTo<SaveService>().AsSingle();
        }

        private void BindScore()
        {
            Container.BindInterfacesAndSelfTo<ScoreService>().AsSingle();
        }

        private void BindInput<T>() where T : IInputSource
        {
            Container.BindInterfacesAndSelfTo<T>().AsSingle();
        }
    }
}