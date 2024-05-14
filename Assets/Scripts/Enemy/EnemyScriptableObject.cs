using StatePattern.Enemy.Bullet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern.Enemy
{
    [CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/EnemyScriptableObject")]
    public class EnemyScriptableObject : ScriptableObject
    {
        public int LevelID;
        public EnemyView EnemyPrefab;
        public EnemyType Type;
        public Vector3 SpawnPosition;
        public Vector3 SpawnRotation;
        public float MovementSpeed;
        public int MaximumHealth;
        public float RangeRadius;

        public float IdleTime;
        public float RotationSpeed;
        public float RotationThreshold;

        public BulletScriptableObject BulletData;
        public float RateOfFire;

        public List<Vector3> PatrollingPoints;
        public float PlayerStoppingDistance;

        public float TeleportingRadius;
    }
}