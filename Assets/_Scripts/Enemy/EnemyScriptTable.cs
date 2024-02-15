using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScriptTable : ScriptableObject
{

    public List<SaveEnemy> _BaseEnemy;
    public List<SaveBossEnemy> _BossEnemy; // sử dụng trong trường hợp có nhiều nhiệm vụ con
}

[Serializable]
public class SaveEnemy
{
    public CreateEnemy EnemyCreate;
}
public class SaveBossEnemy
{
    public CreateBossEnemy EnemyBossCreate;
}
