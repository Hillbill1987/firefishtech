using CandidateFirefish.DTo;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace CandidateFirefish.SqlStatements
{
    public class CandidateSql
    {
        /*
         * this is the candidate sql where its run the sql commands to do with the candidates
         * as required on the tech test
         */
       static string ConnString = @"Data Source=DESKTOP-64FVVHI;Initial Catalog=Web_API_Task;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public List<Candidate> GetAllCandidates()
        {
            List<Candidate> candidates = new List<Candidate>();
            using (SqlConnection con = new SqlConnection(ConnString))
            {
                con.Open();
                using (SqlCommand sql = new SqlCommand(@"Select * FROM Candidate",con))
                {
                    SqlDataReader reader = sql.ExecuteReader();
                    while (reader.Read())
                    {
                        int ID = reader.GetInt32(0);
                        string Firstname = reader.GetString(1);
                        string Surname = reader.GetString(2);
                        DateTime DateOfBirth = reader.GetDateTime(3);
                        string Address1 = reader.GetString(4);
                        string Town = reader.GetString(5);
                        string Country = reader.GetString(6);
                        string PostCode = reader.GetString(7);
                        string PhoneHome = reader.GetString(8);
                        string PhoneMobile = reader.GetString(9);
                        string PhoneWork = reader.GetString(10);
                        DateTime CreatedDate = reader.GetDateTime(11);
                        DateTime UploadDate = reader.GetDateTime(12);
                        candidates.Add(new Candidate()
                        {
                            ID = ID,
                            FirstName = Firstname,
                            Surname = Surname,
                            DateOfBirth = DateOfBirth,
                            Address1 = Address1,
                            Town = Town,
                            Country = Country,
                            PostCode = PostCode,
                            PhoneHome = PhoneHome,
                            PhoneMobile = PhoneMobile,
                            PhoneWork = PhoneWork,
                            CreatedDate = CreatedDate,
                            UpdatedDate = UploadDate
                        });
                    }
                }
            }
            return candidates;
        }

        public void AddNewCandidate(Candidate model)
        {
          
            using(SqlConnection con = new SqlConnection(ConnString))
            {
                con.Open();
                using (SqlCommand sql = new SqlCommand(@"INSERT INTO Candidate VALUES(@ID,@Firstname,@Surname,@DateOfBirth,@Address1,@Town,@Country,@PostCode,
                @PhoneHome,@PhoneMobile,@PhoneWork,@CreatedDate,@UploadDate)",con))
                {
                    var Candidates = GetAllCandidates();
                    
                    try
                    {
                        sql.Parameters.Add(new SqlParameter("@ID", Candidates.Count + 1));
                        sql.Parameters.Add(new SqlParameter("@Firstname", model.FirstName));
                        sql.Parameters.Add(new SqlParameter("@Surname", model.Surname));
                        sql.Parameters.Add(new SqlParameter("@DateOfBirth", model.DateOfBirth));
                        sql.Parameters.Add(new SqlParameter("@Address1", model.Address1));
                        sql.Parameters.Add(new SqlParameter("@Town", model.Town));
                        sql.Parameters.Add(new SqlParameter("@Country", model.Country));
                        sql.Parameters.Add(new SqlParameter("@PostCode", model.PostCode));
                        sql.Parameters.Add(new SqlParameter("@PhoneHome", model.PhoneHome));
                        sql.Parameters.Add(new SqlParameter("@PhoneMobile", model.PhoneMobile));
                        sql.Parameters.Add(new SqlParameter("@PhoneWork", model.PhoneWork));
                        sql.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now));
                        sql.Parameters.Add(new SqlParameter("@UploadDate", DateTime.Now));
                        sql.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                    
                    }
              
                }
                
            }
        }

        public void UpdateCandidate(Candidate model)
        {
       
            using(SqlConnection con = new SqlConnection(ConnString))
            {
                con.Open();
                using (SqlCommand sql = new SqlCommand(@"UPDATE Candidate SET Firstname = @Firstname, 
                Surname = @Surname, DateOfBirth = @DateOfBirth, Address1 = @Address1, Town = @Town, Country = 
                @Country, PostCode = @PostCode, PhoneHome = @PhoneHome, PhoneMobile = @PhoneMobile, PhoneWork = @PhoneWork,
                UpdatedDate = @UpdatedDate WHERE @ID = id",con))
                {
                    try
                    {
                        sql.Parameters.Add(new SqlParameter("@Firstname", model.FirstName));
                        sql.Parameters.Add(new SqlParameter("@Surname", model.Surname));
                        sql.Parameters.Add(new SqlParameter("@DateOfBirth", model.DateOfBirth));
                        sql.Parameters.Add(new SqlParameter("@Address1", model.Address1));
                        sql.Parameters.Add(new SqlParameter("@Town", model.Town));
                        sql.Parameters.Add(new SqlParameter("@Country", model.Country));
                        sql.Parameters.Add(new SqlParameter("@PostCode", model.PostCode));
                        sql.Parameters.Add(new SqlParameter("@PhoneHome", model.PhoneHome));
                        sql.Parameters.Add(new SqlParameter("@PhoneMobile", model.PhoneMobile));
                        sql.Parameters.Add(new SqlParameter("@PhoneWork", model.PhoneWork));
                        sql.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
                        sql.Parameters.Add(new SqlParameter("@ID", model.ID));
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








    }
}
