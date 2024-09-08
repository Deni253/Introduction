using Introduction.Model;

namespace Introduction.Service.Common
{
    public interface IDogService
    {
        bool DeleteDog(Guid id);
        bool PostDog(Dog dog);
        Dog GetDog(Guid id);
        bool UpdateDog(Guid id);
    }
}
