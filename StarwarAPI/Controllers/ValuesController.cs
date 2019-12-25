using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

using StarwarAPI.Models;
namespace StarwarAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
        [HttpGet]
        [Route("~/api/getmoviename")]
        public string GetMovie()
        {

            string title = "";
            string CS = ConfigurationManager.ConnectionStrings["starwarconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select top 1  a.title,a.id,count(b.people_id) as cnt from films a join films_characters b on a.id =b.film_id group by a.title,a.id order by  cnt desc", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {


                    title = rdr["title"].ToString();

                }
            }
            return title;


        }


        [HttpGet]
        [Route("~/api/getcharactername")]
        public string GetCharacter()
        {

            string name = "";
            string CS = ConfigurationManager.ConnectionStrings["starwarconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select top 1 b.name,a.people_id,count(a.film_id) as cnt from films_characters a join people b on a.people_id=b.id group by  b.name,a.people_id order by cnt desc", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {


                    name = rdr["name"].ToString();

                }
            }
            return name;

        }

        [HttpGet]
        [Route("~/api/getspecieslist")]
        public List<species> GetSpecies()
        {

            List<species> species = new List<species>();
            string CS = ConfigurationManager.ConnectionStrings["starwarconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select b.name,count(species_id) as cnt from films_species a join    species b on a.species_id=b.id group by b.name", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    species obj = new species();

                    obj.name = rdr["name"].ToString();
                    obj.count = Convert.ToInt32(rdr["cnt"]);
                    species.Add(obj);
                }
            }
            return species;

        }


    }
}
