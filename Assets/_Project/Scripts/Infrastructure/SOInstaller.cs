using UnityEngine;
using Zenject;

namespace Gisha.Destruction.Infrastructure
{
    [CreateAssetMenu(fileName = "SOInstaller", menuName = "Installers/SOInstaller")]
    public class SOInstaller : ScriptableObjectInstaller<SOInstaller>
    {
        public override void InstallBindings()
        {
        }
    }
}