using IJunior.TypedScenes;
using UnityEngine;

public class SpawnerArtefact : Spawner, ISceneLoadHandler<DataLoadScene>
{
    private void Update()
    {
        Spawned();
    }

    public void OnSceneLoaded(DataLoadScene argument)
    {
        _count = argument.LevelConfig.CountArtefact;
    }
}
