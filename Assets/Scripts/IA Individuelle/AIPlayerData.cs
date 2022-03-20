using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerData : MonoBehaviour
{
    [SerializeField] public Teams.Teamcolor color;
    public Placement placement = Placement.Unassigned;
    public bool isAI;
}
