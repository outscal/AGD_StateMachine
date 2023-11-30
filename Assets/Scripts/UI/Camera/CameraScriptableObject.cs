using UnityEngine;

namespace StatePattern.CameraEffects
{
    [CreateAssetMenu(fileName = "CameraScriptableObject", menuName = "ScriptableObjects/CameraScriptableObject")]
    public class CameraScriptableObject : ScriptableObject
    {
        public CameraView Prefab;
        public float shakeDuration;
        public float shakeMagnitude;
    }
}