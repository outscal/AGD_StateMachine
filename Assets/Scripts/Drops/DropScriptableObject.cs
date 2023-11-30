using UnityEngine;

namespace StatePattern.Drop
{
    [CreateAssetMenu(fileName = "DropScriptableObject", menuName = "ScriptableObjects/DropScriptableObject")]
    public class DropScriptableObject : ScriptableObject
    {
        public DropType dropType;
        public DropView dropView;
        public float dropRadius;

        //an editor script can be written for defining certain properties that can only be changed based on dropType
        public int coinValue;
        public int freezeTime;
        public float freezeFactor;
        public Vector3 teleportationPosition;
        public int healthValue;
    }
}