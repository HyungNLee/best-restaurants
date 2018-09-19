using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BestRestaurants;
using System;

namespace BestRestaurants.Models
{
  public class Restaurant
  {
    private string _name;
    private string _city;
    private int _cuisineId;
    private int _id;

    public Restaurant (string newName, string newCity, int newCuisineId, int newId = 0)
    {
      _name = newName;
      _city = newCity;
      _cuisineId = newCuisineId;
      _id = newId;
    }

    public string GetName()
    {
      return _name;
    }

    public string GetCity()
    {
      return _city;
    }

    public int GetId()
    {
      return _id;
    }

    public int GetCuisineId()
    {
      return _cuisineId;
    }

    public void Edit(string newName, string newCity, int newCuisineId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE restaurants SET name = @newName, cuisine_id = @newCuisineId WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter restaurantName = new MySqlParameter();
      restaurantName.ParameterName = "@newName";
      restaurantName.Value = newName;
      cmd.Parameters.Add(restaurantName);

      MySqlParameter restaurantCuisineId = new MySqlParameter();
      restaurantCuisineId.ParameterName = "@newCuisineId";
      restaurantCuisineId.Value = newCuisineId;
      cmd.Parameters.Add(restaurantCuisineId);

      cmd.ExecuteNonQuery();

      _name = newName;
      _cuisineId = newCuisineId;

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public void Delete(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM restaurants WHERE id = @thisId";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@thisId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public static List<Restaurant> GetAll()
    {
      List<Restaurant> allRestaurants = new List<Restaurant> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM restaurants;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        string restaurantCity = rdr.GetString(2);
        int restaurantCuisineId = rdr.GetInt32(3);
        Restaurant newRestaurant = new Restaurant(restaurantName, restaurantCity, restaurantCuisineId, restaurantId);
        allRestaurants.Add(newRestaurant);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return allRestaurants;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO restaurants (name, city, cuisine_id) VALUES (@itemName, @itemCity, @itemCuisineId);";

      MySqlParameter Name = new MySqlParameter();
      Name.ParameterName = "@itemName";
      Name.Value = this._name;
      cmd.Parameters.Add(Name);

      MySqlParameter newCity = new MySqlParameter();
      newCity.ParameterName = "@itemCity";
      newCity.Value = this._city;
      cmd.Parameters.Add(newCity);

      MySqlParameter newCuisineId = new MySqlParameter();
      newCuisineId.ParameterName = "@itemCuisineId";
      newCuisineId.Value = this._cuisineId;
      cmd.Parameters.Add(newCuisineId);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
    }

    public static List<Restaurant> Filter(string sortOrder)
    {
      List<Restaurant> allRestaurants = new List<Restaurant> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM restaurants ORDER BY city " + sortOrder + ";";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        string restaurantCity = rdr.GetString(2);
        int restaurantCuisineId = rdr.GetInt32(3);
        Restaurant newRestaurant = new Restaurant(restaurantName, restaurantCity, restaurantCuisineId, restaurantId);
        allRestaurants.Add(newRestaurant);
      }
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return allRestaurants;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM restaurants;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public override bool Equals(System.Object otherRestaurant)
    {
      if (!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {

        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool nameEquality = (this._name == newRestaurant.GetName());
        bool cityEquality = (this._city == newRestaurant.GetCity());
        bool cuisineIdEquality = (this._cuisineId == newRestaurant.GetCuisineId());
        
        return (nameEquality && cityEquality && cuisineIdEquality);
      }
    }

    public override int GetHashCode()
    {
      string combinedHash = this.GetName() + this.GetCity() + this.GetCuisineId();
      return combinedHash.GetHashCode();
    }

    public static Restaurant Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `restaurants` WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int restaurantId = 0;
      string restaurantName = "";
      string restaurantCity = "";
      int restaurantCuisineId = 0;

      while (rdr.Read())
      {
          restaurantId = rdr.GetInt32(0);
          restaurantName = rdr.GetString(1);
          restaurantCity = rdr.GetString(2);
          restaurantCuisineId = rdr.GetInt32(3);
      }

      Restaurant foundRestaurant= new Restaurant(restaurantName, restaurantCity, restaurantCuisineId, restaurantId);  // This line is new!

       conn.Close();
       if (conn != null)
       {
           conn.Dispose();
       }

      return foundRestaurant;  // This line is new!
    }
  }
}
