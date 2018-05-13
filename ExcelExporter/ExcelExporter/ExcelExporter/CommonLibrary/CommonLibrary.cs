using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelExporter.CommonLibrary
{
    public sealed class CommonLibrary
    {
        public DataTable BindDatabases(string connectionString)
        {
            var dsDatabases = new DataTable();
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    var query = "SELECT NAME FROM sys.databases ORDER BY NAME ";
                    con.Open();
                    var cmd = new SqlCommand(query, con);
                    var daTab = new SqlDataAdapter(cmd);
                    daTab.Fill(dsDatabases);
                    return dsDatabases;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    dsDatabases.Dispose();
                }
            }
        }

        public string ValidateQuery(string query)
        {
            var message = string.Empty;

            if (query.Contains("INSERT"))
            {
                message = "Stored Procedure Must Only Contain Select clause";
            }
            else if (query.Contains("UPDATE"))
            {
                message = "Stored Procedure Must Only Contain Select clause";
            }
            else if (query.Contains("DELETE"))
            {
                message = "Stored Procedure Must Only Contain Select clause";
            }
            else if (query.Contains("@"))
            {
                message = "Stored Procedure Must Not Have Parameter [Should be Self Executing]";
            }
            else if (query.Contains("#"))
            {
                message = "Stored Procedure Must Not Have Parameter [Should be Self Executing]";
            }

            return message;
        }

        public bool ValidateQueryMustContain(string query)
        {
            bool result = false;

            if (!query.ToUpper().Contains("SELECT"))
            {
                result = true;
            }
            else if (!query.ToUpper().Contains("FROM"))
            {
                result = true;
            }
            return result;
        }

        public List<PartitionValues> Listgenerator(int minRecordCount, int maxRecordCount)
        {
            var partitionList = new List<PartitionValues>();

            var tempmin = minRecordCount;

            int current = minRecordCount;

            for (int i = 1; i <= 100; i++)
            {
                if (maxRecordCount >= tempmin)
                {
                    tempmin += 50000;
                    partitionList.Add(new PartitionValues()
                    {
                        From = current,
                        To = tempmin
                    });

                    current = tempmin;
                }
                else
                {
                    break;
                }
            }

            return partitionList;

        }
    }
}
