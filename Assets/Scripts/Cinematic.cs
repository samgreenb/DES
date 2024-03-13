using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic : MonoBehaviour
{
    [SerializeField]
    private List<Shot> shotList = new List<Shot>();

    public List<Shot> ShotList { get { return shotList; } }

}
