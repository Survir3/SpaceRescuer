using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TrainingIcon", menuName = "Training/Icon")]
public class TrainingIcon : ScriptableObject
{
    [SerializeField] private List<Sprite> _inputs;
    [SerializeField] private List<Sprite> _enemies;
    [SerializeField] private List<Sprite> _artefacts;
    [SerializeField] private List<Sprite> _survivor;

    public IReadOnlyList<Sprite> Inputs => _inputs;
    public IReadOnlyList<Sprite> Enemies => _enemies;
    public IReadOnlyList<Sprite> Artefacts => _artefacts;
    public IReadOnlyList<Sprite> Survivor => _survivor;
}
