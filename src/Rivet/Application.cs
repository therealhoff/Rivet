using System.Collections.Generic;

namespace Rivet
{
    public abstract class Application
    {
        private readonly IList<IModule> _modules = new List<IModule>();

        protected void Module(IModule module)
        {
            _modules.Add(module);
        }

        public virtual void Start()
        {
            foreach (var module in _modules)
            {
                module.Init();
            }
            foreach (var module in _modules)
            {
                module.Start();
            }
        }

        public virtual void Stop()
        {
            foreach (var module in _modules)
            {
                module.Stop();
            }
        }
    }
}