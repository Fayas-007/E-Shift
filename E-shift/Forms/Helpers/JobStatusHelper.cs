using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using E_shift.DataAccess;

namespace E_shift.Helpers
{
    public static class JobStatusHelper
    {
        public static void SyncJobStatus()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    string sql = @"
                        UPDATE Job
                        SET Status = 
                            CASE
                                WHEN EXISTS (
                                    SELECT 1 FROM Load 
                                    WHERE Load.JobID = Job.JobID AND Load.Status = 'InProgress'
                                ) THEN 'InProgress'

                                WHEN EXISTS (
                                    SELECT 1 FROM Load 
                                    WHERE Load.JobID = Job.JobID AND Load.Status = 'Pending'
                                ) THEN 'Pending'

                                WHEN NOT EXISTS (
                                    SELECT 1 FROM Load 
                                    WHERE Load.JobID = Job.JobID AND Load.Status NOT IN ('Completed', 'Cancelled')
                                ) AND EXISTS (
                                    SELECT 1 FROM Load 
                                    WHERE Load.JobID = Job.JobID AND Load.Status = 'Completed'
                                ) THEN 'Completed'

                                WHEN NOT EXISTS (
                                    SELECT 1 FROM Load 
                                    WHERE Load.JobID = Job.JobID AND Load.Status NOT IN ('Cancelled')
                                ) THEN 'Cancelled'

                                ELSE Job.Status
                            END
                        WHERE EXISTS (
                            SELECT 1 FROM Load WHERE Load.JobID = Job.JobID
                        );
                    ";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error syncing job status: " + ex.Message);
            }
        }
    }
}
