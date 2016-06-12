using System;
using System.Collections.Generic;
using System.Diagnostics;
using CQRSCode.ReadModel.Events;
using CQRSlite.Domain;
using CQRSlite.Events;

namespace CQRSCode.WriteModel.Domain
{
    public class InventoryItem : AggregateRoot
    {
        private bool _activated;

        private readonly IDictionary<Type, Action<IEvent>> _applyEvent = new Dictionary<Type, Action<IEvent>>();

        public override void Apply(IEvent @event)
        {
            Debug.WriteLine(@event);
            var eventType = @event.GetType();
            if (_applyEvent.ContainsKey(eventType)) _applyEvent[eventType].Invoke(@event);
        }

        public void ChangeName(string newName)
        {
            if (string.IsNullOrEmpty(newName)) throw new ArgumentException("newName");
            ApplyChange(new InventoryItemRenamed(Id, newName));
        }

        public void Remove(int count)
        {
            if (count <= 0) throw new InvalidOperationException("cant remove negative count from inventory");
            ApplyChange(new ItemsRemovedFromInventory(Id, count));
        }


        public void CheckIn(int count)
        {
            if(count <= 0) throw new InvalidOperationException("must have a count greater than 0 to add to inventory");
            ApplyChange(new ItemsCheckedInToInventory(Id, count));
        }

        public void Deactivate()
        {
            if(!_activated) throw new InvalidOperationException("already deactivated");
            ApplyChange(new InventoryItemDeactivated(Id));
        }

        public InventoryItem()
        {
            _applyEvent.Add(typeof(InventoryItemCreated), e => _activated = true);
            _applyEvent.Add(typeof(InventoryItemDeactivated), e => _activated = false);
        }
        public InventoryItem(Guid id, string name) : this()
        {
            Id = id;
            ApplyChange(new InventoryItemCreated(id, name));
        }
    }
}