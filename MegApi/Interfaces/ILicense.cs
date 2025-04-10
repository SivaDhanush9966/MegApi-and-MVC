using System.ComponentModel;
using MegApi.Models;

namespace MegApi.Interfaces
{
    public interface ILicense
    {
      public string CreateLicense(LicenseeFullDetails license);
    }
}
