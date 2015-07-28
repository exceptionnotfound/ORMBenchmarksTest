using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMBenchmarksTest.Mapping
{
    public interface IMapper<T>
    {
        T Map(DataRow obj);

        T Map(DataTable table);
    }
}
