using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class MovementEffect : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 2f);
    }
}
