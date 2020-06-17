using System;
using System.Threading;
using System.Threading.Tasks;

namespace SoloContacts.Library.Interfaces
{
    public interface IContactService<TContact> : IDisposable where TContact : class
    {
        TContact RetrieveContact(int id);
    }
}
