using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
Canon,
Collision
}


public class GenericHull
{
    public int ID { get; set; }
    public string Name { get; set; }
    
    public string ModelPath { get; set; }

    public float Price { get; set; }
    public float MovementPenalty { get; set; }
    public float Armor { get; set; }

    public float Damage { get; set; }
    public DamageType DamageType { get; set; }
}
