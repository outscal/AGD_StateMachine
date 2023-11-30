using UnityEngine;

namespace StatePattern.Enemy.Bullet
{
    [CreateAssetMenu(fileName = "BulletScriptableObject", menuName = "ScriptableObjects/BulletScriptableObject")]
    public class BulletScriptableObject : ScriptableObject
    {
        public BulletView Prefab;
        public Vector3 SpawnPositionOffset;
        public float Speed;
        public int Damage;
    }
}