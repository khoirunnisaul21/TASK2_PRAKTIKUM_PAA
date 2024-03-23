using Tugas2.Helpers;
using Npgsql;

namespace Tugas2.Models
{
    using Npgsql;
    public class PersonContext
    {
        private string __constr;
        private string __ErrorMsg;
        public PersonContext(string pConstr)
        {
            __constr = pConstr;
        }
        public List<Person> ListPerson()
        {
            List<Person> list1 = new List<Person>();
            string query = string.Format(@"SELECT id_person, nama, alamat, email FROM users.person;");
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list1.Add(new Person()
                    {
                        id_person = int.Parse(reader["id_person"].ToString()),
                        nama = reader["nama"].ToString(),
                        alamat = reader["alamat"].ToString(),
                        email = reader["email"].ToString()
                    });
                }
                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }
            return list1;
        }
        //create
        public bool CreatePerson(Person person)
        {
            string query = string.Format(@"INSERT INTO users.person (nama, alamat, email) VALUES (@nama, @alamat, @email)");
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@nama", person.nama);
                cmd.Parameters.AddWithValue("@alamat", person.alamat);
                cmd.Parameters.AddWithValue("@email", person.email);
                int rowsAffected = cmd.ExecuteNonQuery();
                cmd.Dispose();
                db.closeConnection();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
                return false;
            }
        }

        public bool UpdatePerson(int id, Person updatedPerson)
        {
            string query = string.Format(@"UPDATE users.person SET nama = @nama, alamat = @alamat, email = @email WHERE id_person = @id");
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nama", updatedPerson.nama);
                cmd.Parameters.AddWithValue("@alamat", updatedPerson.alamat);
                cmd.Parameters.AddWithValue("@email", updatedPerson.email);
                int rowsAffected = cmd.ExecuteNonQuery();
                cmd.Dispose();
                db.closeConnection();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
                return false;
            }
        }

        public bool DeletePerson(int id)
        {
            string query = string.Format(@"DELETE FROM users.person WHERE id_person = @id");
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                int rowsAffected = cmd.ExecuteNonQuery();
                cmd.Dispose();
                db.closeConnection();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
                return false;
            }
        }

    }
}