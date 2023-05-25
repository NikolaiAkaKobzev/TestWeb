namespace WebApi.Services
{
    public interface IStringStorage
    {
        string? GetString(int key);

        int AddString(string value);

        bool UpdateString(int key, string value);

        bool DeleteString(int key);
    }
}