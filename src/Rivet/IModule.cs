namespace Rivet
{
    public interface IModule
    {
        void Init();
        void Start();
        void Stop();
    }
}