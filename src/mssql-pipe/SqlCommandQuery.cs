using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mssql_pipe
{
	enum QueryType
	{
		@default,
		nonQuery,
		scalar,
		xml
	}

	class SqlCommandQuery
	{
		public string connectionString { get; set; }
		public string commandText { get; set; }
		public QueryType type { get; set; }
	}
}
