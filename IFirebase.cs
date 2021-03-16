using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.Forms.Firebase
{
    public interface IFirebase<T>
    {
        Task<bool> CreateObject(T Object);
        Task<bool> DeleteObject(T Object);
    }
}
