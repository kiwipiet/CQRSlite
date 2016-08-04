using System;

namespace CQRSCode.ReadModel.Dtos
{
    public class InventoryItemDetailsDto
    {
        public int CurrentCount;
        public Guid Id;
        public string Name;
        public int Version;

        public InventoryItemDetailsDto(Guid id, string name, int currentCount, int version)
        {
            Id = id;
            Name = name;
            CurrentCount = currentCount;
            Version = version;
        }
    }
}