using DG.Tweening;
using UnityEngine;

namespace Components
{
    public class CameraShakeComponent : MonoBehaviour
    {
        [SerializeField] float amount = 1.0f;
        [SerializeField] float timeOfShake = 1;
        [SerializeField] int vibration = 5;

        void Start()
        {
            GlobalCommander.Commander.AddListener<ShakeCamereGlobalCommand>(this, ShakeCameraReact);
        }

        private void ShakeCameraReact(ShakeCamereGlobalCommand obj)
        {
            transform.DOShakePosition(timeOfShake, amount, vibration);
        }
    }

    public struct ShakeCamereGlobalCommand { }
}
