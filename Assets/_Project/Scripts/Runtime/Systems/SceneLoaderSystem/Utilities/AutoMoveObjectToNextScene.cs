using PainfulSmile.Runtime.Systems.SceneLoaderSystem.Core;
using UnityEngine;

namespace PainfulSmile.Runtime.Systems.SceneLoaderSystem.Utilities
{
    public class AutoMoveObjectToNextScene : MonoBehaviour
    {
        [SerializeField] private GameObject objectToMove;
        
        private void Start()
        {
            int targetSceneIndex = SceneLoader.Instance.GetNextSceneIndex();

            if(targetSceneIndex < 0 )
            {
                return;
            }

            SceneLoader.Instance.MoveToOtherScene(targetSceneIndex, objectToMove);
        }
    }
}