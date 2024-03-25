using System;

namespace Interfaces
{
    public interface IHasProgress
    {
        public event EventHandler<float> OnProgressChanged;
    }
}