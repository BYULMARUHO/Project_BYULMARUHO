using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    private List<Transform> targets;
    private bool[] targetPossibles;

    private void Start()
    {
        targets = GetComponentsInChildren<Transform>().ToList();
        targets.RemoveAt(0);
        targetPossibles = new bool[targets.Count];

        for (int i = 0; i < targets.Count; i++) 
            targetPossibles[i] = true;
    }

    public Transform TargetHandler()
    {
        for(int i = 0; i < targetPossibles.Length; i++)
        {
            if (targetPossibles[i])
            {
                targetPossibles[i] = false;
                return targets[i];
            }
        }

        return null;
    }
}
