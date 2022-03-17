using UnityEngine;

public abstract class PlayerBrain : MonoBehaviour
{
    

    protected Player Player { get; private set; }

    protected Team Allies => Player.Team;
    protected Team Enemies => Allies == Field.Team1 ? Field.Team2 : Field.Team1;

    protected virtual void Awake()
    {
        Player = GetComponent<Player>();
    }

    protected bool IsOther(Player player)
    {
        return player != Player;
    }

    /// <summary>
    /// Calcule le d�placement que l'IA doit appliquer au joueur/que la manette d�tecte
    /// </summary>
    /// <param name="team">L'�quipe du joueur</param>
    /// <returns>Le vecteur de d�placement.</returns>
    public abstract Vector3 Move();

    public abstract Action Act();
}
