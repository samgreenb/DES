using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Shot 
{
    public CinemachineVirtualCamera virtualCamera;
    public float time;
    public List<IceStatueEffect> effects;
}
