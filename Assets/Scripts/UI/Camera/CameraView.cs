using System.Collections;
using UnityEngine;

namespace StatePattern.CameraEffects{
    [RequireComponent(typeof(Camera))]
    public class CameraView : MonoBehaviour
    {
        private CameraController cameraController;

        private Vector3 originalPosition;

        public void SetController(CameraController cameraController) => this.cameraController = cameraController;

        public void ShakeCamera(float magnitude, float duration){
            originalPosition = transform.localPosition;
            StartCoroutine(Shake(magnitude, duration));
        }

        private IEnumerator Shake(float magnitude, float duration)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                Vector3 randomPos = originalPosition + Random.insideUnitSphere * magnitude;
                transform.localPosition = randomPos;

                elapsed += Time.deltaTime;

                yield return null;
            }

            transform.localPosition = originalPosition;
        }
    }   
}