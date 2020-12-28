using Flunt.Notifications;
using System;
using Flunt.Validations;

namespace AWS.SQS.Publish.Model
{
    public class OrderRequest:Notifiable
    {
        public Guid Id { get; private  set; }
        public Guid IdCliente { get; private set; } 
        public string Name { get; private set; }
        public Adress Adress { get; private  set; }
        public decimal Total { get; private set; }

        public OrderRequest( string name, Adress adress, Guid idCliente, decimal total)
        {
            Id = Guid.NewGuid();
            Name = name;
            Adress = adress;
            IdCliente = idCliente;
            Total = total;
            Validation();
        }

        private void Validation()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(Id, nameof(Id), "O Id do perdido não pode ser nulo"));
            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(IdCliente, nameof(IdCliente), "O IdCliente do perdido não pode ser nulo"));
            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(Id, nameof(Total), "O valor total do perdido não pode ser nulo"));
            AddNotifications(new Contract()
                .Requires()
                .IsLowerOrEqualsThan(Total, 1,nameof(Total), "O valor total do perdido não pode ser zero"));
        }
    }
}