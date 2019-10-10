using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "PlayerData/PlayerScriptableObject", order = 1)]
public class PlayerInfoScriptableObject : ScriptableObject
{
    public int startingScore = 0;

    // Element 0 -> Body
    // Element 1 -> Joints
    public List<Material> player1Mats = new List<Material>();
    public List<Material> player2Mats = new List<Material>();
}
