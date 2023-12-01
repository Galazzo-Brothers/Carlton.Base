using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carlton.Core.Components.Flux.Contracts;

public interface IBrowserStorageService
{
    public Task CommitLogs();
}
