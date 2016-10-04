namespace Agl.Service.Contract
{
    public interface IAgl
    {
        T Get<T>(string endPoint);
    }
}
