using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Model;

namespace Server {

	internal partial class CodeBuild {
		public void SetOutput(bool[] outputs) {
			if (this._tables.Count == outputs.Length) {
				for (int a = 0; a < outputs.Length; a++) {
					this._tables[a].IsOutput = outputs[a];
				}
			}
		}

		public List<BuildInfo> Build(string solutionName, bool isSolution, bool isMakeAdmin, bool isDownloadRes) {
			Logger.remotor.Info("Build: " + solutionName + ",isSolution: " + isSolution + ",isMakeAdmin: " + isMakeAdmin + ",isDownloadRes: " + isDownloadRes + "(" + _client.Server + "," + _client.Username + "," + _client.Password + "," + _client.Database + ")");
			List<BuildInfo> loc1 = new List<BuildInfo>();

			//solutionName = CodeBuild.UFString(solutionName);
			string dbName = CodeBuild.UFString(CodeBuild.GetCSName(_client.Database));
			string connectionStringName = _client.Database + "ConnectionString";
			string basicName = "Build";

			string slnGuid = Guid.NewGuid().ToString().ToUpper();
			string dbGuid = Guid.NewGuid().ToString().ToUpper();
			string webGuid = Guid.NewGuid().ToString().ToUpper();
			string adminGuid = Guid.NewGuid().ToString().ToUpper();
			string commonGuid = Guid.NewGuid().ToString().ToUpper();

			string place3, files = string.Empty, pfiles = string.Empty;
			Dictionary<string, bool> isMakedHtmlSelect = new Dictionary<string, bool>();
			string admin_web_sitemap = string.Empty;
			StringBuilder Model_Build_ExtensionMethods_cs = new StringBuilder();
			List<string> admin_init_sysdir_aspx = new List<string>();

			StringBuilder sb1 = new StringBuilder();
			StringBuilder sb2 = new StringBuilder();
			StringBuilder sb3 = new StringBuilder();
			StringBuilder sb4 = new StringBuilder();
			StringBuilder sb5 = new StringBuilder();
			StringBuilder sb6 = new StringBuilder();
			StringBuilder sb7 = new StringBuilder();
			StringBuilder sb8 = new StringBuilder();
			StringBuilder sb9 = new StringBuilder();
			StringBuilder sb10 = new StringBuilder();
			StringBuilder sb11 = new StringBuilder();
			StringBuilder sb12 = new StringBuilder();
			StringBuilder sb13 = new StringBuilder();
			StringBuilder sb14 = new StringBuilder();
			StringBuilder sb15 = new StringBuilder();
			StringBuilder sb16 = new StringBuilder();
			StringBuilder sb17 = new StringBuilder();
			StringBuilder sb18 = new StringBuilder();
			StringBuilder sb19 = new StringBuilder();
			StringBuilder sb20 = new StringBuilder();
			StringBuilder sb21 = new StringBuilder();
			StringBuilder sb22 = new StringBuilder();
			StringBuilder sb23 = new StringBuilder();
			StringBuilder sb24 = new StringBuilder();
			StringBuilder sb25 = new StringBuilder();
			StringBuilder sb26 = new StringBuilder();
			StringBuilder sb27 = new StringBuilder();
			StringBuilder sb28 = new StringBuilder();
			StringBuilder sb29 = new StringBuilder();
			AnonymousHandler clearSb = delegate () {
				sb1.Remove(0, sb1.Length);
				sb2.Remove(0, sb2.Length);
				sb3.Remove(0, sb3.Length);
				sb4.Remove(0, sb4.Length);
				sb5.Remove(0, sb5.Length);
				sb6.Remove(0, sb6.Length);
				sb7.Remove(0, sb7.Length);
				sb8.Remove(0, sb8.Length);
				sb9.Remove(0, sb9.Length);
				sb10.Remove(0, sb10.Length);
				sb11.Remove(0, sb11.Length);
				sb12.Remove(0, sb12.Length);
				sb13.Remove(0, sb13.Length);
				sb14.Remove(0, sb14.Length);
				sb15.Remove(0, sb15.Length);
				sb16.Remove(0, sb16.Length);
				sb17.Remove(0, sb17.Length);
				sb18.Remove(0, sb18.Length);
				sb19.Remove(0, sb19.Length);
				sb20.Remove(0, sb20.Length);
				sb21.Remove(0, sb21.Length);
				sb22.Remove(0, sb22.Length);
				sb23.Remove(0, sb23.Length);
				sb24.Remove(0, sb24.Length);
				sb25.Remove(0, sb25.Length);
				sb26.Remove(0, sb26.Length);
				sb27.Remove(0, sb27.Length);
				sb28.Remove(0, sb28.Length);
				sb29.Remove(0, sb29.Length);
			};

			if (isSolution) {
				#region solution.sln
				sb1.AppendFormat(CONST.sln, slnGuid, solutionName, dbGuid, commonGuid);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, ".sln"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion

				#region Project Common
				#region BmwNet.cs
				sb1.AppendFormat(CONST.Common_BmwNet_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\BmwNet.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region IniHelper.cs
				sb1.AppendFormat(CONST.Common_IniHelper_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\IniHelper.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region GraphicHelper.cs
				sb1.AppendFormat(CONST.Common_GraphicHelper_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\GraphicHelper.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region JSDecoder.cs
				sb1.AppendFormat(CONST.Common_JSDecoder_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\JSDecoder.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region Lib.cs
				sb1.AppendFormat(CONST.Common_Lib_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\Lib.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region Logger.cs
				sb1.AppendFormat(CONST.Common_Logger_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\Logger.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion

				#region Http/BaseHttpProxy.cs
				sb1.AppendFormat(CONST.Common_Http_BaseHttpProxy_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\Http\BaseHttpProxy.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region Http/Deflate.cs
				sb1.AppendFormat(CONST.Common_Http_Deflate_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\Http\Deflate.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region Http/GZip.cs
				sb1.AppendFormat(CONST.Common_Http_GZip_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\Http\GZip.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region Http/HttpAccessException.cs
				sb1.AppendFormat(CONST.Common_Http_HttpAccessException_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\Http\HttpAccessException.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region Http/HttpCookie2.cs
				sb1.AppendFormat(CONST.Common_Http_HttpCookie2_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\Http\HttpCookie2.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region Http/HttpStream.cs
				sb1.AppendFormat(CONST.Common_Http_HttpStream_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\Http\HttpStream.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region Http/HttpRequest.cs
				sb1.AppendFormat(CONST.Common_Http_HttpRequest_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\Http\HttpRequest.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region Http/HttpResponse.cs
				sb1.AppendFormat(CONST.Common_Http_HttpResponse_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\Http\HttpResponse.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion

				#region WinFormClass/Socket/BaseSocket.cs
				sb1.AppendFormat(CONST.Common_WinFormClass_Socket_BaseSocket_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\WinFormClass\Socket\BaseSocket.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region WinFormClass/Socket/ClientSocket.cs
				sb1.AppendFormat(CONST.Common_WinFormClass_Socket_ClientSocket_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\WinFormClass\Socket\ClientSocket.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region WinFormClass/Socket/ServerSocket.cs
				sb1.AppendFormat(CONST.Common_WinFormClass_Socket_ServerSocket_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\WinFormClass\Socket\ServerSocket.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region WinFormClass/Robot.cs
				sb1.AppendFormat(CONST.Common_WinFormClass_Robot_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\WinFormClass\Robot.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region WinFormClass/WorkQueue.cs
				sb1.AppendFormat(CONST.Common_WinFormClass_WorkQueue_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\WinFormClass\WorkQueue.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion

				#region Project.csproj
				place3 = string.Format(CONST.Common_csproj);
				sb1.AppendFormat(CONST.csproj, commonGuid, @"Common", place3);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\Common.csproj"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#endregion
			}

			foreach (TableInfo table in _tables) {
				if (table.IsOutput == false) continue;
				if (table.Type == "P") continue;

				if (table.Uniques.Count == 0) {
					throw new Exception("检查到表 “" + table.Owner + "." + table.Name + "” 没有设定惟一键！");
				}

				#region commom variable define
				string uClass_Name = CodeBuild.UFString(table.ClassName);
				string nClass_Name = table.ClassName;
				string nTable_Name = "[" + table.Owner + "].[" + table.Name + "]";
				string Class_Name_BLL_Full = string.Format(@"{0}.BLL.{1}", solutionName, uClass_Name);
				string Class_Name_Model_Full = string.Format(@"{0}.Model.{1}", solutionName, uClass_Name);

				string pkCsParam = "";
				string pkCsParamNoType = "";
				string pkSqlParamFormat = "";
				string pkSqlParam = "";
				string pkSpNotNull = "";
				string pkEvalsQuerystring = "";
				string CsParam1 = "";
				string CsParamNoType1 = "";
				string CsParam2 = "";
				string CsParamNoType2 = "";
				string pkMvcRoute = "";
				string orderBy = table.Clustereds.Count > 0 ?
					string.Join(", ", table.Clustereds.ConvertAll<string>(delegate (ColumnInfo cli) {
						return "a.[" + cli.Name + "]" + (cli.Orderby == DataSort.ASC ? string.Empty : string.Concat(" ", cli.Orderby));
					}).ToArray()) :
					string.Join(", ", table.Uniques[0].ConvertAll<string>(delegate (ColumnInfo cli) {
						return "a.[" + cli.Name + "]" + (cli.Orderby == DataSort.ASC ? string.Empty : string.Concat(" ", cli.Orderby));
					}).ToArray());

				int pkSqlParamFormat_idx = -1;
				foreach (ColumnInfo columnInfo in table.PrimaryKeys) {
					pkCsParam += CodeBuild.GetCSType(columnInfo.Type) + " " + CodeBuild.UFString(columnInfo.Name) + ", ";
					pkCsParamNoType += CodeBuild.UFString(columnInfo.Name) + ", ";
					pkSqlParamFormat += "[" + columnInfo.Name + "] = {" + ++pkSqlParamFormat_idx + "} AND ";
					pkSqlParam += "[" + columnInfo.Name + "] = @" + columnInfo.Name + " AND ";
					pkSpNotNull += "NOT @" + columnInfo.Name + " IS NULL AND ";
					pkEvalsQuerystring += string.Format("{0}=<%# Eval(\"{0}\") %>&", CodeBuild.UFString(columnInfo.Name));
					pkMvcRoute += "{" + CodeBuild.UFString(columnInfo.Name) + "}/";
				}
				pkCsParam = pkCsParam.Substring(0, pkCsParam.Length - 2);
				pkCsParamNoType = pkCsParamNoType.Substring(0, pkCsParamNoType.Length - 2);
				pkSqlParamFormat = pkSqlParamFormat.Substring(0, pkSqlParamFormat.Length - 5);
				pkSqlParam = pkSqlParam.Substring(0, pkSqlParam.Length - 5);
				pkSpNotNull = pkSpNotNull.Substring(0, pkSpNotNull.Length - 5);
				pkEvalsQuerystring = pkEvalsQuerystring.Substring(0, pkEvalsQuerystring.Length - 1);
				foreach (ColumnInfo columnInfo in table.Columns) {
					CsParam1 += CodeBuild.GetCSType(columnInfo.Type) + " " + CodeBuild.UFString(columnInfo.Name) + ", ";
					CsParamNoType1 += CodeBuild.UFString(columnInfo.Name) + ", ";
					if (columnInfo.IsIdentity) {
						CsParamNoType2 += "null, ";
					} else {
						CsParam2 += CodeBuild.GetCSType(columnInfo.Type) + " " + CodeBuild.UFString(columnInfo.Name) + ", ";
						CsParamNoType2 += CodeBuild.UFString(columnInfo.Name) + ", ";
					}
				}
				CsParam1 = CsParam1.Substring(0, CsParam1.Length - 2);
				CsParamNoType1 = CsParamNoType1.Substring(0, CsParamNoType1.Length - 2);
				if (CsParam2.Length > 0) CsParam2 = CsParam2.Substring(0, CsParam2.Length - 2);
				if (CsParamNoType2.Length > 0) CsParamNoType2 = CsParamNoType2.Substring(0, CsParamNoType2.Length - 2);
				#endregion

				files += string.Format(@"		<Compile Include=""|2|\{0}\|0|" + uClass_Name + "|1|.cs\" />\r\n", basicName);

				#region Model *.cs
				sb1.AppendFormat(
	@"using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace {0}.Model {{

	public partial class {1}Info {{
		#region fields
", solutionName, uClass_Name);
				int column_idx = -1;
				foreach (ColumnInfo column in table.Columns) {
					column_idx++;
					string csType = CodeBuild.GetCSType(column.Type);
					string nColumn_Name = column.Name;
					string uColumn_Name = CodeBuild.UFString(column.Name);

					sb1.AppendFormat(
	@"		private {0} _{1};
", csType, uColumn_Name);

					string tmpinfo = string.Empty;
					List<string> tsvarr = new List<string>();
					List<ForeignKeyInfo> fks = table.ForeignKeys.FindAll(delegate (ForeignKeyInfo fk) {
						int fkc1idx = 0;
						string fkcsBy = "By";
						string fkcsParms = string.Empty;
						ColumnInfo fkc = fk.Columns.Find(delegate (ColumnInfo c1) {
							fkc1idx++;
							fkcsParms += "_" + CodeBuild.UFString(c1.Name) + ", ";
							return c1.Name == column.Name;
						});
						if (fk.ReferencedTable != null) {
							fk.ReferencedColumns.ForEach(delegate (ColumnInfo c1) {
								fkcsBy += CodeBuild.UFString(c1.Name) + "And";
							});
						} else {
							fk.ReferencedColumnNames.ForEach(delegate (string c1) {
								fkcsBy += CodeBuild.UFString(c1) + "And";
							});
						}
						if (fkc == null) return false;
						string FK_uClass_Name = fk.ReferencedTable != null ? CodeBuild.UFString(fk.ReferencedTable.ClassName) :
							CodeBuild.UFString(TableInfo.GetClassName(fk.ReferencedTableName));
						string FK_uClass_Name_full = fk.ReferencedTable != null ? FK_uClass_Name :
							string.Format(@"{0}.Model.{1}", solutionName, FK_uClass_Name);
						string FK_uEntry_Name = fk.ReferencedTable != null ? CodeBuild.GetCSName(fk.ReferencedTable.Name) :
							CodeBuild.GetCSName(TableInfo.GetEntryName(fk.ReferencedTableName));
						string tableNamefe3 = fk.ReferencedTable != null ? fk.ReferencedTable.Name :
							FK_uEntry_Name;
						string memberName = fk.Columns[0].Name.IndexOf(tableNamefe3) == -1 ? CodeBuild.LFString(tableNamefe3) :
							(CodeBuild.LFString(fk.Columns[0].Name.Substring(0, fk.Columns[0].Name.IndexOf(tableNamefe3)) + tableNamefe3));

						tsvarr.Add(string.Format(@"_obj_{0} = null;", memberName));
						if (fkc1idx == fk.Columns.Count) {
							fkcsBy = fkcsBy.Remove(fkcsBy.Length - 3);
							fkcsParms = fkcsParms.Remove(fkcsParms.Length - 2);
							if (fk.ReferencedColumns.Count > 0 && fk.ReferencedColumns[0].IsPrimaryKey ||
								fk.ReferencedTable == null && fk.ReferencedIsPrimaryKey) {
								fkcsBy = string.Empty;
							}
							sb1.AppendFormat(
		@"		private {0}Info _obj_{1};
", FK_uClass_Name_full, memberName);
							tmpinfo += string.Format(
		@"		public {0}Info Obj_{1} {{
			get {{
				if (_obj_{1} == null) _obj_{1} = {2}.BLL.{5}.GetItem{3}({4});
				return _obj_{1};
			}}
			internal set {{ _obj_{1} = value; }}
		}}
", FK_uClass_Name_full, memberName, solutionName, fkcsBy, fkcsParms, FK_uClass_Name);
						}
						return fkc != null;
					});
					if (fks.Count > 0) {
						string tmpsetvalue = string.Format(
@"		public {0} {1} {{
			get {{ return _{1}; }}
			set {{
				if (_{1} != value) ", csType, uColumn_Name);
						string tsvstr = string.Join(@"
					", tsvarr.ToArray());
						if (fks.Count > 1) {
							tmpsetvalue += string.Format(@"{{
					{0}
				}}", tsvstr);
						} else {
							tmpsetvalue += tsvstr;
						}
						tmpsetvalue += string.Format(@"
				_{0} = value;
			}}
		}}
", uColumn_Name);
						sb2.Append(tmpsetvalue);
						sb2.Append(tmpinfo);
					} else {
						sb2.AppendFormat(
	@"		public {0} {1} {{
			get {{ return _{1}; }}
			set {{ _{1} = value; }}
		}}
", csType, uColumn_Name);
					}
					sb3.AppendFormat("{0} {1}, ", csType, uColumn_Name);
					sb4.AppendFormat(
	@"			_{0} = {0};
", uColumn_Name);
					//sb5.AppendFormat(@" {0} : {1}, ", uColumn_Name, CodeBuild.GetToStringFieldConcat(column));
					sb5.AppendFormat(@"
				__jsonIgnore.ContainsKey(""{0}"") ? string.Empty : string.Format("", {0} : {{0}}"", {1}), ", uColumn_Name, CodeBuild.GetToStringFieldConcat(column));
					sb10.AppendFormat(@"
			if (!__jsonIgnore.ContainsKey(""{0}"")) ht[""{0}""] = {0};", uColumn_Name);
					sb7.AppendFormat(@"
				{0}, ""|"",", GetToStringStringify(column));
                    sb8.AppendFormat(@"
			if (string.Compare(""null"", ret[{2}]) != 0) _{0} = {1};", 
                        uColumn_Name, string.Format(CodeBuild.GetStringifyParse(column.Type), "ret[" + column_idx +"]"), column_idx);
                }

				if (sb2.Length != 0) {
					sb2.Remove(sb2.Length - 2, 2);
					sb3.Remove(sb3.Length - 2, 2);
					sb5.Remove(sb5.Length - 2, 2);
					sb7.Remove(sb7.Length - 6, 6);
				}

				Dictionary<string, string> dic_objs = new Dictionary<string, string>();
				// m -> n
				_tables.ForEach(delegate (TableInfo t2) {
					if (t2.ForeignKeys.Count > 2) {
						foreach(TableInfo t3 in _tables) {
							if (t3.FullName == t2.FullName) continue;
							ForeignKeyInfo fk3 = t3.ForeignKeys.Find(delegate (ForeignKeyInfo ffk3) {
								return ffk3.ReferencedTable.FullName == t2.FullName;
							});
							if (fk3 != null) {
								if (fk3.Columns[0].IsPrimaryKey) return;
							}
						}
					}
					ForeignKeyInfo fk_Common = null;
					ForeignKeyInfo fk = t2.ForeignKeys.Find(delegate (ForeignKeyInfo ffk) {
						if (ffk.ReferencedTable.FullName == table.FullName/* && 
							ffk.Table.FullName != table.FullName*/) { //注释这行条件为了增加 parent_id 的 obj 对象
							fk_Common = ffk;
							return true;
						}
						return false;
					});
					if (fk == null) return;
					//if (fk.Table.FullName == table.FullName) return; //注释这行条件为了增加 parent_id 的 obj 对象
					List<ForeignKeyInfo> fk2 = t2.ForeignKeys.FindAll(delegate (ForeignKeyInfo ffk2) {
						return ffk2 != fk;
					});
					// 1 -> 1
					ForeignKeyInfo fk1v1 = table.ForeignKeys.Find(delegate (ForeignKeyInfo ffk2) {
						return ffk2.ReferencedTable.FullName == t2.FullName
							&& ffk2.ReferencedColumns[0].IsPrimaryKey && ffk2.Columns[0].IsPrimaryKey; //这行条件为了增加 parent_id 的 obj 对象
					});
					if (fk1v1 != null) return;

					//t2.Columns
					string t2name = t2.Name;
					string tablename = table.Name;
					string addname = t2name;
					if (t2name.StartsWith(tablename + "_")) {
						addname = t2name.Substring(tablename.Length + 1);
					} else if (t2name.EndsWith("_" + tablename)) {
						addname = t2name.Remove(addname.Length - tablename.Length - 1);
					} else if (fk2.Count == 1 && t2name.EndsWith("_" + tablename)) {
						addname = t2name.Remove(t2name.Length - tablename.Length - 1);
					} else if (fk2.Count == 1 && t2name.EndsWith("_" + fk2[0].ReferencedTable.Name)) {
						addname = t2name;
					}

					string parms1 = "";
					string parmsNoneType1 = "";
					string parms2 = "";
					string parmsNoneType2 = "";
					string parms3 = "";
					string parmsNoneType3 = "";
					string parms4 = "";
					string parmsNoneType4 = "";
					string pkNamesNoneType = "";
					string updateDiySet = "";
					string add_or_flag = "Add";
					int ms = 0;
					foreach (ColumnInfo columnInfo in t2.Columns) {
						if (columnInfo.Name == fk.Columns[0].Name) {
							parmsNoneType2 += "this." + CodeBuild.UFString(table.PrimaryKeys[0].Name) + ", ";
							parmsNoneType4 += "this." + CodeBuild.UFString(table.PrimaryKeys[0].Name) + ", ";
							if (columnInfo.IsPrimaryKey) pkNamesNoneType += "this." + CodeBuild.UFString(table.PrimaryKeys[0].Name) + ", ";
							continue;
						}
						if (columnInfo.IsPrimaryKey) pkNamesNoneType += CodeBuild.UFString(columnInfo.Name) + ", ";
						else updateDiySet += string.Format("\r\n\t\t\t\t\t.Set{0}({0})", CodeBuild.UFString(columnInfo.Name));

						if (columnInfo.IsIdentity)
							continue;
						parms2 += CodeBuild.GetCSType(columnInfo.Type) + " " + CodeBuild.UFString(columnInfo.Name) + ", ";
						parmsNoneType2 += CodeBuild.UFString(columnInfo.Name) + ", ";

						ForeignKeyInfo fkk3 = t2.ForeignKeys.Find(delegate (ForeignKeyInfo fkk33) {
							return fkk33.Columns[0].Name == columnInfo.Name;
						});
						if (fkk3 == null) {
							parms1 += CodeBuild.GetCSType(columnInfo.Type) + " " + CodeBuild.UFString(columnInfo.Name) + ", ";
							parmsNoneType1 += CodeBuild.UFString(columnInfo.Name) + ", ";
						} else {
							string fkk3_ReferencedTable_ObjName = fkk3.ReferencedTable.Name;
							string endStr = "_" + fkk3.ReferencedTable.Name + "_" + fkk3.ReferencedColumns[0].Name;
							if (columnInfo.Name.EndsWith(endStr))
								fkk3_ReferencedTable_ObjName = columnInfo.Name.Remove(columnInfo.Name.Length - fkk3.ReferencedColumns[0].Name.Length - 1);

							fkk3_ReferencedTable_ObjName = CodeBuild.UFString(fkk3_ReferencedTable_ObjName);
							parms1 += CodeBuild.UFString(fkk3.ReferencedTable.ClassName) + "Info " + fkk3_ReferencedTable_ObjName + ", ";
							parmsNoneType1 += fkk3_ReferencedTable_ObjName + "." + CodeBuild.UFString(fkk3.ReferencedColumns[0].Name) + ", ";
							parms3 += CodeBuild.UFString(fkk3.ReferencedTable.ClassName) + "Info " + fkk3_ReferencedTable_ObjName + ", ";
							parmsNoneType3 += fkk3_ReferencedTable_ObjName + "." + CodeBuild.UFString(fkk3.ReferencedColumns[0].Name) + ", ";

							parms4 += CodeBuild.GetCSType(columnInfo.Type) + " " + CodeBuild.UFString(columnInfo.Name) + ", ";
							parmsNoneType4 += CodeBuild.UFString(columnInfo.Name) + ", ";
							if (add_or_flag != "Flag" && fk.Columns[0].IsPrimaryKey) //中间表关系键，必须为主键
								t2.Uniques.ForEach(delegate (List<ColumnInfo> cs) {
									if (cs.Count < 2) return;
									ms = 0;
									foreach (ColumnInfo c in cs) {
										if (t2.ForeignKeys.Find(delegate (ForeignKeyInfo ffkk2) {
											return ffkk2.Columns[0].Name == c.Name;
										}) != null) ms++;
									}
									if (ms == cs.Count) {
										add_or_flag = "Flag";
									}
								});
						}
					}
					if (parms1.Length > 0) parms1 = parms1.Remove(parms1.Length - 2);
					if (parmsNoneType1.Length > 0) parmsNoneType1 = parmsNoneType1.Remove(parmsNoneType1.Length - 2);
					if (parms2.Length > 0) parms2 = parms2.Remove(parms2.Length - 2);
					if (parmsNoneType2.Length > 0) parmsNoneType2 = parmsNoneType2.Remove(parmsNoneType2.Length - 2);
					if (parms3.Length > 0) parms3 = parms3.Remove(parms3.Length - 2);
					if (parmsNoneType3.Length > 0) parmsNoneType3 = parmsNoneType3.Remove(parmsNoneType3.Length - 2);
					if (parms4.Length > 0) parms4 = parms4.Remove(parms4.Length - 2);
					if (parmsNoneType4.Length > 0) parmsNoneType4 = parmsNoneType4.Remove(parmsNoneType4.Length - 2);
					if (pkNamesNoneType.Length > 0) pkNamesNoneType = pkNamesNoneType.Remove(pkNamesNoneType.Length - 2);

					if (add_or_flag == "Flag") {
						if (parms1 != parms2)
							sb6.AppendFormat(@"
		public {0}Info Flag{1}({2}) {{
			return Flag{1}({3});
		}}", CodeBuild.UFString(t2.ClassName), CodeBuild.UFString(addname), parms1, parmsNoneType1);
						sb6.AppendFormat(@"
		public {0}Info Flag{1}({2}) {{
			{0}Info item = {4}.BLL.{0}.GetItem({5});
			if (item == null) item = {4}.BLL.{0}.Insert({3});{6}
			return item;
		}}
", CodeBuild.UFString(t2.ClassName), CodeBuild.UFString(addname), parms2, parmsNoneType2, solutionName, pkNamesNoneType, updateDiySet.Length > 0 ? "\r\n\t\t\telse item.UpdateDiy" + updateDiySet + ".ExecuteNonQuery();" : string.Empty);
					} else {
						if (parms1 != parms2)
							sb6.AppendFormat(@"
		public {0}Info Add{1}({2}) {{
			return Add{1}({3});
		}}", CodeBuild.UFString(t2.ClassName), CodeBuild.UFString(addname), parms1, parmsNoneType1);
						sb6.AppendFormat(@"
		public {0}Info Add{1}({2}) {{
			return {4}.BLL.{0}.Insert({3});
		}}
", CodeBuild.UFString(t2.ClassName), CodeBuild.UFString(addname), parms2, parmsNoneType2, solutionName);
					}

					if (add_or_flag == "Flag") {
						string deleteByUniqui = string.Empty;
						for (int deleteByUniqui_a = 0; deleteByUniqui_a < fk.Table.Uniques.Count; deleteByUniqui_a++)
							if (fk.Table.Uniques[deleteByUniqui_a].Count > 1 && fk.Table.Uniques[deleteByUniqui_a][0].IsPrimaryKey == false) {
								foreach (ColumnInfo deleteByuniquiCol in fk.Table.Uniques[deleteByUniqui_a])
									deleteByUniqui = deleteByUniqui + "And" + CodeBuild.UFString(deleteByuniquiCol.Name);
								deleteByUniqui = "By" + deleteByUniqui.Substring(3);
								break;
							}
						sb6.AppendFormat(@"
		public int Unflag{1}({2}) {{
			return Unflag{1}({3});
		}}
		public int Unflag{1}({4}) {{
			return {6}.BLL.{0}.Delete{9}({5});
		}}
		public int Unflag{1}ALL() {{
			return {6}.BLL.{0}.DeleteBy{8}(this.{7});
		}}
", CodeBuild.UFString(t2.ClassName), CodeBuild.UFString(addname), parms3, parmsNoneType3, parms4, parmsNoneType4,
	solutionName, CodeBuild.UFString(table.PrimaryKeys[0].Name), CodeBuild.UFString(fk.Columns[0].Name), deleteByUniqui);

						if (ms > 2) {

						} else {
							string civ = string.Format(GetCSTypeValue(table.PrimaryKeys[0].Type), "_" + CodeBuild.UFString(table.PrimaryKeys[0].Name));
							string f5 = tablename;
							if (addname != f5) {
								string fk20_ReferencedTable_Name = fk2[0].ReferencedTable.Name;
								string fk_ReferencedTable_Name = fk.ReferencedTable.Name;
								if (addname.EndsWith("_" + fk20_ReferencedTable_Name))
									f5 = addname.Remove(addname.Length - fk20_ReferencedTable_Name.Length - 1);
								else if (string.Compare(t2name, fk20_ReferencedTable_Name + "_" + fk_ReferencedTable_Name) != 0 &&
									string.Compare(t2name, fk_ReferencedTable_Name + "_" + fk20_ReferencedTable_Name) != 0)
									f5 = t2name;
							}
							string objs_value = string.Format(@"
		private List<{0}Info> _obj_{1}s;
		public List<{0}Info> Obj_{1}s {{
			get {{
				if (_obj_{1}s == null) _obj_{1}s = {2}.BLL.{0}.SelectBy{5}_{4}({3}).ToList();
				return _obj_{1}s;
			}}
		}}", CodeBuild.UFString(fk2[0].ReferencedTable.ClassName), CodeBuild.LFString(addname), solutionName, civ, table.PrimaryKeys[0].Name, CodeBuild.UFString(f5));
							string objs_key = string.Format("Obj_{0}s", CodeBuild.LFString(addname));
							if (dic_objs.ContainsKey(objs_key))
								dic_objs[objs_key] = objs_value;
							else
								dic_objs.Add(objs_key, objs_value);
						}
					} else {
						string f2 = fk.Columns[0].Name.CompareTo("parent_id") == 0 ? t2name : fk.Columns[0].Name.Replace(tablename + "_" + table.PrimaryKeys[0].Name, "") + CodeBuild.LFString(t2name);
						string objs_value = string.Format(@"
		private List<{0}Info> _obj_{1}s;
		public List<{0}Info> Obj_{1}s {{
			get {{
				if (_obj_{1}s == null) _obj_{1}s = {2}.BLL.{0}.SelectBy{3}(_{4}).Limit(500).ToList();
				return _obj_{1}s;
			}}
		}}", CodeBuild.UFString(t2.ClassName), f2, solutionName, CodeBuild.UFString(fk.Columns[0].Name), CodeBuild.UFString(table.PrimaryKeys[0].Name));
						string objs_key = string.Format("Obj_{0}s", f2);
						if (!dic_objs.ContainsKey(objs_key))
							dic_objs.Add(objs_key, objs_value);
					}
				});
				string[] dic_objs_values = new string[dic_objs.Count];
				dic_objs.Values.CopyTo(dic_objs_values, 0);
				sb9.Append(string.Join("", dic_objs_values));

				sb6.Insert(0, string.Format(@"
		public {0}.DAL.{1}.SqlUpdateBuild UpdateDiy {{
			get {{ return {0}.BLL.{1}.UpdateDiy(this, _{2}); }}
		}}", solutionName, uClass_Name, pkCsParamNoType.Replace(", ", ", _")));

				sb1.AppendFormat(
	@"		#endregion

		public {0}Info() {{ }}

		public {0}Info({1}) {{
{2}		}}

		#region 独创的序列化，反序列化
		protected static readonly string StringifySplit = ""@<{0}(Info]?#>"";
		public string Stringify() {{
			return string.Concat({7});
		}}
		public {0}Info(string stringify) {{
			string[] ret = stringify.Split(new char[] {{ '|' }}, {6}, StringSplitOptions.None);
			if (ret.Length != {6}) throw new Exception(""格式不正确，{0}Info："" + stringify);{8}
		}}
		#endregion

		#region override
		private static Dictionary<string, bool> __jsonIgnore;
		private static object __jsonIgnore_lock = new object();
		public override string ToString() {{
			this.Init__jsonIgnore();
			string json = string.Concat({3}, "" }}"");
			return string.Concat(""{{"", json.Substring(1));
		}}
		public IDictionary ToBson() {{
			this.Init__jsonIgnore();
			IDictionary ht = new Hashtable();{10}
			return ht;
		}}
		private void Init__jsonIgnore() {{
			if (__jsonIgnore == null) {{
				lock (__jsonIgnore_lock) {{
					if (__jsonIgnore == null) {{
						FieldInfo field = typeof({0}Info).GetField(""JsonIgnore"");
						__jsonIgnore = new Dictionary<string, bool>();
						if (field != null) {{
							string[] fs = string.Concat(field.GetValue(null)).Split(',');
							foreach (string f in fs) if (!string.IsNullOrEmpty(f)) __jsonIgnore[f] = true;
						}}
					}}
				}}
			}}
		}}
		public override bool Equals(object obj) {{
			{0}Info item = obj as {0}Info;
			if (item == null) return false;
			return this.ToString().Equals(item.ToString());
		}}
		public override int GetHashCode() {{
			return this.ToString().GetHashCode();
		}}
		public static bool operator ==({0}Info op1, {0}Info op2) {{
			if (object.Equals(op1, null)) return object.Equals(op2, null);
			return op1.Equals(op2);
		}}
		public static bool operator !=({0}Info op1, {0}Info op2) {{
			return !(op1 == op2);
		}}
		public object this[string key] {{
			get {{ return this.GetType().GetProperty(key).GetValue(this, null); }}
			set {{ this.GetType().GetProperty(key).SetValue(this, value, null); }}
		}}
		#endregion

		#region properties
{4}{9}
		#endregion
{5}
	}}
}}

", uClass_Name, sb3.ToString(), sb4.ToString(), sb5.ToString(), sb2.ToString(), sb6.ToString(), table.Columns.Count, sb7.ToString(), sb8.ToString(), sb9.ToString(), sb10.ToString());

				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, @".db\Model\", basicName, @"\", uClass_Name, "Info.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();

				Model_Build_ExtensionMethods_cs.AppendFormat(@"
		public static string ToJson(this {0}Info item) {{ return string.Concat(item); }}
		public static string ToJson(this {0}Info[] items) {{ return GetJson(items); }}
		public static string ToJson(this IEnumerable<{0}Info> items) {{ return GetJson(items); }}
		public static IDictionary[] ToBson(this {0}Info[] items) {{ return GetBson(items); }}
		public static IDictionary[] ToBson(this IEnumerable<{0}Info> items) {{ return GetBson(items); }}
", uClass_Name);
				#endregion

				#region DAL *.cs

				#region use t-sql
				string sqlTable = "declare @table table(";
				string sqlFields = "";
				string sqlDelete = string.Format("DELETE FROM {0} ", nTable_Name);
				string sqlUpdate = string.Format("UPDATE {0} SET ", nTable_Name);
				string sqlInsert = string.Format("INSERT INTO {0}(", nTable_Name);
				string sqlSelect = string.Format("SELECT <top> \" + GetFields(null) + \"");
				string temp1 = string.Empty;
				string temp2 = string.Empty;
				string temp3 = string.Empty;
				string temp4 = string.Empty;
				foreach (ColumnInfo columnInfo in table.Columns) {
					if (columnInfo.IsIdentity == false) {
						temp1 += string.Format("[{0}] = @{0}, ", columnInfo.Name);
						temp2 += string.Format("[{0}], ", columnInfo.Name);
						temp3 += string.Format("@{0}, ", columnInfo.Name);
					}
					temp4 += string.Format("a.[{0}], ", columnInfo.Name);
					sqlTable += string.Format("[{0}] {1},", columnInfo.Name, columnInfo.SqlType);
				}
				temp1 = temp1.Substring(0, temp1.Length - 2);
				temp2 = temp2.Substring(0, temp2.Length - 2);
				temp3 = temp3.Substring(0, temp3.Length - 2);
				temp4 = temp4.Substring(0, temp4.Length - 2);
				sqlTable = sqlTable.Substring(0, sqlTable.Length - 1) + ")\\r\\n";
				sqlFields = temp4;
				sqlDelete += "WHERE ";
				sqlUpdate += temp1 + string.Format(" WHERE {0}", pkSqlParam);
				sqlInsert += string.Format("{0}) OUTPUT \" + Field.Replace(\"a.\", \"INSERTED.\") + \" INTO @table VALUES({1})\\r\\nselect * from @table", temp2, temp3);
				sqlSelect += string.Format(", row_number() over(<order by>) AS rownum FROM {0}", nTable_Name);

				temp1 = "";
				temp2 = "";
				temp3 = "";
				temp4 = "";

				sb1.AppendFormat(
	@"using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using {0}.Model;

namespace {0}.DAL {{

	public partial class {1} : SqlHelper.IDAL {{
		#region transact-sql define
		public string Table {{ get {{ return TSQL.Table; }} }}
		public string Field {{ get {{ return TSQL.Field; }} }}
		public string Sort {{ get {{ return TSQL.Sort; }} }}
		internal class TSQL {{
			internal static readonly string Table = ""{3}"";
			internal static readonly string Field = ""{5}"";
			internal static readonly string Sort = ""{6}"";
			public static readonly string Delete = ""DELETE FROM {3} WHERE "";
			public static readonly string Insert = ""{2}{4}"";
		}}
		#endregion

		#region common call
		protected static SqlParameter GetParameter(string name, SqlDbType type, int size, object value) {{
			SqlParameter parm = new SqlParameter(name, type, size);
			parm.Value = value;
			return parm;
		}}
		protected static SqlParameter[] GetParameters({1}Info item) {{
			return new SqlParameter[] {{
{7}}};
		}}", solutionName, uClass_Name, sqlTable, nTable_Name, sqlInsert, sqlFields, orderBy, CodeBuild.AppendParameters(table, "				"));

				/*
				//下面是 DataReader 块，但是和 SqlHelper.Transcation 有冲突，以后统一使用 ExecuteDataSet
				sb1.AppendFormat(@"
		public {0}Info GetItem(IDataReader dr) {{
			int index = -1;
			return GetItem(dr, ref index) as {0}Info;
		}}
		public object GetItem(IDataReader dr, ref int index) {{
			return new {0}Info(", uClass_Name);

				foreach (ColumnInfo columnInfo in table.Columns) {
					if (columnInfo.Type == SqlDbType.Image ||
						columnInfo.Type == SqlDbType.Binary ||
						columnInfo.Type == SqlDbType.VarBinary) {
						if (sb4.Length == 0) {
							sb4.AppendFormat(@"
		public byte[] GetBytes(IDataReader dr, int index) {{
			if (dr.IsDBNull(index)) return null;
			using(MemoryStream ms = new MemoryStream()) {{
				byte[] bt = new byte[1048576 * 8];
				int size = 0;
				while ((size = (int)dr.GetBytes(index, ms.Position, bt, 0, bt.Length)) > 0) ms.Write(bt, 0, size);
				return ms.ToArray();
			}}
		}}");
						}
						sb1.AppendFormat(
	@"
				GetBytes(dr, ++index), ");
					} else {
						sb1.AppendFormat(
	@"
				dr.IsDBNull(++index) ? null : {0}dr.{1}(index), ", CodeBuild.GetDbToCsConvert(columnInfo.Type), CodeBuild.GetDataReaderMethod(columnInfo.Type));
					}
				}
				sb1 = sb1.Remove(sb1.Length - 2, 2);
				sb1.AppendFormat(@");
		}}");
				*/

				sb1.AppendFormat(@"
{1}
		public {0}Info GetItem(DataRow dr) {{
			int index = -1;
			return GetItem(dr, ref index) as {0}Info;
		}}
		public object GetItem(DataRow dr, ref int index) {{
			return new {0}Info(", uClass_Name, sb4.ToString());
				foreach (ColumnInfo columnInfo in table.Columns) {
					sb1.AppendFormat(
	@"
				dr[++index] == DBNull.Value ? null : {0}dr[index], ", CodeBuild.GetDbToCsConvert(columnInfo.Type));
				}
				sb1 = sb1.Remove(sb1.Length - 2, 2);

				sb1.AppendFormat(@");
		}}

		public SqlHelper.SelectBuild<{0}Info> Select {{
			get {{ return SqlHelper.SelectBuild<{0}Info>.From(this); }}
		}}
		#endregion", uClass_Name, table.Columns.Count + 1);
				Dictionary<string, bool> del_exists = new Dictionary<string, bool>();
				foreach (List<ColumnInfo> cs in table.Uniques) {
					string parms = string.Empty;
					string parmsBy = "By";
					string sqlParms = string.Empty;
					string sqlParmsA = string.Empty;
					string sqlParmsANoneType = string.Empty;
					int sqlParmsAIndex = 0;
					foreach (ColumnInfo columnInfo in cs) {
						parms += CodeBuild.GetCSType(columnInfo.Type) + " " + CodeBuild.UFString(columnInfo.Name) + ", ";
						parmsBy += CodeBuild.UFString(columnInfo.Name) + "And";
						sqlParms += "[" + columnInfo.Name + "] = @" + columnInfo.Name + " AND ";
						sqlParmsA += "a.[" + columnInfo.Name + "] = {" + sqlParmsAIndex++ + "} AND ";
						sqlParmsANoneType += CodeBuild.UFString(columnInfo.Name) + ", ";
					}
					parms = parms.Substring(0, parms.Length - 2);
					parmsBy = parmsBy.Substring(0, parmsBy.Length - 3);
					sqlParms = sqlParms.Substring(0, sqlParms.Length - 5);
					sqlParmsA = sqlParmsA.Substring(0, sqlParmsA.Length - 5);
					sqlParmsANoneType = sqlParmsANoneType.Substring(0, sqlParmsANoneType.Length - 2);
					del_exists.Add(parms, true);
					sb2.AppendFormat(@"
		public int Delete{2}({0}) {{
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, ""{1}""), 
{3});
		}}", parms, sqlParms, cs[0].IsPrimaryKey ? string.Empty : parmsBy, CodeBuild.AppendParameters(cs, "				"));

					sb3.AppendFormat(@"
		public {0}Info GetItem{3}({1}) {{
			return this.Select.Where(""{2}"", {4}).ToOne();
		}}", uClass_Name, parms, sqlParmsA, cs[0].IsPrimaryKey ? string.Empty : parmsBy, sqlParmsANoneType);
				}
				table.ForeignKeys.ForEach(delegate (ForeignKeyInfo fkk) {
					string parms = string.Empty;
					string parmsBy = "By";
					string sqlParms = string.Empty;
					foreach (ColumnInfo columnInfo in fkk.Columns) {
						parms += CodeBuild.GetCSType(columnInfo.Type) + " " + CodeBuild.UFString(columnInfo.Name) + ", ";
						parmsBy += CodeBuild.UFString(columnInfo.Name) + "And";
						sqlParms += "[" + columnInfo.Name + "] = @" + columnInfo.Name + " AND ";
					}
					parms = parms.Substring(0, parms.Length - 2);
					parmsBy = parmsBy.Substring(0, parmsBy.Length - 3);
					sqlParms = sqlParms.Substring(0, sqlParms.Length - 5);
					if (del_exists.ContainsKey(parms)) return;
					del_exists.Add(parms, true);

					sb2.AppendFormat(@"
		public int Delete{2}({0}) {{
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, ""{1}""), 
{3});
		}}", parms, sqlParms, parmsBy, CodeBuild.AppendParameters(fkk.Columns, "				"));
				});

				foreach (ColumnInfo columnInfo in table.Columns) {
					if (columnInfo.IsIdentity ||
						columnInfo.IsPrimaryKey ||
						table.PrimaryKeys.FindIndex(delegate (ColumnInfo pkf) { return pkf.Name == columnInfo.Name; }) != -1) continue;
					string valueParm = CodeBuild.AppendParameters(columnInfo, "");
					valueParm = valueParm.Remove(valueParm.LastIndexOf(", ") + 2);
					sb5.AppendFormat(@"
			public SqlUpdateBuild Set{0}({2} value) {{
				if (_item != null) _item.{0} = value;
				return this.Set(""[{1}]"", string.Concat(""@{1}_"", _parameters.Count), 
					{3}value));
			}}", CodeBuild.UFString(columnInfo.Name), columnInfo.Name, CodeBuild.GetCSType(columnInfo.Type), valueParm.Replace("\"@" + columnInfo.Name + "\"", "string.Concat(\"@" + columnInfo.Name + "_\", _parameters.Count)"));
					if ((columnInfo.Type == SqlDbType.BigInt ||
						columnInfo.Type == SqlDbType.Decimal ||
						columnInfo.Type == SqlDbType.Float ||
						columnInfo.Type == SqlDbType.Int ||
						columnInfo.Type == SqlDbType.Money ||
						columnInfo.Type == SqlDbType.Real ||
						columnInfo.Type == SqlDbType.SmallInt ||
						columnInfo.Type == SqlDbType.SmallMoney ||
						columnInfo.Type == SqlDbType.TinyInt) && 
						table.ForeignKeys.FindIndex(delegate(ForeignKeyInfo fkf) { return fkf.Columns.FindIndex(delegate (ColumnInfo fkfpkf) { return fkfpkf.Name == columnInfo.Name; }) != -1; }) == -1) {
						
						sb5.AppendFormat(@"
			public SqlUpdateBuild Set{0}Increment({2} value) {{
				if (_item != null) _item.{0} += value;
				return this.Set(""[{1}]"", string.Concat(""[{1}] + @{1}_"", _parameters.Count), 
					{3}value));
			}}", CodeBuild.UFString(columnInfo.Name), columnInfo.Name, CodeBuild.GetCSType(columnInfo.Type), valueParm.Replace("\"@" + columnInfo.Name + "\"", "string.Concat(\"@" + columnInfo.Name + "_\", _parameters.Count)"));
					}
					sb6.AppendFormat(@"
				.Set{0}(item.{0})", CodeBuild.UFString(columnInfo.Name));
				}

				sb1.AppendFormat(@"
{1}

		public int Update({0}Info item) {{
			return new SqlUpdateBuild(null, item.{7}){8}.ExecuteNonQuery();
		}}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {{
			protected {0}Info _item;
			protected string _fields;
			protected string _where;
			protected List<SqlParameter> _parameters = new List<SqlParameter>();
			public SqlUpdateBuild({0}Info item, {3}) {{
				_item = item;
				_where = SqlHelper.Addslashes(""{4}"", {5});
			}}
			public SqlUpdateBuild() {{ }}
			public override string ToString() {{
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception(""防止 {9}.DAL.{0}.SqlUpdateBuild 误修改，请必须设置 where 条件。"");
				return string.Concat(""UPDATE "", TSQL.Table, "" SET "", _fields.Substring(1), "" WHERE "", _where);
			}}
			public int ExecuteNonQuery() {{
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				return SqlHelper.ExecuteNonQuery(this.ToString(), _parameters.ToArray());
			}}
			public SqlUpdateBuild Where(string filterFormat, params object[] values) {{
				if (!string.IsNullOrEmpty(_where)) _where = string.Concat(_where, "" AND "");
				_where = string.Concat(_where, ""("", SqlHelper.Addslashes(filterFormat, values), "")"");
				return this;
			}}
			public SqlUpdateBuild Set(string field, string value, params SqlParameter[] parms) {{
				if (value.IndexOf('\'') != -1) throw new Exception(""{9}.DAL.{0}.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。"");
				_fields = string.Concat(_fields, "", "", field, "" = "", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}}{6}
		}}
		#endregion

		public {0}Info Insert({0}Info item) {{
			DataSet ds = SqlHelper.ExecuteDataSet(TSQL.Insert, GetParameters(item));
			return ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 ? GetItem(ds.Tables[0].Rows[0]) : null;
		}}
{2}
	}}
}}", uClass_Name, sb2.ToString(), sb3.ToString(), pkCsParam, pkSqlParamFormat, pkCsParamNoType, sb5.ToString(),
pkCsParamNoType.Replace(", ", ", item."), sb6.ToString(), solutionName);
				#endregion

				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, @".db\DAL\", basicName, @"\", uClass_Name, ".cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion

				#region BLL *.cs
				sb1.AppendFormat(
	@"using System;
using System.Configuration;
using System.Collections.Generic;
using System.Threading;
using {0}.Model;

namespace {0}.BLL {{

	public partial class {1} {{

		protected static readonly {0}.DAL.{1} dal = new {0}.DAL.{1}();
		protected static readonly int itemCacheTimeout;

		static {1}() {{
			if (!int.TryParse(ConfigurationManager.AppSettings[""{0}_ITEM_CACHE_TIMEOUT_{1}""], out itemCacheTimeout))
				int.TryParse(ConfigurationManager.AppSettings[""{0}_ITEM_CACHE_TIMEOUT""], out itemCacheTimeout);
		}}

		#region delete, update, insert", solutionName, uClass_Name);

				string removeCacheCode = string.Format(@"
			if (itemCacheTimeout > 0) RemoveCache(GetItem({1}));", uClass_Name, pkCsParamNoType);
				Dictionary<string, bool> del_exists2 = new Dictionary<string, bool>();
				foreach (List<ColumnInfo> cs in table.Uniques) {
					string parms = string.Empty;
					string parmsBy = "By";
					string parmsNoneType = string.Empty;
					string parmsNodeTypeUpdateCacheRemove = string.Empty;
					string cacheCond = string.Empty;
					string cacheRemoveCode = string.Empty;
					foreach (ColumnInfo columnInfo in cs) {
						parms += CodeBuild.GetCSType(columnInfo.Type) + " " + CodeBuild.UFString(columnInfo.Name) + ", ";
						parmsBy += CodeBuild.UFString(columnInfo.Name) + "And";
						parmsNoneType += CodeBuild.UFString(columnInfo.Name) + ", ";
						parmsNodeTypeUpdateCacheRemove += "item." + CodeBuild.UFString(columnInfo.Name) + ", \"_,_\", ";
						cacheCond += CodeBuild.UFString(columnInfo.Name) + " == null || ";
					}
					parms = parms.Substring(0, parms.Length - 2);
					parmsBy = parmsBy.Substring(0, parmsBy.Length - 3);
					parmsNoneType = parmsNoneType.Substring(0, parmsNoneType.Length - 2);
					parmsNodeTypeUpdateCacheRemove = parmsNodeTypeUpdateCacheRemove.Substring(0, parmsNodeTypeUpdateCacheRemove.Length - 9);
					cacheCond = cacheCond.Substring(0, cacheCond.Length - 4);

					del_exists2.Add(parms, true);
					sb2.AppendFormat(@"
		public static int Delete{2}({0}) {{{3}
			return dal.Delete{2}({1});
		}}", parms, parmsNoneType, cs[0].IsPrimaryKey ? string.Empty : parmsBy, cs[0].IsPrimaryKey ? removeCacheCode : string.Empty);


					sb3.AppendFormat(@"
		public static {1}Info GetItem{2}({4}) {{
			if ({6}) return null;
			if (itemCacheTimeout <= 0) return dal.GetItem{2}({5});
			string key = string.Concat(""{0}_BLL_{1}{2}_"", {3});
			string value = ItemCache.Get(key);
			if (!string.IsNullOrEmpty(value))
				try {{ return new {1}Info(value); }} catch {{ }}
			{1}Info item = dal.GetItem{2}({5});
			if (item == null) return null;
			ItemCache.Set(key, item.Stringify(), itemCacheTimeout);
			return item;
		}}", solutionName, uClass_Name, cs[0].IsPrimaryKey ? string.Empty : parmsBy, parmsNodeTypeUpdateCacheRemove.Replace("item.", ""),
		parms, parmsNoneType, cacheCond);

					sb4.AppendFormat(@"
			ItemCache.Remove(string.Concat(""{0}_BLL_{1}{2}_"", {3}));", solutionName, uClass_Name, cs[0].IsPrimaryKey ? string.Empty : parmsBy, parmsNodeTypeUpdateCacheRemove);
				}

				sb2.AppendFormat(@"|deleteby_fk|");

				//string UpdateDiyPkParms = string.Empty;
				//string UpdateDiyPkParmsNoneType = string.Empty;
				//table.PrimaryKeys.ForEach(delegate (ColumnInfo UpdateDiyPk) {
				//	UpdateDiyPkParms += CodeBuild.GetCSType(UpdateDiyPk.Type) + " " + CodeBuild.UFString(UpdateDiyPk.Name) + ", ";
				//	UpdateDiyPkParmsNoneType += CodeBuild.UFString(UpdateDiyPk.Name) + ", ";
				//});
				//UpdateDiyPkParms = UpdateDiyPkParms.Substring(0, UpdateDiyPkParms.Length - 2);
				//UpdateDiyPkParmsNoneType = UpdateDiyPkParmsNoneType.Substring(0, UpdateDiyPkParmsNoneType.Length - 2);

				sb1.AppendFormat(@"
{0}
", sb2.ToString());
				//if (table.Columns.Count < 6)
					sb1.AppendFormat(@"
		public static int Update({1}) {{
			return Update(new {0}Info({2}));
		}}", uClass_Name, CsParam1, CsParamNoType1);

				sb1.AppendFormat(@"
		public static int Update({1}Info item) {{
			if (itemCacheTimeout > 0) RemoveCache(item);
			return dal.Update(item);
		}}
		public static {0}.DAL.{1}.SqlUpdateBuild UpdateDiy({2}) {{
			return UpdateDiy(null, {3});
		}}
		public static {0}.DAL.{1}.SqlUpdateBuild UpdateDiy({1}Info item, {2}) {{
			if (itemCacheTimeout > 0) RemoveCache(item != null ? item : GetItem({3}));
			return new {0}.DAL.{1}.SqlUpdateBuild(item, {3});
		}}
		/// <summary>
		/// 用于批量更新
		/// </summary>
		public static {0}.DAL.{1}.SqlUpdateBuild UpdateDiyDangerous {{
			get {{ return new {0}.DAL.{1}.SqlUpdateBuild(); }}
		}}
", solutionName, uClass_Name, pkCsParam, pkCsParamNoType);
				//if (table.Columns.Count < 6)
					sb1.AppendFormat(@"
		public static {0}Info Insert({1}) {{
			return Insert(new {0}Info({2}));
		}}", uClass_Name, CsParam2, CsParamNoType2);

				sb1.AppendFormat(@"
		public static {0}Info Insert({0}Info item) {{
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}}
		private static void RemoveCache({0}Info item) {{
			if (item == null) return;{2}
		}}
		#endregion
{1}
", uClass_Name, sb3.ToString(), sb4.ToString());

				sb1.AppendFormat(@"
		public static List<{0}Info> GetItems() {{
			return Select.ToList();
		}}
		public static {0}SelectBuild Select {{
			get {{ return new {0}SelectBuild(dal); }}
		}}", uClass_Name, solutionName);

				Dictionary<string, bool> byItems = new Dictionary<string, bool>();
				foreach (ForeignKeyInfo fk in table.ForeignKeys) {
					string fkcsBy = string.Empty;
					string fkcsParms = string.Empty;
					string fkcsTypeParms = string.Empty;
					string fkcsFilter = string.Empty;
					int fkcsFilterIdx = 0;
					foreach (ColumnInfo c1 in fk.Columns) {
						fkcsBy += CodeBuild.UFString(c1.Name) + "And";
						fkcsParms += CodeBuild.UFString(c1.Name) + ", ";
						fkcsTypeParms += CodeBuild.GetCSType(c1.Type) + " " + CodeBuild.UFString(c1.Name) + ", ";
						fkcsFilter += "a.[" + c1.Name + "] = {" + fkcsFilterIdx++ + "} and ";
					}
					fkcsBy = fkcsBy.Remove(fkcsBy.Length - 3);
					fkcsParms = fkcsParms.Remove(fkcsParms.Length - 2);
					fkcsTypeParms = fkcsTypeParms.Remove(fkcsTypeParms.Length - 2);
					fkcsFilter = fkcsFilter.Remove(fkcsFilter.Length - 4);
					if (byItems.ContainsKey(fkcsBy)) {
						continue;
					}
					byItems.Add(fkcsBy, true);

					if (!del_exists2.ContainsKey(fkcsTypeParms)) {
						sb5.AppendFormat(@"
		public static int DeleteBy{2}({0}) {{
			return dal.DeleteBy{2}({1});
		}}", fkcsTypeParms, fkcsParms, fkcsBy);
						del_exists2.Add(fkcsTypeParms, true);
					}
					if (fk.Columns.Count > 1) {
						sb1.AppendFormat(
		@"
		public static List<{0}Info> GetItemsBy{1}({2}) {{
			return Select.Where{1}({3}).ToList();
		}}
		public static List<{0}Info> GetItemsBy{1}({2}, int limit) {{
			return Select.Where{1}({3}).Limit(limit).ToList();
		}}
		public static {0}SelectBuild SelectBy{1}({2}) {{
			return Select.Where{1}({3});
		}}", uClass_Name, fkcsBy, fkcsTypeParms, fkcsParms);
						sb6.AppendFormat(@"
		public {0}SelectBuild Where{1}({2}) {{
			return base.Where(""{4}"", {3}) as {0}SelectBuild;
		}}", uClass_Name, fkcsBy, fkcsTypeParms, fkcsParms, fkcsFilter, solutionName);
					} else if (fk.Columns.Count == 1/* && fk.Columns[0].IsPrimaryKey == false*/) {
						string csType = CodeBuild.GetCSType(fk.Columns[0].Type);
						sb1.AppendFormat(
		@"
		public static List<{0}Info> GetItemsBy{1}(params {2}[] {1}) {{
			return Select.Where{1}({1}).ToList();
		}}
		public static List<{0}Info> GetItemsBy{1}({2}[] {1}, int limit) {{
			return Select.Where{1}({1}).Limit(limit).ToList();
		}}
		public static {0}SelectBuild SelectBy{1}(params {2}[] {1}) {{
			return Select.Where{1}({1});
		}}", uClass_Name, fkcsBy, csType);
						sb6.AppendFormat(@"
		public {0}SelectBuild Where{1}(params {2}[] {1}) {{
			return this.Where1Or(""a.[{1}] = {{0}}"", {1});
		}}", uClass_Name, fkcsBy, csType);
					}
				}

				_tables.ForEach(delegate (TableInfo t2) {
					ForeignKeyInfo fk = t2.ForeignKeys.Find(delegate (ForeignKeyInfo ffk) {
						if (ffk.ReferencedTable.FullName == table.FullName) {
							return true;
						}
						return false;
					});
					if (fk == null) return;
					if (fk.Table.FullName == table.FullName) return;
					List<ForeignKeyInfo> fk2 = t2.ForeignKeys.FindAll(delegate (ForeignKeyInfo ffk2) {
						return ffk2 != fk;
					});
					if (fk2.Count != 1) return;
					if (fk.Columns[0].IsPrimaryKey == false) return; //中间表关系键，必须为主键

					//t2.Columns
					string t2name = t2.Name;
					string tablename = table.Name;
					string addname = t2name;
					if (t2name.StartsWith(tablename + "_")) {
						addname = t2name.Substring(tablename.Length + 1);
					} else if (t2name.EndsWith("_" + tablename)) {
						addname = t2name.Remove(addname.Length - tablename.Length - 1);
					} else if (fk2.Count == 1 && t2name.EndsWith("_" + tablename)) {
						addname = t2name.Remove(t2name.Length - tablename.Length - 1);
					} else if (fk2.Count == 1 && t2name.EndsWith("_" + fk2[0].ReferencedTable.Name)) {
						addname = t2name;
					}
					//if (string.Compare(fk2[0].ReferencedTable.Name, addname, true) != 0) return;

					string orgInfo = CodeBuild.UFString(fk2[0].ReferencedTable.ClassName);
					string fkcsBy = CodeBuild.UFString(addname);
					if (byItems.ContainsKey(fkcsBy)) {
						return;
					}
					byItems.Add(fkcsBy, true);

					string civ = string.Format(GetCSTypeValue(fk2[0].ReferencedTable.PrimaryKeys[0].Type), CodeBuild.UFString(fk2[0].ReferencedTable.PrimaryKeys[0].Name));
					sb1.AppendFormat(@"
		public static {0}SelectBuild SelectBy{1}(params {2}Info[] items) {{
			return Select.Where{1}(items);
		}}
		public static {0}SelectBuild SelectBy{1}_{4}(params {3}[] ids) {{
			return Select.Where{1}_{4}(ids);
		}}", uClass_Name, fkcsBy, orgInfo,
		GetCSType(fk2[0].ReferencedTable.PrimaryKeys[0].Type).Replace("?", ""), table.PrimaryKeys[0].Name);

					string _f6 = fk.Columns[0].Name;
					string _f7 = fk.ReferencedTable.PrimaryKeys[0].Name;
					string _f8 = fk2[0].Columns[0].Name;
					string _f9 = GetCSType(fk2[0].ReferencedTable.PrimaryKeys[0].Type).Replace("?", "");

					if (fk.ReferencedTable.ClassName == fk2[0].ReferencedTable.ClassName) {
						_f6 = fk2[0].Columns[0].Name;
						_f7 = fk2[0].ReferencedTable.PrimaryKeys[0].Name;
						_f8 = fk.Columns[0].Name;
						_f9 = GetCSType(fk2[0].ReferencedTable.PrimaryKeys[0].Type).Replace("?", "");
					}
					sb6.AppendFormat(@"
		public {0}SelectBuild Where{1}(params {2}Info[] items) {{
			if (items == null || items.Length == 0) return this;
			return Where{1}_{7}(Array.ConvertAll<{2}Info, {9}>(items, delegate ({2}Info c) {{ return c.{3}; }}));
		}}
		public {0}SelectBuild Where{1}_{7}(params {9}[] ids) {{
			if (ids == null || ids.Length == 0) return this;
			return base.Where(string.Format(@""EXISTS( SELECT [{6}] FROM [{4}].[{5}] WHERE [{6}] = a.[{7}] AND [{8}] IN ({{0}}) )"", 
				string.Join("","", Array.ConvertAll<{9}, string>(ids, delegate ({9} c) {{ return string.Concat(c); }})))) as {0}SelectBuild;
		}}", uClass_Name, fkcsBy, orgInfo, civ, t2.Owner, t2.Name, _f6, _f7, _f8, _f9);
					//				sb6.AppendFormat(@"
					//public {0}SelectBuild Where{1}(params {2}Info[] items) {{
					//	if (items == null || items.Length == 0) return this;
					//	return Where{1}_{7}(Array.ConvertAll<{2}Info, {9}>(items, delegate ({2}Info c) {{ return c.{3}; }}));
					//}}
					//public {0}SelectBuild Where{1}_{7}(params {9}[] ids) {{
					//	if (ids == null || ids.Length == 0) return this;
					//	return base.Where(string.Format(@""EXISTS( SELECT [{6}] FROM [{4}].[{5}] WHERE [{6}] = a.[{7}] AND [{8}] IN ({{0}}) )"", 
					//		string.Join("","", Array.ConvertAll<{9}, string>(ids, delegate ({9} c) {{ return string.Concat(c); }})))) as {0}SelectBuild;
					//}}", uClass_Name, fkcsBy, orgInfo, civ,
					//	t2.Owner, t2.Name, fk.Columns[0].Name, fk.ReferencedTable.PrimaryKeys[0].Name, fk2[0].Columns[0].Name,
					//	GetCSType(fk.ReferencedTable.PrimaryKeys[0].Type).Replace("?", ""));
				});

				table.Columns.ForEach(delegate (ColumnInfo col) {
					string csType = CodeBuild.GetCSType(col.Type);
					string lname = col.Name.ToLower();
					//if (col.IsPrimaryKey) return;
					//if (lname == "create_time" ||
					//	lname == "update_time") return;
					string fkcsBy = CodeBuild.UFString(col.Name);
					if (byItems.ContainsKey(fkcsBy)) {
						return;
					}
					byItems.Add(fkcsBy, true);

					if (csType == "bool?") {
						sb6.AppendFormat(@"
		public {0}SelectBuild Where{1}(params {2}[] {1}) {{
			return this.Where1Or(""a.[{1}] = {{0}}"", {1});
		}}", uClass_Name, fkcsBy, csType);
						return;
					}
					if ((col.Type == SqlDbType.BigInt || col.Type == SqlDbType.Int) && (lname == "status" || lname.StartsWith("status_") || lname.EndsWith("_status"))) {
						sb6.AppendFormat(@"
		public {0}SelectBuild Where{1}(params int[] _0_16) {{
			if (_0_16 == null || _0_16.Length == 0) return this;
			{2}[] copy = new {2}[_0_16.Length];
			for (int a = 0; a < _0_16.Length; a++) copy[a] = ({2})Math.Pow(_0_16[a], 2);
			return this.Where1Or(""(a.[{1}] & {{0}}) = {{0}}"", copy);
		}}", uClass_Name, fkcsBy, csType.Replace("?", ""));
						return;
					}
					if (col.Type == SqlDbType.BigInt || col.Type == SqlDbType.Int || col.Type == SqlDbType.SmallInt) {
						sb6.AppendFormat(@"
		public {0}SelectBuild Where{1}(params {2}[] {1}) {{
			return this.Where1Or(""a.[{1}] = {{0}}"", {1});
		}}", uClass_Name, fkcsBy, csType);
						return;
					}
					if (col.Type == SqlDbType.Decimal || col.Type == SqlDbType.Float || col.Type == SqlDbType.Money || col.Type == SqlDbType.Real) {
						sb6.AppendFormat(@"
		public {0}SelectBuild Where{1}(params {2}[] {1}) {{
			return this.Where1Or(""a.[{1}] = {{0}}"", {1});
		}}", uClass_Name, fkcsBy, csType);
						sb6.AppendFormat(@"
		public {0}SelectBuild Where{1}Range({2} begin) {{
			return base.Where(""a.[{1}] >= {{0}}"", begin) as {0}SelectBuild;
		}}
		public {0}SelectBuild Where{1}Range({2} begin, {2} end) {{
			if (end == null) return Where{1}Range(begin);
			return base.Where(""a.[{1}] between {{0}} and {{1}}"", begin, end) as {0}SelectBuild;
		}}", uClass_Name, fkcsBy, csType);
						return;
					}
					if (col.Type == SqlDbType.DateTime || col.Type == SqlDbType.SmallDateTime || col.Type == SqlDbType.Date) {
						sb6.AppendFormat(@"
		public {0}SelectBuild Where{1}Range({2} begin) {{
			return base.Where(""a.[{1}] >= {{0}}"", begin) as {0}SelectBuild;
		}}
		public {0}SelectBuild Where{1}Range({2} begin, {2} end) {{
			if (end == null) return Where{1}Range(begin);
			return base.Where(""a.[{1}] between {{0}} and {{1}}"", begin, end) as {0}SelectBuild;
		}}", uClass_Name, fkcsBy, csType);
						return;
					}
					if (col.Length > 0 && col.Length < 101 && (col.Type == SqlDbType.VarChar || col.Type == SqlDbType.NVarChar)) {
						sb6.AppendFormat(@"
		public {0}SelectBuild Where{1}(params {2}[] {1}) {{
			return this.Where1Or(""a.[{1}] = {{0}}"", {1});
		}}", uClass_Name, fkcsBy, csType);
						return;
					}
				});

				sb1.AppendFormat(@"
	}}
	public partial class {0}SelectBuild : {1}.DAL.SqlHelper.SelectBuild<{0}Info, {0}SelectBuild> {{{2}
		protected new {0}SelectBuild Where1Or(string filterFormat, Array values) {{
			return base.Where1Or(filterFormat, values) as {0}SelectBuild;
		}}
		public {0}SelectBuild({1}.DAL.SqlHelper.IDAL dal) : base(dal) {{ }}
	}}
}}", uClass_Name, solutionName, sb6.ToString());

				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, @".db\BLL\", basicName, @"\", uClass_Name, ".cs"), Deflate.Compress(sb1.ToString().Replace("|deleteby_fk|", sb5.ToString()))));
				clearSb();
				#endregion

				#region admin
				if (isMakeAdmin) {

					#region common define
					string pkNames = string.Empty;
					string pkUrlQuerys = string.Empty;
					for (int a = 0; a < table.PrimaryKeys.Count; a++) {
						ColumnInfo col88 = table.PrimaryKeys[a];
						pkNames += CodeBuild.UFString(col88.Name) + ",";
						pkUrlQuerys += col88.Name + "={" + a + "}&";
					}
					if (pkNames.Length > 0) pkNames = pkNames.Remove(pkNames.Length - 1);
					if (pkUrlQuerys.Length > 0) pkUrlQuerys = pkUrlQuerys.Remove(pkUrlQuerys.Length - 1);
					#endregion

					#region web.sitemap
					admin_web_sitemap += string.Format(@"
			<siteMapNode url=""~/{0}/"" title=""{1}"">
				<siteMapNode url=""~/{0}/edit.aspx"" title=""修改"" />
				<siteMapNode url=""~/{0}/del.aspx"" title=""删除"" />
			</siteMapNode>", nClass_Name, nClass_Name);
					#endregion

					#region init_sysdir.aspx
					admin_init_sysdir_aspx.Add(string.Format(@"

		dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, ""{0}"", {1}, ""/{0}/"");
		dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, ""列表"", 1, ""/{0}/"");
		dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, ""添加"", 2, ""/{0}/add.aspx"");
		dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, ""编辑"", 3, ""/{0}/edit.aspx"");
		dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, ""删除"", 4, ""/{0}/del.aspx"");", nClass_Name, admin_init_sysdir_aspx.Count + 1));
					#endregion

					#region default.aspx
					string keyLikes = string.Empty;
					foreach (ColumnInfo col in table.Columns) {
						string csType = CodeBuild.GetCSType(col.Type);
						string csUName = CodeBuild.UFString(col.Name);
						if (csType == "string") {
							keyLikes += "a.[" + col.Name + "] like {0} or ";
						}
						List<ForeignKeyInfo> fks = table.ForeignKeys.FindAll(delegate (ForeignKeyInfo fk88) {
							return fk88.Columns.Find(delegate (ColumnInfo col88) {
								return col88.Name == col.Name;
							}) != null;
						});

						ForeignKeyInfo fk = null;
						string FK_uEntry_Name = string.Empty;
						string tableNamefe3 = string.Empty;
						string memberName = string.Empty;
						string strName = string.Empty;
						if (fks.Count > 0) {
							fk = fks[0];
							FK_uEntry_Name = fk.ReferencedTable != null ? CodeBuild.GetCSName(fk.ReferencedTable.Name) :
								CodeBuild.GetCSName(TableInfo.GetEntryName(fk.ReferencedTableName));
							tableNamefe3 = fk.ReferencedTable != null ? fk.ReferencedTable.Name :
								FK_uEntry_Name;
							memberName = fk.Columns[0].Name.IndexOf(tableNamefe3) == -1 ? CodeBuild.LFString(tableNamefe3) :
								(CodeBuild.LFString(fk.Columns[0].Name.Substring(0, fk.Columns[0].Name.IndexOf(tableNamefe3)) + tableNamefe3));

							ColumnInfo strNameCol = null;
							if (fk.ReferencedTable != null) {
								strNameCol = fk.ReferencedTable.Columns.Find(delegate (ColumnInfo col88) {
									return col88.Name.ToLower().IndexOf("name") != -1 || col88.Name.ToLower().IndexOf("title") != -1;
								});
								if (strNameCol == null) strNameCol = fk.ReferencedTable.Columns.Find(delegate (ColumnInfo col88) {
									return GetCSType(col88.Type) == "string" && col88.Length > 0 && col88.Length < 128;
								});
							}
							strName = strNameCol != null ? "." + CodeBuild.UFString(strNameCol.Name) : string.Empty;
						}

						if (csType == "bool?") {
							sb4.AppendFormat(@"<asp:CheckBoxField DataField=""{0}"" HeaderText=""{0}"" />
		", csUName);
						} else if (!col.IsIdentity && fks.Count == 1) {
							ForeignKeyInfo fkcb = fks[0];
							string FK_uClass_Name = fkcb.ReferencedTable != null ? CodeBuild.UFString(fkcb.ReferencedTable.ClassName) :
								CodeBuild.UFString(TableInfo.GetClassName(fkcb.ReferencedTableName));

							sb4.AppendFormat(@"<asp:TemplateField HeaderText=""{0}"">
			<ItemTemplate><%# Eval(""Obj_{1}{2}"") %></ItemTemplate>
		</asp:TemplateField>
		", csUName, memberName, strName);
							sb3.AppendFormat(@"
			.Where(!string.IsNullOrEmpty(Request.QueryString[""{0}""]), ""a.[{0}] = {{0}}"", Request.QueryString[""{0}""])", col.Name);
							sb6.AppendFormat(@"
		base.search_add(new RichControls.HtmlSelect{1}(), ""Value"", ""{0}"");", col.Name, FK_uClass_Name);
						} else {
							sb4.AppendFormat(@"<asp:BoundField DataField=""{0}"" HeaderText=""{0}"" />
		", csUName);
						}

						List<ForeignKeyInfo> ffks = new List<ForeignKeyInfo>();
						foreach (TableInfo fti in _tables) {
							ffks.AddRange(fti.ForeignKeys.FindAll(delegate (ForeignKeyInfo ffk) {
								if (ffk.ReferencedTable != null && ffk.ReferencedTable.FullName == table.FullName) {
									return ffk.ReferencedColumns.Find(delegate (ColumnInfo col88) {
										return col88.Name == col.Name;
									}) != null;
								}
								return false;
							}));
						}
						foreach (ForeignKeyInfo ffk in ffks) {
							string FFK_uClass_Name = CodeBuild.UFString(ffk.Table.ClassName);
							string FFK_nClass_Name = ffk.Table.ClassName;

							string urlFields = string.Empty;
							string urlQuerys = string.Empty;
							int ffk_idx = 0;
							ffk.Columns.ForEach(delegate (ColumnInfo col88) {
								string FFK_csType = CodeBuild.GetCSType(col.Type);
								string FFK_csUName = CodeBuild.UFString(col.Name);

								urlFields += string.Format("{0},", FFK_csUName);
								urlQuerys += string.Format("{0}={{{1}}}&", col88.Name, ffk_idx++);
							});
							if (urlFields.Length > 0) urlFields = urlFields.Remove(urlFields.Length - 1);
							if (urlQuerys.Length > 0) urlQuerys = urlQuerys.Remove(urlQuerys.Length - 1);

							sb5.AppendFormat(@"<asp:HyperLinkField DataNavigateUrlFields=""{1}"" DataNavigateUrlFormatString=""~/{0}/?{2}"" Text=""{0}"" />
		", FFK_nClass_Name, urlFields, urlQuerys);
						}
					}
					if (keyLikes.Length > 0) {
						keyLikes = keyLikes.Remove(keyLikes.Length - 4);
						sb2.AppendFormat(@"
			.Where(!string.IsNullOrEmpty(Request.QueryString[""key""]), ""{0}"", ""%"" + Lib.StrConvSimplified(Request.QueryString[""key""]) + ""%"")", keyLikes);
					}
					sb1.AppendFormat(@"<%@ Page Language=""C#"" %>
<%@ Register Src=""~/controls/search_bar.ascx"" TagPrefix=""uc"" TagName=""search_bar"" %>

<script runat=""server"">

	protected void Page_Load(object sender, EventArgs e) {{
	}}
	public void set_search() {{{8}
	}}
	public void searching() {{
		int rc;
		GridView1.DataSource = {0}.Select{4}{5}
			.Count(out rc).Sort(""{1}"").Skip(GridView1.PageSize * (GridView1.PageIndex - 1)).Limit(GridView1.PageSize).ToList();
		GridView1.SetRecordCount(rc);
		GridView1.DataBind();
	}}
</script>

<div class=""box"">
	<div class=""box-header with-border"">
		<h3 id=""box-title"" class=""box-title""></h3>
		<span class=""form-group mr15""></span><a href=""./add.aspx"" data-toggle=""modal"" class=""btn btn-success pull-right"">添加</a>
	</div>
	<div class=""box-body"">
		<uc:search_bar runat=""server"" id=""search_bar"" />
		<div class=""table-responsive"">

<form id=""form_list"" runat=""server"">
<asp:GridViewPager ID=""GridView1"" runat=""server"" AutoGenerateColumns=""false"" PageNumber=""20"" PageSize=""20"" class=""table table-bordered table-hover"">
	<Columns>
		{6}{7}<asp:HyperLinkField DataNavigateUrlFields=""{2}"" DataNavigateUrlFormatString=""edit.aspx?{3}"" Text=""修改"" />
		<asp:SelField DataFields=""{2}"" />
	</Columns>
</asp:GridViewPager>
</form>

		</div>
	</div>
</div>
", uClass_Name, orderBy, pkNames, pkUrlQuerys, sb2.ToString(), sb3.ToString(), sb4.ToString(), sb5.ToString(), sb6.ToString());

					ForeignKeyInfo ttfk = table.ForeignKeys.Find(delegate (ForeignKeyInfo fkk) {
						return fkk.ReferencedTable != null && fkk.ReferencedTable.FullName == table.FullName;
					});
					if (ttfk != null) {
						clearSb();

						string ttfk_yieldParms = string.Empty;
						string ttfk_getItems = string.Empty;
						string ttfk_getItemsParms = string.Empty;
						string ttfk_callParms = string.Empty;
						string ttfk_callNullParms = string.Empty;
						string ttfk_pk1 = string.Empty;
						string ttfk_pk2 = string.Empty;
						string ttfk_csSB1 = string.Empty;
						string ttfk_csSB2 = string.Empty;
						string ttfk_tds1 = string.Empty;
						string ttfk_tds2 = string.Empty;
						ttfk.Columns.ForEach(delegate (ColumnInfo col88) {
							string csUName = CodeBuild.UFString(col88.Name);
							string csType = CodeBuild.GetCSType(col88.Type);
							ttfk_yieldParms += string.Format("{0} {1}, ", csType, csUName);
							ttfk_getItems += string.Format("{0}And", csUName);
							ttfk_getItemsParms += string.Format("{0}, ", csUName);
						});
						if (ttfk_getItems.Length > 0) ttfk_getItems = ttfk_getItems.Remove(ttfk_getItems.Length - 3);
						ttfk.ReferencedColumns.ForEach(delegate (ColumnInfo col88) {
							string csUName = CodeBuild.UFString(col88.Name);
							string csType = CodeBuild.GetCSType(col88.Type);
							ttfk_callParms += string.Format("item.{0}, ", csUName);
							ttfk_callNullParms += string.Format("null, ");
						});
						table.PrimaryKeys.ForEach(delegate (ColumnInfo col88) {
							string csUName = CodeBuild.UFString(col88.Name);
							string csType = CodeBuild.GetCSType(col88.Type);
							ttfk_pk1 += string.Format(@"{0}="" + item.{1} + ""&", col88.Name, csUName);
							ttfk_pk2 += string.Format(@""" + item.{0} + "",", csUName);
						});
						if (ttfk_pk1.Length > 0) ttfk_pk1 = ttfk_pk1.Remove(ttfk_pk1.Length - 1);
						if (ttfk_pk2.Length > 0) ttfk_pk2 = ttfk_pk2.Remove(ttfk_pk2.Length - 1);

						bool ttfk_flag = false;
						table.Columns.ForEach(delegate (ColumnInfo col88) {
							string csUName = CodeBuild.UFString(col88.Name);
							string csType = CodeBuild.GetCSType(col88.Type);
							if (col88.IsPrimaryKey || col88.IsIdentity) return;
							if (csType == "string" && !ttfk_flag) {
								ttfk_flag = true;
								ttfk_csSB1 += string.Format(@"
				""<td style='/*padding-left:"" + (24 * depth) + ""px*/'>"" + item.{0} + ""</td>"" + ", csUName);
								ttfk_tds1 += string.Format(@"
		<th scope=""col"">{0}</th>", csUName);
							} else {
								ttfk_csSB2 += string.Format(@"
				""<td>"" + item.{0} + ""</td>"" + ", csUName);
								ttfk_tds2 += string.Format(@"
		<th scope=""col"">{0}</th>", csUName);
							}
						});

						ttfk_getItemsParms = ttfk_getItemsParms.Substring(0, ttfk_getItemsParms.Length - 2);
						sb1.AppendFormat(@"<%@ Page Language=""C#"" %>

<script runat=""server"">

	string yieldBind({1}int depth) {{
		StringBuilder sb = new StringBuilder();
		List<{0}Info> items = {0}.SelectBy{2}({3}).Sort(""{12}"").ToList();
		for (int a = 0; a < items.Count; a++) {{
			{0}Info item = items[a];

			sb.Append(""<tr data-tt-id=\""{5}\"" data-tt-parent-id=\"""" + {3} + ""\"">"" + {8}{9}
				""<td><a href=\""Edit.aspx?{4}\"">修改</a></td><td><input id=\""id\"" type=\""checkbox\"" name=\""id\"" value=\""{5}\"" /></td></tr>"");

			sb.Append(yieldBind({6}depth + 1));
		}}
		return sb.ToString();
	}}
</script>

<div class=""box"">
	<div class=""box-header with-border"">
		<h3 id=""box-title"" class=""box-title""></h3>
		<span class=""form-group mr15""></span><a href=""./add.aspx"" data-toggle=""modal"" class=""btn btn-success pull-right"">添加</a>
	</div>
	<div class=""box-body"">
		<div class=""table-responsive"">

<form id=""form_list"" runat=""server"">
<table id=""GridView1"" cellspacing=""0"" rules=""all"" border=""1"" style=""border-collapse:collapse;"" class=""table table-bordered table-hover"">
	<tr>{10}{11}
		<th scope=""col"" style=""width:5%;"">&nbsp;</th>
		<th scope=""col"" style=""width:5%;"">删除</th>
	</tr><%= yieldBind({7}0) %>
</table>
</form>

<script type=""text/javascript"">
$('table#GridView1').treetable({{ expandable: true }});
$('table#GridView1').treetable('expandAll');
</script>

		</div>
	</div>
</div>

<div>
	<font color=""red"">*</font> 删除父项时，请先删除其所有子项。
	<a id=""btn_delete_sel"" href=""#"" class=""btn btn-danger pull-right"">删除选中项</a>
</div>
", uClass_Name, ttfk_yieldParms, ttfk_getItems, ttfk_getItemsParms, ttfk_pk1, ttfk_pk2, ttfk_callParms, ttfk_callNullParms,
	ttfk_csSB1, ttfk_csSB2, ttfk_tds1, ttfk_tds2, orderBy);
					}

					loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, nClass_Name, @"\default.aspx"), Deflate.Compress(sb1.ToString())));
					clearSb();
					#endregion

					#region add.aspx
					int pkfeidx = 0;
					string pkEvals = string.Empty;
					string pkCheckParms = string.Empty;
					string pkFuncParms = string.Empty;
					string pkUrlParms = string.Empty;
					string pkAjaxParms = string.Empty;
					string pkAjaxRequestConvert = string.Empty;
					string pkFilterNotEqualsOr = string.Empty;
					string pkRequestForms = string.Empty;
					string pkCsFuncParms = string.Empty;
					string pkInsertingValues = string.Empty;
					string pkUpdatingNewValues = string.Empty;
					table.PrimaryKeys.ForEach(delegate (ColumnInfo col88) {
						string csUName = CodeBuild.UFString(col88.Name);
						string csType = CodeBuild.GetCSType(col88.Type);
						pkEvals += string.Format(@" {0}1='<%# Eval(""{0}"") %>'", csUName);
						pkCheckParms += string.Format(@",$(this).attr('{0}1')", csUName);
						pkFuncParms += string.Format(@", {0}", csUName);
						pkUrlParms += string.Format(@"&{1}={{{0}}}", pkfeidx + 1, col88.Name);
						pkAjaxParms += string.Format(@", {0} : {1}", col88.Name, csUName);
						pkAjaxRequestConvert += string.Format(@"
		{1} {0};
		{2};", csUName, csType.Trim('?'),
				 string.Format(CodeBuild.GetCSConvert(col88.Type), string.Format(@"string.Concat({0}1)", csUName), csUName));
						pkFilterNotEqualsOr += string.Format(@" or [{1}] <> {{{0}}}", pkfeidx + 1, col88.Name);
						pkRequestForms += string.Format(@", Request.Form[""{0}""]", col88.Name);
						pkCsFuncParms += string.Format(@", object {0}1", csUName);
						pkInsertingValues += string.Format(@", e.Values[""{0}""]", csUName);
						pkfeidx++;
					});
					if (pkFilterNotEqualsOr.Length > 0) pkFilterNotEqualsOr = " and (" + pkFilterNotEqualsOr.Substring(4) + ")";
					foreach (ColumnInfo col in table.Columns) {
						string csType = CodeBuild.GetCSType(col.Type);
						string csType2 = CodeBuild.GetCSType2(col.Type);
						string csUName = CodeBuild.UFString(col.Name);
						string lname = col.Name.ToLower();
						string rfvEmpty = string.Empty;
						List<ColumnInfo> us = table.Uniques.Find(delegate (List<ColumnInfo> cs) {
							return cs.Find(delegate (ColumnInfo col88) {
								return col88.Name == col.Name;
							}) != null;
						});
						if (us == null) us = new List<ColumnInfo>();
						List<ForeignKeyInfo> fks_comb = table.ForeignKeys.FindAll(delegate (ForeignKeyInfo fk) {
							return fk.Columns.Count == 1 && fk.Columns[0].Name == col.Name;
						});

						if (!col.IsIdentity)
							sb12.AppendFormat(@"
		<asp:Parameter Name=""{0}"" Type=""{1}"" />", csUName, csType2);
						sb3.AppendFormat(@"
		<asp:Parameter Name=""{0}"" Type=""{1}"" />", csUName, csType2);
						if (!col.IsNullable && csType == "string") {
							rfvEmpty = string.Format(@"
				<asp:RequiredFieldValidator ID=""rfv{0}"" runat=""server"" ControlToValidate=""textbox{0}"" ErrorMessage=""*"" />", csUName);
							rfvEmpty = "";
						}
						if (csType == "bool?") {
							sb4.AppendFormat(@"
		<asp:CheckBoxField DataField=""{0}"" HeaderText=""{0}"" />", csUName);
						} else if (csType == "DateTime?" && (
							string.Compare(lname, "create_time", true) == 0 ||
							string.Compare(lname, "update_time", true) == 0
							)) {
							sb4.AppendFormat(@"
		<asp:BoundField HeaderText=""{0}"" InsertVisible=""False"" />", csUName);
							if (string.Compare(lname, "create_time", true) == 0) {
								sb10.AppendFormat(@"
		e.Values[""{0}""] = DateTime.Now;", csUName);
							}
							if (string.Compare(lname, "update_time", true) == 0) {
								sb10.AppendFormat(@"
		e.Values[""{0}""] = DateTime.Now;", csUName);
							}
						} else if (col.IsPrimaryKey && col.IsIdentity) {
							//主键自动增值
							sb4.AppendFormat(@"
		<asp:BoundField HeaderText=""{0}"" InsertVisible=""False"" />", csUName);
						} else if (!col.IsIdentity && us.Count == 1 || col.IsPrimaryKey && table.PrimaryKeys.Count == 1) {
							//主键或唯一键，非自动增值
							sb5.AppendFormat(@"
function AjaxCheck{0}(value{3}) {{
	value = value.trim();
	if (value == '') return;
	var args = {{ method : 'AjaxCheck{0}', {1} : value{2} }};
	var callback = function(rt) {{
		$('#textbox{0}Message').html(rt == '1' ? '<font color=red>已存在</font>' : '');
	}};
	<%= Ajax.Register(this, ""callback"", ""args"") %>
}}", csUName, col.Name, pkAjaxParms, pkFuncParms);
							sb4.AppendFormat(@"
		<asp:TemplateField HeaderText=""{0}"">
			<InsertItemTemplate>
				<input type=""text"" ID=""textbox{0}"" runat=""server"" value='<%# Bind(""{0}"") %>'{1} onchange=""this.ajax_changed=true;"" onblur=""if(this.ajax_changed){{AjaxCheck{0}(this.value{2});this.ajax_changed=false;}}"" class=""datepicker"" style=""width:60%;"" />
				<span id=""textbox{0}Message""></span>{3}
			</InsertItemTemplate>
		</asp:TemplateField>", csUName, pkEvals, pkCheckParms, rfvEmpty);

							sb6.AppendFormat(@"
			case ""AjaxCheck{0}"":
				Response.Write(AjaxCheck{0}(Request.Form[""{1}""]{2}) ? 1 : 0);
				break;", csUName, col.Name, pkRequestForms);
							sb7.AppendFormat(@"
	protected static bool AjaxCheck{0}(object value1{6}) {{
		string value = string.Concat(value1).Trim();{3}
		return {2}.Select.Where(""[{1}] = {{0}}{5}"", value{4}).ToOne() != null;
	}}", csUName, col.Name, uClass_Name, pkAjaxRequestConvert, pkFuncParms, pkFilterNotEqualsOr, pkCsFuncParms);

							sb8.AppendFormat(@"
		if (AjaxCheck{0}(e.Values[""{0}""]{1})) {{
			base.End(string.Format(""已存在{0}“{{0}}”！"", e.Values[""{0}""]));
			e.Cancel = true;
			return;
		}}", csUName, pkInsertingValues);
						} else if (fks_comb.Count == 1) {
							//外键下拉框
							ForeignKeyInfo fkcb = fks_comb[0];
							string FK_uClass_Name = fkcb.ReferencedTable != null ? CodeBuild.UFString(fkcb.ReferencedTable.ClassName) :
								CodeBuild.UFString(TableInfo.GetClassName(fkcb.ReferencedTableName));

							sb4.AppendFormat(@"
		<asp:TemplateField HeaderText=""{0}"">
			<InsertItemTemplate><asp:HtmlSelect{1} ID=""{1}{0}"" Value='<%# Bind(""{0}"") %>' runat=""server"" /></InsertItemTemplate>
		</asp:TemplateField>", csUName, FK_uClass_Name);

							if (!col.IsNullable) {
								sb8.AppendFormat(@"
		if (string.IsNullOrEmpty(string.Concat(e.Values[""{0}""]))) {{
			base.End(""请选择一个{0}！"");
			e.Cancel = true;
			return;
		}}", csUName);
							}
							if (fkcb.ReferencedTable != null) {
								#region HtmlSelect.cs
								string path = string.Concat(CONST.adminPath, @"App_Code\RichControls\HtmlSelect\", FK_uClass_Name, ".cs");
								if (!isMakedHtmlSelect.ContainsKey(path)) {
									isMakedHtmlSelect.Add(path, true);
									ForeignKeyInfo fkrr = fkcb.ReferencedTable != null ?
										fkcb.ReferencedTable.ForeignKeys.Find(delegate (ForeignKeyInfo fkkk) {
											return fkkk.ReferencedTable != null && fkcb.ReferencedTable.FullName == fkkk.ReferencedTable.FullName;
										}) : null;
									bool isHtmlSelect2 = fkcb.ReferencedTable != null && fkrr != null;
									string htmlSelectPlace = isHtmlSelect2 ? CONST.Admin_App_Code_RichControls_HtmlSelect_Style2_cs :
										CONST.Admin_App_Code_RichControls_HtmlSelect_Style1_cs;
									string FK_Class_Name_BLL_Space = string.Format(@"{0}.BLL", solutionName);
									string FK_Class_Name_Model_Space = string.Format(@"{0}.Model", solutionName);
									string FK_Class_Name_BLL_Full = string.Format(@"{0}.{1}", FK_Class_Name_BLL_Space, FK_uClass_Name);
									string FK_Class_Name_Model_Full = string.Format(@"{0}.{1}", FK_Class_Name_Model_Space, FK_uClass_Name);
									string FK_OrderBy = fkcb.ReferencedTable != null ?
										string.Join(",", fkcb.ReferencedTable.Clustereds.Count > 0 ?
											fkcb.ReferencedTable.Clustereds.ConvertAll<string>(delegate (ColumnInfo col88) {
												return "a.[" + col88.Name + "]" + (col88.Orderby == DataSort.ASC ? string.Empty : " " + col88.Orderby.ToString());
											}).ToArray() : fkcb.ReferencedTable.PrimaryKeys.ConvertAll<string>(delegate (ColumnInfo col88) {
												return "a.[" + col88.Name + "]" + (col88.Orderby == DataSort.ASC ? string.Empty : " " + col88.Orderby.ToString());
											}).ToArray()) : null;
									string FK_Column = fkcb.ReferencedTable != null ?
										CodeBuild.UFString(fkcb.ReferencedColumns[0].Name) : CodeBuild.UFString(fkcb.ReferencedColumnNames[0]);

									ColumnInfo strCol = fkcb.ReferencedTable.Columns.Find(delegate (ColumnInfo col99) {
										return col99.Name.ToLower().IndexOf("name") != -1 || col99.Name.ToLower().IndexOf("title") != -1;
									});
									if (strCol == null) strCol = fkcb.ReferencedTable.Columns.Find(delegate (ColumnInfo col99) {
										return GetCSType(col99.Type) == "string" && col99.Length > 0 && col99.Length < 128;
									});
									string FK_Column_Text = fkcb.ReferencedTable != null && strCol != null ? CodeBuild.UFString(strCol.Name)
										 : FK_Column;
									string getBy = isHtmlSelect2 ? CodeBuild.UFString(fkrr.Columns[0].Name) : string.Empty;
									string getType = isHtmlSelect2 ? CodeBuild.GetCSType(fkrr.Columns[0].Type) : string.Empty;
									htmlSelectPlace = string.Format(htmlSelectPlace, FK_uClass_Name, FK_OrderBy, FK_Column_Text, FK_Column, getBy,
										string.Format(@"
using {0};
using {1};", FK_Class_Name_BLL_Space, FK_Class_Name_Model_Space), getType, FK_uClass_Name);
									loc1.Add(new BuildInfo(path, Deflate.Compress(htmlSelectPlace)));
								}
								#endregion
							}
						} else if (!string.IsNullOrEmpty(rfvEmpty)) {
							sb4.AppendFormat(@"
		<asp:TemplateField HeaderText=""{0}"">
			<InsertItemTemplate>
				<asp:TextBox ID=""textbox{0}"" runat=""server"" Text='<%# Bind(""{0}"") %>' />{1}
			</InsertItemTemplate>
		</asp:TemplateField>", csUName, rfvEmpty);
						} else if ((col.Type == SqlDbType.BigInt || col.Type == SqlDbType.Int) && (lname == "status" || lname.StartsWith("status_") || lname.EndsWith("_status"))) {
							//加载 multi 多状态字段
							sb4.AppendFormat(@"
		<asp:TemplateField HeaderText=""{0}"">
			<InsertItemTemplate><input ID=""label{0}"" type=""hidden"" value='<%# Bind(""{0}"") %>' runat=""server"" multi_status=""状态1,状态2,状态3,状态4,状态5"" /></InsertItemTemplate>
		</asp:TemplateField>", csUName);
						} else if (col.Type == SqlDbType.BigInt || col.Type == SqlDbType.Int || col.Type == SqlDbType.SmallInt) {
							sb4.AppendFormat(@"
		<asp:TemplateField HeaderText=""{0}"">
			<InsertItemTemplate><input type=""text"" ID=""textbox{0}"" value='<%# Bind(""{0}"") %>' runat=""server"" class=""form-control"" data-inputmask=""'mask': '9', 'repeat': 6, 'greedy': false"" data-mask style=""width:200px;"" /></InsertItemTemplate>
		</asp:TemplateField>", csUName);
						} else if (col.Type == SqlDbType.Decimal || col.Type == SqlDbType.Float || col.Type == SqlDbType.Money || col.Type == SqlDbType.Real) {
							sb4.AppendFormat(@"
		<asp:TemplateField HeaderText=""{0}"">
			<InsertItemTemplate>
<div class=""input-group"" style=""width:200px;"">
	<span class=""input-group-addon"">￥</span>
	<input type=""text"" ID=""textbox{0}"" value='<%# Bind(""{0}"") %>' runat=""server"" class=""form-control"" data-inputmask=""'mask': '9', 'repeat': 10, 'greedy': false"" data-mask />
	<span class=""input-group-addon"">.00</span>
</div>
			</InsertItemTemplate>
		</asp:TemplateField>", csUName);
						} else if (col.Type == SqlDbType.DateTime || col.Type == SqlDbType.SmallDateTime) {
							//日期
							sb4.AppendFormat(@"
		<asp:BoundField DataField=""{0}"" HeaderText=""{0}"">
			<ControlStyle CssClass=""datepicker"" />
		</asp:BoundField>", csUName);
						} else if (col.Type == SqlDbType.Date) {
							//日期控件
							sb4.AppendFormat(@"
		<asp:TemplateField HeaderText=""{0}"">
			<InsertItemTemplate>
<div class=""input-group date"" style=""width:200px;"">
	<div class=""input-group-addon""><i class=""fa fa-calendar""></i></div>
	<input type=""text"" ID=""textbox{0}"" value='<%# Bind(""{0}"") %>' runat=""server"" data-provide=""datepicker"" class=""form-control pull-right"" readonly />
</div>
			</InsertItemTemplate>
		</asp:TemplateField>", csUName);
						} else if (col.Type == SqlDbType.Text || col.Type == SqlDbType.NText ||
							(col.Length == -1 && (col.Type == SqlDbType.Char || col.Type == SqlDbType.NChar ||
								col.Type == SqlDbType.VarChar || col.Type == SqlDbType.NVarChar))) {
							//加载百度编辑器
							sb4.AppendFormat(@"
		<asp:TemplateField HeaderText=""{0}"">
			<InsertItemTemplate><textarea id=""textarea{0}"" Value='<%# Bind(""{0}"") %>' runat=""server"" style=""width:100%;height:100px;"" editor=""ueditor"" /></InsertItemTemplate>
		</asp:TemplateField>", csUName);
						} else if ((lname == "img" || lname.StartsWith("img_") || lname.EndsWith("_img")) && (col.Type == SqlDbType.VarChar || col.Type == SqlDbType.NVarChar)) {
							//上传图片
							sb4.AppendFormat(@"
		<asp:TemplateField HeaderText=""{0}"">
			<InsertItemTemplate><input type=""file"" id=""file{0}"" PostedFile='<%# Bind(""{0}2"") %>' runat=""server"" /></InsertItemTemplate>
		</asp:TemplateField>", csUName);
							sb10.AppendFormat(@"

		HttpPostedFile file{0} = e.Values[""{0}2""] as HttpPostedFile;
		if (file{0}.ContentLength > 0) {{
			string dir{0} = Server.MapPath(string.Concat(""/upload/{1}/{2}/"", DateTime.Now.ToString(""yyyyMM""), ""/""));
			string fn{0} = string.Concat(""/upload/{1}/{2}/"", DateTime.Now.ToString(""yyyyMM""), ""/"", Guid.NewGuid().ToString(), Path.GetExtension(file{0}.FileName));
			if (!System.IO.Directory.Exists(dir{0})) System.IO.Directory.CreateDirectory(dir{0});
			file{0}.SaveAs(Server.MapPath(fn{0}));
			e.Values[""{0}""] = fn{0};
		}}
		e.Values.Remove(""{0}2"");", csUName, table.Name, col.Name);
						} else if (col.Type == SqlDbType.Image || col.Type == SqlDbType.Binary || col.Type == SqlDbType.VarBinary) {
							//上传文件
							sb4.AppendFormat(@"
		<asp:TemplateField HeaderText=""{0}"">
			<InsertItemTemplate><input type=""file"" id=""file{0}"" PostedFile='<%# Bind(""{0}2"") %>' runat=""server"" /></InsertItemTemplate>
		</asp:TemplateField>", csUName);
							sb10.AppendFormat(@"

		HttpPostedFile file{0} = e.Values[""{0}2""] as HttpPostedFile;
		if (file{0}.ContentLength > 0) {{
			MemoryStream ms = new MemoryStream();
			file{0}.InputStream.CopyTo(ms);
			e.Values[""{0}""] = ms.ToArray();
		}}
		e.Values.Remove(""{0}2"");", csUName);
						} else {
							sb4.AppendFormat(@"
		<asp:BoundField DataField=""{0}"" HeaderText=""{0}"">
			<ControlStyle CssClass=""datepicker"" Width=""60%"" />
		</asp:BoundField>", csUName);
						}
					}

					// m -> n 关系
					_tables.ForEach(delegate (TableInfo t2) {
						ForeignKeyInfo fk = t2.ForeignKeys.Find(delegate (ForeignKeyInfo ffk) {
							if (ffk.ReferencedTable.FullName == table.FullName) {
								return true;
							}
							return false;
						});
						if (fk == null) return;
						if (fk.Table.FullName == table.FullName) return;
						List<ForeignKeyInfo> fk2 = t2.ForeignKeys.FindAll(delegate (ForeignKeyInfo ffk2) {
							return ffk2 != fk;
						});
						if (fk2.Count != 1) return;
						if (t2.Columns.Count != 2) return; //mn表若不是两个字段，则不处理

						//t2.Columns
						string addname = t2.ClassName;
						if (t2.ClassName.StartsWith(table.ClassName + "_")) {
							addname = t2.ClassName.Substring(table.ClassName.Length + 1);
						} else if (t2.ClassName.EndsWith("_" + table.ClassName)) {
							addname = t2.ClassName.Remove(addname.Length - table.ClassName.Length - 1);
						} else if (fk2.Count == 1 && t2.ClassName.EndsWith("_" + table.ClassName)) {
							addname = t2.ClassName.Remove(t2.ClassName.Length - table.ClassName.Length - 1);
						} else if (fk2.Count == 1 && t2.ClassName.EndsWith("_" + fk2[0].ReferencedTable.Name)) {
							addname = t2.ClassName;
						}
						if (string.Compare(fk2[0].ReferencedTable.Name, addname, true) != 0) return;

						ColumnInfo textCol = fk2[0].ReferencedTable.Columns.Find(delegate (ColumnInfo fci2) {
							return fci2.Name.ToLower().IndexOf("name") != -1 || fci2.Name.ToLower().IndexOf("title") != -1;
						});
						if (textCol == null) textCol = fk2[0].ReferencedTable.Columns.Find(delegate (ColumnInfo fci2) {
							return GetCSType(fci2.Type) == "string" && fci2.Length > 0 && fci2.Length < 128;
						});
						if (textCol == null) textCol = fk2[0].ReferencedTable.Columns[0];
						if (sb16.ToString().Length == 0)
							sb16.AppendFormat(@"
<%@ Register Src=""~/controls/mn_htmlselect.ascx"" TagPrefix=""uc"" TagName=""mn_htmlselect"" %>");
						sb4.AppendFormat(@"
		<asp:TemplateField HeaderText=""{0}"">
			<InsertItemTemplate><uc:mn_htmlselect id=""mn_htmlselect{0}"" runat=""server"" Value='<%# Bind(""Obj_{3}s"") %>' DataSourceID=""ods{0}"" DataTextField=""{1}"" DataValueField=""{2}"" Placeholder=""Select a {0}"" /></InsertItemTemplate>
		</asp:TemplateField>", CodeBuild.UFString(addname), CodeBuild.UFString(textCol.Name), CodeBuild.UFString(fk2[0].ReferencedTable.PrimaryKeys[0].Name), CodeBuild.LFString(addname));
						sb15.AppendFormat(@"
	string str{0};
	void Flag{0}() {{
		if (string.IsNullOrEmpty(str{0})) return;
		List<string> str{0}s = new List<string>(str{0}.Split(','));
		foreach (string str{0}sin in str{0}s) if (!string.IsNullOrEmpty(str{0}sin)) item.Flag{0}({1});
	}}", CodeBuild.UFString(addname), string.Format(GetCSConvert2(fk2[0].ReferencedTable.PrimaryKeys[0].Type), "str" + CodeBuild.UFString(addname) + "sin"));
						sb14.AppendFormat(@"
		Flag{0}();", CodeBuild.UFString(addname));

						sb10.AppendFormat(@"
		str{0} = string.Concat(e.Values[""Obj_{1}s""]);
		e.Values.Remove(""Obj_{1}s"");", CodeBuild.UFString(addname), CodeBuild.LFString(addname));

						string pkSqlWhere = "";
						int pkSqlWhere_idx = -1;
						string pkValuesParms = "";
						table.PrimaryKeys.ForEach(delegate (ColumnInfo tpfic) {
							pkSqlWhere += " AND a.[" + tpfic.Name + "] = {" + ++pkSqlWhere_idx + "}";
							pkValuesParms += ", " + string.Format(GetCSConvert2(tpfic.Type), string.Format(@"string.Concat(e.NewValues[""{0}""])", CodeBuild.UFString(tpfic.Name)));
							//pkValuesParms += string.Format(GetCSConvert2(tpfic.Type), string.Format(@"string.Concat(e.NewValues[""{0}""])", CodeBuild.UFString(tpfic.Name))) + ", ";
						});
						if (pkSqlWhere.Length > 0) pkSqlWhere = pkSqlWhere.Substring(5);
						if (pkValuesParms.Length > 0) pkValuesParms = pkValuesParms.Substring(2);
						string getitem = string.Format(@"
		if (item == null) item = {0}.Select.Where(""{1}"", {2}).ToOne();", uClass_Name, pkSqlWhere, pkValuesParms);
						if (sb11.ToString().IndexOf(getitem) == -1) sb11.Append(getitem);
						sb13.AppendFormat(@"
<asp:ObjectDataSource ID=""ods{0}"" runat=""server"" SelectMethod=""GetItems"" TypeName=""{1}.BLL.{0}""></asp:ObjectDataSource>",
							CodeBuild.UFString(addname), solutionName);
					});

					sb7.Append(sb15.ToString());
					sb1.AppendFormat(@"<%@ Page Language=""C#"" %>{15}

<script runat=""server"">
	
	protected void dv{11}_ItemInserting(object sender, DetailsViewInsertEventArgs e) {{{6}{8}
	}}
	protected void dv{11}_ItemInserted(object sender, DetailsViewInsertedEventArgs e) {{
		if (e.Exception != null) {{
			base.End(e.Exception.InnerException != null ? e.Exception.InnerException.Message : e.Exception.Message);
			return;
		}}
		base.Goto(""./"", ""添加成功"");
	}}
	
	protected void ods{11}_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {{
		item = e.ReturnValue as {11}Info;{14}
	}}
	{11}Info item;
	{5}
	public static void IAjax(HttpRequest Request, HttpResponse Response) {{
		switch(Request.Form[""method""]) {{
			case ""xxx"":
				break;{4}
		}}
	}}
</script>

<script type=""text/javascript"">{3}
</script>

<div class=""box"">
	<div class=""box-header with-border"">
		<h3 class=""box-title"" id=""box-title""></h3>
	</div>
	<div class=""box-body"">
		<div class=""table-responsive"">

<form id=""form_add"" runat=""server"">
<asp:DetailsView ID=""dv{11}"" runat=""server"" AutoGenerateRows=""False"" DataSourceID=""ods{11}"" DefaultMode=""Insert"" 
	OnItemInserting=""dv{11}_ItemInserting"" OnItemInserted=""dv{11}_ItemInserted"" class=""table table-bordered table-hover"">
	<Fields>{2}
		<asp:CommandField ButtonType=""Button"" ShowInsertButton=""True"" />
	</Fields>
</asp:DetailsView>
</form>

		</div>
	</div>
</div>

<asp:ObjectDataSource ID=""ods{11}"" runat=""server"" TypeName=""{10}"" InsertMethod=""Insert"" OnInserted=""ods{11}_Inserted"">
	<InsertParameters>{12}
	</InsertParameters>
</asp:ObjectDataSource>{13}
", sb2.ToString(), sb3.ToString(), sb4.ToString(), sb5.ToString(), sb6.ToString(), sb7.ToString(), sb8.ToString(), sb9.ToString(),
			   sb10.ToString(), sb11.ToString(), Class_Name_BLL_Full, uClass_Name, sb12.ToString(), sb13.ToString(), sb14.ToString(), sb16.ToString());

					loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, nClass_Name, @"\add.aspx"), Deflate.Compress(sb1.ToString())));
					clearSb();
					#endregion

					#region edit.aspx
					pkfeidx = 0;
					pkEvals = string.Empty;
					pkCheckParms = string.Empty;
					pkFuncParms = string.Empty;
					pkUrlParms = string.Empty;
					pkAjaxParms = string.Empty;
					pkAjaxRequestConvert = string.Empty;
					pkFilterNotEqualsOr = string.Empty;
					pkRequestForms = string.Empty;
					pkCsFuncParms = string.Empty;
					pkInsertingValues = string.Empty;
					pkUpdatingNewValues = string.Empty;
					table.PrimaryKeys.ForEach(delegate (ColumnInfo col88) {
						string csUName = CodeBuild.UFString(col88.Name);
						string csType = CodeBuild.GetCSType(col88.Type);
						pkEvals += string.Format(@" {0}1='<%# Eval(""{0}"") %>'", csUName);
						pkCheckParms += string.Format(@",$(this).attr('{0}1')", csUName);
						pkFuncParms += string.Format(@", {0}", csUName);
						pkUrlParms += string.Format(@"&{1}={{{0}}}", pkfeidx + 1, col88.Name);
						pkAjaxParms += string.Format(@", {0} : {1}", col88.Name, csUName);
						pkAjaxRequestConvert += string.Format(@"
		{1} {0};
		{2};", csUName, csType.Trim('?'),
				 string.Format(CodeBuild.GetCSConvert(col88.Type), string.Format(@"string.Concat({0}1)", csUName), csUName));
						pkFilterNotEqualsOr += string.Format(@" or [{1}] <> {{{0}}}", pkfeidx + 1, col88.Name);
						pkRequestForms += string.Format(@", Request.Form[""{0}""]", col88.Name);
						pkCsFuncParms += string.Format(@", object {0}1", csUName);
						pkUpdatingNewValues += string.Format(@", e.NewValues[""{0}""]", csUName);
						pkfeidx++;
					});
					if (pkFilterNotEqualsOr.Length > 0) pkFilterNotEqualsOr = " and (" + pkFilterNotEqualsOr.Substring(4) + ")";
					foreach (ColumnInfo col in table.Columns) {
						string csType = CodeBuild.GetCSType(col.Type);
						string csType2 = CodeBuild.GetCSType2(col.Type);
						string csUName = CodeBuild.UFString(col.Name);
						string lname = col.Name.ToLower();
						string rfvEmpty = string.Empty;
						List<ColumnInfo> us = table.Uniques.Find(delegate (List<ColumnInfo> cs) {
							return cs.Find(delegate (ColumnInfo col88) {
								return col88.Name == col.Name;
							}) != null;
						});
						if (us == null) us = new List<ColumnInfo>();
						List<ForeignKeyInfo> fks_comb = table.ForeignKeys.FindAll(delegate (ForeignKeyInfo fk) {
							return fk.Columns.Count == 1 && fk.Columns[0].Name == col.Name;
						});

						if (col.IsPrimaryKey) {
							sb2.AppendFormat(@"
		<asp:QueryStringParameter Name=""{0}"" QueryStringField=""{1}"" Type=""{2}"" />", csUName, col.Name, csType2);
						}
						if (!col.IsIdentity)
							sb12.AppendFormat(@"
		<asp:Parameter Name=""{0}"" Type=""{1}"" />", csUName, csType2);
						sb3.AppendFormat(@"
		<asp:Parameter Name=""{0}"" Type=""{1}"" />", csUName, csType2);
						if (!col.IsNullable && csType == "string") {
							rfvEmpty = string.Format(@"
				<asp:RequiredFieldValidator ID=""rfv{0}"" runat=""server"" ControlToValidate=""textbox{0}"" ErrorMessage=""*"" />", csUName);
							rfvEmpty = "";
						}
						if (csType == "bool?") {
							sb4.AppendFormat(@"
		<asp:CheckBoxField DataField=""{0}"" HeaderText=""{0}"" />", csUName);
						} else if (csType == "DateTime?" && (
							string.Compare(lname, "create_time", true) == 0 ||
							string.Compare(lname, "update_time", true) == 0
							)) {
							sb4.AppendFormat(@"
		<asp:TemplateField HeaderText=""{0}"">
			<EditItemTemplate><asp:Label ID=""label{0}"" runat=""server"" Text='<%# Bind(""{0}"") %>' /></EditItemTemplate>
		</asp:TemplateField>", csUName);
							if (string.Compare(lname, "update_time", true) == 0) {
								sb11.AppendFormat(@"
		e.NewValues[""{0}""] = DateTime.Now;", csUName);
							}
						} else if (col.IsPrimaryKey && col.IsIdentity) {
							//主键自动增值
							sb4.AppendFormat(@"
		<asp:TemplateField HeaderText=""{0}"">
			<EditItemTemplate><asp:Label ID=""label{0}"" runat=""server"" Text='<%# Bind(""{0}"") %>' /></EditItemTemplate>
		</asp:TemplateField>", csUName);
						} else if (!col.IsIdentity && us.Count == 1 || col.IsPrimaryKey && table.PrimaryKeys.Count == 1) {
							//主键或唯一键，非自动增值
							string pkEditTemplate = col.IsPrimaryKey ? string.Format(@"<asp:Label ID=""label{0}"" runat=""server"" Text='<%# Bind(""{0}"") %>' />", csUName) :
								string.Format(@"<input type=""text"" ID=""textbox{0}"" runat=""server"" value='<%# Bind(""{0}"") %>'{1} onchange=""this.ajax_changed=true;"" onblur=""if(this.ajax_changed){{AjaxCheck{0}(this.value{2});this.ajax_changed=false;}}"" class=""datepicker"" style=""width:60%;"" />
				<span id=""textbox{0}Message""></span>{3}", csUName, pkEvals, pkCheckParms, rfvEmpty);
							sb5.AppendFormat(@"
function AjaxCheck{0}(value{3}) {{
	value = value.trim();
	if (value == '') return;
	var args = {{ method : 'AjaxCheck{0}', {1} : value{2} }};
	var callback = function(rt) {{
		$('#textbox{0}Message').html(rt == '1' ? '<font color=red>已存在</font>' : '');
	}};
	<%= Ajax.Register(this, ""callback"", ""args"") %>
}}", csUName, col.Name, pkAjaxParms, pkFuncParms);
							sb4.AppendFormat(@"
		<asp:TemplateField HeaderText=""{0}"">
			<EditItemTemplate>
				{1}
			</EditItemTemplate>
		</asp:TemplateField>", csUName, pkEditTemplate);

							sb6.AppendFormat(@"
			case ""AjaxCheck{0}"":
				Response.Write(AjaxCheck{0}(Request.Form[""{1}""]{2}) ? 1 : 0);
				break;", csUName, col.Name, pkRequestForms);
							sb7.AppendFormat(@"
	protected static bool AjaxCheck{0}(object value1{6}) {{
		string value = string.Concat(value1).Trim();{3}
		return {2}.Select.Where(""[{1}] = {{0}}{5}"", value{4}).ToOne() != null;
	}}", csUName, col.Name, uClass_Name, pkAjaxRequestConvert, pkFuncParms, pkFilterNotEqualsOr, pkCsFuncParms);

							sb9.AppendFormat(@"
		if (AjaxCheck{0}(e.NewValues[""{0}""]{1})) {{
			base.End(string.Format(""已存在{0}“{{0}}”！"", e.NewValues[""{0}""]));
			e.Cancel = true;
			return;
		}}", csUName, pkUpdatingNewValues);
						} else if (fks_comb.Count == 1) {
							//外键下拉框
							ForeignKeyInfo fkcb = fks_comb[0];
							string FK_uClass_Name = fkcb.ReferencedTable != null ? CodeBuild.UFString(fkcb.ReferencedTable.ClassName) :
								CodeBuild.UFString(TableInfo.GetClassName(fkcb.ReferencedTableName));

							sb4.AppendFormat(@"
		<asp:TemplateField HeaderText=""{0}"">
			<EditItemTemplate><asp:HtmlSelect{1} ID=""{1}{0}"" Value='<%# Bind(""{0}"") %>' runat=""server"" /></EditItemTemplate>
		</asp:TemplateField>", csUName, FK_uClass_Name);

							if (!col.IsNullable) {
								sb9.AppendFormat(@"
		if (string.IsNullOrEmpty(string.Concat(e.NewValues[""{0}""]))) {{
			base.End(""请选择一个{0}！"");
			e.Cancel = true;
			return;
		}}", csUName);
							}
						} else if (!string.IsNullOrEmpty(rfvEmpty)) {
							sb4.AppendFormat(@"
		<asp:TemplateField HeaderText=""{0}"">
			<EditItemTemplate>
				<asp:TextBox ID=""textbox{0}"" runat=""server"" Text='<%# Bind(""{0}"") %>' />{1}
			</EditItemTemplate>
		</asp:TemplateField>", csUName, rfvEmpty);
						} else if ((col.Type == SqlDbType.BigInt || col.Type == SqlDbType.Int) && (lname == "status" || lname.StartsWith("status_") || lname.EndsWith("_status"))) {
							//加载 multi 多状态字段
							sb4.AppendFormat(@"
		<asp:TemplateField HeaderText=""{0}"">
			<EditItemTemplate><input ID=""label{0}"" type=""hidden"" value='<%# Bind(""{0}"") %>' runat=""server"" multi_status=""状态1,状态2,状态3,状态4,状态5"" /></EditItemTemplate>
		</asp:TemplateField>", csUName);
						} else if (col.Type == SqlDbType.BigInt || col.Type == SqlDbType.Int || col.Type == SqlDbType.SmallInt) {
							sb4.AppendFormat(@"
		<asp:TemplateField HeaderText=""{0}"">
			<EditItemTemplate><input type=""text"" ID=""textbox{0}"" value='<%# Bind(""{0}"") %>' runat=""server"" class=""form-control"" data-inputmask=""'mask': '9', 'repeat': 6, 'greedy': false"" data-mask style=""width:200px;"" /></EditItemTemplate>
		</asp:TemplateField>", csUName);
						} else if (col.Type == SqlDbType.Decimal || col.Type == SqlDbType.Float || col.Type == SqlDbType.Money || col.Type == SqlDbType.Real) {
							sb4.AppendFormat(@"
		<asp:TemplateField HeaderText=""{0}"">
			<EditItemTemplate>
<div class=""input-group"" style=""width:200px;"">
	<span class=""input-group-addon"">￥</span>
	<input type=""text"" ID=""textbox{0}"" value='<%# Bind(""{0}"") %>' runat=""server"" class=""form-control"" data-inputmask=""'mask': '9', 'repeat': 10, 'greedy': false"" data-mask />
	<span class=""input-group-addon"">.00</span>
</div>
			</EditItemTemplate>
		</asp:TemplateField>", csUName);
						} else if (col.Type == SqlDbType.DateTime || col.Type == SqlDbType.SmallDateTime) {
							//日期
							sb4.AppendFormat(@"
		<asp:BoundField DataField=""{0}"" HeaderText=""{0}"">
			<ControlStyle CssClass=""datepicker"" />
		</asp:BoundField>", csUName);
						} else if (col.Type == SqlDbType.Date) {
							//日期控件
							sb4.AppendFormat(@"
		<asp:TemplateField HeaderText=""{0}"">
			<EditItemTemplate>
<div class=""input-group date"" style=""width:200px;"">
	<div class=""input-group-addon""><i class=""fa fa-calendar""></i></div>
	<input type=""text"" ID=""textbox{0}"" value='<%# Bind(""{0}"") %>' runat=""server"" data-provide=""datepicker"" class=""form-control pull-right"" readonly />
</div>
			</EditItemTemplate>
		</asp:TemplateField>", csUName);
						} else if (col.Type == SqlDbType.Text || col.Type == SqlDbType.NText ||
							(col.Length == -1 && (col.Type == SqlDbType.Char || col.Type == SqlDbType.NChar ||
								col.Type == SqlDbType.VarChar || col.Type == SqlDbType.NVarChar))) {
							//加载百度编辑器
							sb4.AppendFormat(@"
		<asp:TemplateField HeaderText=""{0}"">
			<EditItemTemplate><textarea id=""textarea{0}"" Value='<%# Bind(""{0}"") %>' runat=""server"" style=""width:100%;height:100px;"" editor=""ueditor"" /></EditItemTemplate>
		</asp:TemplateField>", csUName);
						} else if ((lname == "img" || lname.StartsWith("img_") || lname.EndsWith("_img")) && (col.Type == SqlDbType.VarChar || col.Type == SqlDbType.NVarChar)) {
							//上传图片
							sb4.AppendFormat(@"
		<asp:TemplateField HeaderText=""{0}"">
			<EditItemTemplate>
				<input type=""text"" id=""textbox{0}"" Value='<%# Bind(""{0}"") %>' runat=""server"" style=""width:80%;"" />
				<input type=""file"" id=""file{0}"" PostedFile='<%# Bind(""{0}2"") %>' runat=""server"" />
			</EditItemTemplate>
		</asp:TemplateField>", csUName);
							sb11.AppendFormat(@"

		HttpPostedFile file{0} = e.NewValues[""{0}2""] as HttpPostedFile;
		if (file{0}.ContentLength > 0) {{
			string dir{0} = Server.MapPath(string.Concat(""/upload/{1}/{2}/"", DateTime.Now.ToString(""yyyyMM""), ""/""));
			string fn{0} = string.Concat(""/upload/{1}/{2}/"", DateTime.Now.ToString(""yyyyMM""), ""/"", Guid.NewGuid().ToString(), Path.GetExtension(file{0}.FileName));
			if (!System.IO.Directory.Exists(dir{0})) System.IO.Directory.CreateDirectory(dir{0});
			file{0}.SaveAs(Server.MapPath(fn{0}));
			e.NewValues[""{0}""] = fn{0};
		}}
		string fn{0}old = string.Concat(e.OldValues[""{0}""]);
		if (file{0}.ContentLength > 0 || string.Compare(fn{0}old, string.Concat(e.NewValues[""{0}""]), true) != 0) {{
			if (!string.IsNullOrEmpty(fn{0}old)) {{
				fn{0}old = Server.MapPath(fn{0}old);
				if (System.IO.File.Exists(fn{0}old)) System.IO.File.Delete(fn{0}old);
			}}
		}}
		e.NewValues.Remove(""{0}2"");", csUName, table.Name, col.Name);
						} else if (col.Type == SqlDbType.Image || col.Type == SqlDbType.Binary || col.Type == SqlDbType.VarBinary) {
							//上传文件
							sb4.AppendFormat(@"
		<asp:TemplateField HeaderText=""{0}"">
			<EditItemTemplate>
				<img src='data:image/png;base64,<%# Eval(""{0}"") %>' width=""800"" />
				<input type=""file"" id=""file{0}"" PostedFile='<%# Bind(""{0}2"") %>' runat=""server"" />
			</EditItemTemplate>
		</asp:TemplateField>", csUName, pkEvalsQuerystring);
							sb11.AppendFormat(@"

		HttpPostedFile file{0} = e.NewValues[""{0}2""] as HttpPostedFile;
		if (file{0}.ContentLength > 0) {{
			MemoryStream ms = new MemoryStream();
			file{0}.InputStream.CopyTo(ms);
			e.NewValues[""{0}""] = ms.ToArray();
		}}
		e.NewValues.Remove(""{0}2"");", csUName);

							if (sb20.Length == 0) {
								sb20.AppendFormat(@"
	protected override void OnInit(EventArgs e) {{
		string field = Request.QueryString[""field""];
		if (!string.IsNullOrEmpty(field)) {{
			int Id = Lib.ConvertTo<int>(Request.QueryString[""Id""]);
			item = Topic_photo.GetItem(Id);
			Response.BinaryWrite(item.Datastream);
			Response.End();
		}}
		base.OnInit(e);
	}}");
							}
						} else {
							sb4.AppendFormat(@"
		<asp:BoundField DataField=""{0}"" HeaderText=""{0}"">
			<ControlStyle CssClass=""datepicker"" Width=""60%"" />
		</asp:BoundField>", csUName);
						}
					}

					// m -> n 关系
					_tables.ForEach(delegate (TableInfo t2) {
						ForeignKeyInfo fk = t2.ForeignKeys.Find(delegate (ForeignKeyInfo ffk) {
							if (ffk.ReferencedTable.FullName == table.FullName) {
								return true;
							}
							return false;
						});
						if (fk == null) return;
						if (fk.Table.FullName == table.FullName) return;
						List<ForeignKeyInfo> fk2 = t2.ForeignKeys.FindAll(delegate (ForeignKeyInfo ffk2) {
							return ffk2 != fk;
						});
						if (fk2.Count != 1) return;
						if (t2.Columns.Count != 2) return; //mn表若不是两个字段，则不处理

						//t2.Columns
						string addname = t2.ClassName;
						if (t2.ClassName.StartsWith(table.ClassName + "_")) {
							addname = t2.ClassName.Substring(table.ClassName.Length + 1);
						} else if (t2.ClassName.EndsWith("_" + table.ClassName)) {
							addname = t2.ClassName.Remove(addname.Length - table.ClassName.Length - 1);
						} else if (fk2.Count == 1 && t2.ClassName.EndsWith("_" + table.ClassName)) {
							addname = t2.ClassName.Remove(t2.ClassName.Length - table.ClassName.Length - 1);
						} else if (fk2.Count == 1 && t2.ClassName.EndsWith("_" + fk2[0].ReferencedTable.Name)) {
							addname = t2.ClassName;
						}
						if (string.Compare(fk2[0].ReferencedTable.Name, addname, true) != 0) return;

						ColumnInfo textCol = fk2[0].ReferencedTable.Columns.Find(delegate (ColumnInfo fci2) {
							return fci2.Name.ToLower().IndexOf("name") != -1 || fci2.Name.ToLower().IndexOf("title") != -1;
						});
						if (textCol == null) textCol = fk2[0].ReferencedTable.Columns.Find(delegate (ColumnInfo fci2) {
							return GetCSType(fci2.Type) == "string" && fci2.Length > 0 && fci2.Length < 128;
						});
						if (textCol == null) textCol = fk2[0].ReferencedTable.Columns[0];
						if (sb16.ToString().Length == 0)
							sb16.AppendFormat(@"
<%@ Register Src=""~/controls/mn_htmlselect.ascx"" TagPrefix=""uc"" TagName=""mn_htmlselect"" %>");
						sb4.AppendFormat(@"
		<asp:TemplateField HeaderText=""{0}"">
			<EditItemTemplate><uc:mn_htmlselect id=""mn_htmlselect{0}"" runat=""server"" Value='<%# Bind(""Obj_{3}s"") %>' DataSourceID=""ods{0}"" DataTextField=""{1}"" DataValueField=""{2}"" Placeholder=""Select a {0}"" /></EditItemTemplate>
		</asp:TemplateField>", CodeBuild.UFString(addname), CodeBuild.UFString(textCol.Name), CodeBuild.UFString(fk2[0].ReferencedTable.PrimaryKeys[0].Name), CodeBuild.LFString(addname));
						sb15.AppendFormat(@"
	string str{0};
	void Flag{0}() {{
		if (string.IsNullOrEmpty(str{0})) {{
			item.Unflag{0}ALL();
			return;
		}}
		List<string> str{0}s = new List<string>(str{0}.Split(','));
		foreach ({0}Info Obj_{1} in item.Obj_{1}s) {{
			int idx = str{0}s.FindIndex(fstr{0} => string.Compare(fstr{0}, string.Concat(Obj_{1}.Id), true) == 0);
			if (idx == -1) item.Unflag{0}(Obj_{1}.Id);
			else str{0}s.RemoveAt(idx);
		}}
		foreach (string str{0}sin in str{0}s) if (!string.IsNullOrEmpty(str{0}sin)) item.Flag{0}({2});
	}}", CodeBuild.UFString(addname), CodeBuild.LFString(addname), string.Format(GetCSConvert2(fk2[0].ReferencedTable.PrimaryKeys[0].Type), "str" + CodeBuild.UFString(addname) + "sin"));
						sb14.AppendFormat(@"
		Flag{0}();", CodeBuild.UFString(addname));

						string pkSqlWhere = "";
						int pkSqlWhere_idx = -1;
						string pkValuesParms = "";
						table.PrimaryKeys.ForEach(delegate (ColumnInfo tpfic) {
							pkSqlWhere += " AND a.[" + tpfic.Name + "] = {" + ++pkSqlWhere_idx + "}";
							pkValuesParms += ", " + string.Format(GetCSConvert2(tpfic.Type), string.Format(@"string.Concat(e.NewValues[""{0}""])", CodeBuild.UFString(tpfic.Name)));
							//pkValuesParms += string.Format(GetCSConvert2(tpfic.Type), string.Format(@"string.Concat(e.NewValues[""{0}""])", CodeBuild.UFString(tpfic.Name))) + ", ";
						});
						if (pkSqlWhere.Length > 0) pkSqlWhere = pkSqlWhere.Substring(5);
						if (pkValuesParms.Length > 0) pkValuesParms = pkValuesParms.Substring(2);
						string getitem = string.Format(@"
		if (item == null) item = {0}.Select.Where(""{1}"", {2}).ToOne();", uClass_Name, pkSqlWhere, pkValuesParms);
						if (sb11.ToString().IndexOf(getitem) == -1) sb11.Append(getitem);
						sb11.AppendFormat(@"
		str{0} = string.Concat(e.NewValues[""Obj_{1}s""]);
		Flag{0}();
		e.NewValues.Remove(""Obj_{1}s"");", CodeBuild.UFString(addname), CodeBuild.LFString(addname));
						sb13.AppendFormat(@"
<asp:ObjectDataSource ID=""ods{0}"" runat=""server"" SelectMethod=""GetItems"" TypeName=""{1}.BLL.{0}""></asp:ObjectDataSource>",
							CodeBuild.UFString(addname), solutionName);
					});

					sb7.Append(sb15.ToString());
					sb1.AppendFormat(@"<%@ Page Language=""C#"" %>{15}

<script runat=""server"">

	protected void dv{11}_ItemUpdating(object sender, DetailsViewUpdateEventArgs e) {{{7}{9}
	}}
	protected void dv{11}_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e) {{
		if (e.Exception != null) {{
			base.End(e.Exception.InnerException != null ? e.Exception.InnerException.Message : e.Exception.Message);
			return;
		}}
		base.Goto(""./"", ""修改成功"");
	}}
	
	protected void ods{11}_Selected(object sender, ObjectDataSourceStatusEventArgs e) {{
		if (e.ReturnValue == null) {{
			base.End(""找不到记录."");
			return;
		}}
		item = e.ReturnValue as {11}Info;
	}}
	{11}Info item;
	{5}
	public static void IAjax(HttpRequest Request, HttpResponse Response) {{
		switch(Request.Form[""method""]) {{
			case ""xxx"":
				break;{4}
		}}
	}}{16}
</script>

<script type=""text/javascript"">{3}
</script>

<div class=""box"">
	<div class=""box-header with-border"">
		<h3 class=""box-title"" id=""box-title""></h3>
	</div>
	<div class=""box-body"">
		<div class=""table-responsive"">

<form id=""form_add"" runat=""server"">
<asp:DetailsView ID=""dv{11}"" runat=""server"" AutoGenerateRows=""False"" DataSourceID=""ods{11}"" DefaultMode=""Edit"" 
	OnItemUpdating=""dv{11}_ItemUpdating"" OnItemUpdated=""dv{11}_ItemUpdated"" class=""table table-bordered table-hover"">
	<Fields>{2}
		<asp:CommandField ButtonType=""Button"" ShowEditButton=""True"" />
	</Fields>
</asp:DetailsView>
</form>

		</div>
	</div>
</div>

<asp:ObjectDataSource ID=""ods{11}"" runat=""server"" TypeName=""{10}"" UpdateMethod=""Update"" SelectMethod=""GetItem"" OnSelected=""ods{11}_Selected"">
	<SelectParameters>{0}
	</SelectParameters>
	<UpdateParameters>{1}
	</UpdateParameters>
</asp:ObjectDataSource>{13}
", sb2.ToString(), sb3.ToString(), sb4.ToString(), sb5.ToString(), sb6.ToString(), sb7.ToString(), sb8.ToString(), sb9.ToString(),
			   sb10.ToString(), sb11.ToString(), Class_Name_BLL_Full, uClass_Name, sb12.ToString(), sb13.ToString(), sb14.ToString(), sb16.ToString(), sb20.ToString());

					loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, nClass_Name, @"\edit.aspx"), Deflate.Compress(sb1.ToString())));
					clearSb();
					#endregion

					#region edit.aspx(旧的，添加/编辑都写在一个文件中)
					//					int pkfeidx = 0;
					//					string pkEvals = string.Empty;
					//					string pkCheckParms = string.Empty;
					//					string pkFuncParms = string.Empty;
					//					string pkUrlParms = string.Empty;
					//					string pkAjaxRequestConvert = string.Empty;
					//					string pkFilterNotEqualsOr = string.Empty;
					//					string pkRequestForms = string.Empty;
					//					string pkCsFuncParms = string.Empty;
					//					string pkInsertingValues = string.Empty;
					//					string pkUpdatingNewValues = string.Empty;
					//					table.PrimaryKeys.ForEach(delegate (ColumnInfo col88) {
					//						string csUName = CodeBuild.UFString(col88.Name);
					//						string csType = CodeBuild.GetCSType(col88.Type);
					//						pkEvals += string.Format(@" {0}1='<%# Eval(""{0}"") %>'", csUName);
					//						pkCheckParms += string.Format(@",$(this).attr('{0}1')", csUName);
					//						pkFuncParms += string.Format(@", {0}", csUName);
					//						pkUrlParms += string.Format(@"&{1}={{{0}}}", pkfeidx + 1, col88.Name);
					//						pkAjaxRequestConvert += string.Format(@"
					//		{1} {0};
					//		{2};", csUName, csType.Trim('?'),
					//				 string.Format(CodeBuild.GetCSConvert(col88.Type), string.Format(@"string.Concat({0}1)", csUName), csUName));
					//						pkFilterNotEqualsOr += string.Format(@" or [{1}] <> {{{0}}}", pkfeidx + 1, col88.Name);
					//						pkRequestForms += string.Format(@", Request.Form[""{0}""]", col88.Name);
					//						pkCsFuncParms += string.Format(@", object {0}1", csUName);
					//						pkInsertingValues += string.Format(@", e.Values[""{0}""]", csUName);
					//						pkUpdatingNewValues += string.Format(@", e.NewValues[""{0}""]", csUName);
					//						pkfeidx++;
					//					});
					//					if (pkFilterNotEqualsOr.Length > 0) pkFilterNotEqualsOr = " and (" + pkFilterNotEqualsOr.Substring(4) + ")";
					//					foreach (ColumnInfo col in table.Columns) {
					//						string csType = CodeBuild.GetCSType(col.Type);
					//						string csType2 = CodeBuild.GetCSType2(col.Type);
					//						string csUName = CodeBuild.UFString(col.Name);
					//						string lname = col.Name.ToLower();
					//						string rfvEmpty = string.Empty;
					//						List<ColumnInfo> us = table.Uniques.Find(delegate (List<ColumnInfo> cs) {
					//							return cs.Find(delegate (ColumnInfo col88) {
					//								return col88.Name == col.Name;
					//							}) != null;
					//						});
					//						if (us == null) us = new List<ColumnInfo>();
					//						List<ForeignKeyInfo> fks_comb = table.ForeignKeys.FindAll(delegate (ForeignKeyInfo fk) {
					//							return fk.Columns.Count == 1 && fk.Columns[0].Name == col.Name;
					//						});

					//						if (col.IsPrimaryKey) {
					//							sb2.AppendFormat(@"
					//		<asp:QueryStringParameter Name=""{0}"" QueryStringField=""{1}"" Type=""{2}"" />", csUName, col.Name, csType2);
					//						}
					//						if (!col.IsIdentity)
					//							sb12.AppendFormat(@"
					//		<asp:Parameter Name=""{0}"" Type=""{1}"" />", csUName, csType2);
					//						sb3.AppendFormat(@"
					//		<asp:Parameter Name=""{0}"" Type=""{1}"" />", csUName, csType2);
					//						if (!col.IsNullable && csType == "string") {
					//							rfvEmpty = string.Format(@"
					//				<asp:RequiredFieldValidator ID=""rfv{0}"" runat=""server"" ControlToValidate=""textbox{0}"" ErrorMessage=""*"" />", csUName);
					//							rfvEmpty = "";
					//						}
					//						if (csType == "bool?") {
					//							sb4.AppendFormat(@"
					//		<asp:CheckBoxField DataField=""{0}"" HeaderText=""{0}"" />", csUName);
					//						} else if (csType == "DateTime?" && (
					//							string.Compare(lname, "create_time", true) == 0 ||
					//							string.Compare(lname, "update_time", true) == 0
					//							)) {
					//							sb4.AppendFormat(@"
					//		<asp:TemplateField HeaderText=""{0}"" InsertVisible=""False"">
					//			<EditItemTemplate><asp:Label ID=""label{0}"" runat=""server"" Text='<%# Bind(""{0}"") %>' /></EditItemTemplate>
					//		</asp:TemplateField>", csUName);
					//							if (string.Compare(lname, "create_time", true) == 0) {
					//								sb10.AppendFormat(@"
					//		e.Values[""{0}""] = DateTime.Now;", csUName);
					//							}
					//							if (string.Compare(lname, "update_time", true) == 0) {
					//								sb10.AppendFormat(@"
					//		e.Values[""{0}""] = DateTime.Now;", csUName);
					//								sb11.AppendFormat(@"
					//		e.NewValues[""{0}""] = DateTime.Now;", csUName);
					//							}
					//						}  else if (col.IsPrimaryKey && col.IsIdentity) {
					//							//主键自动增值
					//							sb4.AppendFormat(@"
					//		<asp:TemplateField HeaderText=""{0}"" InsertVisible=""False"">
					//			<EditItemTemplate><asp:Label ID=""label{0}"" runat=""server"" Text='<%# Bind(""{0}"") %>' /></EditItemTemplate>
					//		</asp:TemplateField>", csUName);
					//						} else if (!col.IsIdentity && us.Count == 1 || col.IsPrimaryKey && table.PrimaryKeys.Count == 1) {
					//							//主键或唯一键，非自动增值
					//							string pkEditTemplate = col.IsPrimaryKey ? string.Format(@"<asp:Label ID=""label{0}"" runat=""server"" Text='<%# Bind(""{0}"") %>' />", csUName) : 
					//								string.Format(@"<input type=""text"" ID=""textbox{0}"" runat=""server"" value='<%# Bind(""{0}"") %>'{1} onchange=""this.ajax_changed=true;"" onblur=""if(this.ajax_changed){{AjaxCheck{0}(this.value{2});this.ajax_changed=false;}}"" class=""datepicker"" style=""width:60%;"" />
					//				<span id=""textbox{0}Message""></span>{3}", csUName, pkEvals, pkCheckParms, rfvEmpty);
					//							sb5.AppendFormat(@"
					//function AjaxCheck{0}(value{3}) {{
					//	value = value.trim();
					//	if (value == '') return;
					//	var args = 'method=AjaxCheck{0}&{1}={{0}}{2}'.format(value{3});
					//	var callback = function(rt) {{
					//		$('#textbox{0}Message').html(rt ? '<font color=red>已存在</font>' : '');
					//	}};
					//	<%= Ajax.Register(this, ""callback"", ""args"") %>
					//}}", csUName, col.Name, pkUrlParms, pkFuncParms);
					//							sb4.AppendFormat(@"
					//		<asp:TemplateField HeaderText=""{0}"">
					//			<InsertItemTemplate>
					//				<input type=""text"" ID=""textbox{0}"" runat=""server"" value='<%# Bind(""{0}"") %>'{1} onchange=""this.ajax_changed=true;"" onblur=""if(this.ajax_changed){{AjaxCheck{0}(this.value{2});this.ajax_changed=false;}}"" class=""datepicker"" style=""width:60%;"" />
					//				<span id=""textbox{0}Message""></span>{3}
					//			</InsertItemTemplate>
					//			<EditItemTemplate>
					//				{4}
					//			</EditItemTemplate>
					//		</asp:TemplateField>", csUName, pkEvals, pkCheckParms, rfvEmpty, pkEditTemplate);

					//							sb6.AppendFormat(@"
					//			case ""AjaxCheck{0}"":
					//				Response.Write(AjaxCheck{0}(Request.Form[""{1}""]{2}) ? 1 : 0);
					//				break;", csUName, col.Name, pkRequestForms);
					//							sb7.AppendFormat(@"
					//	protected static bool AjaxCheck{0}(object value1{6}) {{
					//		string value = string.Concat(value1).Trim();{3}
					//		List<{2}Info> items = {2}.Select.Where(""[{1}] = {{0}}{5}"", value{4}).Limit(1).ToList();
					//		return items.Count == 1;
					//	}}", csUName, col.Name, uClass_Name, pkAjaxRequestConvert, pkFuncParms, pkFilterNotEqualsOr, pkCsFuncParms);

					//							sb8.AppendFormat(@"
					//		if (AjaxCheck{0}(e.Values[""{0}""]{1})) {{
					//			base.End(string.Format(""已存在{0}“{{0}}”！"", e.Values[""{0}""]));
					//			e.Cancel = true;
					//			return;
					//		}}", csUName, pkInsertingValues);
					//							sb9.AppendFormat(@"
					//		if (AjaxCheck{0}(e.NewValues[""{0}""]{1})) {{
					//			base.End(string.Format(""已存在{0}“{{0}}”！"", e.NewValues[""{0}""]));
					//			e.Cancel = true;
					//			return;
					//		}}", csUName, pkUpdatingNewValues);
					//						} else if (fks_comb.Count == 1) {
					//							//外键下拉框
					//							ForeignKeyInfo fkcb = fks_comb[0];
					//							string FK_uClass_Name = fkcb.ReferencedTable != null ? CodeBuild.UFString(fkcb.ReferencedTable.ClassName) :
					//								CodeBuild.UFString(TableInfo.GetClassName(fkcb.ReferencedTableName));

					//							sb4.AppendFormat(@"
					//		<asp:TemplateField HeaderText=""{0}"">
					//			<InsertItemTemplate><asp:HtmlSelect{1} ID=""{1}{0}"" Value='<%# Bind(""{0}"") %>' runat=""server"" /></InsertItemTemplate>
					//			<EditItemTemplate><asp:HtmlSelect{1} ID=""{1}{0}"" Value='<%# Bind(""{0}"") %>' runat=""server"" /></EditItemTemplate>
					//		</asp:TemplateField>", csUName, FK_uClass_Name);

					//							if (!col.IsNullable) {
					//								sb8.AppendFormat(@"
					//		if (string.IsNullOrEmpty(string.Concat(e.Values[""{0}""]))) {{
					//			base.End(""请选择一个{0}！"");
					//			e.Cancel = true;
					//			return;
					//		}}", csUName);
					//								sb9.AppendFormat(@"
					//		if (string.IsNullOrEmpty(string.Concat(e.NewValues[""{0}""]))) {{
					//			base.End(""请选择一个{0}！"");
					//			e.Cancel = true;
					//			return;
					//		}}", csUName);
					//							}
					//							if (fkcb.ReferencedTable != null) {
					//								#region HtmlSelect.cs
					//								string path = string.Concat(CONST.adminPath, @"App_Code\RichControls\HtmlSelect\", FK_uClass_Name, ".cs");
					//								if (!isMakedHtmlSelect.ContainsKey(path)) {
					//									isMakedHtmlSelect.Add(path, true);
					//									ForeignKeyInfo fkrr = fkcb.ReferencedTable != null ?
					//										fkcb.ReferencedTable.ForeignKeys.Find(delegate (ForeignKeyInfo fkkk) {
					//											return fkkk.ReferencedTable != null && fkcb.ReferencedTable.FullName == fkkk.ReferencedTable.FullName;
					//										}) : null;
					//									bool isHtmlSelect2 = fkcb.ReferencedTable != null && fkrr != null;
					//									string htmlSelectPlace = isHtmlSelect2 ? CONST.Admin_App_Code_RichControls_HtmlSelect_Style2_cs :
					//										CONST.Admin_App_Code_RichControls_HtmlSelect_Style1_cs;
					//									string FK_Class_Name_BLL_Space = string.Format(@"{0}.BLL", solutionName);
					//									string FK_Class_Name_Model_Space = string.Format(@"{0}.Model", solutionName);
					//									string FK_Class_Name_BLL_Full = string.Format(@"{0}.{1}", FK_Class_Name_BLL_Space, FK_uClass_Name);
					//									string FK_Class_Name_Model_Full = string.Format(@"{0}.{1}", FK_Class_Name_Model_Space, FK_uClass_Name);
					//									string FK_OrderBy = fkcb.ReferencedTable != null ?
					//										string.Join(",", fkcb.ReferencedTable.Clustereds.Count > 0 ?
					//											fkcb.ReferencedTable.Clustereds.ConvertAll<string>(delegate (ColumnInfo col88) {
					//												return "a.[" + col88.Name + "]" + (col88.Orderby == DataSort.ASC ? string.Empty : " " + col88.Orderby.ToString());
					//											}).ToArray() : fkcb.ReferencedTable.PrimaryKeys.ConvertAll<string>(delegate (ColumnInfo col88) {
					//												return "a.[" + col88.Name + "]" + (col88.Orderby == DataSort.ASC ? string.Empty : " " + col88.Orderby.ToString());
					//											}).ToArray()) : null;
					//									string FK_Column = fkcb.ReferencedTable != null ?
					//										CodeBuild.UFString(fkcb.ReferencedColumns[0].Name) : CodeBuild.UFString(fkcb.ReferencedColumnNames[0]);

					//									ColumnInfo strCol = fkcb.ReferencedTable.Columns.Find(delegate (ColumnInfo col99) {
					//										return col99.Name.ToLower().IndexOf("name") != -1 || col99.Name.ToLower().IndexOf("title") != -1;
					//									});
					//									if (strCol == null) strCol = fkcb.ReferencedTable.Columns.Find(delegate (ColumnInfo col99) {
					//										return GetCSType(col99.Type) == "string" && col99.Length > 0 && col99.Length < 128;
					//									});
					//									string FK_Column_Text = fkcb.ReferencedTable != null && strCol != null ? CodeBuild.UFString(strCol.Name)
					//										 : FK_Column;
					//									string getBy = isHtmlSelect2 ? CodeBuild.UFString(fkrr.Columns[0].Name) : string.Empty;
					//									string getType = isHtmlSelect2 ? CodeBuild.GetCSType(fkrr.Columns[0].Type) : string.Empty;
					//									htmlSelectPlace = string.Format(htmlSelectPlace, FK_uClass_Name, FK_OrderBy, FK_Column_Text, FK_Column, getBy,
					//										string.Format(@"
					//using {0};
					//using {1};", FK_Class_Name_BLL_Space, FK_Class_Name_Model_Space), getType, FK_uClass_Name);
					//									loc1.Add(new BuildInfo(path, Deflate.Compress(htmlSelectPlace)));
					//								}
					//								#endregion
					//							}
					//						} else if (!string.IsNullOrEmpty(rfvEmpty)) {
					//							sb4.AppendFormat(@"
					//		<asp:TemplateField HeaderText=""{0}"">
					//			<InsertItemTemplate>
					//				<asp:TextBox ID=""textbox{0}"" runat=""server"" Text='<%# Bind(""{0}"") %>' />{1}
					//			</InsertItemTemplate>
					//			<EditItemTemplate>
					//				<asp:TextBox ID=""textbox{0}"" runat=""server"" Text='<%# Bind(""{0}"") %>' />{1}
					//			</EditItemTemplate>
					//		</asp:TemplateField>", csUName, rfvEmpty);
					//						} else if ((col.Type == SqlDbType.BigInt || col.Type == SqlDbType.Int) && (lname == "status" || lname.StartsWith("status_") || lname.EndsWith("_status"))) {
					//							//加载 multi 多状态字段
					//							sb4.AppendFormat(@"
					//		<asp:TemplateField HeaderText=""{0}"">
					//			<InsertItemTemplate><input ID=""label{0}"" type=""hidden"" value='<%# Bind(""{0}"") %>' runat=""server"" multi_status=""状态1,状态2,状态3,状态4,状态5"" /></InsertItemTemplate>
					//			<EditItemTemplate><input ID=""label{0}"" type=""hidden"" value='<%# Bind(""{0}"") %>' runat=""server"" multi_status=""状态1,状态2,状态3,状态4,状态5"" /></EditItemTemplate>
					//		</asp:TemplateField>", csUName);
					//						} else if (col.Type == SqlDbType.BigInt || col.Type == SqlDbType.Int || col.Type == SqlDbType.SmallInt) {
					//							sb4.AppendFormat(@"
					//		<asp:TemplateField HeaderText=""{0}"">
					//			<InsertItemTemplate><input type=""text"" ID=""textbox{0}"" value='<%# Bind(""{0}"") %>' runat=""server"" class=""form-control"" data-inputmask=""'mask': '9', 'repeat': 6, 'greedy': false"" data-mask style=""width:200px;"" /></InsertItemTemplate>
					//			<EditItemTemplate><input type=""text"" ID=""textbox{0}"" value='<%# Bind(""{0}"") %>' runat=""server"" class=""form-control"" data-inputmask=""'mask': '9', 'repeat': 6, 'greedy': false"" data-mask style=""width:200px;"" /></EditItemTemplate>
					//		</asp:TemplateField>", csUName);
					//						} else if (col.Type == SqlDbType.Decimal || col.Type == SqlDbType.Float || col.Type == SqlDbType.Money || col.Type == SqlDbType.Real) {
					//							sb4.AppendFormat(@"
					//		<asp:TemplateField HeaderText=""{0}"">
					//			<InsertItemTemplate>
					//<div class=""input-group"" style=""width:200px;"">
					//	<span class=""input-group-addon"">￥</span>
					//	<input type=""text"" ID=""textbox{0}"" value='<%# Bind(""{0}"") %>' runat=""server"" class=""form-control"" data-inputmask=""'mask': '9', 'repeat': 10, 'greedy': false"" data-mask />
					//	<span class=""input-group-addon"">.00</span>
					//</div>
					//			</InsertItemTemplate>
					//			<EditItemTemplate>
					//<div class=""input-group"" style=""width:200px;"">
					//	<span class=""input-group-addon"">￥</span>
					//	<input type=""text"" ID=""textbox{0}"" value='<%# Bind(""{0}"") %>' runat=""server"" class=""form-control"" data-inputmask=""'mask': '9', 'repeat': 10, 'greedy': false"" data-mask />
					//	<span class=""input-group-addon"">.00</span>
					//</div>
					//			</EditItemTemplate>
					//		</asp:TemplateField>", csUName);
					//						} else if (col.Type == SqlDbType.DateTime || col.Type == SqlDbType.SmallDateTime) {
					//							//日期
					//							sb4.AppendFormat(@"
					//		<asp:BoundField DataField=""{0}"" HeaderText=""{0}"">
					//			<ControlStyle CssClass=""datepicker"" />
					//		</asp:BoundField>", csUName);
					//						} else if (col.Type == SqlDbType.Date) {
					//							//日期控件
					//							sb4.AppendFormat(@"
					//		<asp:TemplateField HeaderText=""{0}"">
					//			<InsertItemTemplate>
					//<div class=""input-group date"" style=""width:200px;"">
					//	<div class=""input-group-addon""><i class=""fa fa-calendar""></i></div>
					//	<input type=""text"" ID=""textbox{0}"" value='<%# Bind(""{0}"") %>' runat=""server"" data-provide=""datepicker"" class=""form-control pull-right"" readonly />
					//</div>
					//			</InsertItemTemplate>
					//			<EditItemTemplate>
					//<div class=""input-group date"" style=""width:200px;"">
					//	<div class=""input-group-addon""><i class=""fa fa-calendar""></i></div>
					//	<input type=""text"" ID=""textbox{0}"" value='<%# Bind(""{0}"") %>' runat=""server"" data-provide=""datepicker"" class=""form-control pull-right"" readonly />
					//</div>
					//			</EditItemTemplate>
					//		</asp:TemplateField>", csUName);
					//						} else if (col.Type == SqlDbType.Text || col.Type == SqlDbType.NText ||
					//							(col.Length == -1 && (col.Type == SqlDbType.Char || col.Type == SqlDbType.NChar ||
					//								col.Type == SqlDbType.VarChar || col.Type == SqlDbType.NVarChar))) {
					//							//加载百度编辑器
					//							sb4.AppendFormat(@"
					//		<asp:TemplateField HeaderText=""{0}"">
					//			<InsertItemTemplate><textarea id=""textarea{0}"" Value='<%# Bind(""{0}"") %>' runat=""server"" style=""width:100%;height:100px;"" editor=""ueditor"" /></InsertItemTemplate>
					//			<EditItemTemplate><textarea id=""textarea{0}"" Value='<%# Bind(""{0}"") %>' runat=""server"" style=""width:100%;height:100px;"" editor=""ueditor"" /></EditItemTemplate>
					//		</asp:TemplateField>", csUName);
					//						} else if ((lname == "img" || lname.StartsWith("img_") || lname.EndsWith("_img")) && (col.Type == SqlDbType.VarChar || col.Type == SqlDbType.NVarChar)) {
					//							//上传图片
					//							sb4.AppendFormat(@"
					//		<asp:TemplateField HeaderText=""{0}"">
					//			<InsertItemTemplate><input type=""file"" id=""file{0}"" PostedFile='<%# Bind(""{0}2"") %>' runat=""server"" /></InsertItemTemplate>
					//			<EditItemTemplate>
					//				<input type=""text"" id=""textbox{0}"" Value='<%# Bind(""{0}"") %>' runat=""server"" style=""width:80%;"" />
					//				<input type=""file"" id=""file{0}"" PostedFile='<%# Bind(""{0}2"") %>' runat=""server"" />
					//			</EditItemTemplate>
					//		</asp:TemplateField>", csUName);
					//							sb10.AppendFormat(@"

					//		HttpPostedFile file{0} = e.Values[""{0}2""] as HttpPostedFile;
					//		if (file{0}.ContentLength > 0) {{
					//			string dir{0} = Server.MapPath(string.Concat(""/upload/{1}/{2}/"", DateTime.Now.ToString(""yyyyMM""), ""/""));
					//			string fn{0} = string.Concat(""/upload/{1}/{2}/"", DateTime.Now.ToString(""yyyyMM""), ""/"", Guid.NewGuid().ToString());
					//			if (!System.IO.Directory.Exists(dir{0})) System.IO.Directory.CreateDirectory(dir{0});
					//			file{0}.SaveAs(Server.MapPath(fn{0}));
					//			e.Values[""{0}""] = fn{0};
					//		}}
					//		e.Values.Remove(""{0}2"");", csUName, table.Name, col.Name);
					//							sb11.AppendFormat(@"

					//		HttpPostedFile file{0} = e.NewValues[""{0}2""] as HttpPostedFile;
					//		if (file{0}.ContentLength > 0) {{
					//			string dir{0} = Server.MapPath(string.Concat(""/upload/{1}/{2}/"", DateTime.Now.ToString(""yyyyMM""), ""/""));
					//			string fn{0} = string.Concat(""/upload/{1}/{2}/"", DateTime.Now.ToString(""yyyyMM""), ""/"", Guid.NewGuid().ToString());
					//			if (!System.IO.Directory.Exists(dir{0})) System.IO.Directory.CreateDirectory(dir{0});
					//			file{0}.SaveAs(Server.MapPath(fn{0}));
					//			e.NewValues[""{0}""] = fn{0};
					//		}}
					//		string fn{0}old = string.Concat(e.OldValues[""{0}""]);
					//		if (file{0}.ContentLength > 0 || string.Compare(fn{0}old, string.Concat(e.NewValues[""{0}""]), true) != 0) {{
					//			if (!string.IsNullOrEmpty(fn{0}old)) {{
					//				fn{0}old = Server.MapPath(fn{0}old);
					//				if (System.IO.File.Exists(fn{0}old)) System.IO.File.Delete(fn{0}old);
					//			}}
					//		}}
					//		e.NewValues.Remove(""{0}2"");", csUName, table.Name, col.Name);
					//						} else {
					//							sb4.AppendFormat(@"
					//		<asp:BoundField DataField=""{0}"" HeaderText=""{0}"">
					//			<ControlStyle CssClass=""datepicker"" Width=""60%"" />
					//		</asp:BoundField>", csUName);
					//						}
					//					}

					//					// m -> n 关系
					//					_tables.ForEach(delegate (TableInfo t2) {
					//						ForeignKeyInfo fk = t2.ForeignKeys.Find(delegate (ForeignKeyInfo ffk) {
					//							if (ffk.ReferencedTable.FullName == table.FullName) {
					//								return true;
					//							}
					//							return false;
					//						});
					//						if (fk == null) return;
					//						if (fk.Table.FullName == table.FullName) return;
					//						List<ForeignKeyInfo> fk2 = t2.ForeignKeys.FindAll(delegate (ForeignKeyInfo ffk2) {
					//							return ffk2 != fk;
					//						});
					//						if (fk2.Count != 1) return;
					//						if (t2.Columns.Count != 2) return; //mn表若不是两个字段，则不处理

					//						//t2.Columns
					//						string addname = t2.ClassName;
					//						if (t2.ClassName.StartsWith(table.ClassName + "_")) {
					//							addname = t2.ClassName.Substring(table.ClassName.Length + 1);
					//						} else if (t2.ClassName.EndsWith("_" + table.ClassName)) {
					//							addname = t2.ClassName.Remove(addname.Length - table.ClassName.Length - 1);
					//						} else if (fk2.Count == 1 && t2.ClassName.EndsWith("_" + table.ClassName)) {
					//							addname = t2.ClassName.Remove(t2.ClassName.Length - table.ClassName.Length - 1);
					//						} else if (fk2.Count == 1 && t2.ClassName.EndsWith("_" + fk2[0].ReferencedTable.Name)) {
					//							addname = t2.ClassName;
					//						}
					//						if (string.Compare(fk2[0].ReferencedTable.Name, addname, true) != 0) return;

					//						ColumnInfo textCol = fk2[0].ReferencedTable.Columns.Find(delegate (ColumnInfo fci2) {
					//							return fci2.Name.ToLower().IndexOf("name") != -1 || fci2.Name.ToLower().IndexOf("title") != -1;
					//						});
					//						if (textCol == null) textCol = fk2[0].ReferencedTable.Columns.Find(delegate (ColumnInfo fci2) {
					//							return GetCSType(fci2.Type) == "string" && fci2.Length > 0 && fci2.Length < 128;
					//						});
					//						if (textCol == null) textCol = fk2[0].ReferencedTable.Columns[0];
					//						if (sb16.ToString().Length == 0)
					//							sb16.AppendFormat(@"
					//<%@ Register Src=""~/controls/mn_htmlselect.ascx"" TagPrefix=""uc"" TagName=""mn_htmlselect"" %>");
					//						sb4.AppendFormat(@"
					//		<asp:TemplateField HeaderText=""{0}"">
					//			<InsertItemTemplate><uc:mn_htmlselect id=""mn_htmlselect{0}"" runat=""server"" Value='<%# Bind(""Obj_{3}s"") %>' DataSourceID=""ods{0}"" DataTextField=""{1}"" DataValueField=""{2}"" Placeholder=""Select a {0}"" /></InsertItemTemplate>
					//			<EditItemTemplate><uc:mn_htmlselect id=""mn_htmlselect{0}"" runat=""server"" Value='<%# Bind(""Obj_{3}s"") %>' DataSourceID=""ods{0}"" DataTextField=""{1}"" DataValueField=""{2}"" Placeholder=""Select a {0}"" /></EditItemTemplate>
					//		</asp:TemplateField>", CodeBuild.UFString(addname), CodeBuild.UFString(textCol.Name), CodeBuild.UFString(fk2[0].ReferencedTable.PrimaryKeys[0].Name), CodeBuild.LFString(addname));
					//						sb15.AppendFormat(@"
					//	string str{0};
					//	void Flag{0}() {{
					//		if (string.IsNullOrEmpty(str{0})) return;
					//		List<string> str{0}s = new List<string>(str{0}.Split(','));
					//		foreach ({0}Info Obj_{1} in item.Obj_{1}s) {{
					//			int idx = str{0}s.FindIndex(delegate (string fstr{0}) {{
					//				return string.Compare(fstr{0}, string.Concat(Obj_{1}.Id), true) == 0;
					//			}});
					//			if (idx == -1) item.Unflag{0}(Obj_{1}.Id);
					//			else str{0}s.RemoveAt(idx);
					//		}}
					//		foreach (string str{0}sin in str{0}s) if (!string.IsNullOrEmpty(str{0}sin)) item.Flag{0}({2});
					//	}}", CodeBuild.UFString(addname), CodeBuild.LFString(addname), string.Format(GetCSConvert2(fk2[0].ReferencedTable.PrimaryKeys[0].Type), "str" + CodeBuild.UFString(addname) + "sin"));
					//						sb14.AppendFormat(@"
					//		Flag{0}();", CodeBuild.UFString(addname));

					//						sb10.AppendFormat(@"
					//		str{0} = string.Concat(e.Values[""Obj_{1}s""]);
					//		e.Values.Remove(""Obj_{1}s"");", CodeBuild.UFString(addname), CodeBuild.LFString(addname));

					//						string pkSqlWhere = "";
					//						int pkSqlWhere_idx = -1;
					//						string pkValuesParms = "";
					//						table.PrimaryKeys.ForEach(delegate (ColumnInfo tpfic) {
					//							pkSqlWhere += " AND a.[" + tpfic.Name + "] = {" + ++pkSqlWhere_idx + "}";
					//							pkValuesParms += ", " + string.Format(GetCSConvert2(tpfic.Type), string.Format(@"string.Concat(e.NewValues[""{0}""])", CodeBuild.UFString(tpfic.Name)));
					//							//pkValuesParms += string.Format(GetCSConvert2(tpfic.Type), string.Format(@"string.Concat(e.NewValues[""{0}""])", CodeBuild.UFString(tpfic.Name))) + ", ";
					//						});
					//						if (pkSqlWhere.Length > 0) pkSqlWhere = pkSqlWhere.Substring(5);
					//						if (pkValuesParms.Length > 0) pkValuesParms = pkValuesParms.Substring(2);
					//						string getitem = string.Format(@"
					//		if (item == null) item = {0}.Select.Where(""{1}"", {2}).ToOne();", uClass_Name, pkSqlWhere, pkValuesParms);
					//						if (sb11.ToString().IndexOf(getitem) == -1) sb11.Append(getitem);
					//						sb11.AppendFormat(@"
					//		str{0} = string.Concat(e.NewValues[""Obj_{1}s""]);
					//		Flag{0}();
					//		e.NewValues.Remove(""Obj_{1}s"");", CodeBuild.UFString(addname), CodeBuild.LFString(addname));
					//						sb13.AppendFormat(@"
					//<asp:ObjectDataSource ID=""ods{0}"" runat=""server"" SelectMethod=""GetItems"" TypeName=""{1}.BLL.{0}""></asp:ObjectDataSource>",
					//							CodeBuild.UFString(addname), solutionName);
					//					});

					//					sb7.Append(sb15.ToString());
					//					sb1.AppendFormat(@"<%@ Page Language=""C#"" %>{15}

					//<script runat=""server"">

					//	protected void dv{11}_ItemInserting(object sender, DetailsViewInsertEventArgs e) {{{6}{8}
					//	}}
					//	protected void dv{11}_ItemInserted(object sender, DetailsViewInsertedEventArgs e) {{
					//		if (e.Exception != null) {{
					//			base.End(e.Exception.InnerException != null ? e.Exception.InnerException.Message : e.Exception.Message);
					//			return;
					//		}}
					//		base.Goto(""./"", ""添加成功"");
					//	}}

					//	protected void dv{11}_ItemUpdating(object sender, DetailsViewUpdateEventArgs e) {{{7}{9}
					//	}}
					//	protected void dv{11}_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e) {{
					//		if (e.Exception != null) {{
					//			base.End(e.Exception.InnerException != null ? e.Exception.InnerException.Message : e.Exception.Message);
					//			return;
					//		}}
					//		base.Goto(""./"", ""修改成功"");
					//	}}

					//	protected void ods{11}_Selected(object sender, ObjectDataSourceStatusEventArgs e) {{
					//		if (e.ReturnValue == null) {{
					//			dv{11}.ChangeMode(DetailsViewMode.Insert);
					//		}}
					//	}}
					//	protected void ods{11}_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {{
					//		item = e.ReturnValue as {11}Info;{14}
					//	}}
					//	{11}Info item;
					//	{5}
					//	public static void IAjax(HttpRequest Request, HttpResponse Response) {{
					//		switch(Request.Form[""method""]) {{
					//			case ""xxx"":
					//				break;{4}
					//		}}
					//	}}
					//</script>

					//<script type=""text/javascript"">{3}
					//</script>

					//<div class=""box"">
					//	<div class=""box-header with-border"">
					//		<h3 class=""box-title"" id=""box-title""></h3>
					//	</div>
					//	<div class=""box-body"">
					//		<div class=""table-responsive"">

					//<form id=""form_add"" runat=""server"">
					//<asp:DetailsView ID=""dv{11}"" runat=""server"" AutoGenerateRows=""False"" DataSourceID=""ods{11}"" DefaultMode=""Edit"" 
					//	OnItemInserting=""dv{11}_ItemInserting"" OnItemInserted=""dv{11}_ItemInserted"" 
					//	OnItemUpdating=""dv{11}_ItemUpdating"" OnItemUpdated=""dv{11}_ItemUpdated"" class=""table table-bordered table-hover"">
					//	<Fields>{2}
					//		<asp:CommandField ButtonType=""Button"" ShowEditButton=""True"" ShowInsertButton=""True"" />
					//	</Fields>
					//</asp:DetailsView>
					//</form>

					//		</div>
					//	</div>
					//</div>

					//<asp:ObjectDataSource ID=""ods{11}"" runat=""server"" TypeName=""{10}"" InsertMethod=""Insert"" UpdateMethod=""Update"" 
					//	SelectMethod=""GetItem"" OnSelected=""ods{11}_Selected"" OnInserted=""ods{11}_Inserted"">
					//	<SelectParameters>{0}
					//	</SelectParameters>
					//	<UpdateParameters>{1}
					//	</UpdateParameters>
					//	<InsertParameters>{12}
					//	</InsertParameters>
					//</asp:ObjectDataSource>{13}
					//", sb2.ToString(), sb3.ToString(), sb4.ToString(), sb5.ToString(), sb6.ToString(), sb7.ToString(), sb8.ToString(), sb9.ToString(),
					//			   sb10.ToString(), sb11.ToString(), Class_Name_BLL_Full, uClass_Name, sb12.ToString(), sb13.ToString(), sb14.ToString(), sb16.ToString());

					//					loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, nClass_Name, @"\edit.aspx"), Deflate.Compress(sb1.ToString())));
					//					clearSb();
					#endregion

					#region del.aspx

					int pkidx = 0;
					foreach (ColumnInfo col88 in table.PrimaryKeys) {
						string csUName = CodeBuild.UFString(col88.Name);
						string csType = CodeBuild.GetCSType(col88.Type);
						sb2.AppendFormat(@"
			{1} {0};
			{2};", csUName, csType.Trim('?'),
							string.Format(CodeBuild.GetCSConvert(col88.Type), pkidx++ == 0 ? "ids[a]" : string.Format(@"ids[a + {0}]", pkidx - 1), csUName));
					}
					sb1.AppendFormat(@"<%@ Page Language=""C#"" %>

<script runat=""server"">

protected void Page_Load(object sender, EventArgs e) {{
	int dels = 0;
	try {{
		string[] ids = string.Concat(Request.Form[""id""]).Split(',');
		for (int a = 0; a < ids.Length; a+={0}) {{{1}
			//{2}Info item = {2}.GetItem({3});
			dels += {2}.Delete({3});
		}}
	}} catch (Exception ex) {{
		base.End(""删除失败 "" + ex.Message);
		return;
	}}
	base.Goto(""./"", string.Concat(""删除成功，影响行数："", dels));
}}

</script>", table.PrimaryKeys.Count, sb2.ToString(), uClass_Name, pkCsParamNoType);

					loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, nClass_Name, @"\del.aspx"), Deflate.Compress(sb1.ToString())));
					clearSb();
					#endregion

				}
				#endregion
			}

			#region BLL StoreProcedure.cs
			int spsssss = 0;
			sb1.AppendFormat(@"using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace {0}.BLL {{

	public partial class StoreProcedure {{
", solutionName);

			foreach (TableInfo table in _tables) {
				if (table.IsOutput == false) continue;
				if (table.Type != "P") continue;

				string uClass_Name = CodeBuild.UFString(table.ClassName);
				string nClass_Name = table.ClassName;
				string nTable_Name = "[" + table.Owner + "].[" + table.Name + "]";

				List<string> csParms = new List<string>();
				List<string> csParmsNoType = new List<string>();
				List<string> setOutParmsNull = new List<string>();
				List<string> dimParms = new List<string>();
				List<string> dimOutParms = new List<string>();
				List<string> dimOutParmsInput = new List<string>();
				List<string> dimOutParmsReturn = new List<string>();
				int idx = 0;
				foreach (ColumnInfo column in table.Columns) {
					string name = CodeBuild.GetCSName(column.Name);
					string csType = CodeBuild.GetCSType(column.Type);
					string nameOut = string.Empty;
					string sqlParm = string.Empty;
					if (column.IsIdentity) {
						setOutParmsNull.Add(string.Format(@"{0} = null;", name));
						dimOutParms.Add(string.Format(@"SqlParameter parmO{0} = null;", idx));
						dimOutParmsInput.Add(string.Format(@"parmO{0}.Direction = ParameterDirection.Output;", idx));
						dimOutParmsReturn.Add(string.Format(@"if (parmO{0}.Value != DBNull.Value) {1} = ({2})parmO{0}.Value;", idx, name, csType));
						nameOut = "out ";
						sqlParm = "parmO" + idx++ + " = ";
					}
					csParms.Add(nameOut + csType + " " + name);
					csParmsNoType.Add(nameOut + name);
					dimParms.Add(sqlParm +
						string.Format(@"GetParameter(""{0}"", SqlDbType.{1}, {2}, {3})", column.Name, column.Type, column.Length, name));
				}
				if (table.Columns.Count == 0) {
					sb1.AppendFormat(@"
		#region {0}
		public static void {0}() {{
			{0}(false);
		}}
		public static DataSet {0}_dataset() {{
			return {0}(true);
		}}
		private static DataSet {0}(bool IsReturnDataSet) {{
			DataSet ds = null;
			string sql = @""{1}"";

			if (IsReturnDataSet)
				ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql);
			else
				SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sql);

			return ds;
		}}
		#endregion
", uClass_Name, nTable_Name);
				} else {
					if (setOutParmsNull.Count > 0) setOutParmsNull.Add("");
					if (dimOutParms.Count > 0) dimOutParms.Add("");
					if (dimOutParmsInput.Count > 0) dimOutParmsInput.Add("");
					if (dimOutParmsReturn.Count > 0) dimOutParmsReturn.AddRange(new string[] { "", "" });
					sb1.AppendFormat(@"
		#region {0}
		public static void {0}({1}) {{
			{0}({2}, false);
		}}
		public static DataSet {0}_dataset({1}) {{
			return {0}({2}, true);
		}}
		private static DataSet {0}({1}, bool IsReturnDataSet) {{
			{3}{5}SqlParameter[] parms = new SqlParameter[] {{
				{4}
			}};
			{6}DataSet ds = null;
			string sql = @""{7}"";

			if (IsReturnDataSet)
				ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql, parms);
			else
				SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sql, parms);

			{8}return ds;
		}}
		#endregion
", uClass_Name,
					string.Join(", ", csParms.ToArray()),
					string.Join(", ", csParmsNoType.ToArray()),
					string.Join("\r\n			", setOutParmsNull.ToArray()),
					string.Join(",\r\n				", dimParms.ToArray()),
					string.Join("\r\n			", dimOutParms.ToArray()),
					string.Join("\r\n			", dimOutParmsInput.ToArray()),
					nTable_Name,
					string.Join("\r\n			", dimOutParmsReturn.ToArray()));
				}

				spsssss++;
			}

			sb1.AppendFormat(@"
		public static SqlParameter GetParameter(string name, SqlDbType type, int size, object value) {{
			SqlParameter parm = new SqlParameter(name, type, size);
			parm.Value = value;
			return parm;
		}}
	}}
}}");
			string bll_sp_cs = null;
			if (spsssss > 0) {
				bll_sp_cs = string.Format(@"
		<Compile Include=""BLL\{0}\StoreProcedure.cs"" />", basicName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, @".db\BLL\", basicName, @"\StoreProcedure.cs"), Deflate.Compress(sb1.ToString())));
			}
			clearSb();
			#endregion

			#region BLL SqlHelper.cs
			sb1.AppendFormat(CONST.BLL_Build_SqlHelper_cs, solutionName);
			loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, @".db\BLL\", basicName, @"\SqlHelper.cs"), Deflate.Compress(sb1.ToString())));
			clearSb();
			#endregion
			#region BLL ItemCache.cs
			sb1.AppendFormat(CONST.BLL_Build_ItemCache_cs, solutionName);
			loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, @".db\BLL\", basicName, @"\ItemCache.cs"), Deflate.Compress(sb1.ToString())));
			clearSb();
			#endregion
			#region Model ExtensionMethods.cs 扩展方法
			sb1.AppendFormat(CONST.Model_Build_ExtensionMethods_cs, solutionName, Model_Build_ExtensionMethods_cs.ToString());
			loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, @".db\Model\", basicName, @"\ExtensionMethods.cs"), Deflate.Compress(sb1.ToString())));
			clearSb();
			#endregion

			if (isSolution) {

				#region db.csproj

				#region DBUtility/ConnectionManager.cs
				sb1.AppendFormat(CONST.DAL_ConnectionManager_cs, solutionName, connectionStringName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, @".db\DAL\DBUtility\ConnectionManager.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region DBUtility/SqlHelper.cs
				sb1.AppendFormat(CONST.DAL_SqlHelper_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, @".db\DAL\DBUtility\SqlHelper.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region DBUtility/SqlSelectBuild.cs
				sb1.AppendFormat(CONST.DAL_SqlHelper_SelectBuild_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, @".db\DAL\DBUtility\SqlSelectBuild.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion

				place3 = string.Format(@"<ItemGroup>
		<Reference Include=""System"" />
		<Reference Include=""System.Configuration"" />
		<Reference Include=""System.Data"" />
		<Reference Include=""System.XML"" />
	</ItemGroup>
	<ItemGroup>{5}
		<Compile Include=""BLL\{4}\SqlHelper.cs"" />
		<Compile Include=""BLL\{4}\ItemCache.cs"" />
		<Compile Include=""Model\{4}\ExtensionMethods.cs"" />
		<Compile Include=""DAL\DBUtility\ConnectionManager.cs"" />
		<Compile Include=""DAL\DBUtility\SqlHelper.cs"" />
		<Compile Include=""DAL\DBUtility\SqlSelectBuild.cs"" />
{0}
{1}
{2}</ItemGroup>
	<ItemGroup>
		<ProjectReference Include=""..\Common\Common.csproj"">
			<Project>{{{3}}}</Project>
			<Name>Common</Name>
		</ProjectReference>
	</ItemGroup>", files.Replace("|0|", "").Replace("|1|", "Info").Replace("|2|", "Model"),
				 files.Replace("|0|", "").Replace("|1|", "").Replace("|2|", "DAL"),
				 files.Replace("|0|", "").Replace("|1|", "").Replace("|2|", "BLL"), commonGuid, basicName, bll_sp_cs);
				sb1.AppendFormat(CONST.csproj, dbGuid, string.Concat(solutionName, @".db"), place3);

				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, @".db\", solutionName, ".db.csproj"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
			}

			if (isSolution) {
				#region Project Web
				#region web.config
				sb1.AppendFormat(CONST.Web_web_config, solutionName, connectionStringName);
				loc1.Add(new BuildInfo(string.Concat(CONST.webPath, @"web.config"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region App_code\BasePage.cs
				sb1.AppendFormat(CONST.Web_App_Code_BasePage_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.webPath, @"App_Code\BasePage.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion

				#region connection.aspx
				sb1.AppendFormat(CONST.Web_connection_aspx, solutionName, dbName);
				loc1.Add(new BuildInfo(string.Concat(CONST.webPath, @"connection.aspx"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region index.aspx
				sb1.AppendFormat(CONST.Web_index_aspx, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.webPath, @"index.aspx"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region view\index.html
				sb1.AppendFormat(CONST.Web_view_index_html, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.webPath, @"view\index.html"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion

				#endregion

				loc1.Add(new BuildInfo(string.Concat(@"log4net.dll"), Server.Properties.Resources.log4net));
			}

			if (isMakeAdmin) {
				#region Project Web Admin
				#region web.config
				sb1.AppendFormat(CONST.Admin_web_config, solutionName, connectionStringName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"web.config"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region web.sitemap
				string smp = string.Format(CONST.Admin_web_sitemap, admin_web_sitemap);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"web.sitemap"), Deflate.Compress(smp)));
				clearSb();
				#endregion
				#region init_sysdir.aspx
				smp = string.Format(CONST.Admin_init_sysdir_aspx, string.Join(string.Empty, admin_init_sysdir_aspx.ToArray()));
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"init_sysdir.aspx"), Deflate.Compress(smp)));
				clearSb();
				#endregion

				#region App_Code\BasePage.cs
				sb1.AppendFormat(CONST.Admin_App_Code_BasePage_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"App_Code\BasePage.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region App_Code\SessionManager.cs
				sb1.AppendFormat(CONST.Admin_App_Code_SessionManager_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"App_Code\SessionManager.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion

				#region App_Code\Ajax.cs
				sb1.AppendFormat(CONST.Admin_App_Code_Ajax_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"App_Code\Ajax.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion

				#region App_Code\RichControls\AllPager.cs
				sb1.AppendFormat(CONST.Admin_App_Code_RichControls_AllPager_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"App_Code\RichControls\AllPager.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region App_Code\RichControls\SafeHtmlInputCheckBox.cs
				sb1.AppendFormat(CONST.Admin_App_Code_RichControls_SafeHtmlInputCheckBox_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"App_Code\RichControls\SafeHtmlInputCheckBox.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region App_Code\RichControls\SelField.cs
				sb1.AppendFormat(CONST.Admin_App_Code_RichControls_SelField_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"App_Code\RichControls\SelField.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion

				#region controls/search_bar.ascx
				sb1.AppendFormat(CONST.Admin_controls_search_bar_ascx, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"\controls\search_bar.ascx"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region controls/mn_checkbox.ascx
				sb1.AppendFormat(CONST.Admin_controls_mn_checkbox_ascx, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"\controls\mn_checkbox.ascx"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region controls/mn_htmlselect.ascx
				sb1.AppendFormat(CONST.Admin_controls_mn_htmlselect_ascx, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"\controls\mn_htmlselect.ascx"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region controls/mn_htmlselect_sysdir.ascx
				sb1.AppendFormat(CONST.Admin_controls_mn_htmlselect_sysdir_ascx, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"\controls\mn_htmlselect_sysdir.ascx"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion

				#region exit.aspx
				sb1.AppendFormat(CONST.Admin_exit_aspx, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"exit.aspx"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region default.aspx
				sb1.AppendFormat(CONST.Admin_default_aspx, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"default.aspx"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region login.aspx
				sb1.AppendFormat(CONST.Admin_login_aspx, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"login.aspx"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region connection.aspx
				sb1.AppendFormat(CONST.Admin_connection_aspx, solutionName, dbName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"connection.aspx"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion

				#endregion
			}
			if (isDownloadRes) {
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"htm.zip"), Server.Properties.Resources.htm));
			}

			GC.Collect();
			return loc1;
		}
	}
}
