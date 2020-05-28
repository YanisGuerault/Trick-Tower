using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bonus : MonoBehaviour
{
    public Sprite icone;
    public abstract void Activate(GameObject piece);
}
