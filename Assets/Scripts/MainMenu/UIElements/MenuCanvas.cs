using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    [field: SerializeField] public UIDummy Settings { get; private set; }
    [field: SerializeField] public SwichDamageNumbers SwichDamageNumbers { get; private set;}
    [field: SerializeField] public StartButton StartButton { get; private set;}

}
