using UnityEngine;

public interface IFactory
{
    GameObject Create(string tag);
    GameObject Create(string tag, Vector3 position, Quaternion rotation);
}
