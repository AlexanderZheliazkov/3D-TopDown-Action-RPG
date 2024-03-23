using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Animation Set", menuName = "ScriptableObjects/MovementAnimationSet")]
public class MovementAnimationSet : ScriptableObject
{
    public AnimationClip Idle;
    public AnimationClip Run;
}
