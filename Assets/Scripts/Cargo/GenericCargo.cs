using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GenericCargo
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string IconPath { get; set; }
    public float Price { get; set; }
}
