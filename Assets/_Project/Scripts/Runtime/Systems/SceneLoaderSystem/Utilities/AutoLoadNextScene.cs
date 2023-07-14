using PainfulSmile.Runtime.Systems.SceneLoaderSystem.Core;
using UnityEngine;

namespace PainfulSmile.Runtime.Systems.SceneLoaderSystem.Utilities
{
    public class AutoLoadNextScene : MonoBehaviour
    {
        private void Start()
        {
            SceneLoader.Instance.NextScene();
        }
    }
}