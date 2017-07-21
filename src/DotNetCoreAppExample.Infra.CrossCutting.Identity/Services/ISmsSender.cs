using System.Threading.Tasks;

namespace DotNetCoreAppExample.Infra.CrossCutting.Identity.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
