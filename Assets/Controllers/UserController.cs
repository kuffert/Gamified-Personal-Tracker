using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;
using System.Linq;

// Author: Yvette Kim
// Date: 12/1/2015
public class UserController : MonoBehaviour
{
    private const string baseUrl = "https://api.mongolab.com/api/1";
    private const string database = "softdevfall15";
    private const string collection = "users";
    private const string urlKeyEnd = "?apiKey=ZMZOg1DKKoow4p8XCzVGfX-k8P6szwZj";
    private const string url = baseUrl + "/databases/" + database + "/collections/" + collection + urlKeyEnd;

    // Grab Users from DB, return list of fully populated User objects
    public static List<User> getUsers()
    {
        List<User> users = new List<User>();

        // Grab data
        WWW www = new WWW(url);
        while (!www.isDone) {/*do nothing, does not work otherwise*/}
        var N = JSON.Parse(www.text);

        // Fill Users
        User temp;
        while (N.Count > 0)
        {
            temp = mapUser(N[0]);
            users.Add(temp);
            N.Remove(0);
        }
        return users;
    }

    // Map JSONNode to User objects
    private static User mapUser(JSONNode node)
    {
        User user = new User();

        user.Id = node["_id"]["$oid"];
        user.Username = node["username"];
        user.Password = node["password"];
        user.Major = node["major"];
        user.Year = node["academic_standing"];

        return user;
    }
}
