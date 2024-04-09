using UnityEngine;
using System.Collections;

public abstract class MovementBehaviour : MonoBehaviour
{
    public abstract void EnableMovement();
    public abstract void DisableMovement();

    public abstract void SetLookRotation(Vector3 _targetPosition);
    public abstract void FaceTarget(Vector3 _targerRotation);
}
