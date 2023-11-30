using StatePattern.Drop;
using StatePattern.Enemy.Bullet;
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
        public float RangeAngle;
        public float RangeTeleporting;

        public float IdleTime;
        public float RotationSpeed;
        public float RotationThreshold;

        public BulletScriptableObject BulletData;
        public float RateOfFire;

        public List<Vector3> PatrollingPoints;
        public float PlayerAttackingDistance;

        public int CloneCount;
        public int DelayAfterGameEnd;

        public int SlowPlayerDownDuration; // only used for boss fight with roaring intimidation
        public List<EnemyScriptableObject> BossWave;
        public int FireBreathDamage;
        public int QuadrupleAttackDamage;
        public List<DropScriptableObject> dropData;
    }
}