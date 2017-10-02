using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers {
    public static KeyCode StringToKeycode(string s)
    {
        return (KeyCode)System.Enum.Parse(typeof(KeyCode), s);
    }
}
