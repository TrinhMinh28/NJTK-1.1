using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcScriptable : MonoBehaviour
{
    public List<SaveNpc> NpcList;
}

[Serializable]
public class SaveNpc
{
    public CreateNpc NpcCreate;
}