using System;
using UnityEngine;

namespace StatePattern.Sound
{
    [CreateAssetMenu(fileName = "SoundScriptableObject", menuName = "ScriptableObjects/SoundScriptableObject")]
    public class SoundScriptableObject : ScriptableObject
    {
        public Sounds[] audioList;
    }

    [Serializable]
    public struct Sounds
    {
        public SoundType soundType;
        public AudioClip audio;
    }
}