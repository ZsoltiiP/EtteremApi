namespace Etterem.Services.IRestaurant
{
    public interface IKapcsolo
    {
        Task<object> PostNewRelation(AddRelationDto addRelationDto);
    }
}
