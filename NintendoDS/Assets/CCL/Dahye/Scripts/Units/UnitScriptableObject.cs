using UnityEngine;

//외부 접근가능 
public enum UnitType
{
    Level1,
    Level2,
    Level3,
    Level4,
    Level5,
    Level6,
    Level7,
    Level8,
    Level9,
    Level10,
    Level11
}


[CreateAssetMenu]
public class UnitScriptableObject : ScriptableObject
{
    public UnitType UnitType;
    public GameObject UnitPrefabs;
    public string UnitName;
    public int Score;
    // 애니메이션 

}

