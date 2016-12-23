using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Model;

namespace Server {

	internal partial class CodeBuild {

		protected class CONST {
			public static readonly string corePath = @"";
			public static readonly string webPath = @"Web\";
			public static readonly string adminPath = @"Admin\";
			public static readonly string csproj =
			#region 内容太长已被收起
 @"<Project DefaultTargets=""Build"" xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
	<PropertyGroup>
		<Configuration Condition="" '$(Configuration)' == '' "">Debug</Configuration>
		<Platform Condition="" '$(Platform)' == '' "">AnyCPU</Platform>
		<ProductVersion>8.0.50727</ProductVersion>
		<SchemaVersion>2.0</SchemaVersion>
		<ProjectGuid>{{{0}}}</ProjectGuid>
		<OutputType>Library</OutputType>
		<AppDesignerFolder>Properties</AppDesignerFolder>
		<RootNamespace>{1}</RootNamespace>
		<AssemblyName>{1}</AssemblyName>
  	</PropertyGroup>
	<PropertyGroup Condition="" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' "">
		<DebugSymbols>true</DebugSymbols>
		<DebugType>full</DebugType>
		<Optimize>false</Optimize>
		<OutputPath>..\bin\</OutputPath>
		<DefineConstants>DEBUG;TRACE</DefineConstants>
		<ErrorReport>prompt</ErrorReport>
		<WarningLevel>4</WarningLevel>
	</PropertyGroup>
	<PropertyGroup Condition="" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' "">
		<DebugType>pdbonly</DebugType>
		<Optimize>true</Optimize>
		<OutputPath>..\bin\Release\</OutputPath>
		<DefineConstants>TRACE</DefineConstants>
		<ErrorReport>prompt</ErrorReport>
		<WarningLevel>4</WarningLevel>
	</PropertyGroup>
	{2}
	<Import Project=""$(MSBuildToolsPath)\Microsoft.CSharp.targets"" />
</Project>";
			#endregion
			public static readonly string sln =
			#region 内容太长已被收起
 @"
Microsoft Visual Studio Solution File, Format Version 9.00
# Visual Studio 2005
Project(""{{{0}}}"") = ""{1}.db"", ""{1}.db\{1}.db.csproj"", ""{{{2}}}""
EndProject
Project(""{{{0}}}"") = ""Common"", ""Common\Common.csproj"", ""{{{3}}}""
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{{{2}}}.Debug|.NET.ActiveCfg = Debug|Any CPU
		{{{2}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{{2}}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{{2}}}.Debug|Mixed Platforms.ActiveCfg = Debug|Any CPU
		{{{2}}}.Debug|Mixed Platforms.Build.0 = Debug|Any CPU
		{{{2}}}.Release|.NET.ActiveCfg = Release|Any CPU
		{{{2}}}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{{{2}}}.Release|Any CPU.Build.0 = Release|Any CPU
		{{{2}}}.Release|Mixed Platforms.ActiveCfg = Release|Any CPU
		{{{2}}}.Release|Mixed Platforms.Build.0 = Release|Any CPU
		{{{3}}}.Debug|.NET.ActiveCfg = Debug|Any CPU
		{{{3}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{{3}}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{{3}}}.Debug|Mixed Platforms.ActiveCfg = Debug|Any CPU
		{{{3}}}.Debug|Mixed Platforms.Build.0 = Debug|Any CPU
		{{{3}}}.Release|.NET.ActiveCfg = Release|Any CPU
		{{{3}}}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{{{3}}}.Release|Any CPU.Build.0 = Release|Any CPU
		{{{3}}}.Release|Mixed Platforms.ActiveCfg = Release|Any CPU
		{{{3}}}.Release|Mixed Platforms.Build.0 = Release|Any CPU
EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
EndGlobal
";
			#endregion

			public static readonly string DAL_ConnectionManager_cs =
			#region 内容太长已被收起
 @"using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Collections.Generic;
using System.Configuration;

namespace {0}.DAL {{
	/// <summary>
	/// 数据库链接管理器
	/// </summary>
	public partial class ConnectionManager {{

		public static string ConnectionString = null;
		public static Dictionary<int, List<SqlConnection2>> ConnectionPool = new Dictionary<int, List<SqlConnection2>>();
		public static List<SqlConnection2> ConnectionPool2 = new List<SqlConnection2>();
		private static bool _dels_flag = false;
		private static DateTime _min_last_active = DateTime.MaxValue;
		private static object _lock = new object();

		/// <summary>
		/// 获取当前线程的 SqlConnection 连接
		/// </summary>
		/// <returns>返回一个 SqlConnection，注意：使用完毕后 Close 即可，请不要 Dispose 或相关方法消毁此 SqlConnection 的引用，否则将出现不可预料的错误</returns>
		public static SqlConnection GetConnection() {{

			if (string.IsNullOrEmpty(ConnectionString)) {{
				string key = ""{1}"";
				ConnectionString = ConfigurationManager.ConnectionStrings[key] == null ? null : ConfigurationManager.ConnectionStrings[key].ConnectionString;

				if (ConnectionString == null) throw new ArgumentNullException(key, string.Format(""未定义 Web.Config 里的 ConnectionStrings 键 '{{0}}' 或值不正确！"", key));
			}}

			SqlConnection2 conn = null;
			int tid = Thread.CurrentThread.ManagedThreadId;

			lock (_lock) {{
				if (!ConnectionPool.ContainsKey(tid)) ConnectionPool.Add(tid, new List<SqlConnection2>());
				conn = ConnectionPool[tid].Find(delegate(SqlConnection2 conn2) {{
					return conn2.SqlConnection != null && conn2.SqlConnection.State == ConnectionState.Closed;
				}});
				if (conn == null) {{
					conn = new SqlConnection2();
					conn.ThreadId = tid;
					conn.SqlConnection = new SqlConnection(ConnectionString);
					ConnectionPool[tid].Add(conn);
					ConnectionPool2.Add(conn);
				}}
				conn.LastActive = DateTime.Now;
			}}
			if (conn.LastActive < _min_last_active) {{
				_min_last_active = conn.LastActive;
			}}

			List<SqlConnection2> dels = null;
			TimeSpan ts = DateTime.Now - _min_last_active;
			if ((ConnectionPool.Count > 60 && ts.TotalSeconds > 90 || ts.TotalSeconds > 180) && !_dels_flag) {{
				lock (_lock) {{
					_dels_flag = true;
					_min_last_active = DateTime.MaxValue;
					dels = ConnectionPool2.FindAll(delegate(SqlConnection2 conn2) {{
						TimeSpan ts2 = DateTime.Now - conn2.LastActive;
						if (ts2.TotalMilliseconds <= 90 && conn2.LastActive < _min_last_active) {{
							_min_last_active = conn2.LastActive;
						}}
						return ts2.TotalSeconds > 90;
					}});
					foreach (SqlConnection2 del in dels) {{
						ConnectionPool[del.ThreadId].Remove(del);
						ConnectionPool2.Remove(del);
						if (ConnectionPool[del.ThreadId].Count == 0) {{
							ConnectionPool.Remove(del.ThreadId);
						}}
					}}
					_dels_flag = false;
				}}
			}}
			if (dels != null) {{
				for (int a = 0; a < dels.Count; a++) {{
					if (dels[a] != null) {{
						dels[a].SqlConnection.Close();
						dels[a].SqlConnection.Dispose();
					}}
				}}
			}}

			return conn.SqlConnection;
		}}
	}}

	public class SqlConnection2 {{
		public SqlConnection SqlConnection;
		public DateTime LastActive;
		internal int ThreadId;
	}}
}}";
			#endregion
			public static readonly string DAL_SqlHelper_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace {0}.DAL {{

	/// <summary>
	/// Execute方法当中，除了 ExecuteReader 不走事务以外，其他都走
	/// </summary>
	public partial class SqlHelper {{

		static readonly Logger Log = Logger.dbutility_sqlhelper;

		static void LoggerException(SqlCommand cmd, Exception e) {{
			if (e == null) return;
			string log = ""〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓"" + cmd.CommandText + ""\r\n"";
			foreach (SqlParameter parm in cmd.Parameters) {{
				log += Lib.PadRight(parm.ParameterName, 20) + "" = "" + Lib.PadRight(parm.Value == null ? ""NULL"" : parm.Value, 20) + ""\r\n"";
			}}
			Log.Fatal(log + ""\r\n\r\n"", e);

			RollbackTransaction();
			cmd.Parameters.Clear();
			cmd.Connection.Close();
			throw e;
		}}

		public static string Addslashes(string filter, params object[] parms) {{
			if (filter == null || parms == null) return string.Empty;
			if (parms.Length == 0) return filter;
			object[] nparms = new object[parms.Length];
			for (int a = 0; a < parms.Length; a++) {{
				if (parms[a] == null) nparms[a] = ""NULL"";
				else {{
					if (parms[a] is System.UInt16 ||
						parms[a] is System.UInt32 ||
						parms[a] is System.UInt64 ||
						parms[a] is System.Double ||
						parms[a] is System.Single ||
						parms[a] is System.Decimal ||
						parms[a] is System.Byte) {{
						nparms[a] = parms[a];
					}} else if (parms[a] is DateTime) {{
						DateTime dt = (DateTime)parms[a];
						nparms[a] = string.Concat(""'"", dt.ToString(""yyyy-MM-dd HH:mm:ss""), ""'"");
					}} else if (parms[a] is DateTime?) {{
						DateTime? dt = parms[a] as DateTime?;
						nparms[a] = string.Concat(""'"", dt.Value.ToString(""yyyy-MM-dd HH:mm:ss""), ""'"");
					}} else {{
						nparms[a] = string.Concat(""'"", parms[a].ToString().Replace(""'"", ""''""), ""'"");
						if (parms[a] is string) nparms[a] = string.Concat('N', nparms[a]);
					}}
				}}
			}}
			try {{ string ret = string.Format(filter, nparms); return ret; }} catch {{ return filter; }}
		}}
		public static IDataReader ExecuteReader(string cmdText, params SqlParameter[] cmdParms) {{
			return ExecuteReader(CommandType.Text, cmdText, cmdParms);
		}}
		public static IDataReader ExecuteReader(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {{
			SqlCommand cmd = new SqlCommand();
			SqlConnection conn = ConnectionManager.GetConnection();
			PrepareCommand(cmd, conn, cmdType, cmdText, cmdParms);
			SqlDataReader dr = null;
			Exception ex = Lib.Trys(delegate() {{
				if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
				try {{
					dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
				}} catch {{
					if (!Lib.IsWeb) Thread.CurrentThread.Join(1000);
					throw;
				}}
			}}, Lib.IsWeb ? 1 : 60);

			LoggerException(cmd, ex);
			return dr;
		}}

		public static DataSet ExecuteDataSet(string cmdText, params SqlParameter[] cmdParms) {{
			return ExecuteDataSet(CommandType.Text, cmdText, cmdParms);
		}}
		public static DataSet ExecuteDataSet(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {{
			DataSet ds = new DataSet();

			SqlCommand cmd = new SqlCommand();
			SqlDataAdapter sda = new SqlDataAdapter(cmd);
			PrepareCommand(cmd, null, cmdType, cmdText, cmdParms);
			Exception ex = Lib.Trys(delegate() {{
				if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
				try {{
					sda.Fill(ds);
				}} catch {{
					if (!Lib.IsWeb) Thread.CurrentThread.Join(1000);
					throw;
				}}
			}}, Lib.IsWeb ? 1 : 60);

			if (CurrentThreadTransaction == null) cmd.Connection.Close();
			LoggerException(cmd, ex);
			cmd.Parameters.Clear();
			return ds;
		}}

		public static int ExecuteNonQuery(string cmdText, params SqlParameter[] cmdParms) {{
			return ExecuteNonQuery(CommandType.Text, cmdText, cmdParms);
		}}
		public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {{
			SqlCommand cmd = new SqlCommand();
			PrepareCommand(cmd, null, cmdType, cmdText, cmdParms);
			int val = 0;
			Exception ex = Lib.Trys(delegate() {{
				if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
				try {{
					val = cmd.ExecuteNonQuery();
				}} catch {{
					if (!Lib.IsWeb) Thread.CurrentThread.Join(1000);
					throw;
				}}
			}}, Lib.IsWeb ? 1 : 60);

			if (CurrentThreadTransaction == null) cmd.Connection.Close();
			LoggerException(cmd, ex);
			cmd.Parameters.Clear();
			return val;
		}}

		public static object ExecuteScalar(string cmdText, params SqlParameter[] cmdParms) {{
			return ExecuteScalar(CommandType.Text, cmdText, cmdParms);
		}}
		public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {{
			SqlCommand cmd = new SqlCommand();
			PrepareCommand(cmd, null, cmdType, cmdText, cmdParms);
			object val = null;
			Exception ex = Lib.Trys(delegate() {{
				if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
				try {{
					val = cmd.ExecuteScalar();
				}} catch {{
					if (!Lib.IsWeb) Thread.CurrentThread.Join(1000);
					throw;
				}}
			}}, Lib.IsWeb ? 1 : 60);

			if (CurrentThreadTransaction == null) cmd.Connection.Close();
			LoggerException(cmd, ex);
			cmd.Parameters.Clear();
			return val;
		}}

		private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms) {{
			cmd.CommandType = cmdType;
			cmd.CommandText = cmdText;

			if (conn == null) {{
				SqlTransaction tran = CurrentThreadTransaction;
				cmd.Connection = tran == null ? ConnectionManager.GetConnection() : tran.Connection;
				cmd.Transaction = tran == null ? null : tran;
			}} else {{
				cmd.Connection = conn;
			}}

			if (cmdParms != null) {{
				foreach (SqlParameter parm in cmdParms) {{
					if (parm == null) continue;
					if (parm.Value == null) parm.Value = DBNull.Value;
					cmd.Parameters.Add(parm);
				}}
			}}

			AutoCommitTransaction();
		}}

		#region 事务处理

		class SqlTransaction2 {{
			internal SqlTransaction Transaction;
			internal DateTime RunTime;
			internal TimeSpan Timeout;

			public SqlTransaction2(SqlTransaction tran, TimeSpan timeout) {{
				Transaction = tran;
				RunTime = DateTime.Now;
				Timeout = timeout;
			}}
		}}

		private static Dictionary<int, SqlTransaction2> _trans = new Dictionary<int, SqlTransaction2>();
		private static List<SqlTransaction2> _trans_tmp = new List<SqlTransaction2>();
		private static object _trans_lock = new object();

		private static SqlTransaction CurrentThreadTransaction {{
			get	{{
				int tid = Thread.CurrentThread.ManagedThreadId;

				if (_trans.ContainsKey(tid)) {{
					if (_trans[tid].Transaction.Connection != null) {{
						return _trans[tid].Transaction;
					}}
				}}
				return null;
			}}
		}}

		/// <summary>
		/// 启动事务
		/// </summary>
		public static void BeginTransaction() {{
			BeginTransaction(TimeSpan.FromSeconds(10));
		}}
		public static void BeginTransaction(TimeSpan timeout) {{
			int tid = Thread.CurrentThread.ManagedThreadId;
			SqlConnection conn = ConnectionManager.GetConnection();
			SqlTransaction2 tran = null;

			Exception ex = Lib.Trys(delegate() {{
				if (conn.State != ConnectionState.Open) conn.Open();
				tran = new SqlTransaction2(conn.BeginTransaction(), timeout);
			}}, Lib.IsWeb ? 1 : 60);

			if (ex != null) {{
				Log.Fatal(ex);
				throw ex;
			}}

			if (_trans.ContainsKey(tid)) {{
				CommitTransaction();
				_trans[tid] = tran;
			}} else {{
				_trans.Add(tid, tran);
			}}

			lock (_trans_lock) {{
				_trans_tmp.Add(tran);
			}}
		}}

		/// <summary>
		/// 自动提交事务
		/// </summary>
		private static void AutoCommitTransaction() {{
			if (_trans_tmp.Count > 0) {{
				List<SqlTransaction2> trans = null;

				lock (_trans_lock) {{
					trans = _trans_tmp.FindAll(delegate(SqlTransaction2 st2) {{
						TimeSpan ts = DateTime.Now - st2.RunTime;
						return ts > st2.Timeout;
					}});
				}}

				foreach (SqlTransaction2 tran in trans) {{
					CommitTransaction(true, tran);
				}}
			}}
		}}
		private static void CommitTransaction(bool isCommit, SqlTransaction2 tran) {{
			if (tran == null || tran.Transaction == null || tran.Transaction.Connection == null) return;

			try {{
				SqlConnection conn = tran.Transaction.Connection;
				if (isCommit) {{
					tran.Transaction.Commit();
				}} else {{
					tran.Transaction.Rollback();
				}}
				conn.Close();
			}} catch {{ }}

			lock (_trans_lock) {{
				_trans_tmp.Remove(tran);
			}}
		}}
		private static void CommitTransaction(bool isCommit) {{
			int tid = Thread.CurrentThread.ManagedThreadId;

			if (_trans.ContainsKey(tid)) {{
				CommitTransaction(isCommit, _trans[tid]);
			}}
		}}
		/// <summary>
		/// 提交事务
		/// </summary>
		public static void CommitTransaction() {{
			CommitTransaction(true);
		}}
		/// <summary>
		/// 回滚事务
		/// </summary>
		public static void RollbackTransaction() {{
			CommitTransaction(false);
		}}
		#endregion
	}}
}}";
			#endregion
			public static readonly string DAL_SqlHelper_SelectBuild_cs =
			#region 内容太长已被收起
			@"using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;

namespace {0}.DAL {{

	public partial class SqlHelper {{
		public partial interface IDAL {{
			string Table {{ get; }}
			string Field {{ get; }}
			string Sort {{ get; }}
			object GetItem(DataRow dr, ref int index);
		}}
		public class SelectBuild<TReturnInfo, TLinket> : SelectBuild<TReturnInfo> where TLinket : SelectBuild<TReturnInfo> {{
			protected SelectBuild<TReturnInfo> Where1Or(string filterFormat, Array values) {{
				if (values == null) values = new object[] {{ null }};
				if (values.Length == 0) return this;
				if (values.Length == 1) return base.Where(filterFormat, values.GetValue(0));
				string filter = string.Empty;
				for (int a = 0; a < values.Length; a++) filter = string.Concat(filter, "" OR "", string.Format(filterFormat, ""{{"" + a + ""}}""));
				object[] parms = new object[values.Length];
				values.CopyTo(parms, 0);
				return base.Where(filter.Substring(4), parms);
			}}
			public new TLinket Count(out int count) {{
				return base.Count(out count) as TLinket;
			}}
			public new TLinket Where(string filter, params object[] parms) {{
				return base.Where(true, filter, parms) as TLinket;
			}}
			public new TLinket Where(bool isadd, string filter, params object[] parms) {{
				return base.Where(isadd, filter, parms) as TLinket;
			}}
			public new TLinket GroupBy(string groupby) {{
				return base.GroupBy(groupby) as TLinket;
			}}
			public new TLinket Having(string filter, params object[] parms) {{
				return base.Having(true, filter, parms) as TLinket;
			}}
			public new TLinket Having(bool isadd, string filter, params object[] parms) {{
				return base.Having(isadd, filter, parms) as TLinket;
			}}
			public new TLinket Sort(string sort) {{
				return base.Sort(sort) as TLinket;
			}}
			public new TLinket From<TBLL>() {{
				return base.From<TBLL>() as TLinket;
			}}
			public new TLinket From<TBLL>(string alias) {{
				return base.From<TBLL>(alias) as TLinket;
			}}
			public new TLinket InnerJoin<TBLL>(string alias, string on) {{
				return base.InnerJoin<TBLL>(alias, on) as TLinket;
			}}
			public new TLinket LeftJoin<TBLL>(string alias, string on) {{
				return base.LeftJoin<TBLL>(alias, on) as TLinket;
			}}
			public new TLinket RightJoin<TBLL>(string alias, string on) {{
				return base.RightJoin<TBLL>(alias, on) as TLinket;
			}}
			public new TLinket Skip(int skip) {{
				return base.Skip(skip) as TLinket;
			}}
			public new TLinket Limit(int limit) {{
				return base.Limit(limit) as TLinket;
			}}
			public SelectBuild(IDAL dal) : base(dal) {{ }}
		}}
		public class SelectBuild<TReturnInfo> {{
			protected int _limit, _skip;
			protected string _sort, _field, _table, _join, _where, _groupby, _having;
			protected List<IDAL> _dals = new List<IDAL>();
			public List<TReturnInfo> ToList() {{
				List<TReturnInfo> ret = new List<TReturnInfo>();
				using (DataTable dt = SqlHelper.ExecuteDataSet(this.ToString()).Tables[0]) {{
					foreach(DataRow dr in dt.Rows) {{
						int index = -1;
						TReturnInfo info = (TReturnInfo)_dals[0].GetItem(dr, ref index);
						ret.Add(info);
						for (int a = 1; a < _dals.Count; a++) {{
							object item = _dals[a].GetItem(dr, ref index);
							string name = _dals[a].GetType().Name;
							name = string.Concat(""Obj_"", name[0].ToString().ToLower(), name.Substring(1));
							Type type = info.GetType();
							PropertyInfo pro = type.GetProperty(name);
							if (pro == null) throw new Exception(string.Concat(type.FullName, "" 没有定义属性 "", name));
							pro.SetValue(info, item, null);
						}}
					}}
				}}
				return ret;
			}}
			public TReturnInfo ToOne() {{
				List<TReturnInfo> ret = this.Limit(1).ToList();
				return ret.Count > 0 ? ret[0] : default(TReturnInfo);
			}}
			public DataTable ToDataTable() {{
				DataSet ds = SqlHelper.ExecuteDataSet(this.ToString());
				return ds.Tables.Count > 0 ? ds.Tables[0] : null;
			}}
			public override string ToString() {{
				if (string.IsNullOrEmpty(_sort) && _skip > 0) this.Sort(_dals[0].Sort);
				string top = _skip == 0 && _limit > 0 ? string.Concat(""TOP "", _limit, "" "") : string.Empty;
				string row_number = _skip > 0 ? string.Concat("", row_number() over("", _sort, "") AS rownum"") : string.Empty;
				string where = string.IsNullOrEmpty(_where) ? string.Empty : string.Concat("" \r\nWHERE "", _where.Substring(5));
				string orderby = _skip > 0 ? string.Empty : _sort;
				string sql = string.Concat(""SELECT "", top, _field, row_number, _table, _join, where, orderby);
				if (_skip > 0) sql = string.Concat(""WITH t AS ("", sql, "") \r\nSELECT t.* FROM t WHERE t.rownum "", _limit > 0 ? string.Concat(""between "", _skip + 1, "" and "", _skip + _limit) : string.Concat(""> "", _skip));
				return sql;
			}}
			public DataTable Aggregate(string fields) {{
				string top = _skip == 0 && _limit > 0 ? string.Concat(""TOP "", _limit, "" "") : string.Empty;
				string where = string.IsNullOrEmpty(_where) ? string.Empty : string.Concat("" \r\nWHERE "", _where.Substring(5));
				string having = string.IsNullOrEmpty(_groupby) ||
								string.IsNullOrEmpty(_having) ? string.Empty : string.Concat("" \r\nHAVING "", _having.Substring(5));
				string orderby = _skip > 0 ? string.Empty : _sort;
				string sql = string.Concat(""SELECT "", top, fields, _table, _join, where, _groupby, having, orderby);
				return SqlHelper.ExecuteDataSet(sql).Tables[0];
			}}
			public T Aggregate<T>(string fields) {{
				return Lib.ConvertTo<T>(this.Aggregate(fields).Rows[0][0]);
			}}
			public int Count() {{
				return this.Aggregate<int>(""count(1)"");
			}}
			public SelectBuild<TReturnInfo> Count(out int count) {{
				count = this.Count();
				return this;
			}}
			public static SelectBuild<TReturnInfo> From(IDAL dal) {{
				return new SelectBuild<TReturnInfo>(dal);
			}}
			int _fields_count = 0;
			protected SelectBuild(IDAL dal) {{
				_dals.Add(dal);
				_field = dal.Field;
				_table = string.Concat("" \r\nFROM "", dal.Table, "" a"");
			}}
			public SelectBuild<TReturnInfo> From<TBLL>() {{
				return this.From<TBLL>(string.Empty);
			}}
			public SelectBuild<TReturnInfo> From<TBLL>(string alias) {{
				IDAL dal = this.ConvertTBLL<TBLL>();
				_table = string.Concat(_table, "", "", dal.Table, "" "", alias);
				return this;
			}}
			protected IDAL ConvertTBLL<TBLL>() {{
				string dalTypeName = typeof(TBLL).FullName.Replace("".BLL."", "".DAL."");
				IDAL dal = this.GetType().Assembly.CreateInstance(dalTypeName) as IDAL;
				if (dal == null) throw new Exception(string.Concat(""找不到类型 "", dalTypeName));
				return dal;
			}}
			protected SelectBuild<TReturnInfo> Join<TBLL>(string alias, string on, string joinType) {{
				IDAL dal = this.ConvertTBLL<TBLL>();
				_dals.Add(dal);
				string fields2 = dal.Field.Replace(""a."", string.Concat(alias, "".""));
				string[] names = fields2.Split(new string[] {{ "", "" }}, StringSplitOptions.None);
				for (int a = 0; a < names.Length; a++) {{
					string ast = string.Concat("" as"", ++_fields_count);
					names[a] = string.Concat(names[a], ast);
				}}
				_field = string.Concat(_field, "", \r\n"", string.Join("", "", names));
				_join = string.Concat(_join, "" \r\n"", joinType, "" "", dal.Table, "" "", alias, "" ON "", on);
				return this;
			}}
			public SelectBuild<TReturnInfo> Where(string filter, params object[] parms) {{
				return this.Where(true, filter, parms);
			}}
			public SelectBuild<TReturnInfo> Where(bool isadd, string filter, params object[] parms) {{
				if (isadd) {{
					//将参数 = null 转换成 IS NULL
					if (parms != null && parms.Length > 0) {{
						for (int a = 0; a < parms.Length; a++)
							if (parms[a] == null)
								filter = Regex.Replace(filter, @""\s+=\s+\{{"" + a + @""\}}"", "" IS {{"" + a + ""}}"");
					}}
					_where = string.Concat(_where, "" AND ("", Addslashes(filter, parms), "")"");
				}}
				return this;
			}}
			public SelectBuild<TReturnInfo> GroupBy(string groupby) {{
				_groupby = groupby;
				if (string.IsNullOrEmpty(_groupby)) return this;
				_groupby = string.Concat("" \r\nGROUP BY "", _groupby);
				return this;
			}}
			public SelectBuild<TReturnInfo> Having(string filter, params object[] parms) {{
				return this.Having(true, filter, parms);
			}}
			public SelectBuild<TReturnInfo> Having(bool isadd, string filter, params object[] parms) {{
				if (string.IsNullOrEmpty(_groupby)) return this;
				if (isadd) _having = string.Concat(_having, "" AND ("", Addslashes(filter, parms), "")"");
				return this;
			}}
			public SelectBuild<TReturnInfo> Sort(string sort) {{
				if (!string.IsNullOrEmpty(sort)) _sort = string.Concat("" \r\nORDER BY "", sort);
				return this;
			}}
			public SelectBuild<TReturnInfo> InnerJoin<TBLL>(string alias, string on) {{
				return this.Join<TBLL>(alias, on, ""INNER JOIN"");
			}}
			public SelectBuild<TReturnInfo> LeftJoin<TBLL>(string alias, string on) {{
				return this.Join<TBLL>(alias, on, ""LEFT JOIN"");
			}}
			public SelectBuild<TReturnInfo> RightJoin<TBLL>(string alias, string on) {{
				return this.Join<TBLL>(alias, on, ""RIGHT JOIN"");
			}}
			public SelectBuild<TReturnInfo> Skip(int skip) {{
				_skip = skip;
				return this;
			}}
			public SelectBuild<TReturnInfo> Limit(int limit) {{
				_limit = limit;
				return this;
			}}
		}}
	}}
}}";
			#endregion

			public static readonly string BLL_Build_SqlHelper_cs =
			#region 内容太长已被收起
 @"using System;
using System.Data;
using System.Data.SqlClient;

namespace {0}.BLL {{

	/// <summary>
	/// {0}.DAL.SqlHelper 代理类，全部支持走事务
	/// </summary>
	public abstract class SqlHelper {{

		public static DataSet ExecuteDataSet(string cmdText, params SqlParameter[] cmdParms) {{
			return {0}.DAL.SqlHelper.ExecuteDataSet(CommandType.Text, cmdText, cmdParms);
		}}
		public static DataSet ExecuteDataSet(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {{
			return {0}.DAL.SqlHelper.ExecuteDataSet(cmdType, cmdText, cmdParms);
		}}

		public static int ExecuteNonQuery(string cmdText, params SqlParameter[] cmdParms) {{
			return {0}.DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, cmdParms);
		}}
		public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {{
			return {0}.DAL.SqlHelper.ExecuteNonQuery(cmdType, cmdText, cmdParms);
		}}

		public static object ExecuteScalar(string cmdText, params SqlParameter[] cmdParms) {{
			return {0}.DAL.SqlHelper.ExecuteScalar(CommandType.Text, cmdText, cmdParms);
		}}
		public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {{
			return {0}.DAL.SqlHelper.ExecuteScalar(cmdType, cmdText, cmdParms);
		}}

		/// <summary>
		/// 开启事务（不支持异步），10秒未执行完将超时
		/// </summary>
		/// <param name=""handler"">事务体 () => {{}}</param>
		public static void Transaction(AnonymousHandler handler) {{
			Transaction(handler, TimeSpan.FromSeconds(10));
		}}
		/// <summary>
		/// 开启事务（不支持异步）
		/// </summary>
		/// <param name=""handler"">事务体 () => {{}}</param>
		/// <param name=""timeout"">超时</param>
		public static void Transaction(AnonymousHandler handler, TimeSpan timeout) {{
			try {{
				{0}.DAL.SqlHelper.BeginTransaction(timeout);
				handler();
				{0}.DAL.SqlHelper.CommitTransaction();
			}} catch (Exception ex) {{
				{0}.DAL.SqlHelper.RollbackTransaction();
				throw ex;
			}}
		}}
	}}
}}";
			#endregion
			public static readonly string BLL_Build_ItemCache_cs =
			#region 内容太长已被收起
 @"using System;
using System.Configuration;
using System.Collections.Generic;
using System.Threading;

namespace {0}.BLL {{
	public partial class ItemCache {{

		private static Dictionary<string, long> _dic1 = new Dictionary<string, long>();
		private static Dictionary<long, Dictionary<string, string>> _dic2 = new Dictionary<long, Dictionary<string, string>>();
		private static LinkedList<long> _linked = new LinkedList<long>();
		private static object _dic1_lock = new object();
		private static object _dic2_lock = new object();
		private static object _linked_lock = new object();

		public static void Clear() {{
			lock(_dic1_lock) {{
				_dic1.Clear();
			}}
			lock(_dic2_lock) {{
				_dic2.Clear();
			}}
			lock(_linked_lock) {{
				_linked.Clear();
			}}
		}}
		public static void Remove(string key) {{
			if (string.IsNullOrEmpty(key)) return;
			long time;
			if (_dic1.TryGetValue(key, out time) == false) return;

			lock (_dic1_lock) {{
				_dic1.Remove(key);
			}}
			if (_dic2.ContainsKey(time)) {{
				lock (_dic2_lock) {{
					_dic2.Remove(time);
				}}
			}}
			lock (_linked_lock) {{
				_linked.Remove(time);
			}}
		}}
		public static string Get(string key) {{
			if (string.IsNullOrEmpty(key)) return null;
			long time;
			if (_dic1.TryGetValue(key, out time) == false) return null;
			Dictionary<string, string> dic;
			if (_dic2.TryGetValue(time, out dic) == false) {{
				if (_dic1.ContainsKey(key)) {{
					lock (_dic1_lock) {{
						_dic1.Remove(key);
					}}
				}}
				return null;
			}}
			if (DateTime.Now.Subtract(new DateTime(2016, 5, 1)).TotalSeconds > time) {{
				if (_dic1.ContainsKey(key)) {{
					lock (_dic1_lock) {{
						_dic1.Remove(key);
					}}
				}}
				if (_dic2.ContainsKey(time)) {{
					lock (_dic2_lock) {{
						_dic2.Remove(time);
					}}
				}}
				lock (_linked_lock) {{
					_linked.Remove(time);
				}}
				return null;
			}}
			string ret;
			if (dic.TryGetValue(key, out ret) == false) return null;
			return ret;
		}}
		public static void Set(string key, string value, int expire) {{
			if (string.IsNullOrEmpty(key) || expire <= 0) return;
			long time_cur = (long)DateTime.Now.Subtract(new DateTime(2016, 5, 1)).TotalSeconds;
			long time = time_cur + expire;
			long time2;
			if (_dic1.TryGetValue(key, out time2) == false) {{
				lock (_dic1_lock) {{
					if (_dic1.TryGetValue(key, out time2) == false) {{
						_dic1.Add(key, time2 = time);
					}}
				}}
			}}
			if (time2 != time) {{
				lock (_dic1_lock) {{
					_dic1[key] = time;
				}}
				lock (_dic2_lock) {{
					_dic2.Remove(time2);
				}}
			}}
			Dictionary<string, string> dic;
			bool isNew = false;
			if (_dic2.TryGetValue(time, out dic) == false) {{
				lock (_dic2_lock) {{
					if (_dic2.TryGetValue(time, out dic) == false) {{
						_dic2.Add(time, dic = new Dictionary<string, string>());
						isNew = true;
					}}
					if (dic.ContainsKey(key) == false) dic.Add(key, value);
					else dic[key] = value;
				}}
			}} else {{
				lock (_dic2_lock) {{
					if (dic.ContainsKey(key) == false) dic.Add(key, value);
					else dic[key] = value;
				}}
			}}
			if (isNew == true) {{
				lock (_linked_lock) {{
					if (_linked.Count == 0) {{
						_linked.AddFirst(time);
					}} else {{
						LinkedListNode<long> node = _linked.First;
						while (node != null) {{
							if (node.Value < time_cur) {{
								_linked.Remove(node);
								Dictionary<string, string> dic_del;
								if (_dic2.TryGetValue(node.Value, out dic_del)) {{
									lock (_dic2_lock) {{
										_dic2.Remove(node.Value);
										foreach (KeyValuePair<string, string> dic_del_in in dic_del) {{
											if (_dic1.ContainsKey(dic_del_in.Key)) {{
												lock (_dic1_lock) {{
													_dic1.Remove(dic_del_in.Key);
												}}
											}}
										}}
									}}
								}}
								node = _linked.First;
							}} else break;
						}}
						if (node == null)
							_linked.AddFirst(time);
						else if (node != null && _linked.Last.Value < time)
							_linked.AddLast(time);
						else {{
							while (node != null && node.Value < time) node = node.Next;
							if (node != null && node.Value != time) {{
								_linked.AddBefore(node, time);
							}}
						}}
					}}
				}}
			}}
		}}
	}}
}}";
			#endregion
			public static readonly string Model_Build_ExtensionMethods_cs =
			#region 内容太长已被收起
 @"using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace {0}.Model {{
	public static partial class ExtensionMethods {{{1}
		public static string GetJson(IEnumerable items) {{
			StringBuilder sb = new StringBuilder();
			sb.Append(""["");
			IEnumerator ie = items.GetEnumerator();
			if (ie.MoveNext()) {{
				while (true) {{
					sb.Append(string.Concat(ie.Current));
					if (ie.MoveNext()) sb.Append("","");
					else break;
				}}
			}}
			sb.Append(""]"");
			return sb.ToString();
		}}
		public static IDictionary[] GetBson(IEnumerable items) {{
			List<IDictionary> ret = new List<IDictionary>();
			IEnumerator ie = items.GetEnumerator();
			while (ie.MoveNext())
				ret.Add(ie.Current.GetType().GetMethod(""ToBson"").Invoke(ie.Current, null) as IDictionary);
			return ret.ToArray();
		}}
	}}
}}";
			#endregion

			public static readonly string Common_BmwNet_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;
using System.Threading;
using System.Reflection;
using System.Globalization;
using Microsoft.CSharp;
using System.CodeDom;
using System.CodeDom.Compiler;

public sealed class BmwNet : IDisposable {{
	public interface IBmwNetOutput {{
		/// <summary>
		/// 
		/// </summary>
		/// <param name=""tOuTpUt"">返回内容</param>
		/// <param name=""oPtIoNs"">渲染对象</param>
		/// <param name=""rEfErErFiLeNaMe"">当前文件路径</param>
		/// <param name=""bMwSeNdEr""></param>
		/// <returns></returns>
		BmwNetReturnInfo OuTpUt(StringBuilder tOuTpUt, IDictionary oPtIoNs, string rEfErErFiLeNaMe, BmwNet bMwSeNdEr);
	}}
	public class BmwNetReturnInfo {{
		public Dictionary<string, int[]> Blocks;
		public StringBuilder Sb;
	}}
	public delegate bool BmwNetIf(object exp);
	public delegate void BmwNetPrint(params object[] parms);

	private static int _view = 0;
	private static Regex _reg = new Regex(@""\{{(\$BMW__CODE|\/\$BMW__CODE|import\s+|module\s+|extends\s+|block\s+|include\s+|for\s+|if\s+|#|\/for|elseif|else|\/if|\/block|\/module)([^\}}]*)\}}"", RegexOptions.Compiled);
	private static Regex _reg_forin = new Regex(@""^([\w_]+)\s*,?\s*([\w_]+)?\s+in\s+(.+)"", RegexOptions.Compiled);
	private static Regex _reg_foron = new Regex(@""^([\w_]+)\s*,?\s*([\w_]+)?,?\s*([\w_]+)?\s+on\s+(.+)"", RegexOptions.Compiled);
	private static Regex _reg_forab = new Regex(@""^([\w_]+)\s+([^,]+)\s*,\s*(.+)"", RegexOptions.Compiled);
	private static Regex _reg_miss = new Regex(@""\{{\/?miss\}}"", RegexOptions.Compiled);
	private static Regex _reg_code = new Regex(@""(\{{%|%\}})"", RegexOptions.Compiled);
	private static Regex _reg_syntax = new Regex(@""<(\w+)\s+@(if|for|else)\s*=""""([^""""]*)"""""", RegexOptions.Compiled);
	private static Regex _reg_htmltag = new Regex(@""<\/?\w+[^>]*>"", RegexOptions.Compiled);
	private static Regex _reg_blank = new Regex(@""\s+"", RegexOptions.Compiled);
	private static Regex _reg_complie_undefined = new Regex(@""(当前上下文中不存在名称)?“(\w+)”"", RegexOptions.Compiled);

	private Dictionary<string, IBmwNetOutput> _cache = new Dictionary<string, IBmwNetOutput>();
	private object _cache_lock = new object();
	private string _viewDir;
	private FileSystemWatcher _fsw = new FileSystemWatcher();

	public BmwNet(string viewDir) {{
		_viewDir = Utils.TranslateUrl(viewDir);
		_fsw = new FileSystemWatcher(_viewDir);
		_fsw.IncludeSubdirectories = true;
		_fsw.Changed += ViewDirChange;
		_fsw.Renamed += ViewDirChange;
		_fsw.EnableRaisingEvents = true;
	}}
	public void Dispose() {{
		_fsw.Dispose();
	}}
	void ViewDirChange(object sender, FileSystemEventArgs e) {{
		string filename = e.FullPath.ToLower();
		lock (_cache_lock) {{
			_cache.Remove(filename);
		}}
	}}
	public BmwNetReturnInfo RenderFile2(StringBuilder sb, IDictionary options, string filename, string refererFilename) {{
		if (filename[0] == '/' || string.IsNullOrEmpty(refererFilename)) refererFilename = _viewDir;
		//else refererFilename = Path.GetDirectoryName(refererFilename);
		string filename2 = Utils.TranslateUrl(filename, refererFilename).ToLower();
		IBmwNetOutput bmw;
		if (_cache.TryGetValue(filename2, out bmw) == false) {{
			string tplcode = File.Exists(filename2) == false ? string.Concat(""文件不存在 "", filename) : Utils.ReadTextFile(filename2);
			bmw = Parser(tplcode, options);
			lock (_cache_lock) {{
				if (_cache.ContainsKey(filename2) == false) {{
					_cache.Add(filename2, bmw);
				}}
			}}
		}}
		try {{
			return bmw.OuTpUt(sb, options, filename2, this);
		}} catch (Exception ex) {{
			BmwNetReturnInfo ret = sb == null ?
				new BmwNetReturnInfo {{ Sb = new StringBuilder(), Blocks = new Dictionary<string, int[]>() }} :
				new BmwNetReturnInfo {{ Sb = sb, Blocks = new Dictionary<string, int[]>() }};
			ret.Sb.Append(refererFilename);
			ret.Sb.Append("" -> "");
			ret.Sb.Append(filename);
			ret.Sb.Append(""\r\n"");
			ret.Sb.Append(ex.Message);
			ret.Sb.Append(""\r\n"");
			ret.Sb.Append(ex.StackTrace);
			return ret;
		}}
	}}
	public string RenderFile(string filename, IDictionary options) {{
		BmwNetReturnInfo ret = this.RenderFile2(null, options, filename, null);
		return ret.Sb.ToString();
	}}
	private static IBmwNetOutput Parser(string tplcode, IDictionary options) {{
		int view = Interlocked.Increment(ref _view);
		StringBuilder sb = new StringBuilder();
		IDictionary options_copy = new Hashtable();
		foreach (DictionaryEntry options_de in options) options_copy[options_de.Key] = options_de.Value;
		sb.AppendFormat(@""
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using {0}.BLL;
using {0}.Model;

namespace BmwDynamicCodeGenerate {{{{
	public class view{{0}} : BmwNet.IBmwNetOutput {{{{
		public BmwNet.BmwNetReturnInfo OuTpUt(StringBuilder tOuTpUt, IDictionary oPtIoNs, string rEfErErFiLeNaMe, BmwNet bMwSeNdEr) {{{{
			BmwNet.BmwNetReturnInfo rTn = tOuTpUt == null ? 
				new BmwNet.BmwNetReturnInfo {{{{ Sb = (tOuTpUt = new StringBuilder()), Blocks = new Dictionary<string, int[]>() }}}} :
				new BmwNet.BmwNetReturnInfo {{{{ Sb = tOuTpUt, Blocks = new Dictionary<string, int[]>() }}}};
			Dictionary<string, int[]> BMW__blocks = rTn.Blocks;
			Stack<int[]> BMW__blocks_stack = new Stack<int[]>();
			int[] BMW__blocks_stack_peek;
			List<IDictionary> BMW__forc = new List<IDictionary>();

			Func<IDictionary> pRoCeSsOpTiOnS = new Func<IDictionary>(delegate () {{{{
				IDictionary nEwoPtIoNs = new Hashtable();
				foreach (DictionaryEntry oPtIoNs_dE in oPtIoNs)
					nEwoPtIoNs[oPtIoNs_dE.Key] = oPtIoNs_dE.Value;
				foreach (IDictionary BMW__forc_dIc in BMW__forc)
					foreach (DictionaryEntry BMW__forc_dIc_dE in BMW__forc_dIc)
						nEwoPtIoNs[BMW__forc_dIc_dE.Key] = BMW__forc_dIc_dE.Value;
				return nEwoPtIoNs;
			}}}});
			BmwNet.BmwNetIf bMwIf = delegate(object exp) {{{{
				if (exp is bool) return (bool)exp;
				if (exp == null) return false;
				if (exp is int && (int)exp == 0) return false;
				if (exp is string && (string)exp == string.Empty) return false;

				if (exp is long && (long)exp == 0) return false;
				if (exp is short && (short)exp == 0) return false;
				if (exp is byte && (byte)exp == 0) return false;

				if (exp is double && (double)exp == 0) return false;
				if (exp is float && (float)exp == 0) return false;
				if (exp is decimal && (decimal)exp == 0) return false;
				return true;
			}}}};
			BmwNet.BmwNetPrint print = delegate(object[] pArMs) {{{{
				if (pArMs == null || pArMs.Length == 0) return;
				foreach (object pArMs_A in pArMs) if (pArMs_A != null) tOuTpUt.Append(pArMs_A);
			}}}};
			BmwNet.BmwNetPrint Print = print;"", view);

		#region {{miss}}...{{/miss}}块内容将不被解析
		string[] tmp_content_arr = _reg_miss.Split(tplcode);
		if (tmp_content_arr.Length > 1) {{
			sb.AppendFormat(@""
			string[] BMW__MISS = new string[{{0}}];"", Math.Ceiling(1.0 * (tmp_content_arr.Length - 1) / 2));
			int miss_len = -1;
			for (int a = 1; a < tmp_content_arr.Length; a += 2) {{
				sb.Append(string.Concat(@""
			BMW__MISS["", ++miss_len, @""] = """""", Utils.GetConstString(tmp_content_arr[a]), @"""""";""));
				tmp_content_arr[a] = string.Concat(""{{#BMW__MISS["", miss_len, ""]}}"");
			}}
			tplcode = string.Join("""", tmp_content_arr);
		}}
		#endregion
		#region 扩展语法如 <div @if=""表达式""></div>
		tplcode = htmlSyntax(tplcode, 3); //<div @if=""c#表达式"" @for=""index 1,100""></div>
										  //处理 {{% %}} 块 c#代码
		tmp_content_arr = _reg_code.Split(tplcode);
		if (tmp_content_arr.Length == 1) {{
			tplcode = Utils.GetConstString(tplcode)
				.Replace(""{{%"", ""{{$BMW__CODE}}"")
				.Replace(""%}}"", ""{{/$BMW__CODE}}"");
		}} else {{
			tmp_content_arr[0] = Utils.GetConstString(tmp_content_arr[0]);
			for (int a = 1; a < tmp_content_arr.Length; a += 4) {{
				tmp_content_arr[a] = ""{{$BMW__CODE}}"";
				tmp_content_arr[a + 2] = ""{{/$BMW__CODE}}"";
				tmp_content_arr[a + 3] = Utils.GetConstString(tmp_content_arr[a + 3]);
			}}
			tplcode = string.Join("""", tmp_content_arr);
		}}
		#endregion
		sb.Append(@""
			tOuTpUt.Append("""""");

		string error = null;
		int bmw_tmpid = 0;
		int forc_i = 0;
		string extends = null;
		Stack<string> codeTree = new Stack<string>();
		Stack<string> forEndRepl = new Stack<string>();
		sb.Append(_reg.Replace(tplcode, delegate (Match m) {{
			string _0 = m.Groups[0].Value;
			if (!string.IsNullOrEmpty(error)) return _0;

			string _1 = m.Groups[1].Value.Trim(' ', '\t');
			string _2 = m.Groups[2].Value
				.Replace(""\\\\"", ""\\"")
				.Replace(""\\\"""", ""\"""");
			_2 = Utils.ReplaceSingleQuote(_2);

			switch (_1) {{
				#region $BMW__CODE--------------------------------------------------
				case ""$BMW__CODE"":
					codeTree.Push(_1);
					return @"""""");
"";
				case ""/$BMW__CODE"":
					string pop = codeTree.Pop();
					if (pop != ""$BMW__CODE"") {{
						codeTree.Push(pop);
						error = ""编译出错，{{% 与 %}} 并没有配对"";
						return _0;
					}}
					return @""
			tOuTpUt.Append("""""";
				#endregion
				case ""include"":
					return string.Format(@"""""");
bMwSeNdEr.RenderFile2(tOuTpUt, pRoCeSsOpTiOnS(), """"{{0}}"""", rEfErErFiLeNaMe);
			tOuTpUt.Append("""""", _2);
				case ""import"":
					return _0;
				case ""module"":
					return _0;
				case ""/module"":
					return _0;
				case ""extends"":
					//{{extends ../inc/layout.html}}
					if (string.IsNullOrEmpty(extends) == false) return _0;
					extends = _2;
					return string.Empty;
				case ""block"":
					codeTree.Push(""block"");
					return string.Format(@"""""");
BMW__blocks_stack_peek = new int[] {{{{ tOuTpUt.Length, 0 }}}};
BMW__blocks_stack.Push(BMW__blocks_stack_peek);
BMW__blocks.Add(""""{{0}}"""", BMW__blocks_stack_peek);
tOuTpUt.Append("""""", _2.Trim(' ', '\t'));
				case ""/block"":
					codeTreeEnd(codeTree, ""block"");
					return @"""""");
BMW__blocks_stack_peek = BMW__blocks_stack.Pop();
BMW__blocks_stack_peek[1] = tOuTpUt.Length - BMW__blocks_stack_peek[0];
tOuTpUt.Append("""""";

				#region ##---------------------------------------------------------
				case ""#"":
					if (_2[0] == '#')
						return string.Format(@"""""");
			try {{{{ Print({{0}}); }}}} catch {{{{ }}}}
			tOuTpUt.Append("""""", _2.Substring(1));
					return string.Format(@"""""");
			Print({{0}});
			tOuTpUt.Append("""""", _2);
				#endregion
				#region for--------------------------------------------------------
				case ""for"":
					forc_i++;
					int cur_bmw_tmpid = bmw_tmpid;
					string sb_endRepl = string.Empty;
					StringBuilder sbfor = new StringBuilder();
					sbfor.Append(@"""""");"");
					Match mfor = _reg_forin.Match(_2);
					if (mfor.Success) {{
						string mfor1 = mfor.Groups[1].Value.Trim(' ', '\t');
						string mfor2 = mfor.Groups[2].Value.Trim(' ', '\t');
						sbfor.AppendFormat(@""
//new Action(delegate () {{{{
	IDictionary BMW__tmp{{0}} = new Hashtable();
	BMW__forc.Add(BMW__tmp{{0}});
	var BMW__tmp{{1}} = {{3}};
	var BMW__tmp{{2}} = {{4}};"", ++bmw_tmpid, ++bmw_tmpid, ++bmw_tmpid, mfor.Groups[3].Value, mfor1);
						sb_endRepl = string.Concat(sb_endRepl, string.Format(@""
	{{0}} = BMW__tmp{{1}};"", mfor1, cur_bmw_tmpid + 3));
						if (options_copy.Contains(mfor1) == false) options_copy[mfor1] = null;
						if (!string.IsNullOrEmpty(mfor2)) {{
							sbfor.AppendFormat(@""
	var BMW__tmp{{1}} = {{0}};
	{{0}} = 0;"", mfor2, ++bmw_tmpid);
							sb_endRepl = string.Concat(sb_endRepl, string.Format(@""
	{{0}} = BMW__tmp{{1}};"", mfor2, bmw_tmpid));
							if (options_copy.Contains(mfor2) == false) options_copy[mfor2] = null;
						}}
						sbfor.AppendFormat(@""
	if (BMW__tmp{{1}} != null)
	foreach (var BMW__tmp{{0}} in BMW__tmp{{1}}) {{{{"", ++bmw_tmpid, cur_bmw_tmpid + 2);
						if (!string.IsNullOrEmpty(mfor2))
							sbfor.AppendFormat(@""
		BMW__tmp{{1}}[""""{{0}}""""] = ++ {{0}};"", mfor2, cur_bmw_tmpid + 1);
						sbfor.AppendFormat(@""
		BMW__tmp{{1}}[""""{{0}}""""] = BMW__tmp{{2}};
		{{0}} = BMW__tmp{{2}};
		tOuTpUt.Append("""""", mfor1, cur_bmw_tmpid + 1, bmw_tmpid);
						codeTree.Push(""for"");
						forEndRepl.Push(sb_endRepl);
						return sbfor.ToString();
					}}
					mfor = _reg_foron.Match(_2);
					if (mfor.Success) {{
						string mfor1 = mfor.Groups[1].Value.Trim(' ', '\t');
						string mfor2 = mfor.Groups[2].Value.Trim(' ', '\t');
						string mfor3 = mfor.Groups[3].Value.Trim(' ', '\t');
						sbfor.AppendFormat(@""
//new Action(delegate () {{{{
	IDictionary BMW__tmp{{0}} = new Hashtable();
	BMW__forc.Add(BMW__tmp{{0}});
	var BMW__tmp{{1}} = {{3}};
	var BMW__tmp{{2}} = {{4}};"", ++bmw_tmpid, ++bmw_tmpid, ++bmw_tmpid, mfor.Groups[4].Value, mfor1);
						sb_endRepl = string.Concat(sb_endRepl, string.Format(@""
	{{0}} = BMW__tmp{{1}};"", mfor1, cur_bmw_tmpid + 3));
						if (options_copy.Contains(mfor1) == false) options_copy[mfor1] = null;
						if (!string.IsNullOrEmpty(mfor2)) {{
							sbfor.AppendFormat(@""
	var BMW__tmp{{1}} = {{0}};"", mfor2, ++bmw_tmpid);
							sb_endRepl = string.Concat(sb_endRepl, string.Format(@""
	{{0}} = BMW__tmp{{1}};"", mfor2, bmw_tmpid));
							if (options_copy.Contains(mfor2) == false) options_copy[mfor2] = null;
						}}
						if (!string.IsNullOrEmpty(mfor3)) {{
							sbfor.AppendFormat(@""
	var BMW__tmp{{1}} = {{0}};
	{{0}} = 0;"", mfor3, ++bmw_tmpid);
							sb_endRepl = string.Concat(sb_endRepl, string.Format(@""
	{{0}} = BMW__tmp{{1}};"", mfor3, bmw_tmpid));
							if (options_copy.Contains(mfor3) == false) options_copy[mfor3] = null;
						}}
						sbfor.AppendFormat(@""
	if (BMW__tmp{{2}} != null)
	foreach (DictionaryEntry BMW__tmp{{1}} in BMW__tmp{{2}}) {{{{
		{{0}} = BMW__tmp{{1}}.Key;
		BMW__tmp{{3}}[""""{{0}}""""] = {{0}};"", mfor1, ++bmw_tmpid, cur_bmw_tmpid + 2, cur_bmw_tmpid + 1);
						if (!string.IsNullOrEmpty(mfor2))
							sbfor.AppendFormat(@""
		{{0}} = BMW__tmp{{1}}.Value;
		BMW__tmp{{2}}[""""{{0}}""""] = {{0}};"", mfor2, bmw_tmpid, cur_bmw_tmpid + 1);
						if (!string.IsNullOrEmpty(mfor3))
							sbfor.AppendFormat(@""
		BMW__tmp{{1}}[""""{{0}}""""] = ++ {{0}};"", mfor3, cur_bmw_tmpid + 1);
						sbfor.AppendFormat(@""
		tOuTpUt.Append("""""");
						codeTree.Push(""for"");
						forEndRepl.Push(sb_endRepl);
						return sbfor.ToString();
					}}
					mfor = _reg_forab.Match(_2);
					if (mfor.Success) {{
						string mfor1 = mfor.Groups[1].Value.Trim(' ', '\t');
						sbfor.AppendFormat(@""
//new Action(delegate () {{{{
	IDictionary BMW__tmp{{0}} = new Hashtable();
	BMW__forc.Add(BMW__tmp{{0}});
	var BMW__tmp{{1}} = {{5}};
	{{5}} = {{3}} - 1;
	if ({{5}} == null) {{5}} = 0;
	var BMW__tmp{{2}} = {{4}} + 1;
	while (++{{5}} < BMW__tmp{{2}}) {{{{
		BMW__tmp{{0}}[""""{{5}}""""] = {{5}};
		tOuTpUt.Append("""""", ++bmw_tmpid, ++bmw_tmpid, ++bmw_tmpid, mfor.Groups[2].Value, mfor.Groups[3].Value, mfor1);
						sb_endRepl = string.Concat(sb_endRepl, string.Format(@""
	{{0}} = BMW__tmp{{1}};"", mfor1, cur_bmw_tmpid + 1));
						if (options_copy.Contains(mfor1) == false) options_copy[mfor1] = null;
						codeTree.Push(""for"");
						forEndRepl.Push(sb_endRepl);
						return sbfor.ToString();
					}}
					return _0;
				case ""/for"":
					if (--forc_i < 0) return _0;
					codeTreeEnd(codeTree, ""for"");
					return string.Format(@"""""");
	}}}}{{0}}
	BMW__forc.RemoveAt(BMW__forc.Count - 1);
//}}}})();
			tOuTpUt.Append("""""", forEndRepl.Pop());
				#endregion
				#region if---------------------------------------------------------
				case ""if"":
					codeTree.Push(""if"");
					return string.Format(@"""""");
			if ({{1}}bMwIf({{0}})) {{{{
				tOuTpUt.Append("""""", _2[0] == '!' ? _2.Substring(1) : _2, _2[0] == '!' ? '!' : ' ');
				case ""elseif"":
					codeTreeEnd(codeTree, ""if"");
					codeTree.Push(""if"");
					return string.Format(@"""""");
			}}}} else if ({{1}}bMwIf({{0}})) {{{{
				tOuTpUt.Append("""""", _2[0] == '!' ? _2.Substring(1) : _2, _2[0] == '!' ? '!' : ' ');
				case ""else"":
					codeTreeEnd(codeTree, ""if"");
					codeTree.Push(""if"");
					return @"""""");
			}} else {{
			tOuTpUt.Append("""""";
				case ""/if"":
					codeTreeEnd(codeTree, ""if"");
					return @"""""");
			}}
			tOuTpUt.Append("""""";
					#endregion
			}}
			return _0;
		}}));

		sb.Append(@"""""");"");
		if (string.IsNullOrEmpty(extends) == false) {{
			sb.AppendFormat(@""
BmwNet.BmwNetReturnInfo eXtEnDs_ReT = bMwSeNdEr.RenderFile2(null, pRoCeSsOpTiOnS(), """"{{0}}"""", rEfErErFiLeNaMe);
string rTn_Sb_string = rTn.Sb.ToString();
foreach(string eXtEnDs_ReT_blocks_key in eXtEnDs_ReT.Blocks.Keys) {{{{
	if (rTn.Blocks.ContainsKey(eXtEnDs_ReT_blocks_key)) {{{{
		int[] eXtEnDs_ReT_blocks_value = eXtEnDs_ReT.Blocks[eXtEnDs_ReT_blocks_key];
		eXtEnDs_ReT.Sb.Remove(eXtEnDs_ReT_blocks_value[0], eXtEnDs_ReT_blocks_value[1]);
		int[] rTn_blocks_value = rTn.Blocks[eXtEnDs_ReT_blocks_key];
		eXtEnDs_ReT.Sb.Insert(eXtEnDs_ReT_blocks_value[0], rTn_Sb_string.Substring(rTn_blocks_value[0], rTn_blocks_value[1]));
		foreach(string eXtEnDs_ReT_blocks_keyb in eXtEnDs_ReT.Blocks.Keys) {{{{
			if (eXtEnDs_ReT_blocks_keyb == eXtEnDs_ReT_blocks_key) continue;
			int[] eXtEnDs_ReT_blocks_valueb = eXtEnDs_ReT.Blocks[eXtEnDs_ReT_blocks_keyb];
			if (eXtEnDs_ReT_blocks_valueb[0] >= eXtEnDs_ReT_blocks_value[0])
				eXtEnDs_ReT_blocks_valueb[0] = eXtEnDs_ReT_blocks_valueb[0] - eXtEnDs_ReT_blocks_value[1] + rTn_blocks_value[1];
		}}}}
		eXtEnDs_ReT_blocks_value[1] = rTn_blocks_value[1];
	}}}}
}}}}
return eXtEnDs_ReT;
"", extends);
		}} else {{
			sb.Append(@""
return rTn;"");
		}}
		sb.Append(@""
		}}
	}}
}}
"");
		int dim_idx = sb.ToString().IndexOf(""BmwNet.BmwNetPrint Print = print;"") + 33;
		foreach (string dic_name in options_copy.Keys) {{
			sb.Insert(dim_idx, string.Format(@""
			dynamic {{0}} = oPtIoNs[""""{{0}}""""];"", dic_name));
		}}
		//Console.WriteLine(sb.ToString());
		return Complie(sb.ToString(), @""BmwDynamicCodeGenerate.view"" + view);
	}}
	private static string codeTreeEnd(Stack<string> codeTree, string tag) {{
		string ret = string.Empty;
		Stack<int> pop = new Stack<int>();
		foreach (string ct in codeTree) {{
			if (ct == ""import"" ||
				ct == ""include"") {{
				pop.Push(1);
			}} else if (ct == tag) {{
				pop.Push(2);
				break;
			}} else {{
				if (string.IsNullOrEmpty(tag) == false) pop.Clear();
				break;
			}}
		}}
		if (pop.Count == 0 && string.IsNullOrEmpty(tag) == false)
			return string.Concat(""语法错误，{{"", tag, ""}} {{/"", tag, ""}} 并没配对"");
		while (pop.Count > 0 && pop.Pop() > 0) codeTree.Pop();
		return ret;
	}}
	#region htmlSyntax
	private static string htmlSyntax(string tplcode, int num) {{

		while (num-- > 0) {{
			string[] arr = _reg_syntax.Split(tplcode);

			if (arr.Length == 1) break;
			for (int a = 1; a < arr.Length; a += 4) {{
				string tag = string.Concat('<', arr[a]);
				string end = string.Concat(""</"", arr[a], '>');
				int fc = 1;
				for (int b = a; fc > 0 && b < arr.Length; b += 4) {{
					if (b > a && arr[a].ToLower() == arr[b].ToLower()) fc++;
					int bpos = 0;
					while (true) {{
						int fa = arr[b + 3].IndexOf(tag, bpos);
						int fb = arr[b + 3].IndexOf(end, bpos);
						if (b == a) {{
							var z = arr[b + 3].IndexOf(""/>"");
							if ((fb == -1 || z < fb) && z != -1) {{
								var y = arr[b + 3].Substring(0, z + 2);
								if (_reg_htmltag.IsMatch(y) == false)
									fb = z - end.Length + 2;
							}}
						}}
						if (fa == -1 && fb == -1) break;
						if (fa != -1 && (fa < fb || fb == -1)) {{
							fc++;
							bpos = fa + tag.Length;
							continue;
						}}
						if (fb != -1) fc--;
						if (fc <= 0) {{
							var a1 = arr[a + 1];
							var end3 = string.Concat(""{{/"", a1, ""}}"");
							if (a1.ToLower() == ""else"") {{
								if (_reg_blank.Replace(arr[a - 4 + 3], """").EndsWith(""{{/if}}"", true, null) == true) {{
									var idx = arr[a - 4 + 3].IndexOf(""{{/if}}"");
									arr[a - 4 + 3] = string.Concat(arr[a - 4 + 3].Substring(0, idx), arr[a - 4 + 3].Substring(idx + 5));
									//如果 @else=""有条件内容""，则变换成 elseif 条件内容
									if (_reg_blank.Replace(arr[a + 2], """").Length > 0) a1 = ""elseif"";
									end3 = ""{{/if}}"";
								}} else {{
									arr[a] = string.Concat(""指令 @"", arr[a + 1], ""='"", arr[a + 2], ""' 没紧接着 if/else 指令之后，无效. <"", arr[a]);
									arr[a + 1] = arr[a + 2] = string.Empty;
								}}
							}}
							if (arr[a + 1].Length > 0) {{
								if (_reg_blank.Replace(arr[a + 2], """").Length > 0 || a1.ToLower() == ""else"") {{
									arr[b + 3] = string.Concat(arr[b + 3].Substring(0, fb + end.Length), end3, arr[b + 3].Substring(fb + end.Length));
									arr[a] = string.Concat(""{{"", a1, "" "", arr[a + 2], ""}}<"", arr[a]);
									arr[a + 1] = arr[a + 2] = string.Empty;
								}} else {{
									arr[a] = string.Concat('<', arr[a]);
									arr[a + 1] = arr[a + 2] = string.Empty;
								}}
							}}
							break;
						}}
						bpos = fb + end.Length;
					}}
				}}
				if (fc > 0) {{
					arr[a] = string.Concat(""不严谨的html格式，请检查 "", arr[a], "" 的结束标签, @"", arr[a + 1], ""='"", arr[a + 2], ""' 指令无效. <"", arr[a]);
					arr[a + 1] = arr[a + 2] = string.Empty;
				}}
			}}
			if (arr.Length > 0) tplcode = string.Join(string.Empty, arr);
		}}
		return tplcode;
	}}
	#endregion
	#region Complie
	private static string _db_dll_location;
	private static IBmwNetOutput Complie(string cscode, string typename) {{
		// 1.CSharpCodePrivoder
		CSharpCodeProvider objCSharpCodePrivoder = new CSharpCodeProvider();
		// 3.CompilerParameters
		CompilerParameters objCompilerParameters = new CompilerParameters();
		objCompilerParameters.ReferencedAssemblies.Add(""System.dll"");
		objCompilerParameters.GenerateExecutable = false;
		objCompilerParameters.GenerateInMemory = true;

		if (string.IsNullOrEmpty(_db_dll_location)) _db_dll_location = Type.GetType(""{0}.DAL.SqlHelper, {0}.db"").Assembly.Location;
		objCompilerParameters.ReferencedAssemblies.Add(Assembly.GetCallingAssembly().Location);
		objCompilerParameters.ReferencedAssemblies.Add(_db_dll_location);
		objCompilerParameters.ReferencedAssemblies.Add(""System.Core.dll"");
		objCompilerParameters.ReferencedAssemblies.Add(""Microsoft.CSharp.dll"");
		// 4.CompilerResults
		CompilerResults cr = objCSharpCodePrivoder.CompileAssemblyFromSource(objCompilerParameters, cscode);

		if (cr.Errors.HasErrors) {{
			StringBuilder sb = new StringBuilder();
			sb.Append(""编译错误："");
			int undefined_idx = 0;
			int undefined_cout = 0;
			Dictionary<string, bool> undefined_exists = new Dictionary<string, bool>();
			foreach (CompilerError err in cr.Errors) {{
				sb.Append(err.ErrorText + "" 在第"" + err.Line + ""行\r\n"");
				if (err.ErrorNumber == ""CS0103"") {{
					//如果未定义变量，则自定义变量后重新编译
					Match m = _reg_complie_undefined.Match(err.ErrorText);
					if (m.Success) {{
						string undefined_name = m.Groups[2].Value;
						if (undefined_exists.ContainsKey(undefined_name) == false) {{
							if (undefined_idx <= 0) undefined_idx = cscode.IndexOf(""BmwNet.BmwNetPrint Print = print;"") + 33;
							cscode = cscode.Insert(undefined_idx, string.Format(""\r\n\t\t\tdynamic {{0}} = oPtIoNs[\""{{0}}\""];"", undefined_name));
							undefined_exists.Add(undefined_name, true);
						}}
						undefined_cout++;
					}} else {{
						sb.AppendFormat(""错误编号：CS0103，但是 _reg_undefined({{0}}) 匹配不到 ErrorText({{1}})\r\n"", _reg_complie_undefined, err.ErrorText);
					}}
				}}
			}}
			if (cr.Errors.Count == undefined_cout) {{
				return Complie(cscode, typename);
			}} else {{
				sb.Append(cscode);
				throw new Exception(sb.ToString());
			}}
		}} else {{
			object ret = cr.CompiledAssembly.CreateInstance(typename);
			return ret as IBmwNetOutput;
		}}
	}}
	#endregion

	#region Utils
	public class Utils {{
		public static string ReplaceSingleQuote(object exp) {{
			//将 ' 转换成 ""
			string exp2 = string.Concat(exp);
			int quote_pos = -1;
			while (true) {{
				int first_pos = quote_pos = exp2.IndexOf('\'', quote_pos + 1);
				if (quote_pos == -1) break;
				while (true) {{
					quote_pos = exp2.IndexOf('\'', quote_pos + 1);
					if (quote_pos == -1) break;
					int r_cout = 0;
					for (int p = 1; true; p++) {{
						if (exp2[quote_pos - p] == '\\') r_cout++;
						else break;
					}}
					if (r_cout % 2 == 0/* && quote_pos - first_pos > 2*/) {{
						string str1 = exp2.Substring(0, first_pos);
						string str2 = exp2.Substring(first_pos + 1, quote_pos - first_pos - 1);
						string str3 = exp2.Substring(quote_pos + 1);
						string str4 = str2.Replace(""\"""", ""\\\"""");
						quote_pos += str4.Length - str2.Length;
						exp2 = string.Concat(str1, ""\"""", str4, ""\"""", str3);
						break;
					}}
				}}
				if (quote_pos == -1) break;
			}}
			return exp2;
		}}
		public static string GetConstString(object obj) {{
			return string.Concat(obj)
				.Replace(""\\"", ""\\\\"")
				.Replace(""\"""", ""\\\"""")
				.Replace(""\r"", ""\\r"")
				.Replace(""\n"", ""\\n"");
		}}

		public static string TranslateUrl(string url) {{
			return TranslateUrl(url, null);
		}}
		private static object _ecd_lock = new object();
		public static string TranslateUrl(string url, string baseDir) {{
			if (string.IsNullOrEmpty(baseDir)) baseDir = AppDomain.CurrentDomain.BaseDirectory + ""/"";;
			if (string.IsNullOrEmpty(url)) return Path.GetDirectoryName(baseDir);
			if (url.StartsWith(""/"")) return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(baseDir), url.TrimStart('/')));
			if (url.StartsWith(""\\"")) return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(baseDir), url.TrimStart('\\')));
			if (url.StartsWith(""~/"") && HttpContext.Current != null) return HttpContext.Current.Server.MapPath(url);
			if (url.IndexOf("":\\"") != -1) return url;
			return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(baseDir), url));
		}}

		public static string ReadTextFile(string path) {{
			byte[] bytes = ReadFile(path);
			return Encoding.UTF8.GetString(bytes).TrimStart((char)65279);
		}}

		public static byte[] ReadFile(string path) {{
			path = TranslateUrl(path);

			//if (File.Exists(path)) {{
			//string destFileName = Path.GetTempFileName();
			//File.Copy(path, destFileName, true);
			string destFileName = path;
			int read = 0;
			byte[] data = new byte[1024 * 8];
			MemoryStream ms = new MemoryStream();
			using (FileStream fs = new FileStream(destFileName, FileMode.OpenOrCreate, FileAccess.Read)) {{
				do {{
					read = fs.Read(data, 0, data.Length);
					if (read <= 0) break;
					ms.Write(data, 0, read);
				}} while (true);
				fs.Close();
			}}
			//File.Delete(destFileName);
			data = ms.ToArray();
			ms.Close();
			return data;
			//}}
			//return new byte[] {{ }};
		}}
	}}
	#endregion
}}";
			#endregion
			public static readonly string Common_IniHelper_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;

public class IniHelper {{
	private static Dictionary<string, object> _cache = new Dictionary<string, object>();
	private static Dictionary<string, FileSystemWatcher> _watcher = new Dictionary<string, FileSystemWatcher>();
	private static object _lock = new object();

	private static object loadAndCache(string path) {{
		path = BmwNet.Utils.TranslateUrl(path);
		object ret = null;
		if (!_cache.TryGetValue(path, out ret)) {{
			object value2 = LoadIniNotCache(path);
			string dir = Path.GetDirectoryName(path);
			string name = Path.GetFileName(path);
			FileSystemWatcher fsw = new FileSystemWatcher(dir, name);
			fsw.IncludeSubdirectories = false;
			fsw.Changed += watcher_handler;
			fsw.Renamed += watcher_handler;
			fsw.EnableRaisingEvents = false;
			lock (_lock) {{
				if (!_cache.TryGetValue(path, out ret)) {{
					_cache.Add(path, ret = value2);
					_watcher.Add(path, fsw);
					fsw.EnableRaisingEvents = true;
				}} else {{
					fsw.Dispose();
				}}
			}}
		}}
		return ret;
	}}
	private static void watcher_handler(object sender, FileSystemEventArgs e) {{
		lock (_lock) {{
			_cache.Remove(e.FullPath);
			FileSystemWatcher fsw = null;
			if (_watcher.TryGetValue(e.FullPath, out fsw)) {{
				fsw.EnableRaisingEvents = false;
				fsw.Dispose();
			}}
		}}
	}}

	public static Dictionary<string, NameValueCollection> LoadIni(string path) {{
		return loadAndCache(path) as Dictionary<string, NameValueCollection>;
	}}
	public static Dictionary<string, NameValueCollection> LoadIniNotCache(string path) {{
		Dictionary<string, NameValueCollection> ret = new Dictionary<string, NameValueCollection>();
		string[] lines = ReadTextFile(path).Split(new string[] {{ ""\n"" }}, StringSplitOptions.None);
		string key = """";
		foreach (string line2 in lines) {{
			string line = line2.Trim();
			int idx = line.IndexOf('#');
			if (idx != -1) line = line.Remove(idx);
			if (string.IsNullOrEmpty(line)) continue;

			Match m = Regex.Match(line, @""^\[([^\]]+)\]$"");
			if (m.Success) {{
				key = m.Groups[1].Value;
				continue;
			}}
			if (!ret.ContainsKey(key)) ret.Add(key, new NameValueCollection());
			string[] kv = line.Split(new char[] {{ '=' }}, 2);
			if (!string.IsNullOrEmpty(kv[0])) {{
				ret[key][kv[0]] = kv.Length > 1 ? kv[1] : null;
			}}
		}}
		return ret;
	}}

	public static string ReadTextFile(string path) {{
		byte[] bytes = ReadFile(path);
		return Encoding.UTF8.GetString(bytes).TrimStart((char)65279);
	}}
	public static byte[] ReadFile(string path) {{
		if (File.Exists(path)) {{
			string destFileName = Path.GetTempFileName();
			File.Copy(path, destFileName, true);
			int read = 0;
			byte[] data = new byte[1024];
			using (MemoryStream ms = new MemoryStream()) {{
				using (FileStream fs = new FileStream(destFileName, FileMode.OpenOrCreate, FileAccess.Read)) {{
					do {{
						read = fs.Read(data, 0, data.Length);
						if (read <= 0) break;
						ms.Write(data, 0, read);
					}} while (true);
				}}
				File.Delete(destFileName);
				data = ms.ToArray();
			}}
			return data;
		}}
		return new byte[] {{ }};
	}}
}}";
			#endregion
			public static readonly string Common_GraphicHelper_cs =
			#region 内容太长已被收起
 @"using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

public class GraphicHelper {{

	/// <summary>
	/// 缩放图片后保存到本地路径
	/// 如： GraphicHelper.ResizeAndSave(""http://www.163.com/logo.jpg"", @""d:\web\logo.jpg"", 90, 90)
	/// 最终保存：d:\web\logo-90x90.jpg
	/// </summary>
	/// <param name=""url"">本地图片或Internet地址</param>
	/// <param name=""filename"">本地保存路径</param>
	/// <param name=""whs"">宽度，高度数组</param>
	public static void ResizeAndSave(string url, string filename, params int[] whs) {{
		if (whs == null || whs.Length == 0 || whs.Length % 2 != 0) return;
		string ext = Path.GetExtension(filename);
		filename = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename));

		if (Uri.IsWellFormedUriString(url, UriKind.Relative))
			url = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, url);

		Uri uri = null;
		if (!Uri.TryCreate(url, UriKind.Absolute, out uri)) return;

		MemoryStream stream = new MemoryStream();
		try {{
			if (uri.IsFile) {{
				if (File.Exists(url)) {{
					byte[] bytes = new byte[1024];
					int reads = 0;

					using (FileStream fs = new FileStream(url, FileMode.Open, FileAccess.Read)) {{
						while ((reads = fs.Read(bytes, 0, 1024)) > 0) {{
							stream.Write(bytes, 0, reads);
						}}
						fs.Close();
					}}
				}}
			}} else {{
				HttpRequest hr = new HttpRequest();
				hr.Action = url;
				hr.Send();
				byte[] bytes = hr.Response.GetStream();
				stream.Write(bytes, 0, bytes.Length);
			}}

			for (int a = 0; a < whs.Length; a += 2) {{
				int width = whs[a];
				int height = whs[a + 1];
				string path = filename + ""-"" + width + ""x"" + height + ext;

				if (!File.Exists(path)) {{
					SaveToFile(ResizeAsyncOuterCanvas(stream, width, height), path);
				}}
			}}
		}} finally {{
			stream.Close();
			stream.Dispose();
		}}
	}}

	/// <summary>
	/// 按图像新的高度来同比例缩放
	/// </summary>
	/// <param name=""stream"">图像流</param>
	/// <param name=""height"">新的高度</param>
	/// <returns>新的图像流</returns>
	protected static Stream ResizeAsyncOverflowByHeight(Stream stream, int height) {{
		MemoryStream ms = new MemoryStream();
		using (Bitmap img = new Bitmap(stream)) {{
			if (img.Height > height) {{
				using (Bitmap bmp = new Bitmap(img.Width, height * img.Width / height)) {{
					using (Graphics g = Graphics.FromImage(bmp)) {{
						g.DrawImage(img, 0, 0, bmp.Width, bmp.Height);
						bmp.Save(ms, ImageFormat.Jpeg);
					}}
				}}
			}} else {{
				img.Save(ms, ImageFormat.Jpeg);
			}}
		}}
		return ms;
	}}

	/// 按图像新的宽度来同比例缩放
	/// </summary>
	/// <param name=""stream"">图像流</param>
	/// <param name=""width"">新的宽度</param>
	/// <returns>新的图像流</returns>
	protected static Stream ResizeAsyncOverflowByWidth(Stream stream, int width) {{
		MemoryStream ms = new MemoryStream();
		using (Bitmap img = new Bitmap(stream)) {{
			if (img.Width > width) {{
				using (Bitmap bmp = new Bitmap(width, img.Height * width / img.Width)) {{
					using (Graphics g = Graphics.FromImage(bmp)) {{
						g.DrawImage(img, 0, 0, bmp.Width, bmp.Height);
						bmp.Save(ms, ImageFormat.Jpeg);
					}}
				}}
			}} else {{
				img.Save(ms, ImageFormat.Jpeg);
			}}
		}}
		return ms;
	}}

	/// <summary>
	/// 将图像等比例缩放到最大大小，并能居中显示在指定大小的画布中，将其它区域填充为白色
	/// </summary>
	/// <param name=""stream"">图像流</param>
	/// <param name=""width"">画布宽度</param>
	/// <param name=""height"">画面高度</param>
	/// <returns>新的图像流</returns>
	protected static Stream ResizeAsyncInnerCanvas(Stream stream, int width, int height) {{
		MemoryStream ms = new MemoryStream();
		float newWidth, newHeight;
		float x, y, srcWidth, srcHeight;
		using (Bitmap img = new Bitmap(stream)) {{
			newWidth = (float)img.Width / img.Height * height;
			newHeight = (float)img.Height / img.Width * width;
			if (newWidth <= width) {{
				x = (width - newWidth) / 2;
				y = 0;
				srcWidth = newWidth;
				srcHeight = height;
			}} else {{
				x = 0;
				y = (height - newHeight) / 2;
				srcWidth = width;
				srcHeight = newHeight;
			}}
			using (Bitmap bmp = new Bitmap(width, height)) {{
				using (Graphics g = Graphics.FromImage(bmp)) {{
					g.FillRectangle(Brushes.White, 0, 0, width, height);
					g.DrawImage(img, x, y, srcWidth, srcHeight);
				}}
				bmp.Save(ms, ImageFormat.Jpeg);
			}}
		}}
		return ms;
	}}

	/// <summary>
	/// 将图像等比例缩放到最小大小，并能将画布全部填充
	/// </summary>
	/// <param name=""stream""></param>
	/// <param name=""width""></param>
	/// <param name=""height""></param>
	/// <returns></returns>
	protected static Stream ResizeAsyncOuterCanvas(Stream stream, int width, int height) {{
		MemoryStream ms = new MemoryStream();
		float newWidth, newHeight;
		float srcWidth, srcHeight;
		using (Bitmap img = new Bitmap(stream)) {{
			newWidth = (float)img.Width / img.Height * height;
			newHeight = (float)img.Height / img.Width * width;
			if (newWidth <= width) {{
				srcWidth = width;
				srcHeight = newHeight;
			}} else {{
				srcWidth = newWidth;
				srcHeight = height;
			}}
			using (Bitmap bmp = new Bitmap(width, height)) {{
				using (Graphics g = Graphics.FromImage(bmp)) {{
					g.DrawImage(img, 0, 0, srcWidth, srcHeight);
					bmp.Save(ms, ImageFormat.Jpeg);
				}}
			}}
		}}
		return ms;
	}}

	protected static void SaveToFile(Stream stream, string filename) {{
		string dir = Path.GetDirectoryName(filename);
		if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

		byte[] bytes = new byte[1024];
		int reads = 0;
		stream.Position = 0;

		using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write)) {{
			while ((reads = stream.Read(bytes, 0, 1024)) > 0) {{
				fs.Write(bytes, 0, reads);
			}}
			fs.Close();
		}}
		stream.Close();
	}}

	protected static void DeleteFile(string path) {{
		if (File.Exists(path)) {{
			File.Delete(path);
		}}
	}}
}}";
			#endregion
			public static readonly string Common_JSDecoder_cs =
			#region 内容太长已被收起
 @"using System;
using System.Text;
using System.Text.RegularExpressions;

public class JSDecoder {{
	private const byte STATE_COPY_INPUT = 100;
	private const byte STATE_READLEN = 101;
	private const byte STATE_DECODE = 102;
	private const byte STATE_UNESCAPE = 103;

	private static byte[] _pickEncoding;
	private static byte[] _rawData;
	private static byte[] _digits = new byte[123];
	private static byte[][] _transformed = new byte[3][];

	static JSDecoder() {{
		InitArrayData();
	}}

	private static void InitArrayData() {{
		_pickEncoding = new byte[] {{
			1, 2, 0, 1, 2, 0, 2, 0, 0, 2, 0, 2, 1, 0, 2, 0, 
			1, 0, 2, 0, 1, 1, 2, 0, 0, 2, 1, 0, 2, 0, 0, 2, 
			1, 1, 0, 2, 0, 2, 0, 1, 0, 1, 1, 2, 0, 1, 0, 2, 
			1, 0, 2, 0, 1, 1, 2, 0, 0, 1, 1, 2, 0, 1, 0, 2
		}};

		_rawData = new byte[] {{
			0x64,0x37,0x69, 0x50,0x7E,0x2C, 0x22,0x5A,0x65, 0x4A,0x45,0x72,
			0x61,0x3A,0x5B, 0x5E,0x79,0x66, 0x5D,0x59,0x75, 0x5B,0x27,0x4C,
			0x42,0x76,0x45, 0x60,0x63,0x76, 0x23,0x62,0x2A, 0x65,0x4D,0x43,
			0x5F,0x51,0x33, 0x7E,0x53,0x42, 0x4F,0x52,0x20, 0x52,0x20,0x63,
			0x7A,0x26,0x4A, 0x21,0x54,0x5A, 0x46,0x71,0x38, 0x20,0x2B,0x79,
			0x26,0x66,0x32, 0x63,0x2A,0x57, 0x2A,0x58,0x6C, 0x76,0x7F,0x2B,
			0x47,0x7B,0x46, 0x25,0x30,0x52, 0x2C,0x31,0x4F, 0x29,0x6C,0x3D,
			0x69,0x49,0x70, 0x3F,0x3F,0x3F, 0x27,0x78,0x7B, 0x3F,0x3F,0x3F,
			0x67,0x5F,0x51, 0x3F,0x3F,0x3F, 0x62,0x29,0x7A, 0x41,0x24,0x7E,
			0x5A,0x2F,0x3B, 0x66,0x39,0x47, 0x32,0x33,0x41, 0x73,0x6F,0x77,
			0x4D,0x21,0x56, 0x43,0x75,0x5F, 0x71,0x28,0x26, 0x39,0x42,0x78,
			0x7C,0x46,0x6E, 0x53,0x4A,0x64, 0x48,0x5C,0x74, 0x31,0x48,0x67,
			0x72,0x36,0x7D, 0x6E,0x4B,0x68, 0x70,0x7D,0x35, 0x49,0x5D,0x22,
			0x3F,0x6A,0x55, 0x4B,0x50,0x3A, 0x6A,0x69,0x60, 0x2E,0x23,0x6A,
			0x7F,0x09,0x71, 0x28,0x70,0x6F, 0x35,0x65,0x49, 0x7D,0x74,0x5C,
			0x24,0x2C,0x5D, 0x2D,0x77,0x27, 0x54,0x44,0x59, 0x37,0x3F,0x25,
			0x7B,0x6D,0x7C, 0x3D,0x7C,0x23, 0x6C,0x43,0x6D, 0x34,0x38,0x28,
			0x6D,0x5E,0x31, 0x4E,0x5B,0x39, 0x2B,0x6E,0x7F, 0x30,0x57,0x36,
			0x6F,0x4C,0x54, 0x74,0x34,0x34, 0x6B,0x72,0x62, 0x4C,0x25,0x4E,
			0x33,0x56,0x30, 0x56,0x73,0x5E, 0x3A,0x68,0x73, 0x78,0x55,0x09,
			0x57,0x47,0x4B, 0x77,0x32,0x61, 0x3B,0x35,0x24, 0x44,0x2E,0x4D,
			0x2F,0x64,0x6B, 0x59,0x4F,0x44, 0x45,0x3B,0x21, 0x5C,0x2D,0x37,
			0x68,0x41,0x53, 0x36,0x61,0x58, 0x58,0x7A,0x48, 0x79,0x22,0x2E,
			0x09,0x60,0x50, 0x75,0x6B,0x2D, 0x38,0x4E,0x29, 0x55,0x3D,0x3F
		}};

		for (byte i = 0; i < 3; i++) _transformed[i] = new byte[288];
		for (byte i = 31; i < 127; i++) for (byte j = 0; j < 3; j++) _transformed[j][_rawData[(i - 31) * 3 + j]] = i == 31 ? (byte)9 : i;

		for (byte i = 0; i < 26; i++) {{
			_digits[65 + i] = i;
			_digits[97 + i] = (byte)(i + 26);
		}}

		for (byte i = 0; i < 10; i++)
			_digits[48 + i] = (byte)(i + 52);

		_digits[43] = 62;
		_digits[47] = 63;
	}}

	private static string UnEscape(string s) {{
		string escapes = ""#&!*$"";
		string escaped = ""\r\n<>@"";

		if ((int)s.ToCharArray()[0] > 126) return s;
		if (escapes.IndexOf(s) != -1) return escaped.Substring(escapes.IndexOf(s), 1);
		return ""?"";
	}}

	private static int DecodeBase64(string s) {{
		int val = 0;
		byte[] bs = Encoding.Default.GetBytes(s);

		val += ((int)_digits[bs[0]] << 2);
		val += (_digits[bs[1]] >> 4);
		val += (_digits[bs[1]] & 0xf) << 12;
		val += ((_digits[bs[2]] >> 2) << 8);
		val += ((_digits[bs[2]] & 0x3) << 22);
		val += (_digits[bs[3]] << 16);
		return val;
	}}

	public static string Decode(string encodingString) {{
		string marker = ""#@~^"";
		int stringIndex = 0;
		int scriptIndex = -1;
		int unEncodingIndex = 0;
		string strChar = """";
		string getCodeString = """";
		int unEncodinglength = 0;
		int state = STATE_COPY_INPUT;
		string unEncodingString = """";

		try {{
			while (state != 0) {{
				switch (state) {{
					case STATE_COPY_INPUT:

						scriptIndex = encodingString.IndexOf(marker, stringIndex);
						if (scriptIndex != -1) {{
							unEncodingString += encodingString.Substring(stringIndex, scriptIndex);
							scriptIndex += marker.Length;
							state = STATE_READLEN;
						}} else {{
							stringIndex = stringIndex == 0 ? 0 : stringIndex;
							unEncodingString += encodingString.Substring(stringIndex);
							state = 0;
						}}
						break;
					case STATE_READLEN:

						getCodeString = encodingString.Substring(scriptIndex, 6);
						unEncodinglength = DecodeBase64(getCodeString);
						scriptIndex += 8;
						state = STATE_DECODE;
						break;
					case STATE_DECODE:

						if (unEncodinglength == 0) {{
							stringIndex = scriptIndex + ""DQgAAA==^#~@"".Length;
							unEncodingIndex = 0;
							state = STATE_COPY_INPUT;
						}} else {{
							strChar = encodingString.Substring(scriptIndex, 1);
							if (strChar == ""@"") state = STATE_UNESCAPE;
							else {{
								int b = (int)strChar.ToCharArray()[0];
								if (b < 0xFF) {{
									unEncodingString += (char)_transformed[_pickEncoding[unEncodingIndex % 64]][b];
									unEncodingIndex++;
								}} else {{
									unEncodingString += strChar;
								}}
								scriptIndex++;
								unEncodinglength--;
							}}
						}}
						break;
					case STATE_UNESCAPE:

						unEncodingString += UnEscape(encodingString.Substring(++scriptIndex, 1));
						scriptIndex++;
						unEncodinglength -= 2;
						unEncodingIndex++;
						state = STATE_DECODE;
						break;
				}}
			}}
		}} catch {{ }}
		string Pattern;
		Pattern = ""(JScript|VBscript).encode"";
		unEncodingString = Regex.Replace(unEncodingString, Pattern, """", RegexOptions.IgnoreCase);
		return unEncodingString;
	}}
}}";
			#endregion
			public static readonly string Common_Lib_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using System.Windows.Forms;
using Microsoft.VisualBasic;

public delegate void AnonymousHandler();

/// <summary>
/// 常用函数库
/// </summary>
public class Lib {{

	/// <summary>
	/// 当前程序类型是否为 Web Application
	/// </summary>
	public static bool IsWeb {{ get {{ return AppDomain.CurrentDomain.SetupInformation.DynamicBase == null ? false : true; }} }}

	public static string HtmlEncode(object input) {{ return HttpUtility.HtmlEncode(string.Concat(input)); }}
	public static string HtmlDecode(object input) {{ return HttpUtility.HtmlDecode(string.Concat(input)); }}
	public static string UrlEncode(object input) {{ return HttpUtility.UrlEncode(string.Concat(input)); }}
	public static string UrlDecode(object input) {{ return HttpUtility.UrlDecode(string.Concat(input)); }}

	public static string JSDecode(string input) {{ return JSDecoder.Decode(input); }}

	public static void Alert(object text) {{ Alert(text, MessageBoxIcon.Information); }}
	public static void Alert(object text, MessageBoxIcon icon) {{ MessageBox.Show(string.Concat(text), Application.ProductName, MessageBoxButtons.OK, icon); }}

	#region GetWebControl 递归获取第一个 ID 的 WEB控件
	/// <summary>
	/// 从 HttpContext.Current.Handler 中递归获取第一个 ID 的 WEB控件
	/// </summary>
	/// <param name=""id"">WEB控件的 ID</param>
	/// <returns>WEB控件</returns>
	public static System.Web.UI.Control GetWebControl(string id) {{
		if (HttpContext.Current == null) return null;
		System.Web.UI.Page page = HttpContext.Current.Handler as System.Web.UI.Page;
		if (page == null || string.IsNullOrEmpty(id)) return null;
		System.Web.UI.Control val = page.FindControl(id);
		return val == null ? GetWebControl(id, page.Controls) : val;
	}}
	static System.Web.UI.Control GetWebControl(string id, System.Web.UI.ControlCollection controls) {{
		System.Web.UI.Control val = null;
		foreach (System.Web.UI.Control control in controls) {{
			val = control.FindControl(id);
			if (val != null) return val;
			if (control.Controls.Count > 0) {{
				val = GetWebControl(id, control.Controls);
				if (val != null) return val;
			}}
		}}
		return val;
	}}
	#endregion

	#region 简繁体转换
	readonly static char[] CHS = ""杯翱鳖产叠皋钩硅捍后伙秸凌麼宁钎三尸抬绦为伪锨蝎勋颜彝灶扎钟众"".ToCharArray();
	readonly static char[] CHT = ""盃翺鼈産疊臯鈎矽捍後夥稭淩麼甯釺叁屍擡縧爲僞鍁蠍勳顔彜竈紮鍾衆"".ToCharArray();

	/// <summary>
	/// 转换成繁体并返回
	/// </summary>
	/// <param name=""input"">提供一个输入</param>
	/// <returns>繁体</returns>
	public static string StrConvTraditional(object input) {{
		if (input == null) return string.Empty;
		string text = string.Concat(input);
		for (int a = 0; a < Math.Min(CHS.Length, CHT.Length); a++) text = text.Replace(CHS[a], CHT[a]);
		return Strings.StrConv(text, VbStrConv.TraditionalChinese, 86);
	}}
	/// <summary>
	/// 转换成简体并返回
	/// </summary>
	/// <param name=""input"">提供一个输入</param>
	/// <returns>简体</returns>
	public static string StrConvSimplified(object input) {{
		if (input == null) return string.Empty;
		string text = string.Concat(input);
		for (int a = 0; a < Math.Min(CHS.Length, CHT.Length); a++) text = text.Replace(CHT[a], CHS[a]);
		return Strings.StrConv(text, VbStrConv.SimplifiedChinese, 86);
	}}
	#endregion

	#region 弥补 String.PadRight 和 String.PadLeft 对中文的 Bug
	public static string PadRight(object text, int length) {{ return PadRightLeft(text, length, ' ', true); }}
	public static string PadRight(object text, char paddingChar, int length) {{ return PadRightLeft(text, length, paddingChar, true); }}
	public static string PadLeft(object text, int length) {{ return PadRightLeft(text, length, ' ', false); }}
	public static string PadLeft(object text, char paddingChar, int length) {{ return PadRightLeft(text, length, paddingChar, false); }}
	static string PadRightLeft(object text, int length, char paddingChar, bool isRight) {{
		string str = string.Concat(text);
		int len2 = Encoding.Default.GetBytes(str).Length;
		for (int a = 0; a < length - len2; a++) if (isRight) str += paddingChar; else str = paddingChar + str;
		return str;
	}}
	#endregion

	#region 序列化/反序列化(二进制)
	public static byte[] Serialize(object obj) {{
		IFormatter formatter = new BinaryFormatter();
		MemoryStream ms = new MemoryStream();
		formatter.Serialize(ms, obj);
		byte[] data = ms.ToArray();
		ms.Close();
		return data;
	}}
	public static object Deserialize(byte[] stream) {{
		IFormatter formatter = new BinaryFormatter();
		MemoryStream ms = new MemoryStream(stream);
		object obj = formatter.Deserialize(ms);
		ms.Close();
		return obj;
	}}
	#endregion

	/// <summary>
	/// 重试某过程 maxError 次，直到成功或失败
	/// </summary>
	/// <param name=""handler"">托管函数</param>
	/// <param name=""maxError"">允许失败的次数</param>
	/// <returns>如果执行成功，则返回 null, 否则返回该错误对象</returns>
	public static Exception Trys(AnonymousHandler handler, int maxError) {{
		if (handler != null) {{
			Exception ex = null;
			for (int a = 0; a < maxError; a++) {{
				try {{
					handler();
					return null;
				}} catch (Exception e) {{
					ex = e;
				}}
			}}
			return ex;
		}}
		return null;
	}}

	/// <summary>
	/// 延迟 milliSecond 毫秒后执行 handler，与 javascript 里的 setTimeout 相似
	/// </summary>
	/// <param name=""handler"">托管函数</param>
	/// <param name=""milliSecond"">毫秒</param>
	public static void SetTimeout(AnonymousHandler handler, int milliSecond) {{
		System.Timers.Timer timer = new System.Timers.Timer(milliSecond);
		timer.AutoReset = false;
		timer.Elapsed += delegate(object sender, System.Timers.ElapsedEventArgs e) {{
			try {{
				handler();
			}} catch {{ }}

			timer.Stop();
			timer.Close();
			timer.Dispose();
		}};
		timer.Start();
	}}

	/// <summary>
	/// 将服务器端数据转换成安全的JS字符串
	/// </summary>
	/// <param name=""input"">一个服务器端变量或字符串</param>
	/// <returns>安全的JS字符串</returns>
	public static string GetJsString(object input) {{
		if (input == null) return string.Empty;
		return string.Concat(input).Replace(""\\"", ""\\\\"").Replace(""\r"", ""\\r"").Replace(""\n"", ""\\n"").Replace(""'"", ""\\'"");
	}}

	static Dictionary<string, Type> _InvokeMethod_cache_type = new Dictionary<string, Type>();
	static object _InvokeMethod_cache_type_lock = new object();
	public static object InvokeMethod(string typeName, string method, params object[] parms) {{
		Type type;
		if (!_InvokeMethod_cache_type.TryGetValue(typeName, out type)) {{
			type = System.Type.GetType(typeName);
			lock (_InvokeMethod_cache_type_lock) {{
				if (!_InvokeMethod_cache_type.TryGetValue(typeName, out type))
					_InvokeMethod_cache_type.Add(typeName, type);
			}}
		}}
		return type.InvokeMember(method, BindingFlags.InvokeMethod, null, null, parms);
	}}

	/// <summary>
	/// 获取对象属性
	/// </summary>
	/// <param name=""obj"">对象</param>
	/// <param name=""property"">属性，此属性可为多级属性，如：newsInfo.newsClassInfo...</param>
	/// <returns>对象的（子）属性</returns>
	public static object EvaluateValue(object obj, string property) {{
		if (obj == null) return null;
		string prop = property;
		object ret = string.Empty;
		if (property.Contains(""."")) {{
			prop = property.Substring(0, property.IndexOf("".""));
			PropertyInfo propa = EvaluateValue_GetProperty(obj, prop);
			if (propa != null) {{
				object obja = propa.GetValue(obj, null);
				ret = EvaluateValue(obja, property.Substring(property.IndexOf(""."") + 1));
			}}
		}} else {{
			PropertyInfo propa = EvaluateValue_GetProperty(obj, prop);
			if (propa != null) {{
				ret = propa.GetValue(obj, null);
			}}
		}}
		return ret;
	}}
	private static PropertyInfo EvaluateValue_GetProperty(object obj, string property) {{
		if (obj == null) return null;
		Type type = obj.GetType();
		PropertyInfo ret = type.GetProperty(property);
		if (ret == null) {{
			PropertyInfo[] pis = type.GetProperties();
			foreach (PropertyInfo pi in pis) {{
				if (string.Compare(pi.Name, property, true) == 0) {{
					ret = pi;
					break;
				}}
			}}
		}}
		return ret;
	}}

	/// <summary>
	/// (安全转换)对象/值转换类型
	/// </summary>
	/// <typeparam name=""T"">转换后的类型</typeparam>
	/// <param name=""input"">转换的对象</param>
	/// <returns>转换后的对象/值</returns>
	public static T ConvertTo<T>(object input) {{
		return ConvertTo<T>(input, default(T));
	}}
	public static T ConvertTo<T>(object input, T defaultValue) {{
		if (input == null) return defaultValue;
		object obj = null;

		if (defaultValue is System.Byte ||
			defaultValue is System.Decimal ||

			defaultValue is System.Int16 ||
			defaultValue is System.Int32 ||
			defaultValue is System.Int64 ||
			defaultValue is System.SByte ||
			defaultValue is System.Single ||

			defaultValue is System.UInt16 ||
			defaultValue is System.UInt32 ||
			defaultValue is System.UInt64) {{
			decimal trydec = 0;
			if (decimal.TryParse(string.Concat(input), out trydec)) obj = trydec;
		}} else {{
			if (defaultValue is System.DateTime) {{
				DateTime trydt = DateTime.Now;
				if (DateTime.TryParse(string.Concat(input), out trydt)) obj = trydt;
			}} else {{
				if (defaultValue is System.Boolean) {{
					bool trybool = false;
					if (bool.TryParse(string.Concat(input), out trybool)) obj = trybool;
				}} else {{
					if (defaultValue is System.Double) {{
						double trydb = 0;
						if (double.TryParse(string.Concat(input), out trydb)) obj = trydb;
					}} else {{
						obj = input;
					}}
				}}
			}}
		}}

		try {{
			if (obj != null) return (T)Convert.ChangeType(obj, typeof(T));
		}} catch {{ }}

		return defaultValue;
	}}
}}";
			#endregion
			public static readonly string Common_Logger_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = ""c:\\log4net.config"", Watch = true)]
[Serializable]
public class Logger {{

	protected readonly string _name;
	private log4net.ILog _log;
	protected log4net.ILog Log {{
		get {{
			if (_log == null) _log = log4net.LogManager.GetLogger(_name);
			return _log;
		}}
	}}

	public Logger() {{ }}
	public Logger(string name) {{ this._name = name; }}

	public static readonly Logger web = new Logger(""web"");
	public static readonly Logger web_admin = new Logger(""web_admin"");
	public static readonly Logger dbutility_sqlhelper = new Logger(""dbutility_sqlhelper"");

	private static ISocketLogger _socketLogger;

	public static void SetSocketLogger(ISocketLogger socketLogger) {{
		_socketLogger = socketLogger;
	}}

	private void Send(object args) {{
		if (_socketLogger != null) {{
			_socketLogger.Send(args);
		}}
	}}

	#region log method
	public virtual void Debug(object message, Exception exception) {{
		Log.Debug(message, exception);
		if (Log.IsDebugEnabled) {{
			Send(new object[] {{ _name, ""Debug"", message, exception }});
		}}
	}}
	public void Debug(object message) {{
		Debug(message, null);
	}}
	public void DebugFormat(string format, params object[] args) {{
		Debug(string.Format(format, args), null);
	}}

	public virtual void Error(object message, Exception exception) {{
		Log.Error(message, exception);
		if (Log.IsErrorEnabled) {{
			Send(new object[] {{ _name, ""Error"", message, exception }});
		}}
	}}
	public void Error(object message) {{
		Error(message, null);
	}}
	public void ErrorFormat(string format, params object[] args) {{
		Error(string.Format(format, args), null);
	}}

	public virtual void Fatal(object message, Exception exception) {{
		Log.Fatal(message, exception);
		if (Log.IsFatalEnabled) {{
			Send(new object[] {{ _name, ""Fatal"", message, exception }});
		}}
	}}
	public void Fatal(object message) {{
		Fatal(message, null);
	}}
	public void FatalFormat(string format, params object[] args) {{
		Fatal(string.Format(format, args), null);
	}}

	public virtual void Info(object message, Exception exception) {{
		Log.Info(message, exception);
		if (Log.IsInfoEnabled) {{
			Send(new object[] {{ _name, ""Info"", message, exception }});
		}}
	}}
	public void Info(object message) {{
		Info(message, null);
	}}
	public void InfoFormat(string format, params object[] args) {{
		Info(string.Format(format, args), null);
	}}

	public virtual void Warn(object message, Exception exception) {{
		Log.Warn(message, exception);
		if (Log.IsWarnEnabled) {{
			Send(new object[] {{ _name, ""Warn"", message, exception }});
		}}
	}}
	public void Warn(object message) {{
		Warn(message, null);
	}}
	public void WarnFormat(string format, params object[] args) {{
		Warn(string.Format(format, args), null);
	}}

	public bool IsDebugEnabled {{
		get {{ return Log.IsDebugEnabled; }}
	}}
	public bool IsErrorEnabled {{
		get {{ return Log.IsErrorEnabled; }}
	}}
	public bool IsFatalEnabled {{
		get {{ return Log.IsFatalEnabled; }}
	}}
	public bool IsInfoEnabled {{
		get {{ return Log.IsInfoEnabled; }}
	}}
	public bool IsWarnEnabled {{
		get {{ return Log.IsWarnEnabled; }}
	}}
	#endregion
}}

public interface ISocketLogger : IDisposable {{

	void Send(object args);
}}";
			#endregion
			public static readonly string Common_Http_BaseHttpProxy_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections.Generic;
using System.Text;

public abstract class BaseHttpProxy {{

	public abstract HttpProxyResponse Send(HttpProxyRequest request);
}}

[Serializable]
public class HttpProxyRequest {{
	private string _method;

	public string Method {{
		get {{ return _method; }}
		set {{ _method = value; }}
	}}
	private string _action;

	public string Action {{
		get {{ return _action; }}
		set {{ _action = value; }}
	}}
	private string _charset;

	public string Charset {{
		get {{ return _charset; }}
		set {{ _charset = value; }}
	}}
	private string _head;

	public string Head {{
		get {{ return _head; }}
		set {{ _head = value; }}
	}}
	private string _data;

	public string Data {{
		get {{ return _data; }}
		set {{ _data = value; }}
	}}
	private string _connection;

	public string Connection {{
		get {{ return _connection; }}
		set {{ _connection = value; }}
	}}
	private int _timeout;

	public int Timeout {{
		get {{ return _timeout; }}
		set {{ _timeout = value; }}
	}}
	private int _maximumAutomaticRedirections;

	public int MaximumAutomaticRedirections {{
		get {{ return _maximumAutomaticRedirections; }}
		set {{ _maximumAutomaticRedirections = value; }}
	}}
}}

[Serializable]
public class HttpProxyResponse {{
	private string _requestMethod;

	public string RequestMethod {{
		get {{ return _requestMethod; }}
		set {{ _requestMethod = value; }}
	}}
	private string _requestAction;

	public string RequestAction {{
		get {{ return _requestAction; }}
		set {{ _requestAction = value; }}
	}}
	private string _requestHead;

	public string RequestHead {{
		get {{ return _requestHead; }}
		set {{ _requestHead = value; }}
	}}
	private byte[] _responseHead;

	public byte[] ResponseHead {{
		get {{ return _responseHead; }}
		set {{ _responseHead = value; }}
	}}
	private byte[] _response;

	public byte[] Response {{
		get {{ return _response; }}
		set {{ _response = value; }}
	}}
}}";
			#endregion
			public static readonly string Common_Http_Deflate_cs =
			#region 内容太长已被收起
 @"using System;
using System.IO;
using System.IO.Compression;
using System.Text;

public partial class Http {{

	public static class Deflate {{

		public static byte[] Decompress(Stream stream) {{
			try {{
				stream.Position = 0;
				using (MemoryStream ms = new MemoryStream()) {{
					using (DeflateStream def = new DeflateStream(stream, CompressionMode.Decompress)) {{
						byte[] data = new byte[1024];
						int size = 0;

						while ((size = def.Read(data, 0, data.Length)) > 0) {{
							ms.Write(data, 0, size);
						}}
					}}
					return ms.ToArray();
				}}
			}} catch {{ return (stream as MemoryStream).ToArray(); }};
		}}
		public static byte[] Decompress(byte[] bt) {{
			return Decompress(new MemoryStream(bt));
		}}

		public static byte[] Compress(string text) {{
			return Compress(Encoding.UTF8.GetBytes(text));
		}}
		public static byte[] Compress(byte[] bt) {{
			return Compress(bt, 0, bt.Length);
		}}
		public static byte[] Compress(byte[] bt, int startIndex, int length) {{
			using (MemoryStream ms = new MemoryStream()) {{
				using (DeflateStream def = new DeflateStream(ms, CompressionMode.Compress)) {{
					def.Write(bt, startIndex, length);
				}}
				return ms.ToArray();
			}}
		}}
	}}
}}";
			#endregion
			public static readonly string Common_Http_GZip_cs =
			#region 内容太长已被收起
 @"using System;
using System.IO;
using System.IO.Compression;
using System.Text;

public partial class Http {{

	public static class GZip {{

		public static byte[] Decompress(Stream stream) {{
			try {{
				stream.Position = 0;
				using (MemoryStream ms = new MemoryStream()) {{
					using (GZipStream gzip = new GZipStream(stream, CompressionMode.Decompress)) {{
						byte[] data = new byte[1024];
						int size = 0;

						while ((size = gzip.Read(data, 0, data.Length)) > 0) {{
							ms.Write(data, 0, size);
						}}
					}}
					return ms.ToArray();
				}}
			}} catch {{ return (stream as MemoryStream).ToArray(); }};
		}}
		public static byte[] Decompress(byte[] bt) {{
			return Decompress(new MemoryStream(bt));
		}}

		public static byte[] Compress(string text) {{
			return Compress(Encoding.UTF8.GetBytes(text));
		}}
		public static byte[] Compress(byte[] bt) {{
			return Compress(bt, 0, bt.Length);
		}}
		public static byte[] Compress(byte[] bt, int startIndex, int length) {{
			using (MemoryStream ms = new MemoryStream()) {{
				using (GZipStream gzip = new GZipStream(ms, CompressionMode.Compress)) {{
					gzip.Write(bt, startIndex, length);
				}}
				return ms.ToArray();
			}}
		}}
	}}
}}";
			#endregion
			public static readonly string Common_Http_HttpAccessException_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections.Generic;
using System.Text;

public class HttpAccessException : Exception {{

	public HttpAccessException() : base() {{ }}
	public HttpAccessException(string message) : base(message) {{ }}
	public HttpAccessException(string message, Exception innerException) : base(message, innerException) {{ }}
}}";
			#endregion
			public static readonly string Common_Http_HttpCookie2_cs =
			#region 内容太长已被收起
 @"using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

public class HttpCookie2 {{

	private string _name;
	private string _value;

	public HttpCookie2(string name, string value) {{
		this._name = name;
		this._value = value == null ? string.Empty : value;
	}}

	public string Name {{
		get {{ return _name; }}
	}}
	public string Value {{
		get {{ return this._value; }}
	}}

	internal void UpdateCookie(CookieCollection cc) {{
		if (_name.IndexOf('=') == -1) cc.Add(new Cookie(_name, _value));
		else {{
			string[] ns = new string[] {{
					_name.Substring(0, _name.IndexOf('=')),
					_name.Substring(_name.IndexOf('=') + 1)}};
			Cookie c = cc[ns[0]];
			if (c == null) cc.Add(new Cookie(ns[0], ns[1] + ""="" + _value));
			else {{
				if (Regex.IsMatch(c.Value, ns[1] + @""=[^,;&]*"", RegexOptions.IgnoreCase))
					c.Value = Regex.Replace(c.Value, ns[1] + @""=[^,;&]*"", ns[1] + @""="" + _value);
				else
					c.Value += ""&"" + ns[1] + @""="" + _value;
			}}
		}}
	}}
}}";
			#endregion
			public static readonly string Common_Http_HttpStream_cs =
			#region 内容太长已被收起
 @"using System;
using System.IO;
using System.Net;
using System.Text;

public class HttpStream : MemoryStream {{

	private string _encode;
	private HttpWebResponse _response;

	public HttpWebResponse Response {{
		get {{ return _response; }}
	}}

	public HttpStream(string encode, HttpWebResponse response) {{
		_encode = encode;
		_response = response;
	}}

	public bool Save(string filename) {{
		using (FileStream fs = new FileStream(filename, FileMode.Create)) {{
			try {{
				base.Position = 0;

				if (_response.ContentEncoding.ToLower() == ""gzip"") {{
					byte[] buf = Http.GZip.Decompress(this);
					fs.Write(buf, 0, buf.Length);
				}} else if (_response.ContentEncoding.ToLower() == ""deflate"") {{
					byte[] buf = Http.Deflate.Decompress(this);
					fs.Write(buf, 0, buf.Length);
				}} else {{
					byte[] buf = new byte[1024];
					int len = 0;
					while ((len = base.Read(buf, 0, 1024)) > 0)
						fs.Write(buf, 0, len);
				}}
			}} catch {{
				return false;
			}} finally {{
				fs.Close();
			}}
		}}
		return true;
	}}

	private string _html;

	public string Html {{
		get {{
			if (_response == null) _html = string.Empty;
			if (_html == null) {{
				if (_response.ContentEncoding.ToLower() == ""gzip"") {{
					_html = Lib.HtmlDecode(Encoding.GetEncoding(_encode).GetString(Http.GZip.Decompress(this)));
				}} else if (_response.ContentEncoding.ToLower() == ""deflate"") {{
					_html = Lib.HtmlDecode(Encoding.GetEncoding(_encode).GetString(Http.Deflate.Decompress(this)));
				}} else {{
					_html = Lib.HtmlDecode(Encoding.GetEncoding(_encode).GetString(base.ToArray()));
				}}
			}}
			return _html;
		}}
	}}
}}";
			#endregion
			public static readonly string Common_Http_HttpRequest_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

public class HttpRequest : IDisposable {{
	private static CookieContainer _cookies = new CookieContainer();
	private TcpClient _client;
	private Stream _stream;
	private Uri _remote;
	private object _close_lock = new object();

	private string _method = ""GET"";
	private string _action;
	private string _charset = ""utf-8"";
	private string _head;
	private string _data;
	private string _proxyConnection = ""default"";

	private int _autoSendError;
	private int _timeout = 20000;
	private int _maximumAutomaticRedirections = 500;
	private CookieContainer _cookieContainer = _cookies;
	internal object _cookieContainer_lock = new object();
	private BaseHttpProxy _proxy;
	private HttpResponse _response;
	private NameValueCollection _headers = new NameValueCollection();

	public HttpRequest() {{
		_headers.Add(""Connection"", ""Keep-Alive"");
		_headers.Add(""Accept"", ""*/*"");
		_headers.Add(""User-Agent"", ""Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2)"");
		_headers.Add(""Accept-Language"", ""en-us"");
		_headers.Add(""Accept-Encoding"", ""gzip, deflate"");
	}}

	public void Close() {{
		if (_client != null) {{
			lock (_close_lock) {{
				if (_client != null) {{
					if (_stream != null) {{
						try {{
							_stream.Close();
							_stream.Dispose();
						}} finally {{
							_stream = null;
						}}
					}}
					_client.Close();
					_client = null;
				}}
			}}
		}}
	}}

	public void Send() {{
		Send(string.Empty);
	}}
	public void Send(NameValueCollection vars) {{
		Encoding encode = Encoding.GetEncoding(_charset);
		List<string> sb = new List<string>();
		foreach (string key in vars.AllKeys) {{
			sb.Add(key + ""="" + HttpUtility.UrlEncode(vars[key], encode));
		}}
		Send(string.Join(""&"", sb.ToArray()));
	}}
	public virtual void Send(string data) {{
		Send(data, 0);
	}}

	private void Send(string data, int redirections) {{
		_data = data;
		Encoding encode = Encoding.GetEncoding(_charset);
        _headers.Remove(""Content-Length"");
		if (!string.IsNullOrEmpty(data) && string.Compare(_method, ""post"", true) == 0) {{
			_headers[""Content-Length""] = string.Concat(Encoding.GetEncoding(_charset).GetBytes(data).Length);
			if (string.IsNullOrEmpty(_headers[""Content-Type""])) {{
				_headers[""Content-Type""] = ""application/x-www-form-urlencoded; charset="" + _charset;
			}} else if (_headers[""Content-Type""].IndexOf(""multipart/form-data"") == -1) {{
				if (_headers[""Content-Type""].IndexOf(""application/x-www-form-urlencoded"") == -1) {{
					_headers[""Content-Type""] += ""; application/x-www-form-urlencoded"";
				}}
                if (_headers[""Content-Type""].IndexOf(""charset="") == -1) {{
                    _headers[""Content-Type""] += ""; charset="" + _charset;
                }}
			}}
			data += ""\r\n\r\n"";
		}}
		Uri uri = new Uri(_action);
		if (_cookieContainer != null) {{
			CookieContainer cc = new CookieContainer();
			if (_headers[""Cookie""] != null) {{
				cc.SetCookies(uri, _headers[""Cookie""]);
			}}
			Uri uri2 = new Uri(uri.AbsoluteUri.Insert(uri.Scheme.Length + 3, ""httprequest.""));
			CookieCollection cookies = _cookieContainer.GetCookies(uri);
			foreach (Cookie cookie in cookies) {{
				cc.SetCookies(uri, string.Concat(cookie));
			}}
			cookies = _cookieContainer.GetCookies(uri2);
			foreach (Cookie cookie in cookies) {{
				cc.SetCookies(uri, string.Concat(cookie));
			}}
			_headers[""Cookie""] = cc.GetCookieHeader(uri);
			if (string.IsNullOrEmpty(_headers[""Cookie""])) {{
				_headers.Remove(""Cookie"");
			}}
		}}
		_headers[""Host""] = uri.Authority;
		string http = _method + "" "" + uri.PathAndQuery + "" HTTP/1.1\r\n"";
		foreach (string head in _headers) {{
			http += head + "": "" + _headers[head] + ""\r\n"";
		}}
		http += ""\r\n"" + data;
		_head = http;

		if (_proxy != null) {{
			HttpProxyRequest pr = new HttpProxyRequest();
			pr.Method = _method;
			pr.Action = _action;
			pr.Charset = _charset;
			pr.Head = _head;
			pr.Data = data;
			pr.Connection = _proxyConnection;
			pr.Timeout = _timeout;
			pr.MaximumAutomaticRedirections = _maximumAutomaticRedirections;
			HttpProxyResponse response = _proxy.Send(pr);
			Action = response.RequestAction;
			_method = response.RequestMethod;
			_headers = HttpRequest.ParseHttpRequestHeader(response.RequestHead);
			_response = new HttpResponse(this, response.ResponseHead);
			_response.SetStream(response.Response);
		}} else {{
			byte[] request = encode.GetBytes(http);

			if (_client == null || _remote == null || string.Compare(_remote.Authority, uri.Authority, true) != 0) {{
				_remote = uri;
				this.Close();
				_client = new TcpClient(uri.Host, uri.Port);
			}}
			try {{
				_stream = getStream(uri);
				_stream.Write(request, 0, request.Length);
			}} catch {{
				this.Close();
				_client = new TcpClient(uri.Host, uri.Port);
				_stream = getStream(uri);
				_stream.Write(request, 0, request.Length);
			}}
			receive(_stream, redirections, uri, encode);
		}}
	}}

	protected void receive(Stream stream, int redirections, Uri uri, Encoding encode) {{
		stream.ReadTimeout = _timeout;
		_response = null;

		byte[] bytes = new Byte[1024];
		int overs = bytes.Length;

		MemoryStream headStream = new MemoryStream();
		MemoryStream bodyStream = new MemoryStream();
		MemoryStream chunkStream = new MemoryStream();
		Exception exception = null;

		while (overs > 0) {{
			int idx = -1;
			try {{
				overs = stream.Read(bytes, 0, bytes.Length);
				if (headStream.Length == 0 && overs == 0) {{
					throw new Exception(""连接已关闭"");
				}}
			}} catch (Exception e) {{
				if (headStream.Length == 0 && _autoSendError++ < 5) {{
					headStream.Close();
					bodyStream.Close();
					chunkStream.Close();

					_remote = null;
					Send(_data, 0);
					return;
				}}
				exception = e;
				break;
			}}
			if (_response == null) {{
				headStream.Write(bytes, 0, overs);

				bytes = headStream.ToArray();
				idx = BaseSocket.findBytes(bytes, new byte[] {{ 13, 10, 13, 10 }}, 0);
				if (idx != -1) {{
					headStream.Close();
					headStream = new MemoryStream();
					headStream.Write(bytes, 0, idx);
					chunkStream.Write(bytes, idx + 4, bytes.Length - idx - 4);
					_response = new HttpResponse(this, headStream.ToArray());
					_response.Received += bytes.Length - idx - 4;
					if (_response.StatusCode == HttpStatusCode.Redirect ||
						_response.StatusCode == HttpStatusCode.Moved) {{
						if (string.Compare(_method, ""post"", true) == 0) {{
							_headers.Remove(""Content-Length"");
							if (!string.IsNullOrEmpty(_headers[""Content-Type""])) {{
								_headers[""Content-Type""] = _headers[""Content-Type""]
									.Replace(""; application/x-www-form-urlencoded"", string.Empty)
									.Replace(""application/x-www-form-urlencoded"", string.Empty);
								if (string.IsNullOrEmpty(_headers[""Content-Type""])) {{
									_headers.Remove(""Content-Type"");
								}}
							}}
						}}

						headStream.Close();
						bodyStream.Close();
						chunkStream.Close();
						this.closeTcp();

						if (++redirections > _maximumAutomaticRedirections) {{
							throw new WebException(""重定向超过 "" + _maximumAutomaticRedirections + "" 次。"");
						}}
						string url = _response.Headers[""Location""];
						if (!string.IsNullOrEmpty(url)) {{
							if (Uri.IsWellFormedUriString(url, UriKind.Relative)) {{
								if (!url.StartsWith(""/"")) {{
									int eidx = Address.AbsolutePath.LastIndexOf('/');
									url = Address.AbsolutePath.Remove(eidx) + ""/"" + url;
								}}
								url = Address.Scheme + ""://"" + Address.Authority + url;
								url = new Uri(url).AbsoluteUri;
							}}
						}}
						Action = url;
						Method = ""get"";
						Headers.Remove(""Cookie"");
						Send(null, redirections);
						return;
					}} else if (_response.StatusCode == HttpStatusCode.Continue) {{
						_response = null;
						headStream = new MemoryStream();
					}}
				}}
			}} else {{
				_response.Received += overs;
				chunkStream.Write(bytes, 0, overs);
			}}
			if (_response != null) {{
				if (string.Compare(_response.TransferEncoding, ""chunked"") == 0) {{
					byte[] chunks = chunkStream.ToArray();
					int lidx = 0;
					int chunkSize = -1;
					bool isContinue = false;
					bool isBreak = false;
					do {{
						isContinue = false;
						idx = BaseSocket.findBytes(chunks, new byte[] {{ 13, 10 }}, lidx);
						if (idx != -1) {{
							string[] chu = encode.GetString(chunks, lidx, idx - lidx).Split(new char[] {{ ';' }}, 1);
							if (int.TryParse(chu[0], System.Globalization.NumberStyles.HexNumber, null, out chunkSize)) {{
								int esize = chunks.Length - idx - 2;
								if (esize >= chunkSize + 2) {{
									chunkStream.Close();
									chunkStream = new MemoryStream();
									chunkStream.Write(chunks, idx + 2 + chunkSize + 2, esize - chunkSize - 2);
									bodyStream.Write(chunks, idx + 2, chunkSize);
									lidx = idx + 2 + chunkSize + 2;

									if (chunkStream.Length == 5) {{
										idx = BaseSocket.findBytes(chunks, new byte[] {{ 48, 13, 10, 13, 10 }}, 0);
										if (idx != 0) {{
											isBreak = true;
											break;
										}}
									}}
									isContinue = true;
								}}
							}} else {{
								chunkSize = -1;
							}}
						}}
					}} while (isContinue);
					if (isBreak) {{
						break;
					}}
				}} else if (_response.ContentLength >= 0) {{
					if (_response.ContentLength <= chunkStream.Length) {{
						break;
					}}
				}}
			}}
		}}
		_autoSendError = 0;
		if (_response == null) {{
			headStream.Close();
			bodyStream.Close();
			chunkStream.Close();
			this.closeTcp();

			List<string> sb = new List<string>();
			sb.Add(_method.ToUpper() + "" "" + new Uri(_action).PathAndQuery + "" HTTP/1.1"");
			foreach (string header in _headers) {{
				sb.Add(header + "": "" + _headers[header]);
			}}
			if (exception == null) {{
				throw new WebException(""读取失败。"" + string.Join(""\r\n"", sb.ToArray()));
			}} else {{
				throw new WebException(exception.Message + ""\r\n"" + string.Join(""\r\n"", sb.ToArray()), exception);
			}}
		}}
		if (string.Compare(_response.TransferEncoding, ""chunked"") == 0) {{
			_response.SetStream(bodyStream.ToArray());
		}} else if (_response.ContentLength >= 0) {{
			_response.SetStream(chunkStream.ToArray());
		}} else {{
			_response.SetStream(chunkStream.ToArray());
		}}
		headStream.Close();
		bodyStream.Close();
		chunkStream.Close();
		this.closeTcp();
	}}

	protected bool closeTcp() {{
		if (_client != null && _response != null && (
			string.Compare(_headers[""Connection""], ""keep-alive"", true) != 0 ||
			string.Compare(_response.Headers[""Connection""], ""keep-alive"", true) != 0
			)) {{
			this.Close();
			return true;
		}}
		return false;
	}}

	protected Stream getStream(Uri uri) {{
		if (string.Compare(uri.Scheme, ""https"", false) == 0) {{
			SslStream ssl = new SslStream(_client.GetStream(), false, delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {{
				return sslPolicyErrors == SslPolicyErrors.None;
			}}, null);
			ssl.AuthenticateAsClient(uri.Host);
			return ssl;
		}} else {{
			return _client.GetStream();
		}}
	}}

	public static NameValueCollection ParseHttpRequestHeader(string head) {{
		NameValueCollection headers = new NameValueCollection();
		if (!string.IsNullOrEmpty(head)) {{
			string[] hs = head.Split(new string[] {{ ""\r\n"" }}, StringSplitOptions.None);
			foreach (string h in hs) {{
				string[] nv = h.Split(new char[] {{ ':' }}, 2);
				if (nv.Length == 2) {{
					string n = nv[0].Trim();
					string v = nv[1].Trim();
					headers.Add(n, v);
				}}
			}}
		}}
		return headers;
	}}

	public string Method {{
		get {{
			return _method;
		}}
		set {{
			_method = value.ToUpper();
		}}
	}}
	public string Action {{
		get {{
			return _action;
		}}
		set {{
			if (string.Compare(_action, value, true) != 0) {{
				if (!Uri.IsWellFormedUriString(value, UriKind.Absolute)) {{
					throw new WebException(""不正确的URI“"" + value + ""”"");
				}}
				Uri uri = new Uri(value);
				_action = uri.AbsoluteUri;
			}}
		}}
	}}
	public Uri Address {{
		get {{
			return new Uri(_action);
		}}
	}}
	public string Charset {{
		get {{ return _charset; }}
		set {{ _charset = value; }}
	}}
	public string Head {{
		get {{ return _head; }}
	}}
	public string ProxyConnection {{
		get {{ return _proxyConnection; }}
		set {{ _proxyConnection = value; }}
	}}
	public int MaximumAutomaticRedirections {{
		get {{ return _maximumAutomaticRedirections; }}
		set {{ _maximumAutomaticRedirections = value; }}
	}}
	public int Timeout {{
		get {{ return _timeout; }}
		set {{ _timeout = value; }}
	}}
	public CookieContainer CookieContainer {{
		get {{ return _cookieContainer; }}
		set {{ _cookieContainer = value; }}
	}}
	public BaseHttpProxy Proxy {{
		get {{ return _proxy; }}
		set {{ _proxy = value; }}
	}}
	public HttpResponse Response {{
		get {{ return _response; }}
	}}
	public NameValueCollection Headers {{
		get {{ return _headers; }}
	}}
	public void SetHeaders(string headers) {{
		_headers = HttpRequest.ParseHttpRequestHeader(headers);
	}}

	public string Accept {{
		get {{ return _headers[""Accept""]; }}
		set {{ _headers[""Accept""] = value; }}
	}}
	public string AcceptLanguage {{
		get {{ return _headers[""Accept-Language""]; }}
		set {{ _headers[""Accept-Language""] = value; }}
	}}
	public string Connection {{
		get {{ return _headers[""Connection""]; }}
		set {{ _headers[""Connection""] = value; }}
	}}
	public int ContentLength {{
		get {{ int tryint; int.TryParse(_headers[""Content-Length""], out tryint); return tryint; }}
		set {{ _headers[""Content-Length""] = string.Concat(value); }}
	}}
	public string ContentType {{
		get {{ return _headers[""Content-Type""]; }}
		set {{ _headers[""Content-Type""] = value; }}
	}}
	public string Expect {{
		get {{ return _headers[""Expect""]; }}
		set {{ _headers[""Expect""] = value; }}
	}}
	public string MediaType {{
		get {{ return _headers[""Media-Type""]; }}
		set {{ _headers[""Media-Type""] = value; }}
	}}
	public string Referer {{
		get {{ return _headers[""Referer""]; }}
		set {{ _headers[""Referer""] = value; }}
	}}
	public string TransferEncoding {{
		get {{ return _headers[""Transfer-Encoding""]; }}
		set {{ _headers[""Transfer-Encoding""] = value; }}
	}}
	public string UserAgent {{
		get {{ return _headers[""User-Agent""]; }}
		set {{ _headers[""User-Agent""] = value; }}
	}}

	#region IDisposable 成员

	public void Dispose() {{
		try {{
			_headers.Clear();
			this.Close();
		}} catch {{ }}
	}}

	#endregion
}}";
			#endregion
			public static readonly string Common_Http_HttpResponse_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

public partial class HttpResponse {{
	private string _action;
	private string _method;
	private string _charset;

	private string _head;
	private string _contentEncoding = string.Empty;
	private int _contentLength = -1;
	private int _received = 0;
	private string _contentType;
	private string _server;
	private NameValueCollection _headers = new NameValueCollection();
	private HttpStatusCode _statusCode;
	private byte[] _stream = new byte[] {{ }};
	private string _html;
	private string _xml;

	public HttpResponse(HttpRequest ie, byte[] headBytes) {{
		_action = ie.Action;
		_method = ie.Method;
		_charset = ie.Charset;

		Encoding encode = Encoding.GetEncoding(_charset);
		string head = encode.GetString(headBytes);
		_head = head = head.Trim();

		int idx = head.IndexOf(' ');
		if (idx != -1) {{
			head = head.Substring(idx + 1);
		}}
		idx = head.IndexOf(' ');
		if (idx != -1) {{
			_statusCode = (HttpStatusCode)int.Parse(head.Remove(idx));
			head = head.Substring(idx + 1);
		}}
		idx = head.IndexOf(""\r\n"");
		if (idx != -1) {{
			head = head.Substring(idx + 2);
		}}
		string[] heads = head.Split(new string[] {{ ""\r\n"" }}, StringSplitOptions.None);
		foreach (string h in heads) {{
			string[] nv = h.Split(new char[] {{ ':' }}, 2);
			if (nv.Length == 2) {{
				string n = nv[0].Trim();
				string v = nv[1].Trim();
                if (v.EndsWith(""; Secure"")) v = v.Replace(""; Secure"", """");
                if (v.EndsWith(""; version=1"")) v = v.Replace(""; version=1"", """");
				switch (n.ToLower()) {{
					case ""set-cookie"":
						if (ie.CookieContainer != null) {{
							Uri addr = Address;
							string[] v2d = Regex.Split(v, @""\bdomain="", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
							if (v2d.Length > 1) {{
								string domain = v2d[1];
								idx = domain.IndexOf("";"");
								if (idx != -1) domain = domain.Remove(idx);
								while (domain.StartsWith(""."")) domain = domain.Substring(1);
								domain = ""http://"" + domain + ""/"";
								if (Uri.IsWellFormedUriString(domain, UriKind.Absolute)) {{
									Uri du = new Uri(domain);
									if (string.Compare(addr.Authority, du.Authority, true) != 0) {{
										addr = du;
									}}
								}}
							}}
							lock (ie._cookieContainer_lock) {{
								ie.CookieContainer.SetCookies(addr, v);
							}}
						}}
						break;
					case ""content-length"":
						if (!int.TryParse(v, out _contentLength)) _contentLength = -1;
						break;
					case ""content-type"":
						idx = v.IndexOf(""charset="", StringComparison.CurrentCultureIgnoreCase);
						if (idx != -1) {{
							string charset = v.Substring(idx + 8);
							idx = charset.IndexOf("";"");
							if (idx != -1) charset = charset.Remove(idx);
							if (string.Compare(_charset, charset, true) != 0) {{
								try {{
									Encoding testEncode = Encoding.GetEncoding(charset);
									_charset = charset;
								}} catch {{
								}}
							}}
						}}
						_contentType = v;
						break;
					case ""server"":
						_server = v;
						break;
					case ""content-encoding"":
						_contentEncoding = v;
						break;
					default:
						_headers.Add(n, v);
						break;
				}}
			}}
		}}
	}}

	public void SetStream(byte[] bodyBytes) {{
		_stream = bodyBytes;
		_contentLength = bodyBytes.Length;
	}}
	public byte[] GetStream() {{
		switch (_contentEncoding.ToLower()) {{
			case ""gzip"":
				return Http.GZip.Decompress(_stream);
			case ""deflate"":
				return Http.Deflate.Decompress(_stream);
			default:
				return _stream;
		}}
	}}

	public void Save(string filename) {{
		using (FileStream fs = new FileStream(filename, FileMode.Create)) {{
			switch (_contentEncoding.ToLower()) {{
				case ""gzip"":
					byte[] gzip = Http.GZip.Decompress(_stream);
					fs.Write(gzip, 0, gzip.Length);
					break;
				case ""deflate"":
					byte[] deflate = Http.Deflate.Decompress(_stream);
					fs.Write(deflate, 0, deflate.Length);
					break;
				default:
					fs.Write(_stream, 0, _stream.Length);
					break;
			}}
		}}
	}}

	public string TranslateUrlToAbsolute(string url) {{
		if (!string.IsNullOrEmpty(url)) {{
			if (Uri.IsWellFormedUriString(url, UriKind.Relative)) {{
				if (!url.StartsWith(""/"")) {{
					int eidx = Address.AbsolutePath.LastIndexOf('/');
					url = Address.AbsolutePath.Remove(eidx) + ""/"" + url;
				}}
				url = Address.Scheme + ""://"" + Address.Authority + url;
				url = new Uri(url).AbsoluteUri;
			}}
		}} else {{
			int eidx = Address.AbsolutePath.LastIndexOf('/');
			url = Address.Scheme + ""://"" + Address.Authority + Address.AbsolutePath.Remove(eidx);
		}}
		return url;
	}}

	public string Html {{
		get {{
			if (_html == null) {{
				_html = Lib.HtmlDecode(Xml);
			}}
			return _html;
		}}
	}}

	public string Xml {{
		get {{
			if (_xml == null) {{
				switch (_contentEncoding.ToLower()) {{
					case ""gzip"":
						_xml = Encoding.GetEncoding(_charset).GetString(Http.GZip.Decompress(_stream));
						break;
					case ""deflate"":
						_xml = Encoding.GetEncoding(_charset).GetString(Http.Deflate.Decompress(_stream));
						break;
					default:
						_xml = Encoding.GetEncoding(_charset).GetString(_stream);
						break;
				}}
			}}
			return _xml;
		}}
	}}

	public string Method {{
		get {{
			return _method;
		}}
	}}
	public string Action {{
		get {{
			return _action;
		}}
	}}
	public Uri Address {{
		get {{
			return new Uri(_action);
		}}
	}}
	public string Charset {{
		get {{ return _charset; }}
	}}
	public string Head {{
		get {{ return _head; }}
	}}
	public string ContentEncoding {{
		get {{ return _contentEncoding; }}
	}}
	public int ContentLength {{
		get {{ return _contentLength; }}
	}}
	public int Received {{
		get {{ return _received; }}
		internal set {{ _received = value; }}
	}}
	public string ContentType {{
		get {{ return _contentType; }}
	}}
	public string Server {{
		get {{ return _server; }}
	}}
	public NameValueCollection Headers {{
		get {{ return _headers; }}
	}}
	public HttpStatusCode StatusCode {{
		get {{ return _statusCode; }}
	}}

	public string TransferEncoding {{
		get {{ return _headers[""Transfer-Encoding""]; }}
		set {{ _headers[""Transfer-Encoding""] = value; }}
	}}
}}";
			#endregion

			public static readonly string Common_WinFormClass_Socket_BaseSocket_cs =
			#region 内容太长已被收起
 @"using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class BaseSocket {{

	public static byte[] Read(Stream stream, byte[] end) {{
		MemoryStream ms = new MemoryStream();
		byte[] data = new byte[1];
		int bytes = data.Length;
		while (bytes > 0 && BaseSocket.findBytes(ms.ToArray(), end, 0) == -1) {{
			bytes = stream.Read(data, 0, data.Length);
			ms.Write(data, 0, data.Length);
		}}
		data = ms.ToArray();
		ms.Close();
		return data;
	}}
	protected void Write(Stream stream, SocketMessager messager) {{
		MemoryStream ms = new MemoryStream();
		byte[] buff = Encoding.UTF8.GetBytes(messager.GetCanParseString());
		ms.Write(buff, 0, buff.Length);
		if (messager.Arg != null) {{
			buff = Http.Deflate.Compress(Lib.Serialize(messager.Arg));
			ms.Write(buff, 0, buff.Length);
		}}
		this.Write(stream, ms.ToArray());
		ms.Close();
	}}
	private void Write(Stream stream, byte[] data) {{
		MemoryStream ms = new MemoryStream();
		byte[] buff = Encoding.UTF8.GetBytes(Convert.ToString(data.Length + 8, 16).PadRight(8));
		ms.Write(buff, 0, buff.Length);
		ms.Write(data, 0, data.Length);
		buff = ms.ToArray();
		ms.Close();
		stream.Write(buff, 0, buff.Length);
	}}

	protected SocketMessager Read(Stream stream) {{
		byte[] data = new byte[8];
		int bytes = 0;
		int overs = data.Length;
		string size = string.Empty;
		while (overs > 0) {{
			bytes = stream.Read(data, 0, overs);
			overs -= bytes;
			size += Encoding.UTF8.GetString(data, 0, bytes);
		}}
		
		if (int.TryParse(size, NumberStyles.HexNumber, null, out overs) == false) {{
			return null;
		}}
		overs -= data.Length;
		MemoryStream ms = new MemoryStream();
		data = new Byte[1024];
		while (overs > 0) {{
			bytes = stream.Read(data, 0, overs < data.Length ? overs : data.Length);
			overs -= bytes;
			ms.Write(data, 0, bytes);
		}}
		data = ms.ToArray();
		ms.Close();
		return SocketMessager.Parse(data);
	}}

	public static int findBytes(byte[] source, byte[] find, int startIndex) {{
		if (find == null) return -1;
		if (find.Length == 0) return -1;
		if (source == null) return -1;
		if (source.Length == 0) return -1;
		if (startIndex < 0) startIndex = 0;
		int idx = -1, idx2 = startIndex - 1;
		do {{
			idx2 = idx = Array.FindIndex<byte>(source, Math.Min(idx2 + 1, source.Length), delegate(byte b) {{
				return b == find[0];
			}});
			if (idx2 != -1) {{
				for (int a = 1; a < find.Length; a++) {{
					if (++idx2 >= source.Length || source[idx2] != find[a]) {{
						idx = -1;
						break;
					}}
				}}
				if (idx != -1) break;
			}}
		}} while (idx2 != -1);
		return idx;
	}}

	public static string formatKBit(int kbit) {{
		double mb = kbit;
		string unt = ""bit"";
		if (mb >= 8) {{
			unt = ""Byte"";
			mb = mb / 8;
			if (mb >= 1024) {{
				unt = ""KB"";
				mb = kbit / 1024;
				if (mb >= 1024) {{
					unt = ""MB"";
					mb = mb / 1024;
					if (mb >= 1024) {{
						unt = ""G"";
						mb = mb / 1024;
					}}
				}}
			}}
		}}
		return Math.Round(mb, 1) + unt;
	}}
}}

public class SocketMessager {{
	private static int _identity;
	internal static readonly SocketMessager SYS_TEST_LINK = new SocketMessager(""\0"");
	internal static readonly SocketMessager SYS_HELLO_WELCOME = new SocketMessager(""Hello, Welcome!"");
	internal static readonly SocketMessager SYS_ACCESS_DENIED = new SocketMessager(""Access Denied."");

	private int _id;
	private string _action;
	private string _permission;
	private DateTime _remoteTime;
	private object _arg;
	private Exception _exception;

	public SocketMessager(string action)
		: this(action, null, null) {{
	}}
	public SocketMessager(string action, object arg)
		: this(action, null, arg) {{
	}}
	public SocketMessager(string action, string permission, object arg) {{
		this._id = Interlocked.Increment(ref _identity);
		this._action = action == null ? string.Empty : action;
		this._permission = permission == null ? string.Empty : permission;
		this._arg = arg;
		this._remoteTime = DateTime.Now;
	}}

	public override string ToString() {{
		return
			this._remoteTime.ToString(""yyyy-MM-dd HH:mm:ss"") + ""\t"" +
			this._id + ""\t"" +
			this._action.Replace(""\t"", ""\\t"") + ""\t"" +
			this._permission.Replace(""\t"", ""\\t"") + ""\t"" +
			this._arg;
	}}

	public string GetCanParseString() {{
		if (string.Compare(this._action, SocketMessager.SYS_TEST_LINK.Action) == 0) {{
			return this.Action;
		}} else if (
			string.Compare(this._action, SocketMessager.SYS_HELLO_WELCOME.Action) == 0 ||
			string.Compare(this._action, SocketMessager.SYS_ACCESS_DENIED.Action) == 0) {{
			return
				this._id + ""\t"" +
				this.Action + ""\r\n"";
		}} else {{
			return
				this._id + ""\t"" +
				this._action.Replace(""\\"", ""\\\\"").Replace(""\t"", ""\\t"").Replace(""\r\n"", ""\\n"") + ""\t"" +
				this._permission.Replace(""\\"", ""\\\\"").Replace(""\t"", ""\\t"").Replace(""\r\n"", ""\\n"") + ""\t"" +
				this._remoteTime.ToString(""yyyy-MM-dd HH:mm:ss"") + ""\r\n"";
		}}
	}}

	public static SocketMessager Parse(byte[] data) {{
		if (data == null) return new SocketMessager(""NULL"");
		if (data.Length == 1 && data[0] == 0) return SocketMessager.SYS_TEST_LINK;
		int idx = BaseSocket.findBytes(data, new byte[] {{ 13, 10 }}, 0);
		string text = Encoding.UTF8.GetString(data, 0, idx);
		string[] loc1 = text.Split(new string[] {{ ""\t"" }}, 4, StringSplitOptions.None);
		string loc2 = loc1[0];
		string loc3 = loc1.Length > 1 ? loc1[1].Replace(""\\\\"", ""\\"").Replace(""\\t"", ""\t"").Replace(""\\n"", ""\r\n"") : null;
		string loc4 = loc1.Length > 2 ? loc1[2].Replace(""\\\\"", ""\\"").Replace(""\\t"", ""\t"").Replace(""\\n"", ""\r\n"") : null;
		string loc5 = loc1.Length > 3 ? loc1[3] : null;
		MemoryStream ms = new MemoryStream();
		ms.Write(data, idx + 2, data.Length - idx - 2);
		SocketMessager messager = new SocketMessager(loc3, loc4, 
			ms.Length > 0 ? Lib.Deserialize(Http.Deflate.Decompress(ms.ToArray())) : null);
		if (int.TryParse(loc2, out idx)) messager._id = idx;
		if (!string.IsNullOrEmpty(loc5)) DateTime.TryParse(loc5, out messager._remoteTime);
		if (messager._arg is Exception) messager._exception = messager._arg as Exception;
		return messager;
	}}

	/// <summary>
	/// 消息ID，每个一消息ID都是惟一的，同步发送时用
	/// </summary>
	public int Id {{
		get {{ return _id; }}
		set {{ _id = value; }}
	}}
	public string Action {{
		get {{ return _action; }}
	}}
	public string Permission {{
		get {{ return _permission; }}
	}}
	public DateTime RemoteTime {{
		get {{ return _remoteTime; }}
	}}
	public object Arg {{
		get {{ return _arg; }}
	}}
	public Exception Exception {{
		get {{ return _exception; }}
	}}
}}";
			#endregion
			public static readonly string Common_WinFormClass_Socket_ClientSocket_cs =
			#region 内容太长已被收起
 @"using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class ClientSocket : BaseSocket, IDisposable {{

	private bool _isDisposed;
	private IPEndPoint _remotePoint;
	private TcpClient _tcpClient;
	private Thread _thread;
	private bool _running;
	private int _receives;
	private int _errors;
	private object _errors_lock = new object();
	private object _write_lock = new object();
	private Dictionary<int, SyncReceive> _receiveHandlers = new Dictionary<int, SyncReceive>();
	private object _receiveHandlers_lock = new object();
	private DateTime _lastActive;
	public event ClientSocketClosedEventHandler Closed;
	public event ClientSocketReceiveEventHandler Receive;
	public event ClientSocketErrorEventHandler Error;

	private WorkQueue _receiveWQ;
	private WorkQueue _receiveSyncWQ;

	public void Connect(string hostname, int port) {{
		if (this._isDisposed == false && this._running == false) {{
			this._running = true;
			try {{
				IPAddress[] ips = Dns.GetHostAddresses(hostname);
				if (ips.Length == 0) throw new Exception(""无法解析“"" + hostname + ""”"");
				this._remotePoint = new IPEndPoint(ips[0], port);
				this._tcpClient = new TcpClient();
				this._tcpClient.Connect(this._remotePoint);
				this._receiveWQ = new WorkQueue();
				this._receiveSyncWQ = new WorkQueue();
			}} catch (Exception ex) {{
				this._running = false;
				this.OnError(ex);
				this.OnClosed();
				return;
			}}
			this._receives = 0;
			this._errors = 0;
			this._lastActive = DateTime.Now;
			this._thread = new Thread(delegate() {{
				while (this._running) {{
					try {{
						NetworkStream ns = this._tcpClient.GetStream();
						ns.ReadTimeout = 1000 * 20;
						if (ns.DataAvailable) {{
							SocketMessager messager = base.Read(ns);
							if (string.Compare(messager.Action, SocketMessager.SYS_TEST_LINK.Action) == 0) {{
							}} else if (this._receives == 0 &&
								string.Compare(messager.Action, SocketMessager.SYS_HELLO_WELCOME.Action) == 0) {{
								this._receives++;
								this.Write(messager);
							}} else if (string.Compare(messager.Action, SocketMessager.SYS_ACCESS_DENIED.Action) == 0) {{
								throw new Exception(SocketMessager.SYS_ACCESS_DENIED.Action);
							}} else {{
								ClientSocketReceiveEventArgs e = new ClientSocketReceiveEventArgs(this._receives++, messager);
								SyncReceive receive = null;

								if (this._receiveHandlers.TryGetValue(messager.Id, out receive)) {{
									this._receiveSyncWQ.Enqueue(delegate() {{
										try {{
											receive.ReceiveHandler(this, e);
										}} catch (Exception ex) {{
											this.OnError(ex);
										}} finally {{
											receive.Wait.Set();
										}}
									}});
								}} else if (this.Receive != null) {{
									this._receiveWQ.Enqueue(delegate() {{
										this.OnReceive(e);
									}});
								}}
							}}
							this._lastActive = DateTime.Now;
						}} else {{
							TimeSpan ts = DateTime.Now - _lastActive;
							if (ts.TotalSeconds > 3) {{
								this.Write(SocketMessager.SYS_TEST_LINK);
							}}
						}}
						if (!ns.DataAvailable) Thread.CurrentThread.Join(1);
					}} catch (Exception ex) {{
						this._running = false;
						this.OnError(ex);
					}}
				}}
				this.Close();
				this.OnClosed();
			}});
			this._thread.Start();
		}}
	}}

	public void Close() {{
		if (this._tcpClient != null) {{
			this._tcpClient.Close();
		}}
		int[] keys = new int[this._receiveHandlers.Count];
		try {{
			this._receiveHandlers.Keys.CopyTo(keys, 0);
		}} catch {{
			lock (this._receiveHandlers_lock) {{
				keys = new int[this._receiveHandlers.Count];
				this._receiveHandlers.Keys.CopyTo(keys, 0);
			}}
		}}
		foreach (int key in keys) {{
			SyncReceive receiveHandler = null;
			if (this._receiveHandlers.TryGetValue(key, out receiveHandler)) {{
				receiveHandler.Wait.Set();
			}}
		}}
		lock (this._receiveHandlers_lock) {{
			this._receiveHandlers.Clear();
		}}
	}}

	public void Write(SocketMessager messager) {{
		this.Write(messager, null, TimeSpan.Zero);
	}}
	public void Write(SocketMessager messager, ClientSocketReceiveEventHandler receiveHandler) {{
		this.Write(messager, receiveHandler, TimeSpan.FromSeconds(20));
	}}
	public void Write(SocketMessager messager, ClientSocketReceiveEventHandler receiveHandler, TimeSpan timeout) {{
		SyncReceive syncReceive = null;
		try {{
			if (receiveHandler != null) {{
				syncReceive = new SyncReceive(receiveHandler);
				lock (this._receiveHandlers_lock) {{
					if (!this._receiveHandlers.ContainsKey(messager.Id)) {{
						this._receiveHandlers.Add(messager.Id, syncReceive);
					}} else {{
						this._receiveHandlers[messager.Id] = syncReceive;
					}}
				}}
			}}
			lock (_write_lock) {{
				NetworkStream ns = this._tcpClient.GetStream();
				base.Write(ns, messager);
			}}
			this._lastActive = DateTime.Now;
			if (syncReceive != null) {{
				syncReceive.Wait.Reset();
				syncReceive.Wait.WaitOne(timeout, false);
				syncReceive.Wait.Set();
				lock (this._receiveHandlers_lock) {{
					this._receiveHandlers.Remove(messager.Id);
				}}
			}}
		}} catch (Exception ex) {{
			this._running = false;
			this.OnError(ex);
			if (syncReceive != null) {{
				syncReceive.Wait.Set();
				lock (this._receiveHandlers_lock) {{
					this._receiveHandlers.Remove(messager.Id);
				}}
			}}
		}}
	}}

	protected virtual void OnClosed(EventArgs e) {{
		if (this.Closed != null) {{
			new Thread(delegate() {{
				try {{
					this.Closed(this, e);
				}} catch (Exception ex) {{
					this.OnError(ex);
				}}
			}}).Start();
		}}
	}}
	protected void OnClosed() {{
		this.OnClosed(new EventArgs());
	}}

	protected virtual void OnReceive(ClientSocketReceiveEventArgs e) {{
		if (this.Receive != null) {{
			try {{
				this.Receive(this, e);
			}} catch (Exception ex) {{
				this.OnError(ex);
			}}
		}}
	}}

	protected virtual void OnError(ClientSocketErrorEventArgs e) {{
		if (this.Error != null) {{
			this.Error(this, e);
		}}
	}}
	protected void OnError(Exception ex) {{
		int errors = 0;
		lock (this._errors_lock) {{
			errors = ++this._errors;
		}}
		ClientSocketErrorEventArgs e = new ClientSocketErrorEventArgs(ex, errors);
		this.OnError(e);
	}}

	public bool Running {{
		get {{ return this._running; }}
	}}

	class SyncReceive : IDisposable {{
		private ClientSocketReceiveEventHandler _receiveHandler;
		private ManualResetEvent _wait;

		public SyncReceive(ClientSocketReceiveEventHandler receiveHandler) {{
			this._receiveHandler = receiveHandler;
			this._wait = new ManualResetEvent(false);
		}}

		public ClientSocketReceiveEventHandler ReceiveHandler {{
			get {{ return _receiveHandler; }}
		}}
		public ManualResetEvent Wait {{
			get {{ return _wait; }}
		}}

		#region IDisposable 成员

		public void Dispose() {{
			this._wait.Set();
			this._wait.Close();
		}}

		#endregion
	}}

	#region IDisposable 成员

	public void Dispose() {{
		this._isDisposed = true;
		this.Close();
	}}

	#endregion
}}

public delegate void ClientSocketClosedEventHandler(object sender, EventArgs e);
public delegate void ClientSocketErrorEventHandler(object sender, ClientSocketErrorEventArgs e);
public delegate void ClientSocketReceiveEventHandler(object sender, ClientSocketReceiveEventArgs e);

public class ClientSocketErrorEventArgs : EventArgs {{

	private int _errors;
	private Exception _exception;

	public ClientSocketErrorEventArgs(Exception exception, int errors) {{
		this._exception = exception;
		this._errors = errors;
	}}

	public int Errors {{
		get {{ return _errors; }}
	}}
	public Exception Exception {{
		get {{ return _exception; }}
	}}
}}

public class ClientSocketReceiveEventArgs : EventArgs {{

	private int _receives;
	private SocketMessager _messager;

	public ClientSocketReceiveEventArgs(int receives, SocketMessager messager) {{
		this._receives = receives;
		this._messager = messager;
	}}

	public int Receives {{
		get {{ return _receives; }}
	}}
	public SocketMessager Messager {{
		get {{ return _messager; }}
	}}
}}";
			#endregion
			public static readonly string Common_WinFormClass_Socket_ServerSocket_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class ServerSocket : IDisposable {{

	private TcpListener _tcpListener;
	private Thread _tcpListenerThread;
	private Dictionary<int, AcceptSocket> _clients = new Dictionary<int, AcceptSocket>();
	private object _clients_lock = new object();
	private int _id = 1;
	private int _port;
	private bool _running;
	private ManualResetEvent _stopWait;
	public event ServerSocketAcceptedEventHandler Accepted;
	public event ServerSocketClosedEventHandler Closed;
	public event ServerSocketReceiveEventHandler Receive;
	public event ServerSocketErrorEventHandler Error;

	private WorkQueue _acceptWQ;
	internal WorkQueue _receiveWQ;
	internal WorkQueue _receiveSyncWQ;
	private WorkQueue _writeWQ;

	public ServerSocket(int port) {{
		this._port = port;
	}}

	public void Start() {{
		if (this._running == false) {{
			this._running = true;
			try {{
				this._tcpListener = new TcpListener(IPAddress.Any, this._port);
				this._tcpListener.Start();
				this._acceptWQ = new WorkQueue();
				this._receiveWQ = new WorkQueue();
				this._receiveSyncWQ = new WorkQueue();
				this._writeWQ = new WorkQueue();
			}} catch (Exception ex) {{
				this._running = false;
				this.OnError(ex);
				return;
			}}
			this._tcpListenerThread = new Thread(delegate() {{
				while (this._running) {{
					try {{
						TcpClient tcpClient = this._tcpListener.AcceptTcpClient();
						this._acceptWQ.Enqueue(delegate() {{
							try {{
								AcceptSocket acceptSocket = new AcceptSocket(this, tcpClient, this._id);
								this.OnAccepted(acceptSocket);
							}} catch (Exception ex) {{
								this.OnError(ex);
							}}
						}});
					}} catch (Exception ex) {{
						this.OnError(ex);
					}}
				}}

				int[] keys = new int[this._clients.Count];
				try {{
					this._clients.Keys.CopyTo(keys, 0);
				}} catch {{
					lock (this._clients_lock) {{
						keys = new int[this._clients.Count];
						this._clients.Keys.CopyTo(keys, 0);
					}}
				}}
				foreach (int key in keys) {{
					AcceptSocket client = null;
					if (this._clients.TryGetValue(key, out client)) {{
						client.Close();
					}}
				}}
				if (this._acceptWQ != null) {{
					this._acceptWQ.Dispose();
				}}
				if (this._receiveWQ != null) {{
					this._receiveWQ.Dispose();
				}}
				if (this._receiveSyncWQ != null) {{
					this._receiveSyncWQ.Dispose();
				}}
				if (this._writeWQ != null) {{
					this._writeWQ.Dispose();
				}}
				this._clients.Clear();
				this._stopWait.Set();
			}});
			this._tcpListenerThread.Start();
		}}
	}}

	public void Stop() {{
		if (this._tcpListener != null) {{
			this._tcpListener.Stop();
		}}
		if (this._running == true) {{
			this._stopWait = new ManualResetEvent(false);
			this._stopWait.Reset();
			this._running = false;
			this._stopWait.WaitOne();
		}}
	}}

	internal void AccessDenied(AcceptSocket client) {{
		client.Write(SocketMessager.SYS_ACCESS_DENIED, delegate(object sender2, ServerSocketReceiveEventArgs e2) {{
		}}, TimeSpan.FromSeconds(1));
		client.Close();
	}}

	public void Write(SocketMessager messager) {{
		int[] keys = new int[this._clients.Count];
		try {{
			this._clients.Keys.CopyTo(keys, 0);
		}} catch {{
			lock (this._clients_lock) {{
				keys = new int[this._clients.Count];
				this._clients.Keys.CopyTo(keys, 0);
			}}
		}}
		foreach (int key in keys) {{
			AcceptSocket client = null;
			if (this._clients.TryGetValue(key, out client)) {{
				this._writeWQ.Enqueue(delegate() {{
					client.Write(messager);
				}});
			}}
		}}
	}}

	public AcceptSocket GetAcceptSocket(int id) {{
		AcceptSocket socket = null;
		this._clients.TryGetValue(id, out socket);
		return socket;
	}}

	internal void CloseClient(AcceptSocket client) {{
		this._clients.Remove(client.Id);
	}}

	protected virtual void OnAccepted(ServerSocketAcceptedEventArgs e) {{
		SocketMessager helloMessager = new SocketMessager(SocketMessager.SYS_HELLO_WELCOME.Action);
		e.AcceptSocket.Write(helloMessager, delegate(object sender2, ServerSocketReceiveEventArgs e2) {{
			if (e2.Messager.Id == helloMessager.Id &&
				string.Compare(e2.Messager.Action, helloMessager.Action) == 0) {{
				e.AcceptSocket._accepted = true;
			}}
		}}, TimeSpan.FromSeconds(2));
		if (e.AcceptSocket._accepted) {{
			if (this.Accepted != null) {{
				try {{
					this.Accepted(this, e);
				}} catch (Exception ex) {{
					this.OnError(ex);
				}}
			}}
		}} else {{
			e.AcceptSocket.AccessDenied();
		}}
	}}
	private void OnAccepted(AcceptSocket client) {{
		lock (_clients_lock) {{
			_clients.Add(this._id++, client);
		}}
		ServerSocketAcceptedEventArgs e = new ServerSocketAcceptedEventArgs(this._clients.Count, client);
		this.OnAccepted(e);
	}}

	protected virtual void OnClosed(ServerSocketClosedEventArgs e) {{
		if (this.Closed != null) {{
			this.Closed(this, e);
		}}
	}}
	internal void OnClosed(AcceptSocket client) {{
		ServerSocketClosedEventArgs e = new ServerSocketClosedEventArgs(this._clients.Count, client.Id);
		this.OnClosed(e);
	}}

	protected virtual void OnReceive(ServerSocketReceiveEventArgs e) {{
		if (this.Receive != null) {{
			this.Receive(this, e);
		}}
	}}
	internal void OnReceive2(ServerSocketReceiveEventArgs e) {{
		this.OnReceive(e);
	}}

	protected virtual void OnError(ServerSocketErrorEventArgs e) {{
		if (this.Error != null) {{
			this.Error(this, e);
		}}
	}}
	protected void OnError(Exception ex) {{
		ServerSocketErrorEventArgs e = new ServerSocketErrorEventArgs(-1, ex, null);
		this.OnError(e);
	}}
	internal void OnError2(ServerSocketErrorEventArgs e) {{
		this.OnError(e);
	}}

	#region IDisposable 成员

	public void Dispose() {{
		this.Stop();
	}}

	#endregion
}}

public class AcceptSocket : BaseSocket, IDisposable {{

	private ServerSocket _server;
	private TcpClient _tcpClient;
	private Thread _thread;
	private bool _running;
	private int _id;
	private int _receives;
	private int _errors;
	private object _errors_lock = new object();
	private object _write_lock = new object();
	private Dictionary<int, SyncReceive> _receiveHandlers = new Dictionary<int, SyncReceive>();
	private object _receiveHandlers_lock = new object();
	private DateTime _lastActive;
	internal bool _accepted;

	public AcceptSocket(ServerSocket server, TcpClient tcpClient, int id) {{
		this._running = true;
		this._id = id;
		this._server = server;
		this._tcpClient = tcpClient;
		this._lastActive = DateTime.Now;
		this._thread = new Thread(delegate() {{
			while (this._running) {{
				try {{
					NetworkStream ns = this._tcpClient.GetStream();
					ns.ReadTimeout = 1000 * 20;
					if (ns.DataAvailable) {{
						SocketMessager messager = base.Read(ns);
						if (string.Compare(messager.Action, SocketMessager.SYS_TEST_LINK.Action) != 0) {{
							ServerSocketReceiveEventArgs e = new ServerSocketReceiveEventArgs(this._receives++, messager, this);
							SyncReceive receive = null;

							if (this._receiveHandlers.TryGetValue(messager.Id, out receive)) {{
								this._server._receiveSyncWQ.Enqueue(delegate() {{
									try {{
										receive.ReceiveHandler(this, e);
									}} catch (Exception ex) {{
										this.OnError(ex);
									}} finally {{
										receive.Wait.Set();
									}}
								}});
							}} else {{
								this._server._receiveWQ.Enqueue(delegate() {{
									this.OnReceive(e);
								}});
							}}
						}}
						this._lastActive = DateTime.Now;
					}} else if (_accepted) {{
						TimeSpan ts = DateTime.Now - _lastActive;
						if (ts.TotalSeconds > 5) {{
							this.Write(SocketMessager.SYS_TEST_LINK);
						}}
					}}
					if (!ns.DataAvailable) Thread.CurrentThread.Join(1);
				}} catch (Exception ex) {{
					this._running = false;
					this.OnError(ex);
				}}
			}}
			this.Close();
			this.OnClosed();
		}});
		this._thread.Start();
	}}

	public void Close() {{
		this._running = false;
		this._tcpClient.Close();
		this._server.CloseClient(this);
		int[] keys = new int[this._receiveHandlers.Count];
		try {{
			this._receiveHandlers.Keys.CopyTo(keys, 0);
		}} catch {{
			lock (this._receiveHandlers_lock) {{
				keys = new int[this._receiveHandlers.Count];
				this._receiveHandlers.Keys.CopyTo(keys, 0);
			}}
		}}
		foreach (int key in keys) {{
			SyncReceive receiveHandler = null;
			if (this._receiveHandlers.TryGetValue(key, out receiveHandler)) {{
				receiveHandler.Wait.Set();
			}}
		}}
		lock (this._receiveHandlers_lock) {{
			this._receiveHandlers.Clear();
		}}
	}}

	public void Write(SocketMessager messager) {{
		this.Write(messager, null, TimeSpan.Zero);
	}}
	public void Write(SocketMessager messager, ServerSocketReceiveEventHandler receiveHandler) {{
		this.Write(messager, receiveHandler, TimeSpan.FromSeconds(20));
	}}
	public void Write(SocketMessager messager, ServerSocketReceiveEventHandler receiveHandler, TimeSpan timeout) {{
		SyncReceive syncReceive = null;
		try {{
			if (receiveHandler != null) {{
				syncReceive = new SyncReceive(receiveHandler);
				lock (this._receiveHandlers_lock) {{
					if (!this._receiveHandlers.ContainsKey(messager.Id)) {{
						this._receiveHandlers.Add(messager.Id, syncReceive);
					}} else {{
						this._receiveHandlers[messager.Id] = syncReceive;
					}}
				}}
			}}
			lock (_write_lock) {{
				NetworkStream ns = this._tcpClient.GetStream();
				base.Write(ns, messager);
			}}
			this._lastActive = DateTime.Now;
			if (syncReceive != null) {{
				syncReceive.Wait.Reset();
				syncReceive.Wait.WaitOne(timeout, false);
				syncReceive.Wait.Set();
				lock (this._receiveHandlers_lock) {{
					this._receiveHandlers.Remove(messager.Id);
				}}
			}}
		}} catch (Exception ex) {{
			this._running = false;
			this.OnError(ex);
			if (syncReceive != null) {{
				syncReceive.Wait.Set();
				lock (this._receiveHandlers_lock) {{
					this._receiveHandlers.Remove(messager.Id);
				}}
			}}
		}}
	}}

	/// <summary>
	/// 拒绝访问，并关闭连接
	/// </summary>
	public void AccessDenied() {{
		this._server.AccessDenied(this);
	}}

	protected virtual void OnClosed() {{
		try {{
			this._server.OnClosed(this);
		}} catch (Exception ex) {{
			this.OnError(ex);
		}}
	}}

	protected virtual void OnReceive(ServerSocketReceiveEventArgs e) {{
		try {{
			this._server.OnReceive2(e);
		}} catch (Exception ex) {{
			this.OnError(ex);
		}}
	}}

	protected virtual void OnError(Exception ex) {{
		int errors = 0;
		lock (this._errors_lock) {{
			errors = ++this._errors;
		}}
		ServerSocketErrorEventArgs e = new ServerSocketErrorEventArgs(errors, ex, this);
		this._server.OnError2(e);
	}}

	public int Id {{
		get {{ return _id; }}
	}}

	class SyncReceive : IDisposable {{
		private ServerSocketReceiveEventHandler _receiveHandler;
		private ManualResetEvent _wait;

		public SyncReceive(ServerSocketReceiveEventHandler onReceive) {{
			this._receiveHandler = onReceive;
			this._wait = new ManualResetEvent(false);
		}}

		public ManualResetEvent Wait {{
			get {{ return _wait; }}
		}}
		public ServerSocketReceiveEventHandler ReceiveHandler {{
			get {{ return _receiveHandler; }}
		}}

		#region IDisposable 成员

		public void Dispose() {{
			this._wait.Set();
			this._wait.Close();
		}}

		#endregion
	}}

	#region IDisposable 成员

	void IDisposable.Dispose() {{
		this.Close();
	}}

	#endregion
}}

public delegate void ServerSocketClosedEventHandler(object sender, ServerSocketClosedEventArgs e);
public delegate void ServerSocketAcceptedEventHandler(object sender, ServerSocketAcceptedEventArgs e);
public delegate void ServerSocketErrorEventHandler(object sender, ServerSocketErrorEventArgs e);
public delegate void ServerSocketReceiveEventHandler(object sender, ServerSocketReceiveEventArgs e);

public class ServerSocketClosedEventArgs : EventArgs {{

	private int _accepts;
	private int _acceptSocketId;

	public ServerSocketClosedEventArgs(int accepts, int acceptSocketId) {{
		this._accepts = accepts;
		this._acceptSocketId = acceptSocketId;
	}}

	public int Accepts {{
		get {{ return _accepts; }}
	}}
	public int AcceptSocketId {{
		get {{ return _acceptSocketId; }}
	}}
}}

public class ServerSocketAcceptedEventArgs : EventArgs {{

	private int _accepts;
	private AcceptSocket _acceptSocket;

	public ServerSocketAcceptedEventArgs(int accepts, AcceptSocket acceptSocket) {{
		this._accepts = accepts;
		this._acceptSocket = acceptSocket;
	}}

	public int Accepts {{
		get {{ return _accepts; }}
	}}
	public AcceptSocket AcceptSocket {{
		get {{ return _acceptSocket; }}
	}}
}}

public class ServerSocketErrorEventArgs : EventArgs {{

	private int _errors;
	private Exception _exception;
	private AcceptSocket _acceptSocket;

	public ServerSocketErrorEventArgs(int errors, Exception exception, AcceptSocket acceptSocket) {{
		this._errors = errors;
		this._exception = exception;
		this._acceptSocket = acceptSocket;
	}}

	public int Errors {{
		get {{ return _errors; }}
	}}
	public Exception Exception {{
		get {{ return _exception; }}
	}}
	public AcceptSocket AcceptSocket {{
		get {{ return _acceptSocket; }}
	}}
}}

public class ServerSocketReceiveEventArgs : EventArgs {{

	private int _receives;
	private SocketMessager _messager;
	private AcceptSocket _acceptSocket;

	public ServerSocketReceiveEventArgs(int receives, SocketMessager messager, AcceptSocket acceptSocket) {{
		this._receives = receives;
		this._messager = messager;
		this._acceptSocket = acceptSocket;
	}}

	public int Receives {{
		get {{ return _receives; }}
	}}
	public SocketMessager Messager {{
		get {{ return _messager; }}
	}}
	public AcceptSocket AcceptSocket {{
		get {{ return _acceptSocket; }}
	}}
}}";
			#endregion
			public static readonly string Common_WinFormClass_Robot_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;

/// <summary>
/// 作业调度器，一般运行在控制台
/// </summary>
public class Robot : IDisposable {{

	private string _def_path;
	private List<RobotDef> _robots;
	private Dictionary<string, RobotDef> _dic_robots = new Dictionary<string, RobotDef>();
	private object _robots_lock = new object();
	private FileSystemWatcher _defWatcher;
	public event RobotErrorHandler Error;
	public event RobotRunHandler Run;

	public Robot()
		: this(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @""robot.txt"")) {{
	}}
	public Robot(string path) {{
		_def_path = path;
	}}

	public void Start() {{
		lock (_robots_lock) {{
			_dic_robots.Clear();
			if (_robots != null) {{
				for (int a = 0; a < _robots.Count; a++)
					_dic_robots.Add(_robots[a].Name, _robots[a]);
				_robots.Clear();
			}}
		}}
		if (!File.Exists(_def_path)) return;
		lock (_robots_lock) {{
			_robots = LoadDef();
			foreach (RobotDef bot in _robots)
				if (bot._timer == null) bot.RunNow();
		}}
		if (_defWatcher == null) {{
			_defWatcher = new FileSystemWatcher(Path.GetDirectoryName(_def_path), Path.GetFileName(_def_path));
			_defWatcher.Changed += delegate(object sender, FileSystemEventArgs e) {{
				_defWatcher.EnableRaisingEvents = false;
				if (_robots.Count > 0) {{
					Start();
				}}
				_defWatcher.EnableRaisingEvents = true;
			}};
			_defWatcher.EnableRaisingEvents = true;
		}}
	}}
	public void Stop() {{
		lock (_robots_lock) {{
			if (_robots != null) {{
				for (int a = 0; a < _robots.Count; a++)
					_robots[a].Dispose();
				_robots.Clear();
			}}
		}}
	}}

	#region IDisposable 成员

	public void Dispose() {{
		if (_defWatcher != null)
			_defWatcher.Dispose();
		Stop();
	}}

	#endregion

	public List<RobotDef> LoadDef() {{
		string defDoc = Encoding.UTF8.GetString(readFile(_def_path));
		return LoadDef(defDoc);
	}}
	public List<RobotDef> LoadDef(string defDoc) {{
		Dictionary<string, RobotDef> dic = new Dictionary<string, RobotDef>();
		string[] defs = defDoc.Split(new string[] {{ ""\r\n"" }}, StringSplitOptions.None);
		int row = 1;
		foreach (string def in defs) {{
			string loc1 = def.Trim();
			if (string.IsNullOrEmpty(loc1) || loc1[0] == 65279 || loc1[0] == ';' || loc1[0] == '#') continue;
			string pattern = @""([^\s]+)\s+(NONE|SEC|MIN|HOUR|DAY|RunOnDay|RunOnWeek|RunOnMonth)\s+([^\s]+)\s+([^\s]+)"";
			Match m = Regex.Match(loc1, pattern, RegexOptions.IgnoreCase);
			if (!m.Success) {{
				onError(new Exception(""Robot配置错误“"" + loc1 + ""”, 第"" + row + ""行""));
				continue;
			}}
			string name = m.Groups[1].Value.Trim('\t', ' ');
			RobotRunMode mode = getMode(m.Groups[2].Value.Trim('\t', ' '));
			string param = m.Groups[3].Value.Trim('\t', ' ');
			string runParam = m.Groups[4].Value.Trim('\t', ' ');
			if (dic.ContainsKey(name)) {{
				onError(new Exception(""Robot配置存在重复的名字“"" + name + ""”, 第"" + row + ""行""));
				continue;
			}}
			if (mode == RobotRunMode.NONE) continue;

			RobotDef rd = null;
			if (_dic_robots.ContainsKey(name)) {{
				rd = _dic_robots[name];
				rd.Update(mode, param, runParam);
				_dic_robots.Remove(name);
			}} else rd = new RobotDef(this, name, mode, param, runParam);
			if (rd.Interval < 0) {{
				onError(new Exception(""Robot配置参数错误“"" + def + ""”, 第"" + row + ""行""));
				continue;
			}}
			dic.Add(rd.Name, rd);
			row++;
		}}
		List<RobotDef> rds = new List<RobotDef>();
		foreach (RobotDef rd in dic.Values)
			rds.Add(rd);
		foreach (RobotDef stopBot in _dic_robots.Values)
			stopBot.Dispose();

		return rds;
	}}

	private void onError(Exception ex) {{
		onError(ex, null);
	}}
	internal void onError(Exception ex, RobotDef def) {{
		if (Error != null)
			Error(this, new RobotErrorEventArgs(ex, def));
	}}
	internal void onRun(RobotDef def) {{
		if (Run != null)
			Run(this, def);
	}}
	private byte[] readFile(string path) {{
		if (File.Exists(path)) {{
			string destFileName = Path.GetTempFileName();
			File.Copy(path, destFileName, true);
			int read = 0;
			byte[] data = new byte[1024];
			MemoryStream ms = new MemoryStream();
			using (FileStream fs = new FileStream(destFileName, FileMode.OpenOrCreate, FileAccess.Read)) {{
				do {{
					read = fs.Read(data, 0, data.Length);
					if (read <= 0) break;
					ms.Write(data, 0, read);
				}} while (true);
				fs.Close();
			}}
			File.Delete(destFileName);
			data = ms.ToArray();
			ms.Close();
			return data;
		}}
		return new byte[] {{ }};
	}}
	private RobotRunMode getMode(string mode) {{
		mode = string.Concat(mode).ToUpper();
		switch (mode) {{
			case ""SEC"": return RobotRunMode.SEC;
			case ""MIN"": return RobotRunMode.MIN;
			case ""HOUR"": return RobotRunMode.HOUR;
			case ""DAY"": return RobotRunMode.DAY;
			case ""RUNONDAY"": return RobotRunMode.RunOnDay;
			case ""RUNONWEEK"": return RobotRunMode.RunOnWeek;
			case ""RUNONMONTH"": return RobotRunMode.RunOnMonth;
			default: return RobotRunMode.NONE;
		}}
	}}
}}

public class RobotDef : IDisposable {{
	private string _name;
	private RobotRunMode _mode = RobotRunMode.NONE;
	private string _param;
	private string _runParam;
	private int _runTimes = 0;
	private int _errTimes = 0;

	private Robot _onwer;
	internal Timer _timer;
	private bool _timerIntervalOverflow = false;

	public RobotDef(Robot onwer, string name, RobotRunMode mode, string param, string runParam) {{
		_onwer = onwer;
		_name = name;
		_mode = mode;
		_param = param;
		_runParam = runParam;
	}}
	public void Update(RobotRunMode mode, string param, string runParam) {{
		if (_mode != mode || _param != param || _runParam != runParam) {{
			_mode = mode;
			_param = param;
			_runParam = runParam;
			if (_timer != null) _timer.Stop();
			RunNow();
		}}
	}}

	public void RunNow() {{
		if (_timer == null) {{
			_timer = new Timer();
			_timer.AutoReset = false;
			_timer.Elapsed += delegate (object sender, System.Timers.ElapsedEventArgs e) {{
				if (_timerIntervalOverflow) {{
					RunNow();
					return;
				}}
				_runTimes++;
				string logObj = this.ToString();
				try {{
					_onwer.onRun(this);
				}} catch (Exception ex) {{
					_errTimes++;
					_onwer.onError(ex, this);
				}}
				RunNow();
			}};
		}}
		if (_timer != null) {{
			double interval = this.Interval;
			if (interval <= 0) {{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(""		{{0}} Interval <= 0"", _name);
				Console.ResetColor();
				return;
			}}
			if (!_timerIntervalOverflow && interval > 1000 * 9) {{
				DateTime nextTime = DateTime.Now.AddMilliseconds(interval);
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine(""		{{0}} 下次触发时间：{{1:yyyy-MM-dd HH:mm:ss}}"", _name, nextTime);
				Console.ResetColor();
			}}
			_timerIntervalOverflow = interval > int.MaxValue;
			_timer.Stop();
			_timer.Interval = _timerIntervalOverflow ? int.MaxValue : interval;
			_timer.Start();
		}}
	}}

	public override string ToString() {{
		return Name + "", "" + Mode + "", "" + Param + "", "" + RunParam;
	}}

	#region IDisposable 成员

	public void Dispose() {{
		if (_timer != null) {{
			_timer.Stop();
			_timer.Close();
			_timer.Dispose();
		}}
	}}

	#endregion

	public string Name {{ get {{ return _name; }} }}
	public RobotRunMode Mode {{ get {{ return _mode; }} }}
	public string Param {{ get {{ return _param; }} }}
	public string RunParam {{ get {{ return _runParam; }} }}
	public int RunTimes {{ get {{ return _runTimes; }} }}
	public int ErrTimes {{ get {{ return _errTimes; }} }}

	public double Interval {{
		get {{
			DateTime now = DateTime.Now;
			DateTime curt = DateTime.MinValue;
			TimeSpan ts = TimeSpan.Zero;
			uint ww = 0, dd = 0, hh = 0, mm = 0, ss = 0;
			double interval = -1;
			switch (_mode) {{
				case RobotRunMode.SEC:
					double.TryParse(_param, out interval);
					interval *= 1000;
					break;
				case RobotRunMode.MIN:
					double.TryParse(_param, out interval);
					interval *= 60 * 1000;
					break;
				case RobotRunMode.HOUR:
					double.TryParse(_param, out interval);
					interval *= 60 * 60 * 1000;
					break;
				case RobotRunMode.DAY:
					double.TryParse(_param, out interval);
					interval *= 24 * 60 * 60 * 1000;
					break;
				case RobotRunMode.RunOnDay:
					List<string> hhmmss = new List<string>(string.Concat(_param).Split(':'));
					if (hhmmss.Count == 3)
						if (uint.TryParse(hhmmss[0], out hh) && hh < 24 &&
							uint.TryParse(hhmmss[1], out mm) && mm < 60 &&
							uint.TryParse(hhmmss[2], out ss) && ss < 60) {{
							curt = now.Date.AddHours(hh).AddMinutes(mm).AddSeconds(ss);
							ts = curt.Subtract(now);
							while (!(ts.TotalMilliseconds > 0)) {{
								curt = curt.AddDays(1);
								ts = curt.Subtract(now);
							}}
							interval = ts.TotalMilliseconds;
						}}
					break;
				case RobotRunMode.RunOnWeek:
					string[] wwhhmmss = string.Concat(_param).Split(':');
					if (wwhhmmss.Length == 4)
						if (uint.TryParse(wwhhmmss[0], out ww) && ww < 7 &&
							uint.TryParse(wwhhmmss[1], out hh) && hh < 24 &&
							uint.TryParse(wwhhmmss[2], out mm) && mm < 60 &&
							uint.TryParse(wwhhmmss[3], out ss) && ss < 60) {{
							curt = now.Date.AddHours(hh).AddMinutes(mm).AddSeconds(ss);
							ts = curt.Subtract(now);
							while(!(ts.TotalMilliseconds > 0 && (int)curt.DayOfWeek == ww)) {{
								curt = curt.AddDays(1);
								ts = curt.Subtract(now);
							}}
							interval = ts.TotalMilliseconds;
						}}
					break;
				case RobotRunMode.RunOnMonth:
					string[] ddhhmmss = string.Concat(_param).Split(':');
					if (ddhhmmss.Length == 4)
						if (uint.TryParse(ddhhmmss[0], out dd) && dd > 0 && dd < 32 &&
							uint.TryParse(ddhhmmss[1], out hh) && hh < 24 &&
							uint.TryParse(ddhhmmss[2], out mm) && mm < 60 &&
							uint.TryParse(ddhhmmss[3], out ss) && ss < 60) {{
							curt = new DateTime(now.Year, now.Month, (int)dd).AddHours(hh).AddMinutes(mm).AddSeconds(ss);
							ts = curt.Subtract(now);
							while (!(ts.TotalMilliseconds > 0)) {{
								curt = curt.AddMonths(1);
								ts = curt.Subtract(now);
							}}
							interval = ts.TotalMilliseconds;
						}}
					break;
			}}
			if (interval == 0) interval = 1;
			return interval;
		}}
	}}
}}
/*
; 和 # 匀为行注释
;SEC：					按秒触发
;MIN：					按分触发
;HOUR：					按时触发
;DAY：					按天触发
;RunOnDay：				每天 什么时间 触发
;RunOnWeek：			星期几 什么时间 触发
;RunOnMonth：			每月 第几天 什么时间 触发

;Name1		SEC			2				/schedule/test002.aspx
;Name2		MIN			2				/schedule/test002.aspx
;Name3		HOUR		1				/schedule/test002.aspx
;Name4		DAY			2				/schedule/test002.aspx
;Name5		RunOnDay	15:55:59		/schedule/test002.aspx
;每天15点55分59秒
;Name6		RunOnWeek	1:15:55:59		/schedule/test002.aspx
;每星期一15点55分59秒
;Name7		RunOnMonth	1:15:55:59		/schedule/test002.aspx
;每月1号15点55分59秒
*/
public enum RobotRunMode {{
	NONE = 0,
	SEC = 1,
	MIN = 2,
	HOUR = 3,
	DAY = 4,
	RunOnDay = 11,
	RunOnWeek = 12,
	RunOnMonth = 13
}}

public delegate void RobotErrorHandler(object sender, RobotErrorEventArgs e);
public delegate void RobotRunHandler(object sender, RobotDef e);
public class RobotErrorEventArgs : EventArgs {{

	private Exception _exception;
	private RobotDef _def;

	public RobotErrorEventArgs(Exception exception, RobotDef def) {{
		_exception = exception;
		_def = def;
	}}

	public Exception Exception {{
		get {{ return _exception; }}
	}}
	public RobotDef Def {{
		get {{ return _def; }}
	}}
}}";
			#endregion
			public static readonly string Common_WinFormClass_WorkQueue_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

public class WorkQueue : WorkQueue<AnonymousHandler> {{
	public WorkQueue() : this(16, -1) {{ }}
	public WorkQueue(int thread)
		: this(thread, -1) {{
	}}
	public WorkQueue(int thread, int capacity) {{
		base.Thread = thread;
		base.Capacity = capacity;
		base.Process += delegate(AnonymousHandler ah) {{
			ah();
		}};
	}}
}}

public class WorkQueue<T> : IDisposable {{
	public delegate void WorkQueueProcessHandler(T item);
	public event WorkQueueProcessHandler Process;

	private int _thread = 16;
	private int _capacity = -1;
	private int _work_index = 0;
	private Dictionary<int, WorkInfo> _works = new Dictionary<int, WorkInfo>();
	private object _works_lock = new object();
	private Queue<T> _queue = new Queue<T>();
	private object _queue_lock = new object();

	public WorkQueue() : this(16, -1) {{ }}
	public WorkQueue(int thread)
		: this(thread, -1) {{
	}}
	public WorkQueue(int thread, int capacity) {{
		_thread = thread;
		_capacity = capacity;
	}}

	public void Enqueue(T item) {{
		lock (_queue_lock) {{
			if (_capacity > 0 && _queue.Count >= _capacity) return;
			_queue.Enqueue(item);
		}}
		lock (_works_lock) {{
			foreach (WorkInfo w in _works.Values) {{
				if (w.IsWaiting) {{
					w.Set();
					return;
				}}
			}}
		}}
		if (_works.Count < _thread) {{
			if (_queue.Count > 0) {{
				int index = 0;
				lock (_works_lock) {{
					index = _work_index++;
					_works.Add(index, new WorkInfo());
				}}
				new Thread(delegate() {{
					WorkInfo work = _works[index];
					while (true) {{
						List<T> de = new List<T>();
						if (_queue.Count > 0) {{
							lock (_queue_lock) {{
								if (_queue.Count > 0) {{
									de.Add(_queue.Dequeue());
								}}
							}}
						}}

						if (de.Count > 0) {{
							try {{
								this.OnProcess(de[0]);
							}} catch {{
							}}
						}}

						if (_queue.Count == 0) {{
							work.WaitOne(TimeSpan.FromSeconds(20));

							if (_queue.Count == 0) {{
								break;
							}}
						}}
					}}
					lock (_works_lock) {{
						_works.Remove(index);
					}}
					work.Dispose();
				}}).Start();
			}}
		}}
	}}

	protected virtual void OnProcess(T item) {{
		if (Process != null) {{
			Process(item);
		}}
	}}

	#region IDisposable 成员

	public void Dispose() {{
		lock (_queue_lock) {{
			_queue.Clear();
		}}
		lock (_works_lock) {{
			foreach (WorkInfo w in _works.Values) {{
				w.Dispose();
			}}
		}}
	}}

	#endregion

	public int Thread {{
		get {{ return _thread; }}
		set {{
			if (_thread != value) {{
				_thread = value;
			}}
		}}
	}}
	public int Capacity {{
		get {{ return _capacity; }}
		set {{
			if (_capacity != value) {{
				_capacity = value;
			}}
		}}
	}}

	public int UsedThread {{
		get {{ return _works.Count; }}
	}}
	public int Queue {{
		get {{ return _queue.Count; }}
	}}

	public string Statistics {{
		get {{
			string value = string.Format(@""线程：{{0}}/{{1}}
队列：{{2}}

"", _works.Count, _thread, _queue.Count);
			int[] keys = new int[_works.Count];
			try {{
				_works.Keys.CopyTo(keys, 0);
			}} catch {{
				lock (_works_lock) {{
					keys = new int[_works.Count];
					_works.Keys.CopyTo(keys, 0);
				}}
			}}
			foreach (int k in keys) {{
				WorkInfo w = null;
				if (_works.TryGetValue(k, out w)) {{
					value += string.Format(@""线程{{0}}：{{1}}
"", k, w.IsWaiting);
				}}
			}}
			return value;
		}}
	}}

	class WorkInfo : IDisposable {{
		private ManualResetEvent _reset = new ManualResetEvent(false);
		private bool _isWaiting = false;

		public void WaitOne(TimeSpan timeout) {{
			try {{
				_reset.Reset();
				_isWaiting = true;
				_reset.WaitOne(timeout, false);
			}} catch {{ }}
		}}
		public void Set() {{
			try {{
				_isWaiting = false;
				_reset.Set();
			}} catch {{ }}
		}}

		public bool IsWaiting {{
			get {{ return _isWaiting; }}
		}}

		#region IDisposable 成员

		public void Dispose() {{
			this.Set();
			_reset.Close();
		}}

		#endregion
	}}
}}";
			#endregion

			public static readonly string Common_csproj =
			#region 内容太长已被收起
 @"<ItemGroup>
		<Reference Include=""log4net"">
			<HintPath>..\log4net.dll</HintPath>
		</Reference>
		<Reference Include=""System"" />
		<Reference Include=""System.configuration"" />
		<Reference Include=""System.Data"" />
		<Reference Include=""System.Drawing"" />
		<Reference Include=""System.Messaging"" />
		<Reference Include=""System.Web"" />
		<Reference Include=""System.Windows.Forms"" />
		<Reference Include=""System.Xml"" />
		<Reference Include=""Microsoft.VisualBasic"" />
	</ItemGroup>
	<ItemGroup>
		<Compile Include=""BmwNet.cs"" />
		<Compile Include=""IniHelper.cs"" />
		<Compile Include=""GraphicHelper.cs"" />
		<Compile Include=""JSDecoder.cs"" />
		<Compile Include=""Lib.cs"" />
		<Compile Include=""Logger.cs"" />
		<Compile Include=""Http\BaseHttpProxy.cs"" />
		<Compile Include=""Http\Deflate.cs"" />
		<Compile Include=""Http\GZip.cs"" />
		<Compile Include=""Http\HttpAccessException.cs"" />
		<Compile Include=""Http\HttpCookie2.cs"" />
		<Compile Include=""Http\HttpStream.cs"" />
		<Compile Include=""Http\HttpRequest.cs"" />
		<Compile Include=""Http\HttpResponse.cs"" />
		<Compile Include=""WinFormClass\Socket\BaseSocket.cs"" />
		<Compile Include=""WinFormClass\Socket\ClientSocket.cs"" />
		<Compile Include=""WinFormClass\Socket\ServerSocket.cs"" />
		<Compile Include=""WinFormClass\Robot.cs"" />
		<Compile Include=""WinFormClass\WorkQueue.cs"" />
	</ItemGroup>";
			#endregion

			public static readonly string Web_web_config =
			#region 内容太长已被收起
 @"<?xml version=""1.0"" encoding=""utf-8""?>
<configuration>
	<appSettings>
		<add key=""{0}_ITEM_CACHE_TIMEOUT"" value=""100""/>
		<!--
			设置 {0}.BLL.GetItem 缓存超时值(全局) 单位(秒)
			此键值小于或等于 0 将不缓存
			此键值应用于全局，若想为某一 BLL.GetItem 设置，请新增 ""{0}_ITEM_CACHE_TIMEOUT_类名""
		-->
	</appSettings>
	<connectionStrings>
		<add name=""{1}"" connectionString=""{{connectionString}}""/>
	</connectionStrings>
	<system.web>
		<customErrors mode=""Off""/>
		<pages pageBaseType=""BasePage"">
			<controls>
				<add assembly=""Common"" namespace=""RichControls"" tagPrefix=""asp""/>
			</controls>
			<namespaces>
				<add namespace=""System.Collections.Generic""/>
				<add namespace=""System.Data""/>
				<add namespace=""System.IO""/>
				<add namespace=""{0}.BLL""/>
				<add namespace=""{0}.Model""/>
			</namespaces>
		</pages>
	</system.web>
</configuration>";
			#endregion
			public static readonly string Web_App_Code_BasePage_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using {0}.BLL;
using {0}.Model;

public class BasePage : System.Web.UI.Page {{

	public static BmwNet bmw = new BmwNet(""~/view/"");
	public Hashtable ht = new Hashtable();
	public DateTime startTime = DateTime.Now;

	protected override void OnInit(EventArgs e) {{
		base.OnInit(e);
	}}
	protected override void OnLoadComplete(EventArgs e) {{
		base.OnLoadComplete(e);

		string a = Request.Url.LocalPath.Replace("".aspx"", "".html"").Substring(1);
		Response.Write(bmw.RenderFile(a, ht));
		Response.Write(DateTime.Now.Subtract(startTime).TotalMilliseconds + ""ms"");
		Response.End();
	}}
}}
";
			#endregion

			public static readonly string Web_connection_aspx =
			#region 内容太长已被收起
 @"<%@ Page Language=""C#"" %>
<%@ Import Namespace=""System.Data"" %>
<%@ Import Namespace=""System.Data.SqlClient"" %>
<%@ Import Namespace=""System.Threading"" %>

<div>当前线程： <%= Thread.CurrentThread.ManagedThreadId %></div>

<div style=""padding-top:12px;font-weight:bold;"">{1}</div>
<div style=""padding-left:36px;font-size:10px;"">
<% foreach (int tid in {0}.DAL.ConnectionManager.ConnectionPool.Keys) {{ %>
	线程<%= tid.ToString().PadRight(12).Replace("" "", ""&nbsp;"") %> (<%= {0}.DAL.ConnectionManager.ConnectionPool[tid].Count %>)
	<div style=""padding-left:72px;""><%
		foreach ({0}.DAL.SqlConnection2 conn in {0}.DAL.ConnectionManager.ConnectionPool[tid]) {{
			//conn.SqlConnection.Close();%><span style=""<%= conn.SqlConnection.State != ConnectionState.Closed ? ""color:red"" : string.Empty %>""><%= conn.SqlConnection.Database %>, <%= conn.SqlConnection.State %>, <%= conn.LastActive %></span>
	<% }} %></div>
<% }} %>
</div>
";
			#endregion
			public static readonly string Web_index_aspx =
			#region 内容太长已被收起
 @"<%@ Page Language=""C#"" %>

<script runat=""server"">

	protected void Page_Load(object sender, EventArgs e) {{
		//ht[""topics""] = Topics.Select.Skip(1).Limit(2).ToList();
	}}
</script>
";
			#endregion
			public static readonly string Web_view_index_html =
			#region 内容太长已被收起
 @"<!DOCTYPE html>
<html>
<head>
<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8""/>
    <title></title>
	<meta charset=""utf-8"" />
</head>
<body>

	<div @for=""xxx 1, 100"">
		<h3>{{#xxx}}</h3>
		
		<hr />
	</div>
</body>
</html>
";
			#endregion
			public static readonly string Web_sln =
			#region 内容太长已被收起
 @"
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio 14
VisualStudioVersion = 14.0.25123.0
MinimumVisualStudioVersion = 10.0.40219.1
Project(""{{E24C65DC-7377-472B-9ABA-BC803B73C61A}}"") = ""{0}"", ""{0}"", ""{{{1}}}""
EndProject
";
			#endregion

			public static readonly string Admin_web_config =
			#region 内容太长已被收起
 @"<?xml version=""1.0"" encoding=""utf-8""?>
<configuration>
	<appSettings>
		<add key=""{0}_ITEM_CACHE_TIMEOUT"" value=""100""/>
		<!--
			设置 {0}.BLL.GetItem 缓存超时值(全局) 单位(秒)
			此键值小于或等于 0 将不缓存
			此键值应用于全局，若想为某一 BLL.GetItem 设置，请新增 ""{0}_ITEM_CACHE_TIMEOUT_类名""
		-->
		<add key=""vs:EnableBrowserLink"" value=""false"" />
	</appSettings>
	<connectionStrings>
		<add name=""{1}"" connectionString=""{{connectionString}}""/>
	</connectionStrings>
	<system.webServer>
		<handlers>
			<add name=""admin_ajax"" type=""AjaxHandlerFactory"" path=""ajax.ashx"" verb=""POST,GET"" />
		</handlers>
	</system.webServer>
	<system.web>
		<httpRuntime requestValidationMode=""2.0"" />
		<customErrors mode=""Off""/>
		<pages pageBaseType=""BasePage"" enableViewState=""true"" validateRequest=""false"" enableViewStateMac=""false"">
			<controls>
				<add namespace=""RichControls"" tagPrefix=""asp""/>
			</controls>
			<namespaces>
				<add namespace=""System.Collections.Generic""/>
				<add namespace=""System.Data""/>
				<add namespace=""System.IO""/>
				<add namespace=""{0}.BLL""/>
				<add namespace=""{0}.Model""/>
			</namespaces>
		</pages>
		<compilation debug=""true"" targetFramework=""4.5""/>
	</system.web>
</configuration>
";
			#endregion
			public static readonly string Admin_web_sitemap =
			#region 内容太长已被收起
 @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<siteMap xmlns=""http://schemas.microsoft.com/AspNet/SiteMap-File-1.0"" >
	<siteMapNode url=""~/"" title=""后台管理"">
		<siteMapNode title=""运管管理"">{0}
		</siteMapNode>
	</siteMapNode>
</siteMap>
";
			#endregion
			public static readonly string Admin_init_sysdir_aspx =
			#region 内容太长已被收起
 @"<%@ Page Language=""C#"" Theme="""" Inherits=""System.Web.UI.Page"" %>

<script runat=""server"">

	protected void Page_Load(object sender, EventArgs e) {{
		/*
		if (Sysdir.SelectByParent_id(null).Count() > 0) {{
			Response.Write(""本系统已经初始化过，页面没经过任何操作退出。"");
			Response.End();
			return;
		}}

		SysdirInfo dir1, dir2, dir3;
		dir1 = Sysdir.Insert(null, DateTime.Now, ""运营管理"", 1, null);{0}

		Response.Write(""管理目录已初始化完成。"");
		Response.End();
		*/
	}}
</script>

";
			#endregion
			public static readonly string Admin_App_Code_BasePage_cs =
			#region 内容太长已被收起
 @"using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using {0}.BLL;
using {0}.Model;

public class BasePage : Page {{

	//public SysuserInfo LoginUser {{ get; protected set; }}

	protected override void OnLoad(EventArgs e) {{
		Response.Cache.SetNoStore();
		if (SessionManager.Flash != null) {{
			Response.Write(string.Format(@""<div id=""""msg_alert_div"""" class=""""callout callout-success""""><p>{{0}}</p></div>
<script>setTimeout(function(){{{{$('#msg_alert_div').remove();}}}},3000);</script>"", SessionManager.Flash));
			SessionManager.Flash = null;
		}}
		AdminHandler();
		SearchHandler();
		base.OnLoad(e);
	}}

	#region admin handler
	protected void AdminHandler() {{
		//this.LoginUser = SessionManager.Id > 0 ? Sysuser.GetItem(SessionManager.Id) : null;
		//if (this.LoginUser == null) {{
		//	SessionManager.Id = null;
		//	Response.Redirect(""~/login.aspx?backurl="" + Lib.UrlEncode(Request.Url.PathAndQuery));
		//	return;
		//}}
		//if (sysrole_check(Request.Url.PathAndQuery) == false) {{
		//	this.End(""没有权限."");
		//	return;
		//}}
	}}
	//public bool sysrole_check(string url) {{
	//	url = url.ToLower();
	//	//Response.Write(url + ""<br>"");
	//	if (url == ""/"" || url.IndexOf(""/default.aspx"") == 0) return true;
	//	foreach(var a in this.LoginUser.Obj_sysroles) {{
	//		//Response.Write(a.ToString());
	//		foreach(var b in a.Obj_sysdirs) {{
	//			//Response.Write(""-----------------"" + b.ToString() + ""<br>"");
	//			string tmp = b.Url;
	//			if (tmp.EndsWith(""/"")) tmp += ""default.aspx"";
	//			if (url.IndexOf(tmp) == 0) return true;
	//		}}
	//	}}
	//	return false;
	//}}
	protected bool End(object obj) {{
		Response.Write(string.Concat(""Error:"", obj));
		Response.End();
		return true;
	}}
	protected bool Goto(string url, object flash) {{
		SessionManager.Flash = flash;
		Response.Redirect(url);
		Response.End();
		return true;
	}}
	#endregion

	#region search handler
	protected void SearchHandler() {{
		MethodInfo method = this.Page.GetType().GetMethod(""searching"");
		if (!IsPostBack && method != null) method.Invoke(this.Page, null);
	}}

	private Control search_bar;
	protected void search_add(Control control, string property, string key) {{
		if (search_bar == null) search_bar = Lib.GetWebControl(""search_bar"");
		if (search_bar != null) {{
			MethodInfo method = search_bar.GetType().GetMethod(""add_control"");
			if (method != null) method.Invoke(search_bar, new object[] {{ control, property, key }});
		}}
	}}
	#endregion
}}
";
			#endregion
			public static readonly string Admin_App_Code_SessionManager_cs =
			#region 内容太长已被收起
 @"using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using {0}.BLL;
using {0}.Model;

public class SessionManager {{
	//public static SysuserInfo LoginInfo {{
	//	get {{ return Sysuser.GetItem(Id); }}
	//}}
	public static short? Id {{
		get {{ return HttpContext.Current.Session[""Login.Id""] as short?; }}
		set {{ HttpContext.Current.Session[""Login.Id""] = value; }}
	}}
	public static object Flash {{
		get {{ return HttpContext.Current.Session[""Flash""]; }}
		set {{ HttpContext.Current.Session[""Flash""] = value; }}
	}}
}}
";
			#endregion
			public static readonly string Admin_App_Code_RichControls_AllPager_cs =
			#region 内容太长已被收起
@"using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix(""GridViewPager"", ""asp"")]
[assembly: TagPrefix(""DataListPager"", ""asp"")]
namespace RichControls {{

	[ControlValueProperty(""Pager"")]
	public class GridViewPager : GridView {{
		private Pager _pager = new Pager();
		private int _pageNumber = 10;
		private string _queryStringName = ""Page"";
		private bool _enableViewState = false;

		[Category(""分页"")]
		[Description(""页面跳转数目"")]
		public int PageNumber {{
			get {{ return _pageNumber; }}
			set {{ _pageNumber = value; }}
		}}
		[Category(""分页"")]
		[Description(""每页数量"")]
		public override int PageSize {{
			get {{ return _pager.PageSize; }}
			set {{ _pager.PageSize = value; }}
		}}
		[Category(""分页"")]
		public int RecordCount {{
			get {{ return _pager.RecordCount; }}
		}}
		public void SetRecordCount(int rc) {{
			_pager.RecordCount = rc;
		}}
		[Category(""分页"")]
		[Description(""QueryString 参数名"")]
		public string QueryStringName {{
			get {{ return _queryStringName; }}
			set {{ _queryStringName = value; }}
		}}
		[Category(""分页"")]
		public override int PageCount {{
			get {{ return _pager.PageCount; }}
		}}
		[Category(""分页"")]
		public override int PageIndex {{
			get {{ return _pager.PageIndex; }}
		}}
		public override bool EnableViewState {{
			get {{ return _enableViewState; }}
			set {{ _enableViewState = value; }}
		}}

		protected override void OnInit(EventArgs e) {{
			base.EnableViewState = _enableViewState;
			base.AllowPaging = false;
			int pageIndex;
			if (!int.TryParse(Context.Request.QueryString[_queryStringName], out pageIndex)) pageIndex = 1;
			_pager.PageIndex = pageIndex;

			if (Visible) OnPageIndexChanged(new DataGridPageChangedEventArgs(null, _pager.PageIndex));
			base.OnInit(e);
		}}
		protected override void Render(HtmlTextWriter writer) {{
			StringBuilder sb = new StringBuilder();
			StringWriter sw = new StringWriter(sb);
			HtmlTextWriter htw = new HtmlTextWriter(sw, string.Empty);
			htw.Indent = 0;
			htw.NewLine = string.Empty;
			base.Render(htw);
			htw.Close();
			sw.Close();
			int lastIndexOf = sb.ToString().LastIndexOf(""</table>"",  StringComparison.CurrentCultureIgnoreCase);
			if (lastIndexOf != -1) {{
				sb.Insert(lastIndexOf, ""<tr><td class=\""page\"""" + (base.Columns.Count > 1 ? ("" colspan="" + (base.Columns.Count - 1)) : string.Empty) + "">"" +
					_pager.GetGotoHtml(_queryStringName, _pageNumber) +
					""</td><td style=\""width:1%;\""><a id =\""btn_delete_sel\"" href=\""#\"" class=\""btn btn-danger pull-right\"">删除</a></td></tr>"");
			}}
			writer.Write(sb.ToString());
		}}

		public new event DataGridPageChangedEventHandler PageIndexChanged;
		virtual protected void OnPageIndexChanged(DataGridPageChangedEventArgs e) {{
			if (PageIndexChanged != null) PageIndexChanged(this, e);
		}}
	}}

	[ControlValueProperty(""Pager"")]
	public class DataListPager : DataList {{
		private Pager _pager = new Pager();
		private int _pageNumber = 10;
		private string _queryStringName = ""Page"";

		[Category(""分页"")]
		[Description(""页面跳转数目"")]
		public int PageNumber {{
			get {{ return _pageNumber; }}
			set {{ _pageNumber = value; }}
		}}
		[Category(""分页"")]
		[Description(""每页数量"")]
		public int PageSize {{
			get {{ return _pager.PageSize; }}
			set {{ _pager.PageSize = value; }}
		}}
		[Category(""分页"")]
		public int RecordCount {{
			get {{ return _pager.RecordCount; }}
		}}
		public void SetRecordCount(int rc) {{
			_pager.RecordCount = rc;
		}}
		[Category(""分页"")]
		[Description(""QueryString 参数名"")]
		public string QueryStringName {{
			get {{ return _queryStringName; }}
			set {{ _queryStringName = value; }}
		}}
		[Category(""分页"")]
		public int PageCount {{
			get {{ return _pager.PageCount; }}
		}}
		[Category(""分页"")]
		public int PageIndex {{
			get {{ return _pager.PageIndex; }}
		}}

		protected override void OnInit(EventArgs e) {{
			int pageIndex;
			if (!int.TryParse(Context.Request.QueryString[_queryStringName], out pageIndex)) pageIndex = 1;
			_pager.PageIndex = pageIndex;

			if (Visible) OnPageIndexChanged(new DataGridPageChangedEventArgs(null, _pager.PageIndex));
			base.OnInit(e);
		}}
		protected override void Render(HtmlTextWriter writer) {{
			StringBuilder sb = new StringBuilder();
			StringWriter sw = new StringWriter(sb);
			HtmlTextWriter htw = new HtmlTextWriter(sw, string.Empty);
			htw.Indent = 0;
			htw.NewLine = string.Empty;
			base.Render(htw);
			htw.Close();
			sw.Close();
			int lastIndexOf = sb.ToString().LastIndexOf(""</table>"", StringComparison.CurrentCultureIgnoreCase);
			if (lastIndexOf != -1) {{
				sb.Insert(lastIndexOf, ""<tr><td class=\""page\"""" + (base.RepeatColumns > 1 ? ("" colspan="" + base.RepeatColumns) : string.Empty) + "">"" +
					_pager.GetGotoHtml(_queryStringName, _pageNumber) +
					""</td></tr>"");
			}}
			writer.Write(sb.ToString());
		}}

		public event DataGridPageChangedEventHandler PageIndexChanged;
		virtual protected void OnPageIndexChanged(DataGridPageChangedEventArgs e) {{
			if (PageIndexChanged != null) PageIndexChanged(this, e);
		}}
	}}

	public class Pager {{
		private int _pageSize = 10;
		private int _pageIndex;
		private int _recordCount;

		public int PageSize {{
			get {{ return _pageSize; }}
			set {{ _pageSize = value; }}
		}}
		public int PageIndex {{
			get {{ return _pageIndex; }}
			set {{ _pageIndex = value; }}
		}}
		public int RecordCount {{
			get {{ return _recordCount; }}
			set {{
				_recordCount = value;
				if (_pageIndex < 1) _pageIndex = 1;
				if (_pageIndex > PageCount) _pageIndex = PageCount;
			}}
		}}
		public int PageCount {{
			get {{ return _recordCount / _pageSize + (_recordCount % _pageSize == 0 ? 0 : 1); }}
		}}

		public string GetGotoHtml(string pageQuery, int pageNumber) {{
			int pageCount = this.PageCount;
			pageNumber--;

			string prevText;
			string nextText;
			string statsText;
			switch (Thread.CurrentThread.CurrentUICulture.Name) {{
				case ""zh-CN"":
					prevText = @""&lt; 上一页"";
					nextText = @""下一页 &gt;"";
					statsText = @""<span class=""""statics"""">页数:<input type=""""text"""" ondrop=""""event.returnValue=false"""" onpaste=""""this.ondrop()"""" onkeypress=""""var evt=window.event||e;if((evt.keyCode<48||evt.keyCode>58)&&evt.keyCode!=13)evt.returnValue=false;"""" onkeydown=""""var evt=window.event||e;if(evt.keyCode==13){{{{var nu='{{0}}'.replace('{{{{0}}}}',this.value);if(this.form)this.form.onsubmit=function(){{{{return false;}}}};nav.goto(nu);}}}}"""" value=""""{{1}}"""" style=""""width:30px;"""">/{{2}} 每页:{{3}} 总计:{{4}}</span>"";
					break;
				case ""zh-HK"":
					prevText = @""&lt; 上一頁"";
					nextText = @""下一頁 &gt;"";
					statsText = @""<span class=""""statics"""">頁數:<input type=""""text"""" ondrop=""""event.returnValue=false"""" onpaste=""""this.ondrop()"""" onkeypress=""""var evt=window.event||e;if((evt.keyCode<48||evt.keyCode>58)&&evt.keyCode!=13)evt.returnValue=false;"""" onkeydown=""""var evt=window.event||e;if(evt.keyCode==13){{{{var nu='{{0}}'.replace('{{{{0}}}}',this.value);if(this.form)this.form.onsubmit=function(){{{{return false;}}}};nav.goto(nu);}}}}"""" value=""""{{1}}"""" style=""""width:30px;"""">/{{2}} 每頁:{{3}} 總計:{{4}}</span>"";
					break;
				default:
					prevText = @""&lt; Previous"";
					nextText = @""Next &gt;"";
					statsText = @""<span class=""""statics"""">Page <input type=""""text"""" ondrop=""""event.returnValue=false"""" onpaste=""""this.ondrop()"""" onkeypress=""""var evt=window.event||e;if((evt.keyCode<48||evt.keyCode>58)&&evt.keyCode!=13)evt.returnValue=false;"""" onkeydown=""""var evt=window.event||e;if(evt.keyCode==13){{{{var nu='{{0}}'.replace('{{{{0}}}}',this.value);if(this.form)this.form.onsubmit=function(){{{{return false;}}}};nav.goto(nu);}}}}"""" value=""""{{1}}"""" style=""""width:30px;""""> of {{2}} ({{4}} Items) PageSize: {{3}} </span>"";
					break;
			}}

			StringBuilder sb = new StringBuilder();
			int startIndex = 0, endIndex = 0;
			string query = HttpContext.Current.Request.QueryString[""urlrewrite""];
			if (!string.IsNullOrEmpty(query)) {{
				query = Regex.Replace(query, ""[\u0391-\uFFE5]"", delegate (Match m) {{
					return HttpContext.Current.Server.UrlEncode(m.Groups[0].Value);
				}});
				if (Uri.IsWellFormedUriString(query.Replace(""{{0}}"", ""1""), UriKind.Relative)) {{
					query = query.Replace(""{{0}}"", ""[|pageindex|]"");
				}} else {{
					query = null;
				}}
			}}
			if (string.IsNullOrEmpty(query)) {{
				query = FilterUrlQuery(HttpContext.Current.Request.Url.Query, pageQuery) + string.Format(""&{{0}}=[|pageindex|]"", pageQuery);
				query = ""?"" + query.TrimStart('&');
			}}
			query = query.Replace(""{{"", ""{{{{"").Replace(""}}"", ""}}}}"").Replace(""[|pageindex|]"", ""{{0}}"");
			if (_pageIndex <= 1) new int();
			else sb.AppendFormat(@""<a href=""""{{0}}"""">{{1}}</a>"", string.Format(query, _pageIndex - 1), prevText);
			startIndex = _pageIndex - pageNumber / 2;
			if (startIndex <= 1) startIndex = 1;
			else sb.AppendFormat(@""<a href=""""{{0}}"""">1</a><span class=""""dot"""">...</span>"", string.Format(query, 1));
			endIndex = startIndex + pageNumber;
			if (endIndex > pageCount) endIndex = pageCount;
			for (int a = startIndex; a <= endIndex; a++) {{
				if (a == _pageIndex) sb.AppendFormat(@""<span class=""""normal"""">{{0}}</span>"", a);
				else sb.AppendFormat(@""<a href=""""{{0}}"""">{{1}}</a>"", string.Format(query, a), a);
			}}
			if (endIndex < pageCount) sb.AppendFormat(@""<span class=""""dot"""">...</span><a href=""""{{0}}"""">{{1}}</a>"", string.Format(query, pageCount), pageCount);
			if (_pageIndex >= pageCount) new int();
			else sb.AppendFormat(@""<a href=""""{{0}}"""">{{1}}</a>"", string.Format(query, _pageIndex + 1), nextText);
			sb.AppendFormat(statsText, Lib.GetJsString(string.Format(query, ""{{0}}"")), _pageIndex, pageCount, _pageSize, _recordCount);
			return sb.ToString();
		}}

		public static string ConcatUrlQuery(string query1, string query2) {{
			query1 = string.Concat(query1).TrimStart('?');
			query2 = string.Concat(query2).TrimStart('?');
			if (string.IsNullOrEmpty(query1)) return string.Concat(query2);
			if (string.IsNullOrEmpty(query2)) return string.Concat(query1);
			string[] qfs = query2.Split('&');
			foreach (string qf in qfs) {{
				if (!string.IsNullOrEmpty(qf)) {{
					string[] kv = qf.Split(new char[] {{ '=' }}, 2);
					string pattern = string.Format(@""\b{{0}}=[^&]*\b&?"", kv[0]);
					query1 = Regex.Replace(query1, pattern, string.Empty, RegexOptions.IgnoreCase);
				}}
			}}
			return query1.Trim('&') + ""&"" + query2.Trim('&');
		}}
		public static string FilterUrlQuery(string query, string field) {{
			query = string.Concat(query).TrimStart('?');
			if (string.IsNullOrEmpty(query)) return string.Empty;
			string pattern = string.Format(@""\b{{0}}=[^&]*&?"", field);
			query = Regex.Replace(query, pattern, string.Empty, RegexOptions.IgnoreCase);
			return query.Trim('&');
		}}
	}}
}}
";
			#endregion
			public static readonly string Admin_App_Code_RichControls_SafeHtmlInputCheckBox_cs =
			#region 内容太长已被收起
 @"using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public class SafeHtmlInputCheckBox : HtmlInputCheckBox {{

	private string _text;

	protected override void Render(HtmlTextWriter writer) {{
		base.Render(writer);
		writer.Write(string.Format(""<label for=\""{{0}}\"">{{1}}</label>"", this.ClientID, Lib.HtmlEncode(_text)));
	}}

	public string Text {{
		get {{ return _text; }}
		set {{ _text = value; }}
	}}

	public string select {{
		get {{ return base.Checked ? ""1"" : ""0""; }}
		set {{ base.Checked = value == ""1"" ? true : false; }}
	}}
}}
";
			#endregion
			public static readonly string Admin_App_Code_RichControls_SelField_cs =
			#region 内容太长已被收起
 @"using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing.Design;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace RichControls {{
	public class SelField : DataControlField {{
		private string[] _dataFields;

		protected override DataControlField CreateField() {{
			return new SelField();
		}}

		public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex) {{
			base.InitializeCell(cell, cellType, rowState, rowIndex);
			if (cellType == DataControlCellType.Header) {{
				cell.Style[""text-align""] = ""center"";
				cell.Text = ""<input type=\""checkbox\"" onclick=\""var checked = this.checked; $('input#id[type=\\'checkbox\\']').each(function(i, el) {{ el.checked = checked; }});\""/>"";
			}}
			if (cellType == DataControlCellType.DataCell) {{
				cell.HorizontalAlign = HorizontalAlign.Center;
				cell.DataBinding += OnDataBindField;
			}}
		}}

		protected virtual void OnDataBindField(object sender, EventArgs e) {{
			Control control = (Control)sender;
			object dataItem = DataBinder.GetDataItem(control.NamingContainer);
			string value = null;
			foreach (string field in _dataFields) {{
				value += HttpUtility.UrlEncode(DataBinder.Eval(dataItem, field).ToString()) + "","";
			}}
			value = value.Remove(value.Length - 1);
			string str = string.Format(""<input id=\""id\"" type=\""checkbox\"" name=\""id\"" value=\""{{0}}\"" />"", value);
			if (control is TableCell) {{
				((TableCell)control).Text = str;
			}}
		}}

		[TypeConverter(typeof(StringArrayConverter)), DefaultValue((string)null), Editor(""System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"", typeof(UITypeEditor))]
		public string[] DataFields {{
			get {{ return _dataFields; }}
			set {{ _dataFields = value; }}
		}}
	}}
}}
";
			#endregion
			public static readonly string Admin_App_Code_RichControls_HtmlSelect_Style1_cs =
			#region 内容太长已被收起
 @"using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;{5}

namespace RichControls {{

	public class HtmlSelect{0} : HtmlSelect {{
		protected override void OnLoad(EventArgs e) {{
			if (!this.Page.IsPostBack) {{
				base.Attributes[""class""] = ""form-control select2"";
				base.Items.Add(new ListItem(""==请选择{7}=="", """"));
				List<{0}Info> items = {0}.Select.Sort(""{1}"").ToList();
				for (int a = 0; a < items.Count; a++) {{
					{0}Info item = items[a];
					base.Items.Add(new ListItem(string.Concat(item.{2}), string.Concat(item.{3})));
				}}
			}}{4}{6}
			base.OnLoad(e);
		}}
	}}
}}
";
			#endregion
			public static readonly string Admin_App_Code_RichControls_HtmlSelect_Style2_cs =
			#region 内容太长已被收起
 @"using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;{5}

namespace RichControls {{

	public class HtmlSelect{0} : HtmlSelect {{
		protected override void OnLoad(EventArgs e) {{
			if (!this.Page.IsPostBack) {{
				base.Items.Add(new ListItem(""==请选择{7}=="", """"));
				yieldAddItem(null, string.Empty);
			}}
			base.OnLoad(e);
		}}

		private int yieldAddItem({6} {4}, string left) {{
			List<{0}Info> items = {0}.SelectBy{4}({4}).Sort(""{1}"").ToList();
			for (int a = 0; a < items.Count; a++) {{
				{0}Info item = items[a];
				string l = a == items.Count - 1 ? ""└"" : ""├"";

				ListItem li = new ListItem(left + l + item.{2}, string.Concat(item.{3}));
				base.Items.Add(li);
				int sc = yieldAddItem(item.{3}, left + (a == items.Count - 1 ? ""　"" : ""│""));
			}}
			return items.Count;
		}}
	}}
}}
";
			#endregion
			public static readonly string Admin_App_Code_Ajax_cs =
			#region 内容太长已被收起
 @"using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.SessionState;
using System.Reflection;

public class Ajax {{
	public static string Register(Control control, string function, string argument) {{
		if (control == null) return ""//This control can not be NULL."";
		string applicationPath = HttpContext.Current.Request.ApplicationPath + (HttpContext.Current.Request.ApplicationPath.EndsWith(""/"") ? """" : ""/"");
		return string.Format(""{{4}}.ajax_target = '{{2}},{{3}}'; $.ajax({{{{ method : 'post', url : '{{1}}ajax.ashx', data : {{4}}, success : {{0}}, dataType : 'html', error: function (jqXHR, textStatus, errorThrown) {{{{ var data = jqXHR.responseText; alert(data); }}}} }}}});"", 
			function,
			applicationPath,
			HttpContext.Current.Server.UrlEncode(control.GetType().FullName),
			HttpContext.Current.Server.UrlEncode(control.GetType().Assembly.FullName.Split(',')[0]),
			argument);
	}}
}}

public class AjaxHandler : IHttpHandler, IRequiresSessionState {{
	private Type _type;

	public AjaxHandler(Type type) : base() {{
		this._type = type;
	}}

	#region IHttpHandler 成员
	public bool IsReusable {{
		get {{ return false; }}
	}}

	public void ProcessRequest(HttpContext context) {{
		context.Response.Cache.SetNoStore();
		//context.Response.Write(""("");
		this._type.InvokeMember(""IAjax"",
			System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.InvokeMethod, null, null, new object[] {{ context.Request, context.Response }});
		//context.Response.Write("")"");
	}}
	#endregion
}}

public class AjaxHandlerFactory : IHttpHandlerFactory {{

	#region IHttpHandlerFactory 成员
	public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated) {{
		string ajax_target = context.Request.Form[""ajax_target""];
		Type type = Type.GetType(ajax_target, true);

		switch (requestType) {{
			case ""GET"":
				break;
			case ""POST"":
				return new AjaxHandler(type);
		}}
		return null;
	}}

	public void ReleaseHandler(IHttpHandler handler) {{
	}}
	#endregion
}}
";
			#endregion

			public static readonly string Admin_controls_search_bar_ascx =
			#region 内容太长已被收起
 @"<%@ Control Language=""C#"" ClassName=""search_bar"" %>
<%@ Import Namespace=""System.IO"" %>

<script runat=""server"">

	protected override void OnInit(EventArgs e) {{
		System.Reflection.MethodInfo set_search = this.Page.GetType().GetMethod(""set_search"");
		if (set_search != null) {{
			set_search.Invoke(this.Page, null);
		}}
		base.OnInit(e);
	}}

	protected override void OnLoad(EventArgs e) {{
		this.EnableViewState = false;
		base.OnLoad(e);
	}}

	public void add_control(Control control, string property, string key) {{
		control.PreRender += delegate (object sender, EventArgs e) {{
			control.GetType().GetProperty(property).SetValue(control, Request.QueryString[key], null);
		}};
		place.Controls.Add(control);
		js += string.Format(@""
$('#{{1}}').attr('name', '{{0}}');"", key, control.ClientID);
	}}

	string js;
</script>

<form id=""form_search"" action="""" method=""get"" class=""form-inline mb20"">
	<input type=""text"" name=""key"" class=""form-control"" value=""<%= Request.QueryString[""key""] %>""/>
	<asp:PlaceHolder ID=""place"" runat=""server""></asp:PlaceHolder>
	<span class=""form-group mr15""><a href=""#Search"" class=""btn btn-primary btn-flat"">搜索</a></span>
	<span class=""form-group mr15""><a href=""?"" class=""btn btn-primary btn-flat"">ALL</a></span>
</form>

<script type=""text/javascript""><%= js %>
$('form#form_search')[0].onkeypress = $('form#form_search input[name=""key""]')[0].onkeypress = function (e) {{
	if (!e || !e.keyCode) e = event;
	if (e.keyCode == 13) $('form#form_search a[href=""#Search""]').trigger('click');
}};
$('form#form_search a[href=""#Search""]').click(function () {{
	$('form#form_search').submit();
}});
</script>
";
			#endregion
			public static readonly string Admin_controls_mn_checkbox_ascx =
			#region 内容太长已被收起
 @"<%@ Control Language=""C#"" ClassName=""mn_checkbox"" %>

<script runat=""server"">
	private string _dataValueField;
	private string _dataTextField;
	private string _placeholder;

	public string DataValueField {{ get {{ return _dataValueField; }} set {{ _dataValueField = value; }} }}
	public string DataTextField {{ get {{ return _dataTextField; }} set {{ _dataTextField = value; }} }}
	public string DataSourceID {{ get {{ return rpt.DataSourceID; }} set {{ rpt.DataSourceID = value; }} }}
	public object DataSource {{ get {{ return rpt.DataSource; }} set {{ rpt.DataSource = value; }} }}
	public string Placeholder {{ get {{ return _placeholder; }} set {{ _placeholder = value; }} }}
	public object Value {{
		get {{ return HV.Value; }}
		set {{
			IEnumerable ie2 = value as IEnumerable;
			if (ie2 == null) {{
				HV.Value = string.Concat(value);
				return;
			}}
			string v = """";
			IEnumerator ie = (value as IEnumerable).GetEnumerator();
			while(ie.MoveNext()) {{
				v = string.Concat(v, "","", Lib.EvaluateValue(ie.Current, _dataValueField));
			}}
			HV.Value = v.Length > 0 ? v.Substring(1) : v;
		}}
	}}
</script>

<div id=""chkdiv"" runat=""server"">
<input type=""hidden"" id=""HV"" runat=""server"" />
<asp:Repeater ID=""rpt"" runat=""server"">
	<ItemTemplate>
		<input id=""<%= chkdiv.ClientID %>__<%# Eval(_dataValueField) %>"" type=""checkbox"" value=""<%# Eval(_dataValueField) %>"" /><label for=""<%# chkdiv.ClientID %>__<%# Eval(_dataValueField) %>""><%# Eval(_dataTextField) %></label>
	</ItemTemplate>
</asp:Repeater>
</div>
<script type=""text/javascript"">
	(function () {{
		var div = $('#<%= chkdiv.ClientID %>');
		var hv = $('#<%= HV.ClientID %>');
		var vs = hv.val().split(',');
		var vso = {{}};
		for (var a = 0; a < vs.length; a++) vso[vs[a]] = 1;
		div.find('input[type=""checkbox""]').each(function(i, el) {{
			if (vso[el.value]) el.checked = true;
		}}).click(function () {{
			var nv = [];
			div.find('input[type=""checkbox""]').each(function (i, el) {{
				if (el.checked) nv.push(el.value);
			}});
			hv.val(nv.join(','));
			//alert(hv.val());
		}});
	}})();
</script>
";
			#endregion
			public static readonly string Admin_controls_mn_htmlselect_ascx =
			#region 内容太长已被收起
 @"<%@ Control Language=""C#"" ClassName=""mn_htmlselect"" %>

<script runat=""server"">
	private string _dataValueField;
	private string _dataTextField;
	private string _placeholder;

	public string DataValueField {{ get {{ return _dataValueField; }} set {{ _dataValueField = value; }} }}
	public string DataTextField {{ get {{ return _dataTextField; }} set {{ _dataTextField = value; }} }}
	public string DataSourceID {{ get {{ return rpt.DataSourceID; }} set {{ rpt.DataSourceID = value; }} }}
	public object DataSource {{ get {{ return rpt.DataSource; }} set {{ rpt.DataSource = value; }} }}
	public string Placeholder {{ get {{ return _placeholder; }} set {{ _placeholder = value; }} }}
	public object Value {{
		get {{ return HV.Value; }}
		set {{
			IEnumerable ie2 = value as IEnumerable;
			if (ie2 == null) {{
				HV.Value = string.Concat(value);
				return;
			}}
			string v = """";
			IEnumerator ie = (value as IEnumerable).GetEnumerator();
			while(ie.MoveNext()) {{
				v = string.Concat(v, "","", Lib.EvaluateValue(ie.Current, _dataValueField));
			}}
			HV.Value = v.Length > 0 ? v.Substring(1) : v;
		}}
	}}
</script>

<div id=""chkdiv"" runat=""server"">
<input type=""hidden"" id=""HV"" runat=""server"" />
<select class=""form-control select2"" multiple=""multiple"" data-placeholder=""<%= _placeholder %>"">
<asp:Repeater ID=""rpt"" runat=""server"">
	<ItemTemplate>
		<option value=""<%# Eval(_dataValueField) %>""><%# Eval(_dataTextField) %></option>
	</ItemTemplate>
</asp:Repeater>
</select>
</div>
<script type=""text/javascript"">
	(function () {{
		var div = $('#<%= chkdiv.ClientID %>');
		var hv = $('#<%= HV.ClientID %>');
		var vs = hv.val().split(',');
		var vso = {{}};
		for (var a = 0; a < vs.length; a++) vso[vs[a]] = 1;
		div.find('select option').each(function (i, el) {{
			el.selected = vso[el.value];
		}});
		div.find('select').change(function () {{
			var nv = [];
			$(this).find('option').each(function (i, el) {{
				if (el.selected) nv.push(el.value);
			}});
			hv.val(nv.join(','));
		}});
	}})();
</script>
";
			#endregion
			public static readonly string Admin_controls_mn_htmlselect_sysdir_ascx =
			#region 内容太长已被收起
 @"<%--<%@ Control Language=""C#"" ClassName=""mn_htmlselect_sysdir"" %>

<script runat=""server"">
	public object Value {{
		get {{ return HV.Value; }}
		set {{
			IEnumerable ie2 = value as IEnumerable;
			if (ie2 == null) {{
				HV.Value = string.Concat(value);
				return;
			}}
			string v = """";
			IEnumerator ie = (value as IEnumerable).GetEnumerator();
			while(ie.MoveNext()) {{
				v = string.Concat(v, "","", Lib.EvaluateValue(ie.Current, ""Id""));
			}}
			HV.Value = v.Length > 0 ? v.Substring(1) : v;
		}}
	}}

	List<SysdirInfo> list = Sysdir.GetItems();
</script>

<div id=""chkdiv"" runat=""server"">
<input type=""hidden"" id=""HV"" runat=""server"" />
<select multiple=""multiple"">
	<% foreach (var a in list.Where(a => a.Parent_id != null && a.Obj_sysdir.Parent_id == null).ToList()) {{ %>
		<optgroup label=""<%= a.Name %>"">
			<% foreach (var b in list.Where(b => b.Parent_id == a.Id).ToList()) {{ %>
				<option value=""<%= b.Id %>""><%= b.Name %></option>
			<% }} %>
		</optgroup>
	<% }} %>
</select>
</div>
<script type=""text/javascript"">
	(function () {{
		var div = $('#<%= chkdiv.ClientID %>');
		var hv = $('#<%= HV.ClientID %>');
		var vs = hv.val().split(',');
		var vso = {{}};
		for (var a = 0; a < vs.length; a++) vso[vs[a]] = 1;
		div.find('select option').each(function (i, el) {{
			el.selected = vso[el.value];
		}});
		div.find('select').change(function () {{
			var nv = [];
			$(this).find('option').each(function (i, el) {{
				if (el.selected) nv.push(el.value);
			}});
			hv.val(nv.join(','));
		}});

		div.find('select').multipleSelect({{
			filter: true,
			multiple: true,
			width: '60%'
		}});
		div.find('.ms-drop').attr('style', 'position:static');
	}})();
</script>--%>
";
			#endregion

			public static readonly string Admin_connection_aspx = Web_connection_aspx;
			public static readonly string Admin_exit_aspx =
			#region 内容太长已被收起
 @"<%@ Page Language=""C#"" Theme="""" Inherits=""System.Web.UI.Page"" %>

<script runat=""server"">
	
	protected void Page_Load(object sender, EventArgs e) {{
		SessionManager.Id = null;
		Response.Redirect(""~/login.aspx"");
		Response.End();
	}}
</script>
";
			#endregion
			public static readonly string Admin_default_aspx =
			#region 内容太长已被收起
 @"<%@ Page Language=""C#"" %>

<script runat=""server"">

	protected void Page_Load(object sender, EventArgs e) {{
	}}
</script>

<!DOCTYPE html>
<html lang=""zh-cmn-Hans"">
	<head>
		<meta charset=""utf-8"" />
		<meta http-equiv=""X-UA-Compatible"" content=""IE=edge"" />
		<title>{0}管理系统</title>
		<meta content=""width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no"" name=""viewport"" />
		<link href=""./htm/bootstrap/css/bootstrap.min.css"" rel=""stylesheet"" />
		<link href=""./htm/plugins/font-awesome/css/font-awesome.min.css"" rel=""stylesheet"" />
		<link href=""./htm/css/skins/_all-skins.css"" rel=""stylesheet"" />
		<link href=""./htm/plugins/pace/pace.min.css"" rel=""stylesheet"" />
		<link href=""./htm/plugins/datepicker/datepicker3.css"" rel=""stylesheet"" />
		<link href=""./htm/plugins/timepicker/bootstrap-timepicker.min.css"" rel=""stylesheet"" />
		<link href=""./htm/plugins/select2/select2.min.css"" rel=""stylesheet"" />
		<link href=""./htm/plugins/treetable/css/jquery.treetable.css"" rel=""stylesheet"" />
		<link href=""./htm/plugins/treetable/css/jquery.treetable.theme.default.css"" rel=""stylesheet"" />
		<link href=""./htm/plugins/multiple-select/multiple-select.css"" rel=""stylesheet"" />
		<link href=""./htm/css/system.css"" rel=""stylesheet"" />
		<link href=""./htm/css/index.css"" rel=""stylesheet"" />
		<script type=""text/javascript"" src=""./htm/js/jQuery-2.1.4.min.js""></script>
		<script type=""text/javascript"" src=""./htm/bootstrap/js/bootstrap.min.js""></script>
		<script type=""text/javascript"" src=""./htm/plugins/pace/pace.min.js""></script>
		<script type=""text/javascript"" src=""./htm/plugins/datepicker/bootstrap-datepicker.js""></script>
		<script type=""text/javascript"" src=""./htm/plugins/timepicker/bootstrap-timepicker.min.js""></script>
		<script type=""text/javascript"" src=""./htm/plugins/select2/select2.full.min.js""></script>
		<script type=""text/javascript"" src=""./htm/plugins/input-mask/jquery.inputmask.js""></script>
		<script type=""text/javascript"" src=""./htm/plugins/input-mask/jquery.inputmask.date.extensions.js""></script>
		<script type=""text/javascript"" src=""./htm/plugins/input-mask/jquery.inputmask.extensions.js""></script>
		<script type=""text/javascript"" src=""./htm/plugins/treetable/jquery.treetable.js""></script>
		<script type=""text/javascript"" src=""./htm/plugins/multiple-select/multiple-select.js""></script>
		<script type=""text/javascript"" src=""./htm/js/lib.js""></script>
		<!--[if lt IE 9]>
		<script type='text/javascript' src='./htm/plugins/html5shiv/html5shiv.min.js'></script>
		<script type='text/javascript' src='./htm/plugins/respond/respond.min.js'></script>
		<![endif]-->
	</head>
	<body class=""hold-transition skin-blue sidebar-mini"">
		<div class=""wrapper"">
			<!-- Main Header-->
			<header class=""main-header"">
				<!-- Logo--><a href=""./"" class=""logo"">
					<!-- mini logo for sidebar mini 50x50 pixels--><span class=""logo-mini""><b>{0}</b></span>
					<!-- logo for regular state and mobile devices--><span class=""logo-lg""><b>{0}管理系统</b></span></a>
				<!-- Header Navbar-->
				<nav role=""navigation"" class=""navbar navbar-static-top"">
					<!-- Sidebar toggle button--><a href=""#"" data-toggle=""offcanvas"" role=""button"" class=""sidebar-toggle""><span class=""sr-only"">Toggle navigation</span></a>
					<!-- Navbar Right Menu-->
					<div class=""navbar-custom-menu"">
						<ul class=""nav navbar-nav"">
							<!-- User Account Menu-->
							<li class=""dropdown user user-menu"">
								<!-- Menu Toggle Button--><a href=""#"" data-toggle=""dropdown"" class=""dropdown-toggle"">
									<!-- The user image in the navbar--><img src=""/htm/img/user2-160x160.jpg"" alt=""User Image"" class=""user-image"">
									<!-- hidden-xs hides the username on small devices so only the image appears.--><span class=""hidden-xs""><%--<%= base.LoginUser.Username %>--%></span></a>
								<ul class=""dropdown-menu"">
									<!-- The user image in the menu-->
									<li class=""user-header""><img src=""/htm/img/user2-160x160.jpg"" alt=""User Image"" class=""img-circle"">
										<p><%--<%= base.LoginUser.Username %><small>权限：【<% foreach (var rls in base.LoginUser.Obj_sysroles) {{ %><%= rls.Name %><% }} %>】</small>--%></p>
									</li>
									<!-- Menu Footer-->
									<li class=""user-footer"">
										<div class=""pull-right""><a href=""#"" onclick=""$('form#form_logout').submit();return false;"" class=""btn btn-default btn-flat"">安全退出</a>
											<form id=""form_logout"" method=""post"" action=""./exit.aspx""></form>
										</div>
									</li>
								</ul>
							</li>
						</ul>
					</div>
				</nav>
			</header>
			<!-- Left side column. contains the logo and sidebar-->
			<aside class=""main-sidebar"">
				<!-- sidebar: style can be found in sidebar.less-->
				<section class=""sidebar"">
					<!-- Sidebar Menu-->
					<ul class=""sidebar-menu"">
						<!-- Optionally, you can add icons to the links-->

<%--<script type=""text/javascript"">
	(function () {{<%
	var dirs = new List<SysdirInfo>();
	var tmp2 = Sysdir.SelectByParent_id(null).Sort(""sort"").ToList();
	dirs.AddRange(tmp2);
	tmp2.ForEach(a => dirs.AddRange(Sysdir.SelectByParent_id(a.Id).Sort(""sort"").ToList()));
	%>
		var tmp = [<% foreach (var a in dirs) {{ %><%= a.ToString() %>,<% }}%>null];
		tmp.pop();
		var dirs = [];
		for (var a = 0; a < tmp.length; a++) if (!tmp[a].Parent_id) {{ dirs.push(tmp[a]); tmp[a]._child = []; }}
		for (var a = 0; a < tmp.length; a++) for (var b = 0; b < dirs.length; b++) if (tmp[a].Parent_id === dirs[b].Id) dirs[b]._child.push(tmp[a]);
		var sb = '';
		for (var a = 0; a < dirs.length; a++) {{
			sb += '<li class=""treeview active""><a href=""#""><i class=""fa fa-laptop""></i><span>{{0}}</span><i class=""fa fa-angle-left pull-right""></i></a><ul class=""treeview-menu"">'.format(dirs[a].Name);
			for (var b = 0; b < dirs[a]._child.length; b++)
				sb += '<li><a href=""{{0}}""><i class=""fa fa-circle-o""></i>{{1}}</a></li>'.format(dirs[a]._child[b].Url, dirs[a]._child[b].Name);
			sb += '</ul></li>';
		}}
		document.write(sb);
	}})();
</script>--%>
<asp:Repeater ID=""Repeater2"" runat=""server"" DataSourceID=""SiteMapDataSource1"">
<ItemTemplate>
	<li class=""treeview active""><a href=""#""><i class=""fa fa-laptop""></i><span><%# Eval(""title"")%></span><i class=""fa fa-angle-left pull-right""></i></a>
		<ul class=""treeview-menu"">
			<asp:Repeater ID=""Repeater2"" DataSource='<%# Eval(""ChildNodes"") %>' runat=""server"">
			<ItemTemplate><li><a href=""<%# Eval(""url"") %>""><i class=""fa fa-circle-o""></i><%# Lib.HtmlDecode(Eval(""title"")) %></a></li></ItemTemplate>
			</asp:Repeater>
		</ul>
	</li>
</ItemTemplate>
</asp:Repeater>
<asp:SiteMapDataSource ID=""SiteMapDataSource1"" StartingNodeUrl=""~/"" runat=""server"" ShowStartingNode=""false"" />

					</ul>
					<!-- /.sidebar-menu-->
				</section>
				<!-- /.sidebar-->
			</aside>
			<!-- Content Wrapper. Contains page content-->
			<div class=""content-wrapper"">
				<!-- Main content-->
				<section id=""right_content"" class=""content"">
					<!-- Your Page Content Here-->
				</section>
				<!-- /.content-->
			</div>
			<!-- /.content-wrapper-->
		</div>
		<!-- ./wrapper-->
		<script type=""text/javascript"" src=""./htm/js/system.js""></script>
		<script type=""text/javascript"" src=""./htm/js/admin.js""></script>
		<script>window.UEDITOR_HOME_URL = ""./htm/ueditor/"";</script>
		<script type=""text/javascript"" charset=""utf-8"" src=""./htm/ueditor/ueditor.config.js""></script>
		<script type=""text/javascript"" charset=""utf-8"" src=""./htm/ueditor/ueditor.all.min.js""></script>
		<script type=""text/javascript"" charset=""utf-8"" src=""./htm/ueditor/lang/zh-cn/zh-cn.js""></script>
		<script type=""text/javascript"">
			// 路由功能
			//针对上面的html初始化路由列表
			function hash_encode(str) {{ return url_encode(base64.encode(str)).replace(/%/g, '_'); }}
			function hash_decode(str) {{ return base64.decode(url_decode(str.replace(/_/g, '%'))); }}
			window.div_left_router = {{}};
			$('li.treeview.active ul li a').each(function(index, ele) {{
				var href = $(ele).attr('href');
				$(ele).attr('href', '#base64url' + hash_encode(href));
				window.div_left_router[href] = $(ele).text();
			}});
			(function () {{
				function Vipspa() {{
				}}
				Vipspa.prototype.start = function (config) {{
					Vipspa.mainView = $(config.view);
					startRouter();
					window.onhashchange = function () {{
						if (location._is_changed) return location._is_changed = false;
						startRouter();
					}};
				}};
				function startRouter() {{
					var hash = location.hash;
					if (hash === '') return location.hash = $('li.treeview.active ul li a:first').attr('href');//'#base64url' + hash_encode('/resume_type/');
					if (hash.indexOf('#base64url') !== 0) return;
					var act = hash_decode(hash.substr(10, hash.length - 10));
					//叶湘勤增加的代码，加载或者提交form后，显示内容
					function ajax_success(refererUrl) {{
						if (refererUrl == location.pathname) {{ startRouter(); return function(){{}}; }}
						var hash = '#base64url' + hash_encode(refererUrl);
						if (location.hash != hash) {{
							location._is_changed = true;
							location.hash = hash;
						}}'\''
						return function (data, status, xhr) {{
							if (/<body[^>]*>/i.test(data))
								data = data.match(/<body[^>]*>(([^<]|<(?!\/body>))*)<\/body>/i)[1];
							var div = Vipspa.mainView.html(data);
							Function.prototype.ajax = $.ajax;
							admin_init(function (selector) {{
								if (/<[^>]+>/.test(selector)) return $(selector);
								return div.find(selector);
							}}, {{
								url: refererUrl,
								trans: function (url) {{
									var act = url;
									act = act.substr(0, 1) === '/' || act.indexOf('://') !== -1 || act.indexOf('data:') === 0 ? act : join_url(refererUrl, act);
									return act;
								}},
								goto: function (url_or_form, target) {{
									var form = url_or_form;
									if (typeof form === 'string') {{
										var act = this.trans(form);
										act = '#base64url' + hash_encode(act);
										if (String(target).toLowerCase() === '_blank') return window.open(act);
										location.hash = act;
									}}
									else {{
										if (!window.ajax_form_iframe_max) window.ajax_form_iframe_max = 1;
										window.ajax_form_iframe_max++;
										var iframe = $('<iframe name=""ajax_form_iframe{{0}}""></iframe>'.format(window.ajax_form_iframe_max));
										Vipspa.mainView.append(iframe);
										var act = $(form).attr('action') || '';
										act = act.substr(0, 1) === '/' || act.indexOf('://') !== -1 ? act : join_url(refererUrl, act);
										$(form).attr('action', act);
										$(form).attr('target', iframe.attr('name'));
										iframe.on('load', function () {{
											var doc = this.contentWindow ? this.contentWindow.document : this.document;
											if (doc.body.innerHTML.length === 0) return;
											if (doc.body.innerHTML.indexOf('Error:') === 0) return alert(doc.body.innerHTML.substr(6));
											//以下 '<script ' + '是防止与本页面相匹配，不要删除
											if (doc.body.innerHTML.indexOf('<script ' + 'type=""text/javascript"">location.href=""') === -1) {{
												ajax_success(doc.location.pathname + doc.location.search)(doc.body.innerHTML, 200, null);
											}}
										}});
									}}
								}},
								query: qs_parseByUrl(refererUrl)
							}});
						}};
					}};
					$.ajax({{
						type: 'GET',
						url: act,
						dataType: 'html',
						success: ajax_success(act),
						error: function (jqXHR, textStatus, errorThrown) {{
							var data = jqXHR.responseText;
							if (/<body[^>]*>/i.test(data))
								data = data.match(/<body[^>]*>(([^<]|<(?!\/body>))*)<\/body>/i)[1];
							Vipspa.mainView.html(data);
						}}
					}});
				}}
				window.vipspa = new Vipspa();
			}})();
			$(function () {{
				vipspa.start({{
					view: '#right_content',
				}});
			}});
			// 页面加载进度条
			$(document).ajaxStart(function() {{ Pace.restart(); }});
		</script>
	</body>
</html>
";
			#endregion
			public static readonly string Admin_login_aspx =
			#region 内容太长已被收起
 @"<%@ Page Language=""C#"" Inherits=""System.Web.UI.Page"" %>

<script runat=""server"">

	#region sys系列d-sql
	/*
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	SET ANSI_PADDING ON
	GO
	CREATE TABLE [dbo].[sysdir](
		[id] [smallint] IDENTITY(1,1) NOT NULL,
		[parent_id] [smallint] NULL,
		[name] [nvarchar](128) NULL,
		[url] [varchar](128) NULL,
		[sort] [smallint] NULL,
		[create_time] [datetime] NULL,
	 CONSTRAINT [PK_sysdir] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO
	SET ANSI_PADDING OFF
	GO
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE TABLE [dbo].[sysrole](
		[id] [smallint] NOT NULL,
		[parent_id] [smallint] NULL,
		[name] [nvarchar](256) NULL,
		[create_time] [datetime] NULL,
	 CONSTRAINT [PK_sysrole] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE TABLE [dbo].[sysrole_sysdir](
		[sysrole_id] [smallint] NOT NULL,
		[sysdir_id] [smallint] NOT NULL,
	 CONSTRAINT [PK_sysrole_sysdir] PRIMARY KEY CLUSTERED 
	(
		[sysrole_id] ASC,
		[sysdir_id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	SET ANSI_PADDING ON
	GO
	CREATE TABLE [dbo].[sysuser](
		[id] [smallint] IDENTITY(1,1) NOT NULL,
		[username] [varchar](64) NULL,
		[password] [varchar](32) NULL,
		[create_time] [datetime] NULL,
		[last_login_time] [datetime] NULL,
		[last_login_ip] [varchar](32) NULL,
		[is_master] [bit] NULL,
	 CONSTRAINT [PK_sysuser] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO
	SET ANSI_PADDING OFF
	GO
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE TABLE [dbo].[sysuser_sysrole](
		[sysuser_id] [smallint] NOT NULL,
		[sysrole_id] [smallint] NOT NULL,
	 CONSTRAINT [PK_sysuser_sysrole] PRIMARY KEY CLUSTERED 
	(
		[sysuser_id] ASC,
		[sysrole_id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO
	ALTER TABLE [dbo].[sysrole]  WITH NOCHECK ADD  CONSTRAINT [FK_sysrole_sysrole] FOREIGN KEY([parent_id])
	REFERENCES [dbo].[sysrole] ([id])
	NOT FOR REPLICATION 
	GO
	ALTER TABLE [dbo].[sysrole] NOCHECK CONSTRAINT [FK_sysrole_sysrole]
	GO
	ALTER TABLE [dbo].[sysrole_sysdir]  WITH CHECK ADD  CONSTRAINT [FK_sysrole_sysdir_sysdir] FOREIGN KEY([sysdir_id])
	REFERENCES [dbo].[sysdir] ([id])
	GO
	ALTER TABLE [dbo].[sysrole_sysdir] CHECK CONSTRAINT [FK_sysrole_sysdir_sysdir]
	GO
	ALTER TABLE [dbo].[sysrole_sysdir]  WITH CHECK ADD  CONSTRAINT [FK_sysrole_sysdir_sysrole] FOREIGN KEY([sysrole_id])
	REFERENCES [dbo].[sysrole] ([id])
	GO
	ALTER TABLE [dbo].[sysrole_sysdir] CHECK CONSTRAINT [FK_sysrole_sysdir_sysrole]
	GO
	ALTER TABLE [dbo].[sysuser_sysrole]  WITH CHECK ADD  CONSTRAINT [FK_sysuser_sysrole_sysrole] FOREIGN KEY([sysrole_id])
	REFERENCES [dbo].[sysrole] ([id])
	GO
	ALTER TABLE [dbo].[sysuser_sysrole] CHECK CONSTRAINT [FK_sysuser_sysrole_sysrole]
	GO
	ALTER TABLE [dbo].[sysuser_sysrole]  WITH CHECK ADD  CONSTRAINT [FK_sysuser_sysrole_sysuser] FOREIGN KEY([sysuser_id])
	REFERENCES [dbo].[sysuser] ([id])
	GO
	ALTER TABLE [dbo].[sysuser_sysrole] CHECK CONSTRAINT [FK_sysuser_sysrole_sysuser]
	GO
	*/
	#endregion

	protected void Page_Load(object sender, EventArgs e) {{
		//if (SessionManager.Id != null) Response.Redirect(""~/"");

		if (Request.HttpMethod.ToLower() == ""post"") {{
			username = Request.Form[""username""];
			password = Request.Form[""password""];

			if (string.IsNullOrEmpty(username)) {{
				msg = ""用户名不能为空。"";
				return;
			}}
			if (string.IsNullOrEmpty(password)) {{
				msg = ""密码不能为空。"";
				return;
			}}

			//SysuserInfo user = Sysuser.GetItemByUsername(username);
			//if (user == null || string.Compare(user.Password,
			//	FormsAuthentication.HashPasswordForStoringInConfigFile(password, ""md5""), true) != 0) {{
			//	msg = ""用户名或密码错误。"";
			//	return;
			//}}
			//SessionManager.Id = user.Id;
			//string backurl = Request.QueryString[""backurl""];
			//Response.Redirect(string.IsNullOrEmpty(backurl) ? ""~/"" : backurl);
		}}
	}}

	string msg;
	string username;
	string password;
</script>

<!DOCTYPE html>
<html>
<head>
	<meta charset=""utf-8"">
	<meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
	<title>{0}后台管理中心 - 登陆</title>
	<!-- Tell the browser to be responsive to screen width -->
	<meta content=""width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no"" name=""viewport"">
	<link rel=""stylesheet"" href=""./htm/bootstrap/css/bootstrap.min.css"">
	<link rel=""stylesheet"" href=""./htm/plugins/font-awesome/css/font-awesome.min.css"" />
	<link rel=""stylesheet"" href=""./htm/plugins/iCheck/square/blue.css"">
	<link rel=""stylesheet"" href=""./htm/css/system.css"">
	<script type=""text/javascript"" src=""./htm/js/jQuery-2.1.4.min.js""></script>
	<script type=""text/javascript"" src=""./htm/js/lib.js""></script>
	<!--[if lt IE 9]>
	<script type='text/javascript' src='./htm/plugins/html5shiv/html5shiv.min.js'></script>
	<script type='text/javascript' src='./htm/plugins/respond/respond.min.js'></script>
	<![endif]-->

<style type=""text/css"">
 .login-box-body--has-errors{{animation:shake .5s .25s 1;-webkit-animation:shake .5s .25s 1}}
 @keyframes shake{{0%,100%{{transform:translateX(0)}}20%,60%{{transform:translateX(-10px)}}40%,80%{{transform:translateX(10px)}}}}
 @-webkit-keyframes shake{{0%,100%{{-webkit-transform:translateX(0)}}20%,60%{{-webkit-transform:translateX(-10px)}}40%,80%{{-webkit-transform:translateX(10px)}}}}
</style>

</head>
<body class=""hold-transition login-page"">
	<div class=""login-box"">
		<div class=""login-logo"">
			<a href=""./""><b>{0}</b>后台管理中心</a>
		</div>

		<% if (!string.IsNullOrEmpty(msg)) {{ %>
		<div class=""alert alert-warning alert-dismissible"">
			<button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">×</button>
			<h4><i class=""icon fa fa-warning""></i>警告!</h4>
			<%= msg %>
		</div>
		<script type=""text/javascript"">
			$(document).ready(function () {{
				$('div.login-box-body').addClass('login-box-body--has-errors');
				setTimeout(function () {{
					$('div.login-box-body').removeClass('login-box-body--has-errors');
				}}, 2000);
			}});
		</script>
		<% }} %>

		<!-- /.login-logo -->
		<div class=""login-box-body"">
			<p class=""login-box-msg""></p>

			<form action="""" method=""post"">
				<div class=""form-group has-feedback"">
					<input name=""username"" type=""text"" class=""form-control"" placeholder=""Username"" value=""<%= username %>"">
					<span class=""glyphicon glyphicon-envelope form-control-feedback""></span>
				</div>
				<div class=""form-group has-feedback"">
					<input name=""password"" type=""password"" class=""form-control"" placeholder=""Password"" value=""<%= password %>"">
					<span class=""glyphicon glyphicon-lock form-control-feedback""></span>
				</div>
				<div class=""row"">
					<div class=""col-xs-8"">
						<div class=""checkbox icheck"">
							<label>
								<input type=""checkbox"" name=""remember"" value=""30"" />
								记住密码
							</label>
						</div>
					</div>
					<!-- /.col -->
					<div class=""col-xs-4"">
						<button type=""submit"" class=""btn btn-primary btn-block btn-flat"">登 陆</button>
					</div>
					<!-- /.col -->
				</div>
			</form>

		</div>
		<!-- /.login-box-body -->
	</div>
	<!-- /.login-box -->

	<!-- jQuery 2.2.0 -->
	<script src=""./htm/plugins/jQuery/jQuery-2.2.0.min.js""></script>
	<script src=""./htm/bootstrap/js/bootstrap.min.js""></script>
	<script src=""./htm/plugins/iCheck/icheck.min.js""></script>
	<script>
		$(function () {{
			$('input').iCheck({{
				checkboxClass: 'icheckbox_square-blue',
				radioClass: 'iradio_square-blue',
				increaseArea: '20%' // optional
			}});
		}});
</script>
</body>
</html>
";
			#endregion
			public static readonly string Admin_sln = Web_sln;
		}
	}
}
