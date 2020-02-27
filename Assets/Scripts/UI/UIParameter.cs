using UnityEngine;

public abstract class UIParameter<T> : MonoBehaviour
{
    public abstract void ChangeValue(T value);
}
