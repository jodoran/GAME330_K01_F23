using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemySO enemySO;

    [System.Serializable]
    {
        public string name;
        public int count;
        public float rate;

    }
}
