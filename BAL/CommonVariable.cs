using API_Cosumes.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API_Cosumes.BAL
{
	public class CommonVariable : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public List<UserModel> commomFunction(List<UserModel> users , HttpResponseMessage response)
		{
			
				string data = response.Content.ReadAsStringAsync().Result;
				dynamic jsonObject = JsonConvert.DeserializeObject(data);
				var dataOfObject = jsonObject.data;
				var extractedDataJson = JsonConvert.SerializeObject(dataOfObject, Formatting.Indented);
				users = JsonConvert.DeserializeObject<List<UserModel>>(extractedDataJson);
				
			return users;
		}
		public UserModel commomFunction(UserModel user, HttpResponseMessage res)
		{

			string data = res.Content.ReadAsStringAsync().Result;
			dynamic jsonObject = JsonConvert.DeserializeObject(data);
			var dataOfObject = jsonObject.data;
			string stringData = dataOfObject.ToString();
			user = JsonConvert.DeserializeObject<UserModel>(stringData);

			return user;
		}
	}
}
