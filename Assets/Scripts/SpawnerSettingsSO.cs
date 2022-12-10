using UnityEngine;

[CreateAssetMenu(menuName = "Create SpawnerSettingsSO", fileName = "SpawnerSettingsSO", order = 0)]
public class SpawnerSettingsSO: ScriptableObject
{
    public int circleRadius;
    public int yOffset;
    public int spawnCountPerWave;
    public float waveTime;
    public Enemy[] enemyArray = new Enemy[]{};
    public bool randomEnemySpawn;
    
    [Header("Only works if \"Random Enemy Spawn\" is false.")]
    public int spawningEnemyIndex;
    [Space(30)]
    public bool randomPositionSpawn;
    [Header("Only works if \"Random Position Spawn\" is false.")]
    public int spawnFromAngle;
    public int spaceBetween;
}