﻿using Flunt.Notifications;

namespace IWantoApp_Project2.Domain;

public abstract class Entity : Notifiable<Notification>
{
    protected Entity()
    {
        Id = Guid.NewGuid(); // Para Gerar o Id
    }
    public Guid Id { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string EditedBy { get; set; }
    public DateTime EditedOn { get; set; }
    public bool Active { get; private set; } = true;

}
