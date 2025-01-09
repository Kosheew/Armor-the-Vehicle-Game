using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings")]
public class GameSettings : ScriptableObject
{
    [Header("Frame Rate")]
    [SerializeField, Tooltip("Frame rate game")]
    private int frameRate = 30;
    
    [Header("Pool Size")]
    [SerializeField, Tooltip("Maximum number of enemies on the scene")]
    private int maxEnemies = 25;

    [SerializeField, Tooltip("Maximum number of bullets in the pool")]
    private int maxBullets = 10;

    [Header("Time Game")]
    [SerializeField, Tooltip("Time for the game in seconds")]
    private float timeGame = 20;

    [Header("Main Camera settings")]
    [SerializeField, Tooltip("Camera movement speed")]
    private float speedCamera = 5f;

    [SerializeField, Tooltip("Camera offset angle along the X-axis")]
    private float cameraOffsetAngleX = 10f;

    [Header("Ground Settings")]
    [SerializeField, Tooltip("Ground movement speed")]
    private float speedGround = 30f;

    [SerializeField, Tooltip("Ground offset")]
    private float offsetGround = 52f;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnIntervalEnemy = 2f;
    public int MaxEnemies => maxEnemies;
    public int MaxBullets => maxBullets;
    public float TimeGame => timeGame;
    public float SpeedCamera => speedCamera;
    public float CameraOffsetAngleX => cameraOffsetAngleX;
    public float SpeedGround => speedGround;
    public float OffsetGround => offsetGround;
    public float SpawnIntervalEnemy => spawnIntervalEnemy;
    public int FrameRate => frameRate;
}