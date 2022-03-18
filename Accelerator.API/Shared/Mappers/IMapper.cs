using System;
using System.Collections.Generic;
using System.Text;

namespace Accelerator.API.Shared.Mappers
{
    public interface IMapper<A, B>
    {
        A Map(B from);
        B Map(A from);
    }
}
