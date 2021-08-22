namespace Aero.Models
{
    public class IdNameModel<TId>
    {
        public IdNameModel()
        {
        }

        public IdNameModel(TId id, string name)
        {
            Id = id;
            Name = name;
        }

        public TId Id { get; set; }

        public string Name { get; set; }
    }
}
