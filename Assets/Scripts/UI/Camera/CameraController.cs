using UnityEngine;

namespace StatePattern.CameraEffects
{
    public class CameraController 
    {
        private CameraScriptableObject cameraScriptableObject;
        private CameraView cameraView;

        public CameraController(CameraScriptableObject cameraScriptableObject)
        {
            this.cameraScriptableObject = cameraScriptableObject;
            InitializeView();
        }

        private void InitializeView()
        {
            cameraView = Object.Instantiate(cameraScriptableObject.Prefab);
            cameraView.transform.position = new Vector3(0, 10, 0);
            cameraView.transform.rotation = Quaternion.Euler(90, 0, 0);
            cameraView.SetController(this);
        }

        #region Camera Effects
        public void ShakeCamera() => cameraView.ShakeCamera(cameraScriptableObject.shakeMagnitude, cameraScriptableObject.shakeDuration);
        #endregion
    }
}