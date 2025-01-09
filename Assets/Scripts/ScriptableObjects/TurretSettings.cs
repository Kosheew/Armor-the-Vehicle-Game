using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "TurretSettings", menuName = "Settings/TurretSettings")]
public class TurretSettings : ScriptableObject
{ 
    [SerializeField] private float cooldown = 0.4f;
    [SerializeField] private float turnSpeed = 15f;
    
    public float Cooldown => cooldown;
    public float TurnSpeed => turnSpeed;
}
