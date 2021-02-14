using UnityEngine;

public interface IFactory
{
    GameObject Create(string tag); // Ini sebenarnya tidak dibutuhkan, tapi tetap ada karena agar sesuai instruksi
    GameObject Create(string tag, Vector3 position, Quaternion rotation);
}
