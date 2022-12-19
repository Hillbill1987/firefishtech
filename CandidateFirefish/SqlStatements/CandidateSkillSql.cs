using CandidateFirefish.DTO;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace CandidateFirefish.SqlStatements
{
    public class CandidateSkillSql
    {
        /*
         * This is the candidat skill sql where the sql commands have been created to retrieve
         * the data back from the db
        */
        static string ConnString = @"Data Source=DESKTOP-64FVVHI;Initial Catalog=Web_API_Task;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public List<CandidateSkill> GetSkillCandidates()
        {
            List<CandidateSkill> candidateSkills = new List<CandidateSkill>();
            using (SqlConnection con = new SqlConnection(ConnString))
            {
                con.Open();
                using (SqlCommand sql = new SqlCommand(@"select c.FirstName,c.Surname,sk.Name from dbo.candidateSkill as cs
                inner join dbo.candidate as c
                on cs.candidateId = c.ID
                inner join dbo.skill sk
                on cs.skillId = sk.ID",con))
                {
                    SqlDataReader reader = sql.ExecuteReader();
                    while (reader.Read())
                    {
                       
                        string Firstname = reader.GetString(0);
                        string Surname = reader.GetString(1);
                        string Name = reader.GetString(2);
                        candidateSkills.Add(new CandidateSkill()
                        {
                           
                            FirstName = Firstname,
                            Surname = Surname,
                            SkillName = Name

                        });
                            
                    }
                }
                return candidateSkills;
                
            }   
            
        }
        public void AddCandidateSkill(CreateCandidateSkill model)
        {
            
            using(SqlConnection con = new SqlConnection(ConnString))
            {
                con.Open();
                using(SqlCommand sql = new SqlCommand(@"INSERT INTO CandidateSkill VALUES (@ID,@CandidateID,@CreatedDate,@UpdatedDate,@SkillID)",con))
                {
                    var candidateskills = GetSkillCandidates();
                    try
                    {
                        sql.Parameters.Add(new SqlParameter("@ID", candidateskills.Count + 1));
                        sql.Parameters.Add(new SqlParameter("@CandidateID", model.CandidateID));
                        sql.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now));
                        sql.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
                        sql.Parameters.Add(new SqlParameter("@SkillID", model.SkillID));
                        sql.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }

       public void DeleteSkillCandidate(CreateCandidateSkill model)
        {
            using(SqlConnection con = new SqlConnection(ConnString))
            {
                con.Open();
                using(SqlCommand sql = new SqlCommand(@"Delete CandidateSkill WHERE @CandidateID = CandidateID AND @SkillID = SkillID ",con))
                {
                    sql.Parameters.AddWithValue("@candidateID", model.CandidateID);
                    sql.Parameters.AddWithValue("@SkillID", model.SkillID);
                    sql.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}
