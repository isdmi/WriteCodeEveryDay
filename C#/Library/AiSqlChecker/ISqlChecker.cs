using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiSqlChecker
{
    public interface ISqlChecker
    {
        Task<SqlCheckResult> CheckAsync(string sql, SqlCheckOptions? options = null);
    }
}
