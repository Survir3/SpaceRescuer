using IJunior.TypedScenes;
using UnityEngine;

public class SpawnerArtefact : Spawner, ISceneLoadHandler<LevelConfig>
{
    private void Update()
    {
        Spawned();
    }

    public void OnSceneLoaded(LevelConfig argument)
    {
        _count = argument.CountArtefact;
    }
}
