using UnityEngine;
using Utils.EnumTypes;

public class MachineController : MonoBehaviour
{
    public MachineType machineType;
    public GameObject food;

    // 음식 저장
    public void AddStorageFood(GameObject _food, Transform _pos)
    {
        food = _food;
        Instantiate(food, _pos);
    }

    // 음식 빼기
    public void DellStorageFood()
    {
        food = null;
        Destroy(transform.GetChild(0).gameObject);
    }
}