using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class UpdatePathfinding : MonoBehaviour
{
    [SerializeField]
    AstarPath pathfinding;

    private void FixedUpdate() {
        pathfinding.Scan();
    }
}
