namespace NF.ObjectPooling.Runtime
{
    public interface IReusablePrefab
    {
        void OnGet();
        void OnRelease();
    }
}