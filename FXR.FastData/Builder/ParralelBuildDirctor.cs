/// Copyright (C) 2023 FXR, S. Teunisse All rights reserved.
public class ParralelBuildDirctor<IBuildableObject> : IBuilder<IBuildableObject>
    where IBuildableObject : IBuildable
{
    private readonly List<Guid> _objectIds;
    private List<IBuildable>? _resultObjects;

    public ParralelBuildDirctor(List<Guid> objs) => _objectIds = objs;

    public void DirectMultiple()
    {
        List<IBuildable> results = new();

        Parallel.ForEach(_objectIds, async i =>
        {
            var builder = BuildObject(i);
            var buildResult = builder.Build();
            results.Add(buildResult);
        });

        _resultObjects = results;
    }

    public virtual IBuildDirector<IBuildableObject> BuildObject(Guid id) 
    {
        return null;
    }

    public List<IBuildable> GetObjects()
    {
        var result = new List<IBuildable>();
        result.AddRange(_resultObjects);
        return result;
    }
}
