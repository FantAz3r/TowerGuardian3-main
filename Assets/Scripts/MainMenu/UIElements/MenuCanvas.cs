using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    [field: SerializeField] public UIDummy Settings { get; private set; }
    [field: SerializeField] public SwichDamageNumbers SwichDamageNumbers { get; private set;}
    [field: SerializeField] public Mute Mute { get; private set;}
    [field: SerializeField] public ContinueButton ContinueButton { get; private set;}
    [field: SerializeField] public NewGameButton NewGameButton { get; private set;}
}
