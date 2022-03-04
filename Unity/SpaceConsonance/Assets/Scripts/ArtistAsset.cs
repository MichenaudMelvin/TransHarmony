using UnityEngine;

[CreateAssetMenu(fileName = "New Artist", menuName = "Artist")]
public class ArtistAsset : ScriptableObject{

    [Header("Basics")]
    [HideInInspector]
    [Tooltip("Hall de l'artiste")]
    public int currentHall;

    [Tooltip("Nom de l'artiste")]
    public string _name; // https://www.lestrans.com/trans-2021/

    [Tooltip("Musique que l'artiste joue")]
    public AudioClip _music;

    [HideInInspector]
    [Tooltip("Si l'artiste a déjà fait un concert (présent dans un hall)")]
    public bool _alreadyPerform;

    // [HideInInspector]
    [Tooltip("Si l'artiste a déjà joué sa musique")]
    public bool _hasPlayMusic = false;

    [HideInInspector]
    [Tooltip("Si l'artiste a déjà joué sa musique (same que current hall mais prend le hall en référence == mieux pour accéder aux méthodes)")]
    public HallsAttributes _hall;
}
