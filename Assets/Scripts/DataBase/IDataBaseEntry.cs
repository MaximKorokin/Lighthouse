using System;

public interface IDataBaseEntry
{
    string Id { get; set; }

    void GenerateId()
    {
        Id = Guid.NewGuid().ToString();
    }
}
