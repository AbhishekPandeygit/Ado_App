using ado_app.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ado_app.Data
{
    public class CustomerService
    {
        ConnectDB dB;

        public CustomerService()
        {
            dB = new ConnectDB();
        }

        public List<Country> GetCountryList()
        {

            SqlCommand cmd = new SqlCommand("usp_get_country", dB._connect);
            cmd.CommandType = CommandType.StoredProcedure;
            if (dB._connect.State == ConnectionState.Closed)
            {
                dB._connect.Open();
            }

            SqlDataReader dr = cmd.ExecuteReader();
            List<Country> list = new List<Country>();
            while (dr.Read())
            {
                Country country = new Country();
                country.Id = Convert.ToInt32(dr["id"]);
                country.Name = dr["name"].ToString();
                list.Add(country);
            }
            dB._connect.Close();
            return list;

        }

        public List<State> GetStateList(int countryid)
        {

            SqlCommand cmd = new SqlCommand("usp_get_state", dB._connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", countryid);

            if (dB._connect.State == ConnectionState.Closed)
            {
                dB._connect.Open();
            }

            SqlDataReader dr = cmd.ExecuteReader();
            List<State> list = new List<State>();
            while (dr.Read())
            {
                State state = new State();
                state.Id = Convert.ToInt32(dr["id"]);
                state.Name = dr["name"].ToString();
                list.Add(state);
            }
            dB._connect.Close();
            return list;

        }

        public List<City> GetCityList(int stateid)
        {

            SqlCommand cmd = new SqlCommand("usp_get_city", dB._connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", stateid);

            if (dB._connect.State == ConnectionState.Closed)
            {
                dB._connect.Open();
            }
            SqlDataReader dr = cmd.ExecuteReader();
            List<City> list = new List<City>();
            while (dr.Read())
            {
                City city = new City();
                city.Id = Convert.ToInt32(dr["id"]);
                city.Name = dr["name"].ToString();
                list.Add(city);
            }
            dB._connect.Close();
            return list;

        }

        public List<Customer> GetCustomers(Customer customer)
        {
            SqlCommand cmd = new SqlCommand("usp_get_customer_list", dB._connect);
            cmd.CommandType = CommandType.StoredProcedure;

            if (dB._connect.State == ConnectionState.Closed)
            {
                dB._connect.Open();
            }
            SqlDataReader dr = cmd.ExecuteReader();
            List<Customer> list = new List<Customer>();
            while (dr.Read())
            {
                Customer cust = new Customer();
                cust.id = Convert.ToInt32(dr["id"]);
                cust.name = dr["name"].ToString();
                cust.email = dr["email"].ToString();
                cust.mobile = dr["mobile"].ToString();
                cust.gender = dr["gender"].ToString();
                cust.country_id =  Convert.ToInt32(dr["country_id"]);
                cust.state_id = Convert.ToInt32(dr["state_id"]);
       
                cust.city_id =  Convert.ToInt32(dr["city_id"]);

                list.Add(cust);
            }
            dB._connect.Close();
            return list;

        }

        public Customer GetCustomer(int id)
        {
            SqlCommand cmd = new SqlCommand("usp_get_customer", dB._connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            if (dB._connect.State == ConnectionState.Closed)
            {
                dB._connect.Open();
            }
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Customer cust = new Customer();
            cust.id = Convert.ToInt32(dr["id"]);
            cust.name = dr["name"].ToString();
            cust.email = dr["email"].ToString();
            cust.mobile = dr["mobile"].ToString();
            cust.gender = dr["gender"].ToString();
            cust.country_id = Convert.ToInt32(dr["country_id"]);
            cust.state_id = Convert.ToInt32(dr["state_id"]);

            cust.city_id = Convert.ToInt32(dr["city_id"]);
            dB._connect.Close();
            return cust;

        }

        public Models.Action CreateCustomer(Customer customer)
        {
            SqlCommand cmd = new SqlCommand("usp_save_customer", dB._connect);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@name", customer.name);
            cmd.Parameters.AddWithValue("@email", customer.email);
            cmd.Parameters.AddWithValue("@mobile", customer.mobile);
            cmd.Parameters.AddWithValue("@gender", customer.gender);
            cmd.Parameters.AddWithValue("@country_id", customer.country_id);
            cmd.Parameters.AddWithValue("@state_id", customer.state_id);
            cmd.Parameters.AddWithValue("@city_id", customer.city_id);


            if (dB._connect.State == ConnectionState.Closed)
            {
                dB._connect.Open();
            }
            int r = Convert.ToInt32(cmd.ExecuteScalar());
            Models.Action action;

            if (r == 1)
            {
                action = Models.Action.Success;
            }
            else if (r == 2)
            {
                action = Models.Action.EmailExist;
            }
            else
            {
                action = Models.Action.Error;
            }

            dB._connect.Close();
            return action;

        }

        public bool DeleteCustomer(int id)
        {
            SqlCommand cmd = new SqlCommand("usp_delete_customer", dB._connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);

            if (dB._connect.State == ConnectionState.Closed)
            {
                dB._connect.Open();
            }
            int r = Convert.ToInt32(cmd.ExecuteScalar());
            dB._connect.Close();
            if (r == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public Models.Action UpdateCustomer(Customer customer)
        {
            SqlCommand cmd = new SqlCommand("usp_update_customer", dB._connect);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id", customer.id);
            cmd.Parameters.AddWithValue("@name", customer.name);
            cmd.Parameters.AddWithValue("@email", customer.email);
            cmd.Parameters.AddWithValue("@mobile", customer.mobile);
            cmd.Parameters.AddWithValue("@gender", customer.gender);
            cmd.Parameters.AddWithValue("@country_id", customer.country_id);
            cmd.Parameters.AddWithValue("@state_id", customer.state_id);
            cmd.Parameters.AddWithValue("@city_id", customer.city_id);


            if (dB._connect.State == ConnectionState.Closed)
            {
                dB._connect.Open();
            }
            int r = Convert.ToInt32(cmd.ExecuteScalar());
            Models.Action action;

            if (r == 1)
            {
                action = Models.Action.Success;
            }
            else if (r == 2)
            {
                action = Models.Action.EmailExist;
            }
            else
            {
                action = Models.Action.Error;
            }

            dB._connect.Close();
            return action;

        }
        public void UpdateprofileImage(int id, string path)
        {
            SqlCommand cmd = new SqlCommand("update_profile_image", dB._connect);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@path", path);


            if (dB._connect.State == ConnectionState.Closed)
            {
                dB._connect.Open();
            }
            int r = Convert.ToInt32(cmd.ExecuteScalar());

            dB._connect.Close();


        }

    }
}
