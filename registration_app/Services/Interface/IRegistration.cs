namespace registration_app.Services.Interface
{
    public interface IRegistration
    {
        string Add(registration data);
        registration GetBy(registration data);
    }
}