using UnityEngine;

namespace StatePattern.Player
{
    [CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/PlayerScriptableObject")]
    public class PlayerScriptableObject : ScriptableObject
    {
        public PlayerView PlayerPrefab;
        public Vector3 SpawnPosition;
        public Vector3 SpawnRotation;
        public float MovementSpeed;
        public float RotationSpeed;
        public int MaximumHealth;
        public int MeleeDamage;
        public int DelayAfterDeath;
    }
}