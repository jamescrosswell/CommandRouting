using System.Linq;
using System.Net;
using Microsoft.AspNet.Mvc;

namespace CommandRouting
{
    public interface ICommand
    {
        
    }

    public interface ICommand<in TRequest, out TResponse>: ICommand
    {
        TResponse Execute(TRequest request);
    }

    public interface ICommand<in TRequest> : ICommand<TRequest, Unit>
    {
        
    }
}
